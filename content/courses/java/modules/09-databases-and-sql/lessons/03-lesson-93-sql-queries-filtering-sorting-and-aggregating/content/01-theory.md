---
type: "THEORY"
title: "WHERE Clause - Filtering Rows"
---

The WHERE clause filters which rows to return:

COMPARISON OPERATORS:
SELECT * FROM students WHERE age = 21;      // Equal
SELECT * FROM students WHERE age != 21;     // Not equal
SELECT * FROM students WHERE age > 20;      // Greater than
SELECT * FROM students WHERE age >= 20;     // Greater or equal
SELECT * FROM students WHERE age BETWEEN 18 AND 22;

PATTERN MATCHING:
SELECT * FROM students WHERE name LIKE 'A%';      // Starts with A
SELECT * FROM students WHERE email LIKE '%gmail.com';  // Ends with gmail.com
SELECT * FROM students WHERE name LIKE '%son%';   // Contains 'son'

NULL CHECKS:
SELECT * FROM students WHERE email IS NULL;
SELECT * FROM students WHERE email IS NOT NULL;

IN OPERATOR:
SELECT * FROM students WHERE age IN (18, 19, 20);
SELECT * FROM students WHERE name IN ('Alice', 'Bob', 'Carol');