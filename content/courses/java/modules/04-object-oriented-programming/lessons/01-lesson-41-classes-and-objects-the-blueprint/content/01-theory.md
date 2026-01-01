---
type: "THEORY"
title: "The Problem: Managing Complex Data"
---

Imagine you're building a student management system. Each student has:
- name (String)
- age (int)
- gpa (double)
- isEnrolled (boolean)

For 100 students, you'd need:
String student1Name, student2Name, ...student100Name;
int student1Age, student2Age, ...student100Age;
// 400 variables total!

This is:
❌ Impossible to manage
❌ Error-prone (easy to mix up which age belongs to which student)
❌ Can't use loops or methods effectively

You need a way to group related data together and create your OWN data types.

This is what CLASSES do!