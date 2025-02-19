## **1. Setup**

### **Database Setup**

- A console application was created to generate **multiple databases**.
- For this test, a database named **`test_db_1`** was created, containing **100 tables** (`orders_1` to `orders_100`).
- Each table contained approximately **128,975 records**.
- The total size of all **.ibd files** in **`test_db_1`** was approximately **8.59 GB**.

---

### **System Configuration**

- **Operating System:** Ubuntu 20.04.6 LTS
- **Kernel:** Linux 5.4.0-205-generic
- **Architecture:** x86_64
- **Virtualization:** Microsoft Hyper-V
- **CPU:** Intel(R) Core(TM) i7-7700 CPU @ 3.60GHz
- **Cores:** 1 (Single-core, no hyper-threading)
- **Clock Speed:** 2003.516 MHz
- **Hypervisor Vendor:** Microsoft

---

## **2. Performance Tests & Results**

### **MySQL Workbench**

- **Export Time:** 6 minutes 5 seconds
- **Export File Size:** 7.31 GB
- **Import Time:** 18 minutes 59 seconds
- **Total Time:** 25 minutes 4 seconds

### **HeidiSQL**

- **Export Time:** 24 minutes
- **Export File Size:** 12.5 GB
- **Import Process:** Not feasible (file failed to load properly after 1.5 hours)

### **mysqldump (CLI)**

- **Export Time:** 5 minutes
- **Export File Size:** 7.31 GB
- **Import Time:** 17 minutes
- **Total Time:** 22 minutes

### **Percona XtraBackup**

- **Backup Time:** 2 minutes (partial backup)
- **Backup Size:** 8.7 GB
- **Restore Time:** 40 seconds
- **Total Time:** 2 minutes 40 seconds

---

## **3. Performance Comparison Table**

| **Tool**             | **MySQL Workbench**           | **HeidiSQL**                     | **mysqldump (CLI)**               | **Percona XtraBackup**               |
| -------------------- | ----------------------------- | -------------------------------- | --------------------------------- | ------------------------------------ |
| **Backup Type**      | Logical (SQL dump)            | Logical (SQL dump)               | Logical (SQL dump)                | **Physical (Raw Data Copy)**         |
| **Export Time**      | 6 minutes 5 seconds           | 24 minutes                       | **5 minutes**                     | **2 minutes**                        |
| **Export File Size** | 7.31 GB                       | 12.5 GB                          | 7.31 GB                           | **8.7 GB**                           |
| **Import Time**      | 18 minutes 59 seconds         | Not feasible (file did not load) | **17 minutes**                    | **40 seconds**                       |
| **Total Time**       | **25 minutes 4 seconds**      | N/A                              | **22 minutes**                    | **2 minutes 40 seconds**             |
| **Performance**      | Efficient, fast export/import | Slow export, unfeasible import   | **Fastest logical export/import** | **Fastest backup & restore overall** |

---

## **4. Why Is Percona XtraBackup Faster?**

| **Feature**                | **Logical Backup (mysqldump, MySQL Workbench, HeidiSQL)** | **Physical Backup (Percona XtraBackup)** |
| -------------------------- | --------------------------------------------------------- | ---------------------------------------- |
| **Backup Method**          | Reads each row and generates SQL dumps                    | Copies raw InnoDB files (`.ibd`, `.frm`) |
| **Speed**                  | Slower (row-by-row processing)                            | **Faster** (direct file copy)            |
| **MySQL Instance Locking** | **Required** (affects database availability)              | **No Locking** (hot backup possible)     |

---

## **5. Pros & Cons of Percona XtraBackup**

### **Advantages**

✔ **Fastest Backup and Restore** → Copies raw data files instead of executing slow SQL queries.  
✔ **No Downtime (Hot Backup)** → Keeps MySQL running while backing up.  
✔ **Efficient for Large Databases** → Handles 100GB+ datasets efficiently.  
✔ **Binary-Compatible Backups** → Produces exact byte-for-byte copies of database files.

### **Disadvantages**

✖ **InnoDB Only** → Does not support MyISAM (although MySQL 8 primarily uses InnoDB).  
✖ **Requires Extra Storage** → Full `.ibd` file copies require sufficient disk space.  
✖ **More Complex Setup** → Requires running **xtrabackup prepare** before restoration.  
✖ **No Partial Row Exports** → Unlike `mysqldump`, it cannot export individual records.

---

## **6. Conclusion**

- **Percona XtraBackup outperformed all other tools**, completing a **backup in 2 minutes** and a **restore in 40 seconds**, making it the fastest solution.
- **mysqldump (CLI) was the fastest logical backup tool**, completing in **22 minutes** with a **7.31 GB file size**.
- **MySQL Workbench provided decent performance**, completing in **25 minutes 4 seconds**.
- **HeidiSQL was unsuitable for large database imports**, as the exported file was too large to load efficiently.
- **Percona XtraBackup is the best choice for large-scale databases requiring minimal downtime.**

---

## **7. Required Permissions for Percona XtraBackup**

A dedicated **backup user** with specific privileges is needed for Percona XtraBackup. Use the following SQL command to grant the necessary permissions:

```sql
GRANT RELOAD, LOCK TABLES, SELECT, PROCESS, REPLICATION CLIENT, CREATE TABLESPACE, BACKUP_ADMIN ON *.* TO 'backup_user'@'localhost';
```

### **Explanation of Permissions:**

- **RELOAD** → Required for `FLUSH TABLES` operations.
- **LOCK TABLES** → Allows table locking during backup.
- **SELECT** → Grants read access to all tables.
- **PROCESS** → Enables tracking of MySQL processes.
- **REPLICATION CLIENT** → Provides access to binlog positions.
- **CREATE TABLESPACE** → Needed for importing tablespaces.
- **BACKUP_ADMIN** → Allows `LOCK INSTANCE FOR BACKUP` (MySQL 8+).

Without these privileges, Percona XtraBackup may fail to complete the backup process.

---

## **8. Types of Backups in Percona XtraBackup**

| **Backup Type**        | **Description**                                     | **Best For**                          |
| ---------------------- | --------------------------------------------------- | ------------------------------------- |
| **Full Backup**        | Copies **all** MySQL data files (`.ibd`, `ibdata1`) | Disaster recovery, periodic backups   |
| **Incremental Backup** | Backs up **only changes** since the last backup     | Frequent backups with less storage    |
| **Partial Backup**     | Backs up **specific databases or tables**           | Selective data backups                |
| **Compressed Backup**  | Uses gzip/Qpress to reduce storage usage            | Cloud storage, limited disk space     |
| **Encrypted Backup**   | Secures backup data with AES encryption             | Sensitive data, regulatory compliance |
| **Streaming Backup**   | Sends backup directly to another server             | Remote/offsite backups                |

# XtraBackup Commands

## Introduction

## 1. Full Backup

### **1.1 Take a Full Backup**

Run the following command to take a full backup:

```bash
sudo xtrabackup --backup --target-dir="/home/ubuntu/full_backup" --datadir=/var/lib/mysql
```

### **1.2 Prepare the Backup**

Preparing the backup ensures consistency before restoration.

```bash
sudo xtrabackup --prepare --target-dir="/home/ubuntu/full_backup"
```

### **1.3 Restore the Backup**

#### **Option 1: Restore Without Deleting Backup**

```bash
sudo systemctl stop mysql
sudo xtrabackup --copy-back --target-dir="/home/ubuntu/full_backup"
sudo chown -R mysql:mysql /var/lib/mysql
sudo systemctl start mysql
```

#### **Option 2: Restore and Delete Backup**

```bash
sudo systemctl stop mysql
sudo xtrabackup --move-back --target-dir="/home/ubuntu/full_backup"
sudo chown -R mysql:mysql /var/lib/mysql
sudo systemctl start mysql
```

### **1.4 Important Notes**

- The **datadir (`/var/lib/mysql/`) must be empty** before restoring.
- The **MySQL server must be stopped** before restoring.
- **Cannot restore to a running `mysqld` instance.**

---

## 2. Incremental Backup

### **2.1 Take a Full Backup**

```bash
sudo xtrabackup --backup --target-dir="/home/ubuntu/full_backup" --datadir=/var/lib/mysql
```

### **2.2 Take Incremental Backups**

Each incremental backup must reference the previous backup.

#### **First Incremental Backup**

```bash
sudo xtrabackup --backup --target-dir="/home/ubuntu/incremental_1" --incremental-basedir="/home/ubuntu/full_backup"
```

#### **Second Incremental Backup**

```bash
sudo xtrabackup --backup --target-dir="/home/ubuntu/incremental_2" --incremental-basedir="/home/ubuntu/incremental_1"
```

### **2.3 Prepare the Backups**

#### **Prepare the Base Backup**

```bash
sudo xtrabackup --prepare --apply-log-only --target-dir="/home/ubuntu/full_backup"
```

#### **Apply Incremental Backups**

```bash
sudo xtrabackup --prepare --apply-log-only --target-dir="/home/ubuntu/full_backup" --incremental-dir="/home/ubuntu/incremental_1"
sudo xtrabackup --prepare --target-dir="/home/ubuntu/full_backup" --incremental-dir="/home/ubuntu/incremental_2"
```

### **2.4 Restore the Backup**

```bash
sudo systemctl stop mysql
sudo xtrabackup --copy-back --target-dir="/home/ubuntu/full_backup"
sudo chown -R mysql:mysql /var/lib/mysql
sudo systemctl start mysql
```

---

## 3. Compressed Backup

### **3.1 Create a Compressed Backup**

```bash
sudo xtrabackup --backup --compress --target-dir="/home/ubuntu/compressed"
```

### **3.2 Decompress the Backup**

```bash
sudo xtrabackup --decompress --target-dir="/home/ubuntu/compressed"
sudo xtrabackup --decompress --remove-original --target-dir="/home/ubuntu/compressed"
```

### **3.3 Prepare and Restore**

```bash
sudo xtrabackup --prepare --target-dir="/home/ubuntu/compressed"
sudo systemctl stop mysql
sudo xtrabackup --copy-back --target-dir="/home/ubuntu/compressed"
sudo chown -R mysql:mysql /var/lib/mysql
sudo systemctl start mysql
```

---

## 4. Partial Backup

### **4.1 Take Partial Backup**

#### **Backup a Single Table**

```bash
sudo xtrabackup --backup --datadir=/var/lib/mysql --target-dir="/data/backups" --tables="test_db_1.orders_50"
```

#### **Backup Specific Tables from a File**

```bash
echo "test_db_1.orders_1" > /tmp/tables.txt
sudo xtrabackup --backup --datadir=/var/lib/mysql --target-dir="/data/backups" --tables-file=/tmp/tables.txt
```

#### **Backup Specific Databases**

```bash
sudo xtrabackup --backup --datadir=/var/lib/mysql --target-dir="/data/backups" --databases="test_db_1 finance.sales"
```

### **4.2 Prepare the Backup**

```bash
sudo xtrabackup --prepare --export --target-dir="/data/backups"
```

### **4.3 Restore the Backup (Manual Steps)**

1. **Recreate Table Schema**

```sql
CREATE TABLE test_db_1.orders_50 (
    id INT PRIMARY KEY,
    name VARCHAR(100),
    amount DECIMAL(10,2)
) ENGINE=InnoDB;
```

2. **Discard Existing Tablespace**

```sql
ALTER TABLE test_db_1.orders_50 DISCARD TABLESPACE;
```

3. **Copy `.ibd` File**

```bash
sudo cp /data/backups/test_db_1/orders_50.ibd /var/lib/mysql/test_db_1/
```

4. **Import Tablespace**

```sql
ALTER TABLE test_db_1.orders_50 IMPORT TABLESPACE;
```

---

## 5. Encrypted Backup

### **5.1 Generate Encryption Key**

```bash
openssl rand -base64 24 > /data/backups/keyfile
```

### **5.2 Create Encrypted Backup**

```bash
sudo xtrabackup --backup --target-dir="/data/backups" --encrypt=AES256 --encrypt-key-file="/data/backups/keyfile"
```

### **5.3 Decrypt Encrypted Backup**

```bash
sudo xtrabackup --decrypt=AES256 --encrypt-key-file="/data/backups/keyfile" --target-dir="/data/backups" --remove-original
```

### **5.4 Prepare and Restore**

```bash
sudo xtrabackup --prepare --target-dir="/data/backups"
sudo systemctl stop mysql
sudo xtrabackup --copy-back --target-dir="/data/backups"
sudo chown -R mysql:mysql /var/lib/mysql
sudo systemctl start mysql
```
