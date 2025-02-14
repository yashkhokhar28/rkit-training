# XtraBackup Commands

## Introduction

Percona XtraBackup is an open-source hot backup utility for MySQL-based servers that doesn't lock your database during the backup process. It can perform full backups, incremental backups, and even compressed and encrypted backups. This document provides a comprehensive guide to using XtraBackup for various backup and restore operations.

## Full Backup

### Take a Full Backup

The following command performs a full backup of the MySQL database.

```bash
xtrabackup --backup --target-dir="/home/ubuntu/full_backup" --datadir=/var/lib/mysql
```

### Prepare the Backup Before Restore

Preparing the backup before restoring ensures that the backup is consistent and ready to be restored.

```bash
xtrabackup --prepare --target-dir="/home/ubuntu/full_backup"
```

### Restore the Backup (Backup Will Not Be Deleted)

This command restores the backup to the MySQL data directory.

```bash
xtrabackup --copy-back --target-dir="/home/ubuntu/full_backup"
```

### Restore the Backup (Backup Will Be Deleted)

This command restores the backup and then deletes the backup files.

```bash
xtrabackup --move-back --target-dir="/home/ubuntu/full_backup"
```

### Conditions:

- The `datadir` must be empty before restoring the backup.
- The MySQL server must be stopped before restoring.
- You **cannot** restore to a `datadir` of a running `mysqld` instance (except for partial backups).

---

## Incremental Backup

### Steps to Perform Incremental Backup

#### 1. Create a Full Backup

Start by creating a full backup.

```bash
xtrabackup --backup --target-dir="/home/ubuntu/full_backup" --datadir=/var/lib/mysql
```

#### 2. Take an Incremental Backup

This command takes an incremental backup based on the last full backup.

```bash
xtrabackup --backup --target-dir=/home/ubuntu/full_backup/incremental_backup --incremental-basedir=/home/ubuntu/full_backup/
```

To take further incremental backups based on the last incremental backup:

```bash
xtrabackup --backup --target-dir=/home/ubuntu/full_backup/incremental_backup1 --incremental-basedir=/home/ubuntu/full_backup/incremental_backup/
```

#### 3. Prepare the Base Backup

Prepare the base backup before applying incremental backups.

```bash
xtrabackup --prepare --apply-log-only --target-dir=/home/ubuntu/full_backup/
```

#### 4. Apply the Incremental Backup

Apply the incremental backup to the base backup.

```bash
xtrabackup --prepare --target-dir=/home/ubuntu/full_backup/ --incremental-dir=/home/ubuntu/full_backup/incremental_backup/
```

The `--apply-log-only` flag ensures that uncommitted transactions are not rolled back, allowing multiple incremental backups to be merged.

#### 5. Finalize the Preparation

Finalize the preparation to make the backup ready for restoration.

```bash
xtrabackup --prepare --target-dir=/home/ubuntu/full_backup/
```

#### 6. Restore the Backup

Finally, restore the backup to the MySQL data directory.

```bash
xtrabackup --copy-back --target-dir=/home/ubuntu/full_backup/
```

---

## Compressed Backup

### Create a Compressed Backup

The following command creates a compressed backup.

```bash
xtrabackup --backup --compress --target-dir=/home/ubuntu/compressed/
```

### Prepare the Compressed Backup

#### 1. Decompress the Backup

Decompress the backup before preparing it.

```bash
xtrabackup --decompress --target-dir=/home/ubuntu/compressed/
```

To remove compressed files after decompression:

```bash
xtrabackup --decompress --remove-original --target-dir=/home/ubuntu/compressed/
```

#### 2. Prepare the Backup

Prepare the decompressed backup.

```bash
xtrabackup --prepare --target-dir=/home/ubuntu/compressed/
```

### Restore the Backup

#### 1. Stop the MySQL Server

Stop the MySQL server before restoring the backup.

```bash
sudo systemctl stop mysql
```

#### 2. Restore the Backup

Restore the backup to the MySQL data directory.

```bash
xtrabackup --copy-back --target-dir=/home/ubuntu/compressed/
```

#### 3. Adjust Permissions

Adjust the permissions of the restored files.

```bash
sudo chown -R mysql:mysql /var/lib/mysql
```

#### 4. Start the MySQL Server

Start the MySQL server after restoring the backup.

```bash
sudo systemctl start mysql
```

### Important Notes:

- **Compression Algorithms:** Percona XtraBackup supports `zstd` and `lz4`.
- **Ensure** that `zstd` or `lz4` is installed before decompressing backups.

---

## Partial Backup

### Methods to Create Partial Backups

#### 1. Using `--tables` Option

Back up a specific table (`orders_50` table from `test_db_1` database):

```bash
xtrabackup --backup --datadir=/var/lib/mysql --target-dir=/data/backups/ --tables="test_db_1.orders_50"
```

#### 2. Using `--tables-file` Option

Create a file listing tables to back up:

```bash
echo "test_db_1.orders_1" > /tmp/tables.txt
xtrabackup --backup --datadir=/var/lib/mysql --target-dir=/data/backups/ --tables-file=/tmp/tables.txt
```

#### 3. Using `--databases` Option

Back up the entire `test_db_1` database and `sales` table from `finance` database:

```bash
xtrabackup --backup --datadir=/var/lib/mysql --target-dir=/data/backups/ --databases="test_db_1 finance.sales"
```

If restoring system databases (`mysql`, `sys`, `performance_schema`), include them in the backup.

### Preparing Partial Backups

Prepare the partial backup before restoring:

```bash
xtrabackup --prepare --export --target-dir=/data/backups/
```

### Restoring Partial Backups

Partial backups require **manual** restoration using `IMPORT TABLESPACE`.

#### 1. Create the Table Structure

Recreate the table structure on the target MySQL server before restoring:

```sql
CREATE TABLE company.employees (
    id INT PRIMARY KEY,
    name VARCHAR(100),
    position VARCHAR(100),
    salary DECIMAL(10,2)
) ENGINE=InnoDB;
```

#### 2. Discard the Existing Tablespace

Discard the existing tablespace of the target table:

```sql
ALTER TABLE test_db_1.orders_50 DISCARD TABLESPACE;
```

#### 3. Copy the `.ibd` File

Copy the `.ibd` file from the backup to MySQL’s data directory:

```bash
cp /data/backups/company/employees.ibd /var/lib/mysql/company/
```

#### 4. Import the Tablespace

Import the tablespace to the target table:

```sql
ALTER TABLE test_db_1.orders_50 IMPORT TABLESPACE;
```

---

## Encrypted Backup

### Creating Encrypted Backups

#### 1. Generate an Encryption Key

Use the following command to generate a 192-bit (24-byte) encryption key:

```bash
openssl rand -base64 24
```

This command will output a random string, which will serve as your encryption key. For example: `Lg4gpO6/qtOrGQrKPs998qqKO4BRIwqJ`

#### 2. Choose an Encryption Method

- **Method A: Using the `--encrypt-key` Option**

  This method involves specifying the encryption key directly in the command.

  ```bash
  xtrabackup --backup --target-dir=/data/backups/ --encrypt=AES256 --encrypt-key="Lg4gpO6/qtOrGQrKPs998qqKO4BRIwqJ"
  ```

  **Note:** Including the encryption key directly in the command line can expose it to users on the system. Use this method with caution.

- **Method B: Using the `--encrypt-key-file` Option**

  This method involves storing the encryption key in a file and referencing that file in the command.

  ```bash
  # Create a file containing the encryption key
  echo -n "Lg4gpO6/qtOrGQrKPs998qqKO4BRIwqJ" > /data/backups/keyfile

  # Run the backup command
  xtrabackup --backup --target-dir=/data/backups/ --encrypt=AES256 --encrypt-key-file=/data/backups/keyfile
  ```

  **Note:** Ensure that the key file does not contain newline characters, as this can invalidate the key. The `-n` flag in the `echo` command prevents a newline from being added.

### Decrypting Encrypted Backups

To restore data from an encrypted backup, you must first decrypt the backup files. Percona XtraBackup provides the `--decrypt` option for this purpose.

#### 1. Decrypt the Backup

```bash
xtrabackup --decrypt=AES256 --encrypt-key-file=/data/backups/keyfile --target-dir=/data/backups/
```

**Note:** Percona XtraBackup does not automatically remove the encrypted files after decryption. To delete the original encrypted files, use the `--remove-original` option:

```bash
xtrabackup --decrypt=AES256 --encrypt-key-file=/data/backups/keyfile --target-dir=/data/backups/ --remove-original
```

#### 2. Prepare the Backup

After decrypting, prepare the backup for restoration:

```bash
xtrabackup --prepare --target-dir=/data/backups/
```

#### 3. Restore the Backup

Finally, restore the backup to your MySQL data directory:

```bash
# Stop the MySQL server
sudo systemctl stop mysql

# Restore the backup
xtrabackup --copy-back --target-dir=/data/backups/

# Adjust permissions
sudo chown -R mysql:mysql /var/lib/mysql

# Start the MySQL server
sudo systemctl start mysql
```
