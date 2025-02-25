# **Step-by-Step Guide: Installing MyDumper 0.11.3 with MySQL 8.0.27 on Ubuntu 20.04**

This guide provides a step-by-step approach to installing **MyDumper 0.11.3**, ensuring compatibility with **MySQL 8.0.27** on Ubuntu 20.04.

### **Required Files**

Download the following files before proceeding:

- **MySQL Client Runtime Library**
  - [`libmysqlclient21_8.0.27-1ubuntu20.04_amd64.deb`](https://downloads.mysql.com/archives/get/p/23/file/libmysqlclient21_8.0.27-1ubuntu20.04_amd64.deb)
- **MySQL Client Development Package**
  - [`libmysqlclient-dev_8.0.27-1ubuntu20.04_amd64.deb`](https://downloads.mysql.com/archives/get/p/23/file/libmysqlclient21_8.0.27-1ubuntu20.04_amd64.deb)
- **MyDumper Source Code**
  - [`mydumper-0.11.3.tar.gz`](https://github.com/mydumper/mydumper/archive/refs/tags/v0.11.3.tar.gz)

---

## **Step 1: Remove Existing MySQL Client and MyDumper Installations**

### **1. Remove Existing MySQL Client Development Packages**

```bash
sudo apt remove --purge libmysqlclient-dev libmysqlclient21
sudo apt autoremove
```

### **2. Remove Any Existing MyDumper Installation**

```bash
sudo rm -f /usr/local/bin/mydumper /usr/local/bin/myloader
```

---

## **Step 2: Install MySQL 8.0.27 Client Libraries**

### **1. Install the MySQL Client Runtime Library**

Ensure you are in the directory where the `.deb` files are located, then run:

```bash
sudo dpkg -i libmysqlclient21_8.0.27-1ubuntu20.04_amd64.deb
```

### **2. Install the MySQL Client Development Package**

```bash
sudo dpkg -i libmysqlclient-dev_8.0.27-1ubuntu20.04_amd64.deb
```

### **3. Fix Any Dependency Issues**

```bash
sudo apt-get install -f
```

### **4. Verify MySQL Client Installation**

```bash
mysql_config --version
```

**Expected output:**

```
8.0.27
```

---

## **Step 3: Extract MyDumper Source Code**

```bash
tar xzf mydumper-0.11.3.tar.gz
cd mydumper-0.11.3
```

---

## **Step 4: Configure the Build with CMake**

### **1. Create and Enter a Build Directory**

```bash
mkdir build
cd build
```

### **2. Run CMake**

```bash
cmake -DMYSQL_CONFIG=/usr/bin/mysql_config \
      -DMYSQL_LIBRARIES_ssl="/usr/lib/x86_64-linux-gnu/libssl.so" \
      -DMYSQL_LIBRARIES_crypto="/usr/lib/x86_64-linux-gnu/libcrypto.so" \
      ..
```

### **3. Install Sphinx (Optional - For Documentation)**

If warnings related to Sphinx documentation appear, install it:

```bash
sudo apt install python3-sphinx
```

---

## **Step 5: Compile MyDumper**

```bash
make -j$(nproc)
```

---

## **Step 6: Install MyDumper (Optional)**

```bash
sudo make install
```

This installs `mydumper` and `myloader` in `/usr/local/bin`.

---

## **Step 7: Verify Installation**

### **1. Check MyDumper Version**

```bash
mydumper --version
```

**Expected output:**

```
mydumper 0.11.3, built against MySQL 8.0.27
```

### **2. Check MyLoader Version**

```bash
myloader --version
```

**Expected output:**

```
myloader 0.11.3, built against MySQL 8.0.27
```

---

## **Step 8: Perform Backup Using MyDumper**

Run the following command to back up your database:

```bash
mydumper --database test_db_1 --outputdir /home/ubuntu/backup --no-locks --compress --triggers --events  --routines --complete-insert --tz-utc --host localhost --user root --password Miracle@1234 --threads 8 --verbose 3
```

### **MyDumper Options**

- `-B, --database` Database to dump
- `-T, --tables-list` Comma delimited table list to dump (does not exclude regex option)
- `-O, --omit-from-file` File containing a list of database.table entries to skip, one per line (skips before applying regex option)
- `-o, --outputdir` Directory to output files to
- `-s, --statement-size` Attempted size of INSERT statement in bytes, default 1000000
- `-r, --rows` Try to split tables into chunks of this many rows. This option turns off --chunk-filesize
- `-F, --chunk-filesize` Split tables into chunks of this output file size. This value is in MB
- `--max-rows` Limit the amount of rows per block after the table is estimated, default 1000000
- `-c, --compress` Compress output files
- `-e, --build-empty-files` Build dump files even if no data available from table
- `-x, --regex` Regular expression for 'db.table' matching
- `-i, --ignore-engines` Comma delimited list of storage engines to ignore
- `-N, --insert-ignore` Dump rows with INSERT IGNORE
- `-m, --no-schemas` Do not dump table schemas with the data
- `-M, --table-checksums` Dump table checksums with the data
- `-d, --no-data` Do not dump table data
- `--order-by-primary` Sort the data by Primary Key or Unique key if no primary key exists
- `-G, --triggers` Dump triggers
- `-E, --events` Dump events
- `-R, --routines` Dump stored procedures and functions
- `-W, --no-views` Do not dump VIEWs
- `-k, --no-locks` Do not execute the temporary shared read lock. WARNING: This will cause inconsistent backups
- `--no-backup-locks` Do not use Percona backup locks
- `--less-locking` Minimize locking time on InnoDB tables.
- `--long-query-retries` Retry checking for long queries, default 0 (do not retry)
- `--long-query-retry-interval` Time to wait before retrying the long query check in seconds, default 60
- `-l, --long-query-guard` Set long query timer in seconds, default 60
- `-K, --kill-long-queries` Kill long running queries (instead of aborting)
- `-D, --daemon` Enable daemon mode
- `-X, --snapshot-count` Number of snapshots, default 2
- `-I, --snapshot-interval` Interval between each dump snapshot (in minutes), requires --daemon, default 60
- `-L, --logfile` Log file name to use, by default stdout is used
- `--tz-utc` SET TIME_ZONE='+00:00' at top of dump to allow dumping of TIMESTAMP data when a server has data in different time zones or data is being moved between servers with different time zones, defaults to on use --skip-tz-utc to disable.
- `--skip-tz-utc`
- `--use-savepoints` Use savepoints to reduce metadata locking issues, needs SUPER privilege
- `--success-on-1146` Not increment error count and Warning instead of Critical in case of table doesn't exist
- `--lock-all-tables` Use LOCK TABLE for all, instead of FTWRL
- `-U, --updated-since` Use Update_time to dump only tables updated in the last U days
- `--trx-consistency-only` Transactional consistency only
- `--complete-insert` Use complete INSERT statements that include column names
- `--set-names` Sets the names, use it at your own risk, default binary
- `-z, --tidb-snapshot` Snapshot to use for TiDB
- `--sync-wait` WSREP_SYNC_WAIT value to set at SESSION level
- `--where` Dump only selected records.
- `-h, --host` The host to connect to
- `-u, --user` Username with the necessary privileges
- `-p, --password` User password
- `-a, --ask-password` Prompt For User password
- `-P, --port` TCP/IP port to connect to
- `-S, --socket` UNIX domain socket file to use for connection
- `-t, --threads` Number of threads to use, default 4
- `-C, --compress-protocol` Use compression on the MySQL connection
- `-V, --version` Show the program version and exit
- `-v, --verbose` Verbosity of output, 0 = silent, 1 = errors, 2 = warnings, 3 = info, default 2
- `--defaults-file` Use a specific defaults file
- `--ssl` Connect using SSL
- `--ssl-mode` Desired security state of the connection to the server: DISABLED, PREFERRED, REQUIRED, VERIFY_CA, VERIFY_IDENTITY
- `--key` The path name to the key file
- `--cert` The path name to the certificate file
- `--ca` The path name to the certificate authority file
- `--capath` The path name to a directory that contains trusted SSL CA certificates in PEM format
- `--cipher` A list of permissible ciphers to use for SSL encryption
- `--tls-version` Which protocols the server permits for encrypted connections
- `--config` Configuration file
- `--stream` It will stream over STDOUT once the files have been written
- `--no-delete` It will not delete the files after the stream has been completed

---

## **Step 9: Restore Backup Using MyLoader**

Run the following command to restore your database:

```bash
myloader --directory /home/ubuntu/backup --overwrite-tables --database test_db_1 --innodb-optimize-keys --host localhost --user root --password Miracle@1234 --threads 8 --verbose 3txt
```

### **MyLoader Options**

- `-d, --directory` Directory of the dump to import
- `-q, --queries-per-transaction` Number of queries per transaction, default 1000
- `-o, --overwrite-tables` Drop tables if they already exist
- `-B, --database` An alternative database to restore into
- `-s, --source-db` Database to restore
- `-e, --enable-binlog` Enable binary logging of the restore data
- `--innodb-optimize-keys` Creates the table without the indexes and it adds them at the end
- `--set-names` Sets the names, use it at your own risk, default binary
- `-L, --logfile` Log file name to use, by default stdout is used
- `--purge-mode` This specify the truncate mode which can be: NONE, DROP, TRUNCATE and DELETE
- `--disable-redo-log` Disables the REDO_LOG and enables it after, doesn't check initial status
- `-r, --rows` Split the INSERT statement into this many rows.
- `--max-threads-per-table` Maximum number of threads per table to use, default 4
- `-h, --host` The host to connect to
- `-u, --user` Username with the necessary privileges
- `-p, --password` User password
- `-a, --ask-password` Prompt For User password
- `-P, --port` TCP/IP port to connect to
- `-S, --socket` UNIX domain socket file to use for connection
- `-t, --threads` Number of threads to use, default 4
- `-C, --compress-protocol` Use compression on the MySQL connection
- `-V, --version` Show the program version and exit
- `-v, --verbose` Verbosity of output, 0 = silent, 1 = errors, 2 = warnings, 3 = info, default 2
- `--defaults-file` Use a specific defaults file
- `--ssl` Connect using SSL
- `--ssl-mode` Desired security state of the connection to the server: DISABLED, PREFERRED, REQUIRED, VERIFY_CA, VERIFY_IDENTITY
- `--key` The path name to the key file
- `--cert` The path name to the certificate file
- `--ca` The path name to the certificate authority file
- `--capath` The path name to a directory that contains trusted SSL CA certificates in PEM format
- `--cipher` A list of permissible ciphers to use for SSL encryption
- `--tls-version` Which protocols the server permits for encrypted connections
- `--config` Configuration file
- `--stream` It will stream over STDOUT once the files have been written
- `--no-delete` It will not delete the files after the stream has been completed

---

## **Best Practices for Backup & Restore**

### **Performance Optimization**

- **Threads:** Adjust `--threads` to match CPU cores for better performance.
- **Chunk Size:** Use `--rows` to optimize data chunking and parallelism.

### **Data Consistency**

- **Use `--trx-tables`** for transaction consistency in backups.
- **Enable `--lock-all-tables`** for consistency across non-transactional tables.

### **Logging and Monitoring**

- **Use `-L mydumper-logs.txt`** for detailed logging in MyDumper.
- **Redirect `stderr` output** to logs in MyLoader (`2>myloader-logs.txt`).

### **Backup & Restore Performance Summary with MyDumper/MyLoader**

#### **Test Conditions:**

- **Data Size:** ~600 MB (compressed)
- **Database Engine:** MySQL 8.0.27
- **Tool:** MyDumper 0.11.3 / MyLoader
- **System:** Ubuntu 20.04

#### **Performance Comparison:**

| Threads       | Backup Start | Backup End | Backup Duration | Restore Start | Restore End | Restore Duration |
| ------------- | ------------ | ---------- | --------------- | ------------- | ----------- | ---------------- |
| **2 Threads** | 12:11        | 12:15      | **4 mins**      | 12:17         | 12:47       | **30 mins**      |
| **8 Threads** | 10:57        | 11:00      | **3 mins**      | 11:02         | 11:31       | **29 mins**      |

#### **Observations & Insights:**

1. **Backup Time:**

   - Increasing threads from 2 to 8 reduced backup time by **1 minute** (4 mins → 3 mins).
   - Since the backup was already small (600 MB), the improvement was minimal.

2. **Restore Time:**

   - Increasing threads from 2 to 8 **only reduced restore time by 1 minute** (30 mins → 29 mins).
   - This suggests that MySQL write operations were likely the bottleneck, not the MyLoader threads.

3. **Performance Bottlenecks:**

   - **Backup is CPU-bound** (compression and data dumping). More threads helped slightly.
   - **Restore is disk I/O-bound** (inserts and indexing). More threads had minimal impact.

### **Performance Comparison: Compressed vs. Uncompressed Backup & Restore**

| **Threads** | **Compression** | **Backup Start** | **Backup End** | **Backup Time** | **Backup Size** | **Restore Start** | **Restore End** | **Restore Time** |
| ----------- | --------------- | ---------------- | -------------- | --------------- | --------------- | ----------------- | --------------- | ---------------- |
| 2           | ✅ Compressed   | 12:11            | 12:15          | **4 min**       | 600 MB          | 12:17             | 12:47           | **30 min**       |
| 8           | ✅ Compressed   | 10:57            | 11:00          | **3 min**       | 600 MB          | 11:02             | 11:31           | **29 min**       |
| 8           | ❌ Uncompressed | 12:05            | 12:06          | **1 min**       | **6.9 GB**      | 12:14             | 12:42           | **28 min**       |

These three files were generated by **mydumper**, a high-performance MySQL backup tool, when exporting the **`orders_32`** table from the **`test_db_1`** database. Let's break them down:

---

### 1. `test_db_1.orders_32.00000.sql.gz`

- **Purpose:** Contains the actual data (rows) of the **`orders_32`** table in compressed (`.gz`) format.
- **Why it exists?**
  - MyDumper splits large tables into multiple files for parallel processing.
  - Since this file has `.00000.sql.gz`, it means this is the **first (or only) chunk** of the table's data.
  - If the table was large, there might be additional files like `orders_32.00001.sql.gz`, `orders_32.00002.sql.gz`, etc.

---

### 2. `test_db_1.orders_32-metadata`

- **Purpose:** Stores metadata information about the **`orders_32`** table, including:
  - Table structure checksum
  - Binlog position (for point-in-time recovery)
  - GTID (if enabled)
  - Row count and timestamp of the dump
- **Why it exists?**
  - Helps with consistent restores, ensuring data integrity.
  - If replication or binlog-based recovery is needed, this metadata helps restore the exact state.

---

### 3. `test_db_1.orders_32-schema.sql.gz`

- **Purpose:** Contains the **schema definition** (DDL) of the **`orders_32`** table, including:
  - `CREATE TABLE orders_32 (...)`
  - `INDEX` definitions
  - Constraints and foreign keys (if any)
- **Why it exists?**
  - Ensures the table structure is recreated before importing data.
  - Separating schema and data allows **parallel restores** (schema first, then data).

---

## Why MyDumper Creates These 3 Files?

MyDumper follows a **structured, parallel backup approach**, splitting the dump into:

1. **Schema file** (`.schema.sql.gz`) → Defines the table structure.
2. **Data file(s)** (`.00000.sql.gz`, `.00001.sql.gz`, etc.) → Stores actual rows.
3. **Metadata file** (`-metadata`) → Ensures consistency and recovery details.

This approach makes **restores faster and more reliable**, especially for large databases.

---
