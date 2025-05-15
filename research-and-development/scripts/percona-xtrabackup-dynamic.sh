#!/bin/bash

USER="root"
MYSQL="mysql -u $USER"
MYSQLDUMP="mysqldump -u $USER"
XTRABACKUP="xtrabackup"
DATA_DIR="/var/lib/mysql"

log_info()    { echo -e "[INFO]  $(date '+%Y-%m-%d %H:%M:%S') — $*"; }
log_error()   { echo -e "[ERROR] $(date '+%Y-%m-%d %H:%M:%S') — $*" >&2; }
log_success() { echo -e "[OK]    $(date '+%Y-%m-%d %H:%M:%S') — $*"; }

check_mysql_connection() {
    $MYSQL -e "SELECT 1;" > /dev/null 2>&1 || {
        log_error "Cannot connect to MySQL as user '$USER'."
        exit 1
    }
}

list_tables_and_counts() {
    for DB in $DB_LIST; do
        log_info "Analyzing database: $DB"
        TABLES=$($MYSQL -N -e "SHOW TABLES FROM $DB" 2>/dev/null)
        for TABLE in $TABLES; do
            COUNT=$($MYSQL -N -e "SELECT COUNT(*) FROM $DB.$TABLE" 2>/dev/null)
            echo "  → $DB.$TABLE: $COUNT rows"
        done
    done
}

dump_schemas_and_generate_sql() {
    mkdir -p "$BACKUP_DIR"
    for DB in $DB_LIST; do
        log_info "Dumping schema for $DB"
        $MYSQLDUMP --no-data "$DB" > "$BACKUP_DIR/${DB}_schema.sql" || {
            log_error "Failed to dump schema for $DB"
            continue
        }
        
        # During BACKUP phase, per DB:
        DISCARD_SQL="$BACKUP_DIR/${DB}_discard_tables.sql"
        IMPORT_SQL="$BACKUP_DIR/${DB}_import_tables.sql"
        
        echo "-- Discard tablespaces for $DB" > "$DISCARD_SQL"
        echo "-- Import tablespaces for $DB" > "$IMPORT_SQL"
        
        TABLES=$($MYSQL -N -e "SHOW TABLES FROM \`$DB\`;")
        for T in $TABLES; do
            echo "ALTER TABLE \`$DB\`.\`$T\` DISCARD TABLESPACE;" >> "$DISCARD_SQL"
            echo "ALTER TABLE \`$DB\`.\`$T\` IMPORT TABLESPACE;" >> "$IMPORT_SQL"
        done
        
        log_success "Generated SQLs for $DB: discard/import"
    done
}

perform_backup() {
    log_info "Starting partial backup..."
    $XTRABACKUP --backup \
    --datadir="$DATA_DIR" \
    --target-dir="$BACKUP_DIR" \
    --databases="$DB_LIST" \
    --export \
    --user=$USER || {
        log_error "Backup failed!"
        exit 1
    }
    
    log_info "Preparing backup..."
    $XTRABACKUP --prepare --export --target-dir="$BACKUP_DIR" || {
        log_error "Prepare phase failed!"
        exit 1
    }
    
    log_info "Cleaning shared tablespaces from backup directory"
    rm -f "$BACKUP_DIR/ibdata1" "$BACKUP_DIR/mysql.ibd"
    
    echo "$BACKUP_DIR" > "/tmp/last_backup_path.txt"
    log_success "Backup completed and stored at: $BACKUP_DIR"
}

perform_restore() {
    if [[ ! -d "$BACKUP_DIR" ]]; then
        log_error "Backup directory not found: $BACKUP_DIR"
        exit 1
    fi
    
    for DB in $DB_LIST; do
        log_info "Restoring database: $DB"
        
        log_info "Dropping (if exists) and creating $DB"
        $MYSQL -e "DROP DATABASE IF EXISTS \`$DB\`; CREATE DATABASE \`$DB\`;"
        
        TMP_SQL="/tmp/${DB}_schema_with_use.sql"
        echo "USE \`$DB\`;" > "$TMP_SQL"
        cat "$BACKUP_DIR/${DB}_schema.sql" >> "$TMP_SQL"
        
        log_info "Recreating schema for $DB"
        $MYSQL < "$TMP_SQL" || {
            log_error "Failed to recreate schema for $DB"
            rm -f "$TMP_SQL"
            continue
        }
        
        rm -f "$TMP_SQL"
        
        
        
        log_info "Discarding tablespaces for $DB"
        $MYSQL < "$BACKUP_DIR/${DB}_discard_tables.sql"
        
        DB_PATH="$BACKUP_DIR/$DB"
        if [[ ! -d "$DB_PATH" ]]; then
            log_error "Missing directory for $DB at $DB_PATH"
            continue
        fi
        
        for FILE in "$DB_PATH"/*.{ibd,cfg}; do
            [[ -e "$FILE" ]] || continue
            cp "$FILE" "$DATA_DIR/$DB/" || {
                log_error "Failed to copy $FILE"
                continue
            }
        done
        chown mysql:mysql "$DATA_DIR/$DB/"*
        
        log_info "Importing tablespaces for $DB"
        $MYSQL < "$BACKUP_DIR/${DB}_import_tables.sql"
        
        log_info "Verifying row counts for $DB"
        TABLES=$($MYSQL -N -e "SHOW TABLES FROM $DB" 2>/dev/null)
        for TABLE in $TABLES; do
            COUNT=$($MYSQL -N -e "SELECT COUNT(*) FROM $DB.$TABLE" 2>/dev/null)
            echo "  → $DB.$TABLE: $COUNT rows"
        done
        log_success "Restore complete for $DB"
    done
}

# ======================= MAIN FLOW =========================

check_mysql_connection

echo -n "Enter space-separated database names: "
read DB_LIST

list_tables_and_counts

while true; do
    echo -n "Press 1 for Backup, 2 for Restore: "
    read CHOICE
    [[ "$CHOICE" == "1" || "$CHOICE" == "2" ]] && break
    log_error "Invalid choice. Please select 1 or 2."
done

if [[ "$CHOICE" == "1" ]]; then
    echo -n "Enter backup directory path: "
    read BACKUP_DIR
    dump_schemas_and_generate_sql
    perform_backup
else
    echo -n "Enter prepared backup directory path: "
    read BACKUP_DIR
    perform_restore
fi
