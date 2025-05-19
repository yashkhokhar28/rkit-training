#!/bin/bash

MYSQL_USER="root"
mysql_cmd="mysql -u $MYSQL_USER"

# Prompt user for database names and number of rows
read -p "Enter database name(s) (space-separated): " -a DB_NAMES
read -p "Enter the number of rows to insert into each table: " ROW_COUNT

# Validate row count
if [[ -z "$ROW_COUNT" || "$ROW_COUNT" -le 0 ]]; then
  echo "Invalid row count. Exiting."
  exit 1
fi

# Enum and Set values for cycling
enum_values=('small' 'medium' 'large')
set_val='a,b'

# Loop over each database
for DB_NAME in "${DB_NAMES[@]}"; do
  echo "Starting inserts for database: $DB_NAME"

  # Insert into all_types_1 and all_types_2
  for tid in 1 2; do
    echo "Inserting into table all_types_${tid} in $DB_NAME..."

    for ((i=1; i<=ROW_COUNT; i++)); do
      if (( i % 1000 == 1 )); then
        echo "Inserting rows $i - $((i+999 < ROW_COUNT ? i+999 : ROW_COUNT))..."
      fi

      enum_val=${enum_values[$(( (i-1) % 3 ))]}

      $mysql_cmd "$DB_NAME" -e "
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

    echo "Completed inserts into all_types_${tid} in $DB_NAME."
  done

  echo "Finished all inserts for $DB_NAME."
done

echo "✅ All inserts completed for all databases."
