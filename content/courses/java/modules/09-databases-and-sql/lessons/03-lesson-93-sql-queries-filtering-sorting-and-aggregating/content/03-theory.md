---
type: "THEORY"
title: "Aggregate Functions - Calculations"
---

Calculate values across multiple rows:

COUNT - How many rows:
SELECT COUNT(*) FROM students;
SELECT COUNT(*) FROM students WHERE age > 20;

SUM - Total of all values:
SELECT SUM(age) FROM students;

AVG - Average:
SELECT AVG(gpa) FROM students;

MIN and MAX:
SELECT MIN(age) FROM students;
SELECT MAX(gpa) FROM students;

COMBINING:
SELECT 
    COUNT(*) as total_students,
    AVG(gpa) as average_gpa,
    MIN(age) as youngest,
    MAX(age) as oldest
FROM students;