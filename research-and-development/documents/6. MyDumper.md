# Step-by-Step Guide: Installing MyDumper 0.11.3 with MySQL 8.0.27 on Ubuntu 20.04

This guide provides a step-by-step approach to installing **MyDumper 0.11.3**, ensuring compatibility with **MySQL 8.0.27** on Ubuntu 20.04.

## Required Files

Download these files before proceeding:

- **MySQL Client Runtime Library**
  - [`libmysqlclient21_8.0.27-1ubuntu20.04_amd64.deb`](https://downloads.mysql.com/archives/get/p/23/file/libmysqlclient21_8.0.27-1ubuntu20.04_amd64.deb)
- **MySQL Client Development Package**
  - [`libmysqlclient-dev_8.0.27-1ubuntu20.04_amd64.deb`](https://downloads.mysql.com/archives/get/p/23/file/libmysqlclient-dev_8.0.27-1ubuntu20.04_amd64.deb)
- **MyDumper Source Code**
  - [`mydumper-0.11.3.tar.gz`](https://github.com/mydumper/mydumper/archive/refs/tags/v0.11.3.tar.gz)

---

## Step 1: Remove Existing MySQL Client and MyDumper Installations

### 1 Remove Existing MySQL Client Development Packages

```bash
sudo apt remove --purge libmysqlclient-dev libmysqlclient21
sudo apt autoremove
```

**Note:** Check for conflicting packages with `dpkg -l | grep mysql`.

### 2 Remove Any Existing MyDumper Installation

```bash
sudo rm -f /usr/local/bin/mydumper /usr/local/bin/myloader
```

---

## Step 2: Install MySQL 8.0.27 Client Libraries

### 1 Install the MySQL Client Runtime Library

Ensure you are in the directory where the `.deb` files are located, then run:

```bash
sudo dpkg -i libmysqlclient21_8.0.27-1ubuntu20.04_amd64.deb
```

### 2 Install the MySQL Client Development Package

```bash
sudo dpkg -i libmysqlclient-dev_8.0.27-1ubuntu20.04_amd64.deb
```

### 3 Fix Any Dependency Issues

```bash
sudo apt-get install -f
```

### 4 Verify MySQL Client Installation

```bash
mysql_config --version
```

**Expected Output:**

```sql
8.0.27
```

---

## Step 3: Extract MyDumper Source Code

```bash
tar xzf mydumper-0.11.3.tar.gz
cd mydumper-0.11.3
```

---

## Step 4: Configure the Build with CMake

### 1 Create and Enter a Build Directory

```bash
mkdir build
cd build
```

### 2 Run CMake

```bash
cmake -DMYSQL_CONFIG=/usr/bin/mysql_config -DMYSQL_LIBRARIES_ssl="/usr/lib/x86_64-linux-gnu/libssl.so" -DMYSQL_LIBRARIES_crypto="/usr/lib/x86_64-linux-gnu/libcrypto.so" ..
```

**Note:** The `-DMYSQL_LIBRARIES_ssl` and `-DMYSQL_LIBRARIES_crypto` flags specify OpenSSL libraries required for MySQL 8.0.27 compatibility.

### 3 Install Sphinx (Optional - For Documentation)

If warnings related to Sphinx documentation appear, install it:

```bash
sudo apt install python3-sphinx
```

---

## Step 5: Compile MyDumper

```bash
make -j$(nproc)
```

---

## Step 6: Install MyDumper (Optional)

```bash
sudo make install
```

This installs `mydumper` and `myloader` in `/usr/local/bin`.

---

## Step 7: Verify Installation

### 1 Check MyDumper Version

```bash
mydumper --version
```

**Expected Output:**

```bash
mydumper 0.11.3, built against MySQL 8.0.27
```

### 2 Check MyLoader Version

```bash
myloader --version
```

**Expected Output:**

```bash
myloader 0.11.3, built against MySQL 8.0.27
```

---

## Step 8: Perform Backup Using MyDumper

Run the following command to back up your database:

```bash
mydumper --database test_db_1 --outputdir /home/ubuntu/backup --no-locks --compress --triggers --events --routines --complete-insert --tz-utc --host localhost --user <user> --password <password> --threads 8 --verbose 3
```

### Common MyDumper Options

- `-B, --database`: Database to dump
- `-o, --outputdir`: Directory to output files
- `-t, --threads`: Number of threads (default: 4)
- `-v, --verbose`: Verbosity level (0-3, default: 2)
- Full list available in the [MyDumper documentation](https://github.com/mydumper/mydumper).

---

## Step 9: Restore Backup Using MyLoader

Run the following command to restore your database:

```bash
myloader --directory /home/ubuntu/backup --overwrite-tables --database test_db_1 --innodb-optimize-keys --host localhost --user <user> --password <password> --threads 8 --verbose 3
```

### Common MyLoader Options

- `-d, --directory`: Directory of the dump to import
- `-o, --overwrite-tables`: Drop tables if they already exist
- `-t, --threads`: Number of threads (default: 4)
- `-v, --verbose`: Verbosity level (0-3, default: 2)
- Full list available in the [MyDumper documentation](https://github.com/mydumper/mydumper).

---

## Best Practices for Backup & Restore

### Performance Optimization

- **Threads:** Adjust `--threads` to match CPU cores for better performance.
- **Chunk Size:** Use `--rows` to optimize data chunking and parallelism.

### Data Consistency

- **Use `--trx-consistency-only`:** Ensures transactional consistency in backups.
- **Enable `--lock-all-tables`:** Provides consistency across non-transactional tables.

### Logging and Monitoring

- **Use `-L mydumper-logs.txt`:** Enables detailed logging in MyDumper.
- **Redirect `stderr` Output:** Log MyLoader errors with `2>myloader-logs.txt`.

---

## Backup & Restore Performance Summary with MyDumper/MyLoader

### Test Conditions

- **Database Engine:** MySQL 8.0.27
- **Tool:** MyDumper 0.11.3 / MyLoader
- **System:** Ubuntu 20.04
- **Threads:** 8 (unless specified otherwise)
- **Data Size:**
  - Compressed: ~600-607 MB
  - Uncompressed: ~6.9 GB

### Performance Comparison: Thread Variations

## **Backup and Restore Times with 600 MB Compressed Data**

| Threads | Backup Start | Backup End | Backup Time | Restore Start | Restore End | Restore Time |
| ------- | ------------ | ---------- | ----------- | ------------- | ----------- | ------------ |
| 2       | 12:11        | 12:15      | 4 mins      | 12:17         | 12:47       | 30 mins      |
| 8       | 10:57        | 11:00      | 3 mins      | 11:02         | 11:31       | 29 mins      |

### Performance Comparison: Row Size and Compression Variations

## **Backup and Restore Times with 8 Threads**

| Row Size | Compression  | Backup Start | Backup End   | Backup Time   | Backup Size | Restore Start | Restore End  | Restore Time   |
| -------- | ------------ | ------------ | ------------ | ------------- | ----------- | ------------- | ------------ | -------------- |
| 5000     | Compressed   | 11:17:46.675 | 11:20:49.484 | 3 mins 3 sec  | 607 MB      | 11:25:33.959  | 11:52:48.081 | 27 mins 14 sec |
| 5000     | Uncompressed | 11:55:59.312 | 11:57:53.973 | 1 min 55 sec  | 6.9 GB      | 11:59:55.051  | 12:27:51.028 | 27 mins 56 sec |
| 10000    | Compressed   | 12:28:38.185 | 12:32:12.498 | 3 mins 34 sec | 606 MB      | 12:36:20.579  | 13:04:49.003 | 28 mins 28 sec |
| 10000    | Uncompressed | 13:08:53.805 | 13:10:46.370 | 1 min 53 sec  | 6.9 GB      | 13:18:34.834  | 13:46:12.422 | 27 mins 38 sec |
| -        | Compressed   | 10:57        | 11:00        | 3 mins        | 600 MB      | 11:02         | 11:31        | 29 mins        |
| -        | Uncompressed | 12:05        | 12:06        | 1 min         | 6.9 GB      | 12:14         | 12:42        | 28 mins        |

**Notes:**

- Row size refers to the `--rows` parameter in MyDumper, splitting tables into chunks of this many rows.
- Times are rounded to seconds where milliseconds are provided; earlier tests (without milliseconds) are kept as minutes for consistency.

### Observations & Insights

1. **Backup Time:**

   - **Compression Impact:** Compressed backups (606-607 MB) take longer (3-3.5 mins) than uncompressed backups (6.9 GB, ~1-2 mins), due to CPU overhead from compression.
   - **Row Size Impact:** Increasing row size from 5000 to 10000 increases backup time slightly (e.g., 3 mins 3 sec → 3 mins 34 sec with compression), likely due to larger chunk processing.
   - **Thread Impact:** With 8 threads, backup time is consistently faster than with 2 threads (3 mins vs. 4 mins for 600 MB compressed).

2. **Restore Time:**

   - **Compression Impact:** Restore times are similar regardless of compression (27-29 mins), suggesting disk I/O and MySQL write operations dominate over decompression overhead.
   - **Row Size Impact:** Larger row sizes (10000) slightly increase restore time (e.g., 27 mins 14 sec → 28 mins 28 sec with compression), possibly due to larger INSERT statements.
   - **Thread Impact:** 8 threads reduce restore time slightly compared to 2 threads (29 mins vs. 30 mins for 600 MB compressed).

3. **Performance Bottlenecks:**
   - **Backup:** CPU-bound with compression (more threads and smaller rows help). Uncompressed backups are I/O-bound and faster.
   - **Restore:** Disk I/O-bound (inserts and indexing). Neither row size nor compression significantly affects restore time.

## Understanding MyDumper Output Files

These three files were generated by MyDumper when exporting the `orders_32` table from the `test_db_1` database. Here’s what they do:

### 1. `test_db_1.orders_32.00000.sql.gz`

- **Purpose:** Contains the actual data (rows) of the `orders_32` table in compressed (`.gz`) format.
- **Details:** This is the first (or only) chunk of the table’s data. Additional files (e.g., `.00001.sql.gz`) appear for larger tables.

### 2. `test_db_1.orders_32-metadata`

- **Purpose:** Stores metadata about the `orders_32` table, including:
  - Table structure checksum
  - Binlog position (for point-in-time recovery)
  - GTID (if enabled)
  - Row count and timestamp
- **Details:** Ensures consistency and aids in replication or recovery.

### 3. `test_db_1.orders_32-schema.sql.gz`

- **Purpose:** Contains the schema definition (DDL) of the `orders_32` table, including:
  - `CREATE TABLE` statement
  - Index definitions
  - Constraints (if any)
- **Details:** Used to recreate the table structure before importing data.

### Why These Files?

MyDumper’s structured approach separates schema, data, and metadata for parallel processing, making backups and restores faster and more reliable, especially for large databases.

---
