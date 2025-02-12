# **Backup Behavior in `mysqldump` vs. Percona XtraBackup**

#### **1️ `mysqldump --single-transaction` Behavior**

- Takes a **logical backup** (SQL file with `INSERT` statements).
- Uses **MVCC (Multi-Version Concurrency Control)** for **InnoDB tables**.
- Ensures a **consistent snapshot** at the moment the dump starts.
- **Newly inserted records after the dump starts are NOT included.**

  **If `orders_1` had 100,000 records when the dump started, the dump file will contain exactly 100,000 records, even if new records were inserted during the backup.**

---

#### **2️ Percona XtraBackup Partial Backup Behavior**

- Takes a **physical backup** (copies `.ibd`, `.frm`, `.cfg` files).
- Also **uses MVCC**, ensuring a **transactionally consistent snapshot**.
- **New inserts after the backup starts will NOT be included** in the backup.
- Uses **redo logs** to maintain consistency during the restore process.

  **If `orders_1` had 100,000 records when the backup started, the backup will contain exactly 100,000 records, even if new records were inserted during the backup.**

---

#### **3️⃣ How to Capture New Inserts in Percona XtraBackup?**

To include new inserts made **after the backup starts**, you need to **apply binary logs** after restoring the backup:

1. **Enable binary logging (`log-bin=mysql-bin`)**.
2. **Record the binlog position before backup (`SHOW MASTER STATUS;`)**.
3. **After restoring, replay binlogs (`mysqlbinlog` command)** to capture missed inserts.

---
