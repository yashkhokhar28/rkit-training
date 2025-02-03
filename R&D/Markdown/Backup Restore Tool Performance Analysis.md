# Backup and Restore Tool Performance Analysis

## Setup

* Created a console application that generates **N** number of databases.
* For this test, a database named **test_db_1** was created, which contained  **100 tables** , named `order_1` to `order_100`.
* Each table contained  **128,975 records** .
* Size of each table’s **.ibd file** was approximately  **88 MB** .
* Total size of all **.ibd files** in the **test_db_1** folder was approximately **8.59 GB** on disk.

## Test with MySQL Workbench

1. **Export Process**
   * Export started at  **12:46:30** .
   * Export completed at  **12:52:25** .
   * Total export time:  **6 minutes and 5 seconds** .
   * Dump file size:  **7.31 GB** .
2. **Import Process**
   * Import started at  **14:49:09** .
   * Import completed at  **15:08:08** .
   * Total import time:  **18 minutes and 59 seconds** .

## Test with HeidiSQL

1. **Export Process**
   * Total export time:  **24 minutes** .
   * Export file size:  **12.5 GB** .
2. **Import Process**
   * Import process in HeidiSQL was not feasible. The file had to be loaded into the query editor and then executed.
   * After 1.5 hours, the file still didn't load properly, which suggests a significant issue with HeidiSQL's import handling for large files.

# Backup and Restore Tool Performance Comparison

| **Tool**             | **MySQL Workbench**             | **HeidiSQL**                            |
| -------------------------- | ------------------------------------- | --------------------------------------------- |
| **Export Time**      | 6 minutes 5 seconds                   | 24 minutes                                    |
| **Export File Size** | 7.31 GB                               | 12.5 GB                                       |
| **Import Time**      | 18 minutes 59 seconds                 | Not feasible (file did not load in 1.5 hours) |
| **Total Time**       | 25 minutes 4 seconds                  | N/A                                           |
| **Performance**      | Efficient, fast export/import process | Slow export, import process was unfeasible    |

---


## Conclusion

* **MySQL Workbench** performed the export and import operations significantly faster than  **HeidiSQL** .
* The total export time for MySQL Workbench was  **6 minutes and 5 seconds** , while HeidiSQL took  **24 minutes** .
* Importing the data using HeidiSQL was not feasible as the tool could not handle the size of the export file effectively. In contrast, MySQL Workbench was able to import the data in under 19 minutes.
