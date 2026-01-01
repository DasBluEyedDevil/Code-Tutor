---
type: "THEORY"
title: "Creating a Table - The Foundation"
---

Before storing data, you need a TABLE (like a spreadsheet):

CREATE TABLE students (
    id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(100) NOT NULL,
    age INT,
    email VARCHAR(255) UNIQUE,
    gpa DECIMAL(3, 2)
);

BREAKING IT DOWN:
- id: Unique identifier for each student
- PRIMARY KEY: This field uniquely identifies each row
- AUTO_INCREMENT: Database automatically assigns 1, 2, 3...
- VARCHAR(100): Text up to 100 characters
- NOT NULL: This field is required (can't be empty)
- UNIQUE: No two students can have the same email
- DECIMAL(3, 2): Numbers like 3.75 (3 total digits, 2 after decimal)