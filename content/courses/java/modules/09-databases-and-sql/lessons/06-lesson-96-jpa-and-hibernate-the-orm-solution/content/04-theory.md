---
type: "THEORY"
title: "CRUD Operations with JPA"
---

CREATE (persist):
Student student = new Student("Alice", 20, "alice@email.com");
entityManager.persist(student);
// Hibernate generates: INSERT INTO students (name, age, email) VALUES (?, ?, ?)

READ (find):
Student student = entityManager.find(Student.class, 1L);
// Hibernate generates: SELECT * FROM students WHERE id = ?

UPDATE (automatic!):
student.setAge(21);  // Just modify the object
// Hibernate detects change and generates: UPDATE students SET age = ? WHERE id = ?

DELETE (remove):
entityManager.remove(student);
// Hibernate generates: DELETE FROM students WHERE id = ?

THE MAGIC:
JPA tracks entity state. Changes to managed entities are automatically persisted!