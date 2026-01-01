---
type: "THEORY"
title: "Interfaces: Pure Contracts"
---

INTERFACE = 100% abstract contract (no implementation)
Before Java 8, interfaces had ONLY abstract methods.

interface Flyable {
    void fly();  // public abstract (automatically)
    void land();
}

class Bird implements Flyable {
    @Override
    public void fly() {
        System.out.println("Flapping wings!");
    }
    
    @Override
    public void land() {
        System.out.println("Landing on branch.");
    }
}

class Airplane implements Flyable {
    @Override
    public void fly() {
        System.out.println("Jet engines roaring!");
    }
    
    @Override
    public void land() {
        System.out.println("Landing on runway.");
    }
}

KEY: Bird and Airplane are NOT related by inheritance,
but BOTH can be treated as Flyable!