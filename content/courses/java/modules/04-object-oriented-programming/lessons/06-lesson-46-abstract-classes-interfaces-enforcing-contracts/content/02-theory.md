---
type: "THEORY"
title: "Abstract Classes: Templates with Rules"
---

ABSTRACT CLASS = Class that CANNOT be instantiated
ABSTRACT METHOD = Method with NO implementation (body)

abstract class Animal {  // Can't do: new Animal()
    protected String name;
    
    public Animal(String name) {
        this.name = name;
    }
    
    // ABSTRACT METHOD - no body, must be overridden
    public abstract void makeSound();
    
    // CONCRETE METHOD - has implementation
    public void sleep() {
        IO.println(name + " is sleeping.");
    }
}

class Dog extends Animal {
    public Dog(String name) {
        super(name);
    }
    
    @Override
    public void makeSound() {  // MUST implement this!
        IO.println("Woof!");
    }
}

// Animal a = new Animal("Generic");  // COMPILE ERROR!
Dog d = new Dog("Buddy");  // Works!
d.makeSound();  // "Woof!"
d.sleep();  // "Buddy is sleeping." (inherited)