mysql -u root -e "
CREATE DATABASE IF NOT EXISTS test;
USE test;
CREATE TABLE IF NOT EXISTS orders1 (id INT, item VARCHAR(50));
CREATE TABLE IF NOT EXISTS orders2 (id INT, item VARCHAR(50));
CREATE TABLE IF NOT EXISTS orders3 (id INT, item VARCHAR(50));
CREATE TABLE IF NOT EXISTS orders4 (id INT, item VARCHAR(50));
CREATE TABLE IF NOT EXISTS orders5 (id INT, item VARCHAR(50));
CREATE TABLE IF NOT EXISTS orders6 (id INT, item VARCHAR(50));
CREATE TABLE IF NOT EXISTS orders7 (id INT, item VARCHAR(50));
CREATE TABLE IF NOT EXISTS orders8 (id INT, item VARCHAR(50));
CREATE TABLE IF NOT EXISTS orders9 (id INT, item VARCHAR(50));
CREATE TABLE IF NOT EXISTS orders10 (id INT, item VARCHAR(50));
"

for i in {1..10}; do
  mysql -u root -e "
    INSERT INTO test_1.orders1 VALUES ($i, 'Item1_$i');
    INSERT INTO test_1.orders2 VALUES ($i, 'Item2_$i');
    INSERT INTO test_1.orders3 VALUES ($i, 'Item3_$i');
    INSERT INTO test_1.orders4 VALUES ($i, 'Item4_$i');
    INSERT INTO test_1.orders5 VALUES ($i, 'Item5_$i');
    INSERT INTO test_1.orders6 VALUES ($i, 'Item6_$i');
    INSERT INTO test_1.orders7 VALUES ($i, 'Item7_$i');
    INSERT INTO test_1.orders8 VALUES ($i, 'Item8_$i');
    INSERT INTO test_1.orders9 VALUES ($i, 'Item9_$i');
    INSERT INTO test_1.orders10 VALUES ($i, 'Item10_$i');
  "
done

mysql -u root -e "
USE test_1;
SELECT 'orders1' AS table_name, COUNT(*) FROM orders1
UNION
SELECT 'orders2', COUNT(*) FROM orders2
UNION
SELECT 'orders3', COUNT(*) FROM orders3
UNION
SELECT 'orders4', COUNT(*) FROM orders4
UNION
SELECT 'orders5', COUNT(*) FROM orders5
UNION
SELECT 'orders6', COUNT(*) FROM orders6
UNION
SELECT 'orders7', COUNT(*) FROM orders7
UNION
SELECT 'orders8', COUNT(*) FROM orders8
UNION
SELECT 'orders9', COUNT(*) FROM orders9
UNION
SELECT 'orders10', COUNT(*) FROM orders10;
"

mysql -u root -e "
DROP DATABASE IF EXISTS test;
DROP DATABASE IF EXISTS test_1;
DROP DATABASE IF EXISTS test_2;
DROP DATABASE IF EXISTS test_3;
DROP DATABASE IF EXISTS test_4;
DROP DATABASE IF EXISTS test_5;
DROP DATABASE IF EXISTS test_6;
DROP DATABASE IF EXISTS test_7;
DROP DATABASE IF EXISTS test_8;
DROP DATABASE IF EXISTS test_9;
DROP DATABASE IF EXISTS test_10;
"

mysql -u root -e "
CREATE DATABASE IF NOT EXISTS test_all_types;
USE test_all_types;

-- Create 10 tables with same structure (no PRIMARY KEY)
$(for i in {1..10}; do
cat <<EOF
CREATE TABLE IF NOT EXISTS all_types_${i} (
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
EOF
done)

-- Insert dummy data into each table
$(for i in {1..10}; do
cat <<EOF
INSERT INTO all_types_${i} (
    id, tinyint_col, smallint_col, mediumint_col, int_col, bigint_col, decimal_col,
    float_col, double_col, bit_col, bool_col, date_col, datetime_col, timestamp_col,
    time_col, year_col, char_col, varchar_col, binary_col, varbinary_col, tinytext_col,
    text_col, mediumtext_col, longtext_col, tinyblob_col, blob_col, mediumblob_col,
    longblob_col, json_col, enum_col, set_col
) VALUES (
    ${i}, 1, 2, 3, 4, 5, 123.45,
    1.23, 4.56, b'10101010', true, '2023-01-01', '2023-01-01 12:00:00', NULL,
    '12:34:56', 2023, 'char_data', 'varchar data', 'binary_data_123456', 'varbinary_data',
    'tinytext example', 'text example', 'medium text example', 'long text example',
    'tinyblobdata', 'blobdata', 'mediumblobdata', 'longblobdata', 
    JSON_OBJECT('key', 'value'), 'medium', 'a,b'
);
EOF
done)

-- Select counts from all tables
SELECT 'all_types_1' AS table_name, COUNT(*) FROM all_types_1
UNION ALL
SELECT 'all_types_2', COUNT(*) FROM all_types_2
UNION ALL
SELECT 'all_types_3', COUNT(*) FROM all_types_3
UNION ALL
SELECT 'all_types_4', COUNT(*) FROM all_types_4
UNION ALL
SELECT 'all_types_5', COUNT(*) FROM all_types_5
UNION ALL
SELECT 'all_types_6', COUNT(*) FROM all_types_6
UNION ALL
SELECT 'all_types_7', COUNT(*) FROM all_types_7
UNION ALL
SELECT 'all_types_8', COUNT(*) FROM all_types_8
UNION ALL
SELECT 'all_types_9', COUNT(*) FROM all_types_9
UNION ALL
SELECT 'all_types_10', COUNT(*) FROM all_types_10;
"

# Drop database when done:
mysql -u root -e "DROP DATABASE IF EXISTS test_all_types;"
