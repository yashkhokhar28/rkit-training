# **Step-by-Step Guide: Installing MyDumper 0.11.3 with MySQL 8.0.27 on Ubuntu 20.04**

This guide will walk you through installing MyDumper 0.11.3, ensuring compatibility with MySQL 8.0.27, using the following files:

- `libmysqlclient21_8.0.27-1ubuntu20.04_amd64.deb` (runtime library) [Download](https://downloads.mysql.com/archives/get/p/23/file/libmysqlclient21_8.0.27-1ubuntu20.04_amd64.deb)
- `libmysqlclient-dev_8.0.27-1ubuntu20.04_amd64.deb` (development files including `mysql_config`) [Download](https://downloads.mysql.com/archives/get/p/23/file/libmysqlclient21_8.0.27-1ubuntu20.04_amd64.deb)
- `mydumper-0.11.3.tar.gz` (MyDumper source tarball) [Download](https://github.com/mydumper/mydumper/archive/refs/tags/v0.11.3.tar.gz)

---

## **Step 1: Remove Any Existing MySQL Client Development Packages and MyDumper**

Before proceeding, ensure no conflicting MySQL client versions or MyDumper installations exist.

### **1. Remove Existing MySQL Client Development Packages**

```bash
sudo apt remove --purge libmysqlclient-dev libmysqlclient21
sudo apt autoremove
```

### **2. Remove Any Existing MyDumper Installation**

If MyDumper was previously installed (via `apt` or from source), remove its binaries:

```bash
sudo rm -f /usr/local/bin/mydumper /usr/local/bin/myloader
```

---

## **Step 2: Install MySQL 8.0.27 Client Libraries**

### **1. Install the MySQL Client Runtime Library**

Ensure you are in the directory where the `.deb` files are located (e.g., `/home/ubuntu`):

```bash
sudo dpkg -i libmysqlclient21_8.0.27-1ubuntu20.04_amd64.deb
```

### **2. Install the MySQL Client Development Package**

```bash
sudo dpkg -i libmysqlclient-dev_8.0.27-1ubuntu20.04_amd64.deb
```

### **3. Fix Any Dependency Issues**

If `dpkg` reports missing dependencies, resolve them with:

```bash
sudo apt-get install -f
```

### **4. Verify MySQL Client Installation**

Check the installed version of `mysql_config`:

```bash
mysql_config --version
```

Expected output:

```
8.0.27
```

---

## **Step 3: Extract the MyDumper Source Code**

### **1. Extract the Tarball**

Navigate to the directory containing `mydumper-0.11.3.tar.gz` and extract it:

```bash
tar xzf mydumper-0.11.3.tar.gz
cd mydumper-0.11.3
```

---

## **Step 4: Configure the Build with CMake**

### **1. Create and Enter a Build Directory**

It is best practice to build out-of-source:

```bash
mkdir build
cd build
```

### **2. Run CMake**

Run the following command to configure the build:

```bash
cmake -DMYSQL_CONFIG=/usr/bin/mysql_config \
      -DMYSQL_LIBRARIES_ssl="/usr/lib/x86_64-linux-gnu/libssl.so" \
      -DMYSQL_LIBRARIES_crypto="/usr/lib/x86_64-linux-gnu/libcrypto.so" \
      ..
```

**Notes:**

- This ensures that MyDumper uses MySQL 8.0.27’s `mysql_config`.
- It specifies the correct OpenSSL libraries.
- If you see warnings related to Sphinx documentation generation, they can be ignored. Alternatively, install Sphinx with:
  ```bash
  sudo apt install python3-sphinx
  ```

### **3. Verify CMake Output**

Ensure:

- MySQL 8.0.27 is detected correctly.
- No missing dependencies are reported.

---

## **Step 5: Compile MyDumper**

### **1. Build Using `make`**

Use all available CPU cores to speed up compilation:

```bash
make -j$(nproc)
```

---

## **Step 6: Install MyDumper (Optional)**

To install MyDumper system-wide:

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

Expected output:

```
mydumper 0.11.2, built against MySQL 8.0.27
```

### **2. Check MyLoader Version**

```bash
myloader --version
```

Expected output:

```
myloader 0.11.2, built against MySQL 8.0.27
```

Below is a step‐by‐step guide for performing a full backup and restore of a MySQL database using MyDumper and MyLoader. These commands assume that:

- You’re running MySQL 8.0.27.
- MyDumper and MyLoader (version 0.11.2 in your case) are installed and working.
- You have the appropriate privileges on your MySQL server.

---

## **A. Backup Using MyDumper**

1. **Choose or Create a Backup Directory:**  
   Decide on a directory where you want to store the backup files. For example, create one at `/home/ubuntu/backup`:

   ```bash
   mkdir -p /home/ubuntu/backup
   ```

2. **Run the Backup Command:**  
   Execute MyDumper with the appropriate options. For a simple backup of a specific database, use:

   ```bash
   mydumper --user=your_username --password=your_password --host=localhost --database=your_database --outputdir=/home/ubuntu/backup --threads=4 --compress
   ```

   **Explanation of Options:**

   - `--user` and `--password`: Credentials to connect to your MySQL server.
   - `--host`: The MySQL server address (use `localhost` if running locally).
   - `--database`: Specifies the database you wish to back up.
   - `--outputdir`: Directory where the backup files will be saved.
   - `--threads`: Number of parallel threads to use (adjust based on your CPU cores).
   - `--compress`: Compresses the output files to save space (optional).

3. **Monitor the Backup Process:**  
   MyDumper will create multiple files—one per table and additional metadata files—in the specified directory. Check the output directory to ensure files are generated.

---

## **B. Restore Using MyLoader**

1. **Prepare the Target Database:**  
   Ensure that the target database (where you want to restore the backup) exists. If it already exists and you want to overwrite its tables, note that MyLoader’s `--overwrite-tables` option will drop the tables before importing.

   You can create the target database (if needed) with:

   ```bash
   mysql --user=your_username --password=your_password -e "CREATE DATABASE your_database_restore;"
   ```

2. **Run the Restore Command:**  
   Use MyLoader to import the backup files:

   ```bash
   myloader --user=your_username --password=your_password --host=localhost  --directory=/home/ubuntu/backup --threads=4 --overwrite-tables --database=your_database_restore
   ```

   **Explanation of Options:**

   - `--user`, `--password`, `--host`: Credentials and connection details to your MySQL server.
   - `--directory`: The directory where the backup files are located (the same as used in the backup step).
   - `--threads`: Number of parallel threads to use for faster restore.
   - `--overwrite-tables`: Drops existing tables before restoring them.
   - `--database`: Specifies the target database into which the backup will be restored (use this option if your backup contains only one database; otherwise, you can use the metadata inside the backup).

3. **Monitor the Restore Process:**  
   MyLoader will read the files created by MyDumper and restore the database objects. Watch the terminal output for any errors.

---

When MyDumper runs a backup, it splits the dump into several files per table to improve manageability and allow parallel backup/restore. In your case, for each table (here “orders” in database test_db_1) you see three types of files:

1. **Data File (e.g. test_db_1.orders_31.00000.sql.gz):**

   - **What it contains:**  
     This file stores a chunk of the table’s actual data in SQL INSERT statements (or, depending on your options, in a format for LOAD DATA). The “00000” part is the chunk index. If a table is very large, MyDumper splits its rows into several chunks (e.g., 00000, 00001, etc.) so that restoration can be done in parallel and files remain manageable in size.
   - **Use Case:**  
     It allows faster backups/restores by working on multiple chunks in parallel and makes it easier to handle very large tables.

2. **Schema File (e.g. test_db_1.orders_31-schema.sql.gz):**

   - **What it contains:**  
     This file includes the DDL statements—typically the CREATE TABLE command and associated definitions (indexes, constraints, engine type, etc.). It captures the structure of the “orders” table.
   - **Use Case:**  
     It is used during restore to recreate the table structure exactly as it was during the backup, ensuring that all definitions and properties are preserved.

3. **Metadata File (e.g. test_db_1.orders_31-metadata):**
   - **What it contains:**  
     The metadata file holds important information about the backup of that specific table (or chunk). This can include details such as the dump timestamp, snapshot information, table-specific settings, or boundaries (like primary key ranges) for the data chunk.
   - **Use Case:**  
     It’s used by MyLoader during the restore process to ensure consistency, to know how many rows were dumped, and to help reassemble the backup in the correct order. It can also include replication position or other context needed to restore the backup accurately.

---

**Why This Design?**

- **Parallel Processing:**  
  By splitting data into multiple files, both backup and restoration can be performed in parallel, speeding up the overall process.
- **Manageability:**  
  Large tables can produce huge single files that are hard to manage, transfer, or process. Chunking helps keep each file at a reasonable size.
- **Consistency & Flexibility:**  
  Separating the schema from data and including metadata ensures that during restoration, the system can first recreate the table structure and then load data in an organized manner, all while maintaining a consistent snapshot of the database.
