---
type: "THEORY"
title: "The Problem: Duplicate Code Everywhere"
---

Imagine you need these classes:

class Dog {
    String name;
    int age;
    void eat() { IO.println("Eating..."); }
    void sleep() { IO.println("Sleeping..."); }
    void bark() { IO.println("Woof!"); }
}

class Cat {
    String name;  // DUPLICATE!
    int age;  // DUPLICATE!
    void eat() { IO.println("Eating..."); }  // DUPLICATE!
    void sleep() { IO.println("Sleeping..."); }  // DUPLICATE!
    void meow() { IO.println("Meow!"); }
}

This is HORRIBLE:
- Repeated code (name, age, eat, sleep)
- If you change eat(), must change in BOTH places
- Hard to maintain

Solution: INHERITANCE - share common code!