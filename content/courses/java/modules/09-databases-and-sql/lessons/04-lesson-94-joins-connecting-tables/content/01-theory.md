---
type: "THEORY"
title: "The Problem: Data Spread Across Tables"
---

Imagine a school database:

students table:
| id | name  | age |
|----|-------|-----|
| 1  | Alice | 20  |
| 2  | Bob   | 21  |

enrollments table:
| id | student_id | course_name     |
|----|------------|----------------|
| 1  | 1          | Math 101        |
| 2  | 1          | Physics 201     |
| 3  | 2          | Math 101        |

How do you find all courses Alice is enrolled in?
You need to CONNECT (JOIN) these tables!