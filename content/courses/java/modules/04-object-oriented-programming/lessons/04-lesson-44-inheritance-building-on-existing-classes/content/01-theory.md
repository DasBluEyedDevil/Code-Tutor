---
type: "THEORY"
title: "The Problem: Duplicate Code Everywhere"
---

Imagine you need these classes:

class Dog {
    String name;
    int age;
    void eat() { System.out.println("Eating..."); }
    void sleep() { System.out.println("Sleeping..."); }
    void bark() { System.out.println("Woof!"); }
}

class Cat {
    String name;  // DUPLICATE!
    int age;  // DUPLICATE!
    void eat() { System.out.println("Eating..."); }  // DUPLICATE!
    void sleep() { System.out.println("Sleeping..."); }  // DUPLICATE!
    void meow() { System.out.println("Meow!"); }
}

This is HORRIBLE:
- Repeated code (name, age, eat, sleep)
- If you change eat(), must change in BOTH places
- Hard to maintain

Solution: INHERITANCE - share common code!