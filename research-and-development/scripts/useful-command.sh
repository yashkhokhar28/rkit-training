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
    INSERT INTO test.orders1 VALUES ($i, 'Item1_$i');
    INSERT INTO test.orders2 VALUES ($i, 'Item2_$i');
    INSERT INTO test.orders3 VALUES ($i, 'Item3_$i');
    INSERT INTO test.orders4 VALUES ($i, 'Item4_$i');
    INSERT INTO test.orders5 VALUES ($i, 'Item5_$i');
    INSERT INTO test.orders6 VALUES ($i, 'Item6_$i');
    INSERT INTO test.orders7 VALUES ($i, 'Item7_$i');
    INSERT INTO test.orders8 VALUES ($i, 'Item8_$i');
    INSERT INTO test.orders9 VALUES ($i, 'Item9_$i');
    INSERT INTO test.orders10 VALUES ($i, 'Item10_$i');
  "
done

mysql -u root -e "
USE test;
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
