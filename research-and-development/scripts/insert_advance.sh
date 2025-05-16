#!/bin/bash

MYSQL_USER="root"
mysql_cmd="mysql -u $MYSQL_USER"

enum_values=('small' 'medium' 'large')

for dbid in {1..10}; do
  for tid in {1..10}; do
    echo "Starting inserts into all_types_${tid} in test_db_${dbid}..."
    for i in {1..5000}; do
      # Log every 1000 rows inserted
      if (( i % 1000 == 1 )); then
        echo "Inserting rows $i - $((i+999)) into all_types_${tid} in test_db_${dbid}..."
      fi

      enum_val=${enum_values[$(( (i-1) % 3 ))]}   # cycles through small, medium, large
      set_val='a,b'  # example fixed value

      $mysql_cmd test_db_${dbid} -e "
        INSERT INTO all_types_${tid} (
          id, tinyint_col, smallint_col, mediumint_col, int_col, bigint_col,
          decimal_col, float_col, double_col, bit_col, bool_col, date_col,
          datetime_col, timestamp_col, time_col, year_col, char_col, varchar_col,
          binary_col, varbinary_col, tinytext_col, text_col, mediumtext_col, longtext_col,
          tinyblob_col, blob_col, mediumblob_col, longblob_col, json_col, enum_col, set_col
        ) VALUES (
          $i, 1, 2, 3, 4, 5,
          123.45, 1.23, 4.56, b'10101010', TRUE, '2020-01-01',
          '2020-01-01 10:10:10', NULL, '10:10:10', 2020, 'char10___', 'varchar_val',
          X'0102030405060708090A0B0C0D0E0F10', X'010203', 'tinytext_val', 'text_val', 'mediumtext_val', 'longtext_val',
          X'01', X'0203', X'040506', X'0708090A', JSON_OBJECT('key','value'), '$enum_val', '$set_val'
        );
      "
    done
    echo "Completed inserts into all_types_${tid} in test_db_${dbid}."
  done
done

echo "All inserts completed."
