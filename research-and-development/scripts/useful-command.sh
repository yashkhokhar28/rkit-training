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
