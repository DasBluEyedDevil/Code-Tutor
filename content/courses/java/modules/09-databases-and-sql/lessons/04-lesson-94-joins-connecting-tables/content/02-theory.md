---
type: "THEORY"
title: "Foreign Keys - Linking Tables"
---

A FOREIGN KEY references another table's PRIMARY KEY:

CREATE TABLE enrollments (
    id INT PRIMARY KEY AUTO_INCREMENT,
    student_id INT,
    course_name VARCHAR(100),
    FOREIGN KEY (student_id) REFERENCES students(id)
);

student_id is a FOREIGN KEY:
- Points to id in students table
- Creates a relationship between tables
- Enforces referential integrity

You can't:
- Add enrollment with student_id = 999 (student doesn't exist)
- Delete a student who has enrollments (without CASCADE)