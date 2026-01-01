---
type: "THEORY"
title: "GROUP BY - Grouping Results"
---

GROUP BY combines rows with same values:

SELECT age, COUNT(*) as student_count
FROM students
GROUP BY age;

Result:
| age | student_count |
|-----|---------------|
| 18  | 5             |
| 19  | 8             |
| 20  | 12            |
| 21  | 7             |

WITH AGGREGATES:
SELECT age, AVG(gpa) as avg_gpa
FROM students
GROUP BY age;

HAVING - Filtering Groups:
SELECT age, COUNT(*) as count
FROM students
GROUP BY age
HAVING count > 5;
// Only show age groups with more than 5 students

NOTE: WHERE filters ROWS, HAVING filters GROUPS