---
type: "THEORY"
title: "JPA - Java Persistence API"
---

JPA is a SPECIFICATION for Object-Relational Mapping (ORM).

It lets you work with databases using JAVA OBJECTS instead of SQL:

// With JPA - one line!
Student student = entityManager.find(Student.class, 123);

// Save a student
Student newStudent = new Student("Alice", 20);
entityManager.persist(newStudent);

// Update
student.setAge(21);
// That's it! Changes are automatically saved.

KEY CONCEPT:
You work with OBJECTS, JPA handles the SQL.

JPA is a specification. HIBERNATE is the most popular implementation.
Think of it like: JPA = Interface, Hibernate = Implementation.