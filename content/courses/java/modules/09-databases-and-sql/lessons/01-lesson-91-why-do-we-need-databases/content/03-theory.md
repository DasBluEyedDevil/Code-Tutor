---
type: "THEORY"
title: "SQL - The Database Language"
---

SQL (Structured Query Language) is how you talk to databases.

Four essential operations (CRUD):

CREATE (Insert data):
INSERT INTO students (name, age) VALUES ('Alice', 20);

READ (Query data):
SELECT * FROM students WHERE age > 18;

UPDATE (Modify data):
UPDATE students SET age = 21 WHERE name = 'Alice';

DELETE (Remove data):
DELETE FROM students WHERE age < 18;