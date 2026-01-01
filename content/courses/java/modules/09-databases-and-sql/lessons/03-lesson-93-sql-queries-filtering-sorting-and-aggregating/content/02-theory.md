---
type: "THEORY"
title: "ORDER BY - Sorting Results"
---

ORDER BY sorts results:

ASCENDING (default):
SELECT * FROM students ORDER BY age;
SELECT * FROM students ORDER BY name ASC;

DESCENDING:
SELECT * FROM students ORDER BY gpa DESC;

MULTIPLE COLUMNS:
SELECT * FROM students ORDER BY age ASC, gpa DESC;
// Sort by age first, then by GPA within same age

LIMIT - Restricting Results:
SELECT * FROM students ORDER BY gpa DESC LIMIT 5;
// Top 5 students by GPA

OFFSET:
SELECT * FROM students LIMIT 10 OFFSET 20;
// Skip first 20 results, then get 10
// Useful for pagination!