---
type: "THEORY"
title: "From Compact Files to Full Class Syntax"
---

Until now, you have been writing code in compact source files with `void main()` and `IO.println`. That style is great for learning the basics -- variables, loops, methods -- without ceremony.

Now we are going to learn WHY Java has classes, and from this point forward, most of our code will use class declarations, because object-oriented programming is how real Java applications are built.

THE PROBLEM: MANAGING COMPLEX DATA

Imagine you are building a student management system. Each student has:
- name (String)
- age (int)
- gpa (double)
- isEnrolled (boolean)

For 100 students, you would need:
String student1Name, student2Name, ...student100Name;
int student1Age, student2Age, ...student100Age;
// 400 variables total!

This is:
- Impossible to manage
- Error-prone (easy to mix up which age belongs to which student)
- Cannot use loops or methods effectively

You need a way to group related data together and create your OWN data types.

This is what CLASSES do!