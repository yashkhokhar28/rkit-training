# **Backup and Restore Tool Performance Analysis**

## Setup

- Created a console application that generates **N** number of databases.
- For this test, a database named **test_db_1** was created, which contained **100 tables**, named `order_1` to `order_100`.
- Each table contained **128,975 records**.
- Size of each table’s **.ibd file** was approximately **88 MB**.
- Total size of all **.ibd files** in the **test_db_1** folder was approximately **8.59 GB** on disk.

---

## Test with MySQL Workbench

1. **Export Process**

   - Export started at **12:46:30**.
   - Export completed at **12:52:25**.
   - **Total export time:** **6 minutes and 5 seconds**.
   - **Dump file size:** **7.31 GB**.

2. **Import Process**
   - Import started at **14:49:09**.
   - Import completed at **15:08:08**.
   - **Total import time:** **18 minutes and 59 seconds**.

---

## Test with HeidiSQL

1. **Export Process**

   - **Total export time:** **24 minutes**.
   - **Export file size:** **12.5 GB**.

2. **Import Process**
   - Import process in HeidiSQL was **not feasible**. The file had to be loaded into the query editor and then executed.
   - After **1.5 hours**, the file still didn't load properly, indicating a significant issue with HeidiSQL's import handling for large files.

---

## Test with mysqldump (CLI)

1. **Export Process**

   - Export started at **10:18**.
   - Export completed at **10:23**.
   - **Total export time:** **5 minutes**.
   - **Dump file size:** **7.31 GB**.

2. **Import Process**
   - Import started at **10:25**.
   - Import completed at **10:42**.
   - **Total import time:** **17 minutes**.

---

# Backup and Restore Tool Performance Comparison

| **Tool**             | **MySQL Workbench**           | **HeidiSQL**                     | **mysqldump (CLI)**                  |
| -------------------- | ----------------------------- | -------------------------------- | ------------------------------------ |
| **Export Time**      | 6 minutes 5 seconds           | 24 minutes                       | **5 minutes**                        |
| **Export File Size** | 7.31 GB                       | 12.5 GB                          | 7.31 GB                              |
| **Import Time**      | 18 minutes 59 seconds         | Not feasible (file did not load) | **17 minutes**                       |
| **Total Time**       | **25 minutes 4 seconds**      | N/A                              | **22 minutes**                       |
| **Performance**      | Efficient, fast export/import | Slow export, unfeasible import   | **Fastest export, efficient import** |

---

## Conclusion

- **mysqldump (CLI)** outperformed both **MySQL Workbench** and **HeidiSQL**, achieving the **fastest export time** (5 minutes) and an **efficient import time** (17 minutes).
- **MySQL Workbench** performed well, completing the entire process in **25 minutes 4 seconds**, with a balanced export/import speed.
- **HeidiSQL** was **not feasible** for large imports, as the exported file was significantly larger (12.5 GB), and the import process failed even after **1.5 hours**.
- **mysqldump (CLI) is the most efficient tool** for large database backups, with both export and import being the fastest among the tested tools.

---
