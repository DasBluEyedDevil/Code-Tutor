---
type: "THEORY"
title: "LEFT JOIN - Keep All Left Side Rows"
---

LEFT JOIN returns ALL rows from left table, even if no match:

SELECT students.name, enrollments.course_name
FROM students
LEFT JOIN enrollments ON students.id = enrollments.student_id;

If student 'Carol' (id=3) has no enrollments:
| name  | course_name |
|-------|-------------|
| Alice | Math 101    |
| Alice | Physics 201 |
| Bob   | Math 101    |
| Carol | NULL        |  ← Carol included even with no enrollments

USE CASE:
Find students who haven't enrolled in anything:
SELECT students.name
FROM students
LEFT JOIN enrollments ON students.id = enrollments.student_id
WHERE enrollments.id IS NULL;