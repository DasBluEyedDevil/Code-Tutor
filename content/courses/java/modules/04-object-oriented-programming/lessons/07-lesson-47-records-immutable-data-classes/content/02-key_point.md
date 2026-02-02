---
type: "KEY_POINT"
title: "Records: One Line = Complete Data Class"
---

A record is a special kind of class designed to hold immutable data.

The entire Person class above can be written as:

public record Person(String name, int age) {}

That's it! ONE LINE!

What you get automatically:
- Private final fields for each component
- A constructor that takes all components
- Accessor methods: name() and age() (not getName/getAge)
- equals() that compares all components
- hashCode() based on all components
- toString() that shows all components

Usage:
Person alice = new Person("Alice", 30);
IO.println(alice.name());   // Alice
IO.println(alice.age());    // 30
IO.println(alice);          // Person[name=Alice, age=30]