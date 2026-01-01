---
type: "THEORY"
title: "Selecting Data - Querying Tables"
---

SELECT columns FROM table WHERE conditions;

GET EVERYTHING:
SELECT * FROM students;
// * means "all columns"

SPECIFIC COLUMNS:
SELECT name, gpa FROM students;

WITH CONDITIONS:
SELECT * FROM students WHERE age >= 21;
SELECT * FROM students WHERE gpa > 3.5;
SELECT name FROM students WHERE email LIKE '%gmail.com';

COMBINING CONDITIONS:
SELECT * FROM students WHERE age > 20 AND gpa > 3.0;
SELECT * FROM students WHERE age < 20 OR gpa > 3.8;