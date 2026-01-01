---
type: "KEY_POINT"
title: "Query Order Matters!"
---

SQL queries are executed in this order:

1. FROM - Which table(s)
2. WHERE - Filter rows
3. GROUP BY - Group rows
4. HAVING - Filter groups
5. SELECT - Choose columns
6. ORDER BY - Sort results
7. LIMIT - Restrict number

Example query using all:

SELECT age, COUNT(*) as count, AVG(gpa) as avg_gpa
FROM students
WHERE email IS NOT NULL
GROUP BY age
HAVING count > 3
ORDER BY avg_gpa DESC
LIMIT 5;