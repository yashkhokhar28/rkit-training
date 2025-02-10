# **Backup and Restore Tool Performance Analysis**

## **Setup**

- Created a console application that generates **N** number of databases.
- For this test, a database named **test_db_1** was created, which contained **100 tables**, named `orders_1` to `orders_100`.
- Each table contained **128,975 records**.
- Size of each table’s **.ibd file** was approximately **88 MB**.
- Total size of all **.ibd files** in the **test_db_1** folder was approximately **8.59 GB** on disk.

---

## **Test with MySQL Workbench**

### **1. Export Process**

- Export started at **12:46:30**.
- Export completed at **12:52:25**.
- **Total export time:** **6 minutes and 5 seconds**.
- **Dump file size:** **7.31 GB**.

### **2. Import Process**

- Import started at **14:49:09**.
- Import completed at **15:08:08**.
- **Total import time:** **18 minutes and 59 seconds**.

---

## **Test with HeidiSQL**

### **1. Export Process**

- **Total export time:** **24 minutes**.
- **Export file size:** **12.5 GB**.

### **2. Import Process**

- Import process in HeidiSQL was **not feasible**. The file had to be loaded into the query editor and then executed.
- After **1.5 hours**, the file still didn't load properly, indicating a significant issue with HeidiSQL's import handling for large files.

---

## **Test with mysqldump (CLI)**

### **1. Export Process**

- Export started at **10:18**.
- Export completed at **10:23**.
- **Total export time:** **5 minutes**.
- **Dump file size:** **7.31 GB**.

### **2. Import Process**

- Import started at **10:25**.
- Import completed at **10:42**.
- **Total import time:** **17 minutes**.

---

## **Test with Percona XtraBackup (CLI)**

### **1. Backup Process**

- Backup started at **07:47:20**.
- Backup completed at **07:48:06**.
- **Total backup time:** **46 seconds**.
- **Backup size:** **8.7 GB**.

### **2. Restore Process**

- Restore started at **07:49:30**.
- Restore completed at **07:50:10**.
- **Total restore time:** **40 seconds**.

---

# **Backup and Restore Tool Performance Comparison**

| **Tool**             | **MySQL Workbench**           | **HeidiSQL**                     | **mysqldump (CLI)**                  | **Percona XtraBackup**               |
| -------------------- | ----------------------------- | -------------------------------- | ------------------------------------ | ------------------------------------ |
| **Export Time**      | 6 minutes 5 seconds           | 24 minutes                       | **5 minutes**                        | **46 seconds**                       |
| **Export File Size** | 7.31 GB                       | 12.5 GB                          | 7.31 GB                              | **8.7 GB**                           |
| **Import Time**      | 18 minutes 59 seconds         | Not feasible (file did not load) | **17 minutes**                       | **40 seconds**                       |
| **Total Time**       | **25 minutes 4 seconds**      | N/A                              | **22 minutes**                       | **1 minute 26 seconds**              |
| **Performance**      | Efficient, fast export/import | Slow export, unfeasible import   | **Fastest export, efficient import** | **Fastest backup & restore overall** |

---

## **Conclusion**

- **Percona XtraBackup outperformed all other tools**, achieving a **backup time of just 46 seconds** and a **restore time of 40 seconds**, making it the fastest method tested.
- **mysqldump (CLI) performed well**, completing the entire process in **22 minutes**, making it the **most efficient logical backup option**.
- **MySQL Workbench performed acceptably**, completing the process in **25 minutes and 4 seconds**.
- **HeidiSQL was not feasible** for large imports due to its inability to handle large SQL files efficiently.

---
