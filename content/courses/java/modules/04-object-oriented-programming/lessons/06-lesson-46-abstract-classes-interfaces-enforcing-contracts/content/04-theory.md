---
type: "THEORY"
title: "Interfaces: Pure Contracts"
---

INTERFACE = a contract that defines what a class can do.
At their simplest, interfaces contain only abstract methods (no implementation).

interface Flyable {
    void fly();  // public abstract (automatically)
    void land();
}

class Bird implements Flyable {
    @Override
    public void fly() {
        IO.println("Flapping wings!");
    }
    
    @Override
    public void land() {
        IO.println("Landing on branch.");
    }
}

class Airplane implements Flyable {
    @Override
    public void fly() {
        IO.println("Jet engines roaring!");
    }
    
    @Override
    public void land() {
        IO.println("Landing on runway.");
    }
}

KEY: Bird and Airplane are NOT related by inheritance,
but BOTH can be treated as Flyable!