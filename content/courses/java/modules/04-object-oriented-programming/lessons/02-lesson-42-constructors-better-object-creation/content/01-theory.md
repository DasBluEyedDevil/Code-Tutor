---
type: "THEORY"
title: "The Problem: Tedious Object Setup"
---

Creating objects the current way is tedious:

Student student = new Student();
student.name = "Alice";
student.age = 20;
student.gpa = 3.8;

Four lines just to create one student! And you might forget to set a field.

Wouldn't it be nice to write:
Student student = new Student("Alice", 20, 3.8);

This is what CONSTRUCTORS allow!