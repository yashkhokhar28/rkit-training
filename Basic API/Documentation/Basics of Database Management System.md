# MySQL Query Rules

## **Format**

- **SELECT**
  - Fields
- **FROM**
  - Table Name (Alias)
- **JOIN**
- **WHERE**
  - Condition
- **GROUP BY**
  - Fields
- **ORDER BY**
  - Fields

---

## **Data Duplication**

- Verify columns to avoid duplication.
- Ensure no duplicate data columns.

---

## **Column Alias**

- Use column aliases **only when necessary**.

---

## **Table Join**

- Use columns that belong to a primary key or index.
- Ensure columns have the **same data type**.
- Use joins **only when necessary**.

---

## **WHERE Clause**

- Use primary key or index column **first**.
- Use date ranges with `BETWEEN` in **brackets**.
- Sequence columns from the **largest subset to the smallest subset**.

---

## **ORDER BY Clause**

- Use **only when necessary**.
- Use only the **required columns**.

---

## **SELECT with Star (`SELECT *`)**

- **Avoid using `SELECT *`**.
- Avoid `COUNT(*)`; instead, use `COUNT` with a key column.

---

## **Unique Row**

- Use `LIMIT 1` to ensure a **unique row**.

---

## **Function with WHERE**

- Use functions in the `WHERE` clause **only when necessary**.

---

## **Explain with SELECT**

- Use `EXPLAIN` and check the output for:
  - Missing keys
  - Row filters
  - Nested loops

---

## **Find_In_Set()**

- Avoid using `FIND_IN_SET()` functions.

---

## **Session Variable**

- Avoid using `@Session` variables; use local variables instead.
- Use the naming format: `v_variableName`.

---

## **Variable Name**

- Variable names must start with **small ‘v’** followed by an underscore (`_`).
  - Example: `v_variable;`
- Assign default values if possible.
  - Example: `v_variable INT DEFAULT 0;`

---

## **Parameter Name**

- Parameter names must start with **small ‘p’** followed by an underscore (`_`).
  - Example: `p_parameter`
- Use proper data types for parameters.
