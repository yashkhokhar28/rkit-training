#!/bin/bash

USER="root"
DATE=$(date +%d%m%Y)

echo "Choose operation: "
select op in "Backup" "Restore"; do
    case $op in
        
        Backup)
            read -p "Enter START index for backup (e.g., 1): " START
            read -p "Enter END index for backup (e.g., 5): " END
            
            DBS=""
            for i in $(seq $START $END); do
                DBS+=" test$i"
            done
            
            BACKUP_DIR="/backup/test_${START}_${END}_${DATE}"
            mkdir -p "$BACKUP_DIR"
            
            echo "🔄 Running backup to: $BACKUP_DIR"
            xtrabackup --backup \
            --datadir=/var/lib/mysql \
            --target-dir="$BACKUP_DIR" \
            --databases="$DBS" \
            --user=$USER
            
            echo "⚙️ Preparing backup..."
            xtrabackup --prepare --export --target-dir="$BACKUP_DIR"
            
            echo "🧹 Removing system tablespace files (ibdata1, mysql.ibd)..."
            rm -f "$BACKUP_DIR/ibdata1" "$BACKUP_DIR/mysql.ibd"
            echo "✅ Removed system tablespace files."
            
            echo "✅ Backup and preparation completed at: $BACKUP_DIR"
            echo "$BACKUP_DIR" > /tmp/last_xtrabackup_path.txt
            break
        ;;
        
        Restore)
            # Restore phase remains as you had it
            if [ ! -f /tmp/last_xtrabackup_path.txt ]; then
                echo "❌ No backup path found. Please run a backup first."
                exit 1
            fi
            BACKUP_DIR=$(cat /tmp/last_xtrabackup_path.txt)
            echo "✅ Using backup directory: $BACKUP_DIR"
            
            read -p "Enter START index to restore (e.g., 2): " START
            read -p "Enter END index to restore (e.g., 3): " END
            
            for i in $(seq $START $END); do
                DB="test$i"
                echo "🔄 Restoring $DB..."
                mysql -u $USER -e "CREATE DATABASE IF NOT EXISTS $DB;"
                
                DB_PATH="$BACKUP_DIR/$DB"
                if [ ! -d "$DB_PATH" ]; then
                    echo "⚠️ Skipping $DB — no directory at $DB_PATH"
                    continue
                fi
                
                for ibd_file in "$DB_PATH"/*.ibd; do
                    [ -e "$ibd_file" ] || { echo "⚠️ No .ibd files for $DB"; break; }
                    
                    TABLE_NAME=$(basename "$ibd_file" .ibd)
                    echo "    ↪ Restoring table: $TABLE_NAME"
                    
                    mysql -u $USER -e "ALTER TABLE $DB.$TABLE_NAME DISCARD TABLESPACE;"
                    cp "$DB_PATH/$TABLE_NAME.ibd" /var/lib/mysql/$DB/
                    cp "$DB_PATH/$TABLE_NAME.cfg" /var/lib/mysql/$DB/
                    chown mysql:mysql /var/lib/mysql/$DB/$TABLE_NAME.*
                    
                    mysql -u $USER -e "ALTER TABLE $DB.$TABLE_NAME IMPORT TABLESPACE;"
                done
            done
            
            echo "📊 Row count for restored tables:"
            for i in $(seq $START $END); do
                DB="test$i"
                TABLES=$(mysql -u $USER -N -e "SHOW TABLES FROM $DB;" 2>/dev/null)
                for TABLE in $TABLES; do
                    COUNT=$(mysql -u $USER -N -e "SELECT COUNT(*) FROM $DB.$TABLE;" 2>/dev/null)
                    echo "$DB.$TABLE → $COUNT rows"
                done
            done
            
            echo "✅ Restore completed."
            break
        ;;
        
        *) echo "❌ Invalid option. Choose 1 or 2." ;;
    esac
done
