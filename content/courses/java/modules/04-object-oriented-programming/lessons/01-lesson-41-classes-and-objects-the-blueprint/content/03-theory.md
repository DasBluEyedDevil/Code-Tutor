---
type: "THEORY"
title: "Creating a Class"
---

A class is a template for creating objects.

public class Student {
    // FIELDS (data the object stores)
    String name;
    int age;
    double gpa;
    
    // METHODS (what the object can do)
    void study() {
        System.out.println(name + " is studying");
    }
}

Breaking it down:
1. 'public class Student' - declares a new type called Student
2. Fields (variables inside the class) - the data each student has
3. Methods (functions inside the class) - actions a student can do

Creating and using objects:

// Create two student objects
Student alice = new Student();
alice.name = "Alice";
alice.age = 20;
alice.gpa = 3.8;

Student bob = new Student();
bob.name = "Bob";
bob.age = 22;
bob.gpa = 3.5;

// Use the objects
alice.study();  // Prints: Alice is studying
bob.study();    // Prints: Bob is studying