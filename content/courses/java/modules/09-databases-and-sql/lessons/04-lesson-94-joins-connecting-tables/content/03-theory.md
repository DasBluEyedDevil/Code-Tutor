---
type: "THEORY"
title: "INNER JOIN - Matching Rows Only"
---

INNER JOIN returns rows that exist in BOTH tables:

SELECT students.name, enrollments.course_name
FROM students
INNER JOIN enrollments ON students.id = enrollments.student_id;

Result:
| name  | course_name |
|-------|-------------|
| Alice | Math 101    |
| Alice | Physics 201 |
| Bob   | Math 101    |

BREAKDOWN:
- FROM students: Start with students table
- INNER JOIN enrollments: Connect to enrollments
- ON students.id = enrollments.student_id: How they connect

Only includes students WHO HAVE enrollments.