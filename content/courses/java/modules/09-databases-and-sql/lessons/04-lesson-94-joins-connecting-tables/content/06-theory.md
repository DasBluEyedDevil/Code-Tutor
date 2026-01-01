---
type: "THEORY"
title: "Many-to-Many Relationships"
---

SCENARIO: Students can take many courses, courses have many students

BAD DESIGN: Store courses in student table
students:
| id | name  | courses_taken          |
|----|-------|------------------------|
| 1  | Alice | Math 101, Physics 201  |  ← Hard to query!

GOOD DESIGN: Junction table (join table)

students:
| id | name  |

courses:
| id | course_name |

enrollments (junction table):
| student_id | course_id |
|------------|----------|
| 1          | 101      |
| 1          | 201      |
| 2          | 101      |

THREE-WAY JOIN:
SELECT students.name, courses.course_name
FROM students
INNER JOIN enrollments ON students.id = enrollments.student_id
INNER JOIN courses ON enrollments.course_id = courses.id;