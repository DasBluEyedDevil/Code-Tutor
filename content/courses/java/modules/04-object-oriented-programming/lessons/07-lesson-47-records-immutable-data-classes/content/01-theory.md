---
type: "THEORY"
title: "The Problem: Too Much Boilerplate"
---

Records are a standard feature since Java 16 and are widely used in modern Java development.

How much code does it take to create a simple data class in Java?

public class Person {
    private final String name;
    private final int age;
    
    public Person(String name, int age) {
        this.name = name;
        this.age = age;
    }
    
    public String getName() { return name; }
    public int getAge() { return age; }
    
    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        Person person = (Person) o;
        return age == person.age && Objects.equals(name, person.name);
    }
    
    @Override
    public int hashCode() {
        return Objects.hash(name, age);
    }
    
    @Override
    public String toString() {
        return "Person[name=" + name + ", age=" + age + "]";
    }
}

That's over 25 lines for a simple class with just 2 fields!

Java introduced RECORDS to solve this problem.