---
type: "THEORY"
title: "Inserting Data - Adding Rows"
---

INSERT INTO table_name (columns) VALUES (values);

SINGLE ROW:
INSERT INTO students (name, age, email, gpa)
VALUES ('Alice Johnson', 20, 'alice@example.com', 3.75);

MULTIPLE ROWS:
INSERT INTO students (name, age, email, gpa) VALUES
    ('Bob Smith', 21, 'bob@example.com', 3.50),
    ('Carol Davis', 19, 'carol@example.com', 3.90),
    ('Dave Wilson', 22, 'dave@example.com', 3.25);

AUTO_INCREMENT ID:
Notice we didn't specify 'id' - database assigns it automatically!

NULL VALUES:
INSERT INTO students (name, age) VALUES ('Eve', 20);
// email and gpa will be NULL