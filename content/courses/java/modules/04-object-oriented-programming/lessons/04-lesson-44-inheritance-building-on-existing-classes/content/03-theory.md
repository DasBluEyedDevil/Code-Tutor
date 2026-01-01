---
type: "THEORY"
title: "The 'extends' Keyword"
---

SYNTAX:
class ChildClass extends ParentClass { ... }

EXAMPLE:
class Animal {
    protected String name;  // 'protected' = accessible to subclasses
    protected int age;
    
    public Animal(String name, int age) {
        this.name = name;
        this.age = age;
    }
    
    public void eat() {
        System.out.println(name + " is eating.");
    }
}

class Dog extends Animal {
    public Dog(String name, int age) {
        super(name, age);  // Call parent constructor
    }
    
    public void bark() {
        System.out.println(name + " says Woof!");  // Can use 'name'
    }
}

Usage:
Dog myDog = new Dog("Buddy", 3);
myDog.eat();  // From Animal: "Buddy is eating."
myDog.bark();  // From Dog: "Buddy says Woof!"