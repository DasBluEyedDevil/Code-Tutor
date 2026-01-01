---
type: "THEORY"
title: "Overriding vs Overloading - Don't Confuse Them!"
---

OVERRIDING (this lesson):
- SAME method signature in child class
- REPLACES parent's version
- Requires inheritance

class Animal {
    void eat() { ... }
}
class Dog extends Animal {
    @Override
    void eat() { ... }  // OVERRIDING
}

OVERLOADING (Epoch 1):
- DIFFERENT parameters, same name
- ADDS new version
- Same class

class Calculator {
    int add(int a, int b) { ... }
    double add(double a, double b) { ... }  // OVERLOADING
}