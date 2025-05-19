#!/bin/bash

mysql_user="root"
mysql_cmd="mysql -u $mysql_user"

# Create 10 databases
for dbid in {1..5}; do
  echo "Creating database test_db_${dbid}..."
  $mysql_cmd -e "CREATE DATABASE IF NOT EXISTS test_db_${dbid};"
done

# Create 10 tables in each DB
for dbid in {1..5}; do
  for tid in {1..2}; do
    echo "Creating table all_types_${tid} in test_db_${dbid}..."
    $mysql_cmd test_db_${dbid} -e "
      CREATE TABLE IF NOT EXISTS all_types_${tid} (
        id INT,
        tinyint_col TINYINT,
        smallint_col SMALLINT,
        mediumint_col MEDIUMINT,
        int_col INT,
        bigint_col BIGINT,
        decimal_col DECIMAL(10,2),
        float_col FLOAT,
        double_col DOUBLE,
        bit_col BIT(8),
        bool_col BOOLEAN,
        date_col DATE,
        datetime_col DATETIME,
        timestamp_col TIMESTAMP NULL DEFAULT NULL,
        time_col TIME,
        year_col YEAR,
        char_col CHAR(10),
        varchar_col VARCHAR(255),
        binary_col BINARY(16),
        varbinary_col VARBINARY(255),
        tinytext_col TINYTEXT,
        text_col TEXT,
        mediumtext_col MEDIUMTEXT,
        longtext_col LONGTEXT,
        tinyblob_col TINYBLOB,
        blob_col BLOB,
        mediumblob_col MEDIUMBLOB,
        longblob_col LONGBLOB,
        json_col JSON,
        enum_col ENUM('small', 'medium', 'large'),
        set_col SET('a', 'b', 'c', 'd')
      );
    "
  done
done

# Insert 1000 rows per table per db, 100 rows per insert batch
for dbid in {1..5}; do
  for tid in {1..2}; do
    for start in {1..1000..100}; do
      end=$((start + 99))
      if (( end > 1000 )); then
        end=1000
      fi
      echo "Inserting rows $start-$end into all_types_${tid} in test_db_${dbid}..."
      # Build bulk insert SQL
      sql="INSERT INTO all_types_${tid} (
        id, tinyint_col, smallint_col, mediumint_col, int_col, bigint_col, decimal_col,
        float_col, double_col, bit_col, bool_col, date_col, datetime_col, timestamp_col,
        time_col, year_col, char_col, varchar_col, binary_col, varbinary_col, tinytext_col,
        text_col, mediumtext_col, longtext_col, tinyblob_col, blob_col, mediumblob_col,
        longblob_col, json_col, enum_col, set_col
      ) VALUES "

      for ((i=start; i<=end; i++)); do
        sql+="(
          ${i}, 1, 2, 3, 4, 5, 123.45,
          1.23, 4.56, b'10101010', true, '2023-01-01', '2023-01-01 12:00:00', NULL,
          '12:34:56', 2023, 'char_data', 'varchar data', 'binary_data_1234', 'varbinary_data',
          'tinytext example', 'text example', 'medium text example', 'long text example',
          'tinyblobdata', 'blobdata', 'mediumblobdata', 'longblobdata', 
          JSON_OBJECT('key', 'value'), 'medium', 'a,b'
        )"
        if (( i < end )); then
          sql+=","
        else
          sql+=";"
        fi
      done

      echo "$sql" | $mysql_cmd test_db_${dbid}
    done
  done
done

# Verify counts
echo "Verifying row counts..."
for dbid in {1..5}; do
  for tid in {1..2}; do
    count=$($mysql_cmd -N -e "SELECT COUNT(*) FROM test_db_${dbid}.all_types_${tid};")
    echo "test_db_${dbid}.all_types_${tid} has $count rows."
  done
done

echo "All done!"
