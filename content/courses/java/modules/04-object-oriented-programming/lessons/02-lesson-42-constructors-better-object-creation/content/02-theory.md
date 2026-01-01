---
type: "THEORY"
title: "Constructor Syntax"
---

A constructor is a special method that runs when you create an object.

public class Student {
    String name;
    int age;
    double gpa;
    
    // Constructor
    public Student(String name, int age, double gpa) {
        this.name = name;
        this.age = age;
        this.gpa = gpa;
    }
}

Key points:
1. Constructor name MUST match class name
2. No return type (not even void)
3. 'this.name' refers to the field, 'name' refers to the parameter

Usage:
Student alice = new Student("Alice", 20, 3.8);