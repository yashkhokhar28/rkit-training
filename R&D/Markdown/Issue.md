# **Summary Report: MyDumper Performance Issues & Fixes**

#### Why No Significant Improvement?

- **Single-Core CPU (`nproc = 1`)**: Your one core can’t parallelize MyDumper’s threading beyond 1-2 threads, making higher counts (e.g., 8) ineffective and compression slow. Backup/restore gains are capped by sequential processing.
- **Tiny InnoDB Buffer Pool (128 MB)**: Only ~1.5% of your 8.5 GB database fits in memory, forcing disk I/O to dominate restores and limit row-size tweak benefits.
- **Small InnoDB Log File (48 MB)**: Frequent log flushes during restores (12.9M rows across 100 tables) stall writes, negating threading or chunking gains.
- **Disk I/O Bottleneck**: With low memory caching and one core, your disk (likely HDD if slow) can’t keep up, flattening performance curves.
- **Suboptimal MyDumper Settings**: High threads waste CPU, compression adds overhead, and row sizes (5000-10000) aren’t ideal for your 128,975-row tables.

#### What Should Be Done Now?

Given your 4 GB RAM and current setup, here’s the tailored fix:

1. **Tune MySQL Settings**

   - **Set `innodb_buffer_pool_size = 2G` (2 GB, 2147483648 bytes)**
     - **Why**: Uses ~50% of your 4 GB RAM, caching ~25% of your 8.5 GB database. Leaves 1.8 GB for OS and MyDumper (2.4 GB available now - 357 MB swap = ~2 GB headroom). Reduces disk I/O significantly.
     - **Impact**: Cuts restore time by 5-10 mins (e.g., 29 mins → 20-23 mins).
   - **Set `innodb_log_file_size = 512M` (512 MB, 536870912 bytes)**
     - **Why**: Handles large transactions (128,975 rows per table) without frequent flushes, speeding up MyLoader writes.
     - **Impact**: Shaves ~5 mins off restores.

   **How to Apply**:

   - Edit `/etc/mysql/my.cnf`:
     ```bash
     sudo nano /etc/mysql/my.cnf
     ```
     Add/update under `[mysqld]`:
     ```
     [mysqld]
     innodb_buffer_pool_size = 2G
     innodb_log_file_size = 512M
     ```
   - Stop MySQL, remove old logs, restart:
     ```bash
     sudo service mysql stop
     sudo rm /var/lib/mysql/ib_logfile*
     sudo service mysql start
     ```

2. **Optimize MyDumper/MyLoader Commands**

   - **Threads**: `--threads 2`. Matches your single core’s capacity without overhead.
   - **Compression**: `--no-compress` for backups. Saves CPU, aligns with your 1-core limit (expect 1-2 mins vs. 3-3.5 mins).
   - **Row Size**: `--rows 20000`. Splits 128,975 rows per table into ~6-7 chunks, reducing file overhead.

   **Backup Command**:

   ```bash
   mydumper --database test_db_1 --outputdir /home/ubuntu/backup --no-locks --no-compress --threads 2 --rows 20000 --verbose 3 -L backup.log
   ```

   **Restore Command**:

   ```bash
   myloader --directory /home/ubuntu/backup --database test_db_1 --threads 2 --verbose 3 2>restore.log
   ```

   **Impact**: Backup drops ~10-30% (e.g., 3 mins → 2-2.5 mins); restore gains from MySQL tweaks.

3. **Monitor and Verify**

   - **Check Resources**: Run `htop` and `iotop` during backup/restore to confirm CPU/disk usage. If CPU is 100% or disk is saturated, that’s your limit.
   - **Log Analysis**: Review `backup.log` and `restore.log` for slow steps (e.g., “writing chunk” or “inserting data”).
   - **Disk Speed**: Test with `dd if=/dev/zero of=testfile bs=1G count=1 oflag=dsync`. If <50 MB/s, an SSD would help.

4. **Long-Term Recommendation**
   - **Upgrade Hardware**: A multi-core CPU (e.g., 4 cores) would unlock MyDumper’s parallelism, potentially halving times. An SSD would boost I/O, cutting restores further.
     - **Impact**: Backup <2 mins, restore <15 mins possible.

#### Expected Outcomes

- **Backup**: ~2-2.5 mins (from 3-4 mins) with no compression and optimized chunks.
- **Restore**: ~20-23 mins (from 29-30 mins) with better caching and log handling.
- **Limit**: Single core and disk speed cap bigger gains without hardware upgrades.

# InnoDB Buffer Pool Size (`innodb_buffer_pool_size`)

#### Use Case

The InnoDB buffer pool is a memory area where MySQL caches table data, indexes, and other structures for the InnoDB storage engine. Its primary purposes are:

- **Speed Up Reads**: Frequently accessed data (rows, indexes) stays in memory, avoiding slow disk reads.
- **Speed Up Writes**: Changes (inserts, updates) are first written to the buffer pool, then flushed to disk later, reducing immediate I/O.
- **Efficiency**: Minimizes disk access for queries, backups, and restores by keeping as much of the database “hot” as possible.

Think of it as a fast-access workspace—bigger means more of your database fits without hitting the disk.

#### Your Current Setting

- **Value**: 128 MB (134217728 bytes).
- **Problem**: Your 8.5 GB database is ~66 times larger than 128 MB. Only ~1.5% fits in memory, so MySQL constantly reads/writes to disk.

#### Impact on Backup (MyDumper)

- **Minor Role**: MyDumper reads data directly from MySQL, exporting it to files. A small buffer pool slows reads slightly if MySQL must fetch uncached data from disk. For your 8.5 GB database, this might add seconds to minutes, depending on disk speed.
- **Consistency Checks**: Options like `--triggers` or `--events` may trigger index lookups, slowed by disk I/O if data isn’t cached.
- **Your Case**: With a single core already maxed, the impact is less noticeable (e.g., 3-4 mins unaffected by row-size tweaks), but it’s there.

#### Impact on Restore (MyLoader)

- **Major Bottleneck**: MyLoader inserts data into tables, rebuilding indexes and writing rows. With 128 MB:
  - Only ~150 MB of your 8.5 GB fits (including overhead), so each insert or index build reads/writes to disk.
  - For 12.9M rows (128,975 × 100 tables), millions of disk operations pile up, dragging restores to ~29-30 mins.
- **Why No Gains**: Tweaking threads or row sizes doesn’t help much when disk I/O dominates—your single core can’t mask this.

#### Proposed Change: 2 GB

- **Why**: With 4 GB RAM, 2 GB (~50%) caches ~25% of your 8.5 GB database. Frequently accessed tables/indexes stay in memory.
- **Backup Impact**: Slightly faster reads (e.g., 3 mins → 2.5 mins), but CPU limits bigger gains.
- **Restore Impact**: Huge win—fewer disk reads/writes for inserts and index builds. Expect ~5-10 mins off (e.g., 29 mins → 20-23 mins).

---

### InnoDB Log File Size (`innodb_log_file_size`)

#### Use Case

The InnoDB log files (aka redo logs) track all changes to the database (inserts, updates, deletes) before they’re permanently written to disk. Their purposes are:

- **Crash Recovery**: Logs ensure data consistency if MySQL crashes—changes are replayed from logs.
- **Write Performance**: Changes are written to logs (fast, sequential I/O) first, then to data files (slower, random I/O) later. Bigger logs mean fewer flushes to disk.
- **Transaction Handling**: Logs buffer transactions until committed, reducing disk syncs.

It’s like a scratchpad—larger logs let MySQL batch more changes before committing them.

#### Your Current Setting

- **Value**: 48 MB (50331648 bytes).
- **Problem**: Tiny for 8.5 GB and 12.9M rows. Logs fill fast, triggering frequent “checkpoints” (flushing to disk), which stalls writes.

#### Impact on Backup (MyDumper)

- **Minimal Role**: MyDumper reads data, not writes, so logs are mostly used for consistency (e.g., `--trx-consistency-only`). A small log might slow transaction snapshots slightly, but impact is negligible (seconds at most).
- **Your Case**: Backup times (e.g., 3-4 mins) aren’t log-bound—CPU and I/O overshadow this.

#### Impact on Restore (MyLoader)

- **Major Bottleneck**: MyLoader writes 12.9M rows, creating millions of log entries:
  - 48 MB fills in seconds with bulk inserts (e.g., 128,975 rows per table × 100 tables).
  - Each flush pauses writes, waiting for disk sync. For 8.5 GB, this happens dozens/hundreds of times.
  - Restores drag (e.g., 29-30 mins) as log flushes pile up.
- **Why No Gains**: Threads/row sizes don’t help when MyLoader waits on disk syncs—your single core can’t hide this delay.

#### Proposed Change: 512 MB

- **Why**: 512 MB holds ~10x more changes before flushing. For 12.9M rows, it batches larger transactions, reducing pauses.
- **Backup Impact**: Negligible—logs aren’t the bottleneck here.
- **Restore Impact**: Cuts write stalls significantly. Expect ~5 mins off (e.g., 29 mins → 23-24 mins alone; 20-23 mins with buffer pool boost).

---

### Combined Impact Here

- **Backup**:
  - Current: ~3-4 mins (guess based on guide and your tweaks).
  - With 2 GB buffer + 512 MB log: ~2-2.5 mins. Mostly from `--no-compress` and slight read boost; logs don’t matter much.
  - Limit: Single core caps parallelism.
- **Restore**:
  - Current: ~29-30 mins (assuming guide-like times).
  - With 2 GB buffer + 512 MB log: ~20-23 mins. Buffer reduces I/O (~5-10 mins), log cuts flush delays (~5 mins). Synergy shaves extra time.
  - Limit: Disk speed and 1 core prevent sub-15 mins without hardware upgrades.

#### How They Work Together

- **Buffer Pool**: Keeps data/indexes in RAM, reducing disk reads/writes.
- **Log Files**: Buffers changes, reducing disk syncs.
- **Result**: Restores shift from I/O-bound to CPU-bound (your single core’s limit), while backups see minor I/O relief.

---

### Why These Matter Here

Your 8.5 GB database and single-core setup amplify these settings’ weaknesses:

- **Small Buffer**: Turns restores into a disk-thrashing slog, especially with 12.9M rows and indexes.
- **Small Log**: Chokes MyLoader’s writes, piling delays on an already slow process.
- Tweaking MyDumper (threads, rows) can’t overcome these—MySQL’s engine needs the fix.

### Action Recap

- Set `innodb_buffer_pool_size = 2G` and `innodb_log_file_size = 512M` in `my.cnf`.
- Restart MySQL (remove old logs first).
- Test with `--threads 2 --no-compress --rows 20000`.
