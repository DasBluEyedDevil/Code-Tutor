---
type: "THEORY"
title: "The Problem: Data Disappears"
---

Your program can store data in variables and arrays:

ArrayList<Student> students = new ArrayList<>();
students.add(new Student("Alice", 20));

But when the program ends... POOF! All data is gone.

Next time you run it, you start from scratch.

Real applications need PERSISTENCE:
- User accounts must survive restarts
- Orders must be saved
- Posts, messages, files must persist

This is why we need DATABASES.