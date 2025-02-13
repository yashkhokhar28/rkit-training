# XtraBackup Commands

## Full Backup

### Command to Take Full Backup

```bash
xtrabackup --backup --target-dir="/home/ubuntu/full_backup" --datadir=/var/lib/mysql
```

### Command to Prepare Backup Before Restore

```bash
xtrabackup --prepare --target-dir="/home/ubuntu/full_backup"
```

### Command to Restore the Backup (Backup Will Not Be Deleted)

```bash
xtrabackup --copy-back --target-dir="/home/ubuntu/full_backup"
```

### Command to Restore the Backup (Backup Will Be Deleted)

```bash
xtrabackup --move-back --target-dir="/home/ubuntu/full_backup"
```

**Conditions:**

- The `datadir` must be empty before restoring the backup.
- MySQL server needs to be shut down before the restore is performed.
- You cannot restore to a `datadir` of a running `mysqld` instance (except when importing a partial backup).

---

## Incremental Backup

### Command to Take Incremental Backup

1. **Create a Full Backup:**

   ```bash
   xtrabackup --backup --target-dir="/home/ubuntu/full_backup" --datadir=/var/lib/mysql
   ```

2. **Create an Incremental Backup:**

   ```bash
   xtrabackup --backup --target-dir=/home/ubuntu/full_backup/incremental_backup --incremental-basedir=/home/ubuntu/full_backup/
   ```

   This command stores the incremental backup in `/home/ubuntu/full_backup/incremental_backup` and uses `/home/ubuntu/full_backup/` as the reference point to determine the changes.

   You can create additional incremental backups by referencing the last incremental backup:

   ```bash
   xtrabackup --backup --target-dir=/home/ubuntu/full_backup/incremental_backup1 --incremental-basedir=/home/ubuntu/full_backup/incremental_backup/
   ```

### Prepare the Base Backup

```bash
xtrabackup --prepare --apply-log-only --target-dir=/home/ubuntu/full_backup/
```

### Apply the Incremental Backup

```bash
xtrabackup --prepare --target-dir=/home/ubuntu/full_backup/ --incremental-dir=/home/ubuntu/full_backup/incremental_backup/
```

The `--apply-log-only` option ensures that uncommitted transactions are not rolled back, allowing subsequent incremental backups to be applied.

### Finalize the Preparation

```bash
xtrabackup --prepare --target-dir=/home/ubuntu/full_backup/
```

### Restore the Backup

```bash
xtrabackup --copy-back --target-dir=/home/ubuntu/full_backup/
```

---

## Compressed Backup

### Creating a Compressed Backup

To create a compressed backup, use the `--compress` option along with the `--backup` and `--target-dir` options.

```bash
xtrabackup --backup --compress --target-dir=/data/compressed/
```

**Optional: Parallel Compression**

```bash
xtrabackup --backup --compress --compress-threads=4 --target-dir=/data/compressed/
```

### Preparing the Compressed Backup

#### Decompress the Backup

```bash
xtrabackup --decompress --target-dir=/data/compressed/
```

Remove the compressed files after decompression:

```bash
xtrabackup --decompress --remove-original --target-dir=/data/compressed/
```

#### Prepare the Backup

```bash
xtrabackup --prepare --target-dir=/data/compressed/
```

### Restoring the Backup

#### Stop the MySQL Server

```bash
sudo systemctl stop mysql
```

#### Restore the Backup

```bash
xtrabackup --copy-back --target-dir=/data/compressed/
```

#### Adjust Permissions

```bash
sudo chown -R mysql:mysql /var/lib/mysql
```

#### Start the MySQL Server

```bash
sudo systemctl start mysql
```

### Important Considerations

- **Compression Algorithms:** Percona XtraBackup supports Zstandard (ZSTD) and LZ4.
- **Decompression Tools:** Ensure `zstd` or `lz4` is installed before decompression.
