---
type: "THEORY"
title: "JPQL - Querying with JPA"
---

JPQL (Java Persistence Query Language) is like SQL but for entities:

// Find all students
List<Student> students = entityManager
    .createQuery("SELECT s FROM Student s", Student.class)
    .getResultList();

// With conditions
List<Student> adults = entityManager
    .createQuery("SELECT s FROM Student s WHERE s.age >= :age", Student.class)
    .setParameter("age", 18)
    .getResultList();

// Single result
Student student = entityManager
    .createQuery("SELECT s FROM Student s WHERE s.email = :email", Student.class)
    .setParameter("email", "alice@email.com")
    .getSingleResult();

KEY DIFFERENCE FROM SQL:
- Use entity/field names, not table/column names
- SELECT s FROM Student s (not SELECT * FROM students)