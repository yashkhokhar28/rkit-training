# **MySQL Programs**

## `mysqld`

`mysqld` (MySQL Server) is a single, multithreaded program that performs most MySQL operations without spawning additional processes. It manages access to the MySQL data directory, which contains databases, tables, logs, and status files. A debugging variant, `mysqld-debug`, is available for tracing and memory checks. When launched, `mysqld` listens for client connections and oversees database access.

To explore all startup options, run:

```sh
mysqld --verbose --help
```

### General Server Configuration

| Command                                          | Description                                         |
| ------------------------------------------------ | --------------------------------------------------- |
| `mysqld --defaults-file=/etc/mysql/my.cnf &`     | Loads MySQL configuration from a specified file.    |
| `mysqld --datadir=/var/lib/mysql &`              | Sets the MySQL data directory.                      |
| `mysqld --socket=/var/run/mysqld/mysqld.sock &`  | Defines the Unix socket file for local connections. |
| `mysqld --pid-file=/var/run/mysqld/mysqld.pid &` | Specifies the file storing the MySQL process ID.    |

### Performance & Optimization

| Command                                 | Description                                                        |
| --------------------------------------- | ------------------------------------------------------------------ |
| `mysqld --max-connections=1000 &`       | Sets the maximum number of simultaneous client connections.        |
| `mysqld --innodb-buffer-pool-size=2G &` | Allocates memory for the InnoDB buffer pool (key for performance). |
| `mysqld --query-cache-size=64M &`       | Sets the query cache size (deprecated in MySQL 8).                 |
| `mysqld --thread-cache-size=50 &`       | Caches threads for reuse to boost performance.                     |
| `mysqld --table-open-cache=4000 &`      | Increases the number of open tables MySQL can handle.              |
| `mysqld --tmp-table-size=256M &`        | Defines the maximum size for temporary tables in memory.           |
| `mysqld --join-buffer-size=8M &`        | Increases buffer size for join operations.                         |

### Logging & Debugging

| Command                                                                     | Description                                          |
| --------------------------------------------------------------------------- | ---------------------------------------------------- |
| `mysqld --log-error=/var/log/mysql_error.log &`                             | Logs errors to a specified file.                     |
| `mysqld --general-log=1 --general-log-file=/var/log/mysql_general.log &`    | Enables general query logging.                       |
| `mysqld --slow-query-log=1 --slow-query-log-file=/var/log/mysql_slow.log &` | Logs slow queries for performance analysis.          |
| `mysqld --log-bin=/var/log/mysql-bin.log &`                                 | Enables binary logging for replication and recovery. |
| `mysqld --binlog-format=ROW &`                                              | Sets binary log format (ROW, STATEMENT, or MIXED).   |
| `mysqld --expire-logs-days=7 &`                                             | Purges binary logs older than 7 days automatically.  |

### Security

| Command                                            | Description                                                        |
| -------------------------------------------------- | ------------------------------------------------------------------ |
| `mysqld --skip-grant-tables &`                     | Starts MySQL without authentication (e.g., for password recovery). |
| `mysqld --skip-networking &`                       | Disables networking for local-only access.                         |
| `mysqld --secure-file-priv=/var/lib/mysql-files &` | Restricts file import/export to a specific directory.              |

### Replication

| Command                                         | Description                                       |
| ----------------------------------------------- | ------------------------------------------------- |
| `mysqld --server-id=1 &`                        | Sets a unique server ID for replication.          |
| `mysqld --log-slave-updates &`                  | Logs updates received from the master.            |
| `mysqld --replicate-do-db=mydatabase &`         | Replicates only the specified database.           |
| `mysqld --relay-log=/var/log/mysql-relay-bin &` | Specifies the relay log location for replication. |

### Recovery & Maintenance

| Command                              | Description                                       |
| ------------------------------------ | ------------------------------------------------- |
| `mysqld --innodb-force-recovery=1 &` | Starts MySQL in recovery mode to fix corruption.  |
| `mysqld --skip-log-bin &`            | Disables binary logging (useful for maintenance). |
| `mysqld --flush-time=600 &`          | Flushes tables to disk every 600 seconds.         |

### Miscellaneous

| Command                                                          | Description                                                |
| ---------------------------------------------------------------- | ---------------------------------------------------------- |
| `mysqld --event-scheduler=ON &`                                  | Enables the MySQL Event Scheduler for automated tasks.     |
| `mysqld --default-authentication-plugin=mysql_native_password &` | Forces use of the traditional MySQL authentication method. |

## `mysqld_safe`

`mysqld_safe` is the recommended method for starting a `mysqld` server on Unix systems. It enhances safety by features like restarting the server on errors and logging runtime details to an error log. It attempts to launch the `mysqld` executable by default, but you can override this with the `--mysqld` or `--mysqld-version` options. Use `--ledir` to specify the directory containing the server binary.

### Examples

1. **Basic Usage**  
   Start MySQL with custom user, error log, and port:

   ```sh
   mysqld_safe --user=mysql --log-error=/var/log/mysql_error.log --port=3307 &
   ```

2. **Custom Binary Location**  
   Specify the exact `mysqld` binary path:

   ```sh
   mysqld_safe --mysqld=/usr/local/mysql/bin/mysqld --ledir=/usr/local/mysql/bin/ &
   ```

3. **Custom Option Files**  
   Load specific configuration files:
   ```sh
   mysqld_safe --defaults-file=/etc/my.cnf --defaults-extra-file=/etc/mysql/custom.cnf &
   ```

### Options

| Command                                                    | Description                                                     |
| ---------------------------------------------------------- | --------------------------------------------------------------- |
| `mysqld_safe --basedir=/usr/local/mysql &`                 | Starts MySQL with the specified installation directory.         |
| `mysqld_safe --core-file-size=500M &`                      | Sets the size of the core file `mysqld` can create.             |
| `mysqld_safe --datadir=/var/lib/mysql &`                   | Specifies the path to the data directory.                       |
| `mysqld_safe --defaults-extra-file=/etc/mysql/extra.cnf &` | Reads an additional option file alongside usual files.          |
| `mysqld_safe --defaults-file=/etc/my.cnf &`                | Reads only the specified option file.                           |
| `mysqld_safe --help`                                       | Displays help message and exits.                                |
| `mysqld_safe --ledir=/usr/local/mysql/bin/ &`              | Specifies the directory where the server is located.            |
| `mysqld_safe --log-error=/var/log/mysql_error.log &`       | Writes the error log to the specified file.                     |
| `mysqld_safe --malloc-lib=/usr/lib/malloc.so &`            | Uses an alternative malloc library for `mysqld`.                |
| `mysqld_safe --mysqld=mysqld-debug &`                      | Specifies the name of the server program to start (in `ledir`). |
| `mysqld_safe --mysqld-version=8.0 &`                       | Specifies the suffix for the server program name.               |
| `mysqld_safe --nice=10 &`                                  | Uses `nice` to set server scheduling priority.                  |
| `mysqld_safe --no-defaults &`                              | Reads no option files.                                          |
| `mysqld_safe --open-files-limit=8192 &`                    | Sets the number of files `mysqld` can open.                     |
| `mysqld_safe --pid-file=/var/run/mysqld.pid &`             | Specifies the path to the server process ID file.               |
| `mysqld_safe --plugin-dir=/usr/lib/mysql/plugin &`         | Sets the directory where plugins are installed.                 |
| `mysqld_safe --port=3307 &`                                | Specifies the port number for TCP/IP connections.               |
| `mysqld_safe --skip-kill-mysqld &`                         | Prevents attempts to kill stray `mysqld` processes.             |
| `mysqld_safe --skip-syslog &`                              | Uses error log file instead of syslog for errors.               |
| `mysqld_safe --socket=/var/run/mysqld.sock &`              | Specifies the socket file for Unix socket connections.          |
| `mysqld_safe --syslog &`                                   | Writes error messages to syslog.                                |
| `mysqld_safe --syslog-tag=mysql_server &`                  | Adds a tag suffix for syslog messages.                          |
| `mysqld_safe --timezone=UTC &`                             | Sets the `TZ` time zone environment variable.                   |
| `mysqld_safe --user=mysql &`                               | Runs `mysqld` as the specified user (name or ID).               |

---
