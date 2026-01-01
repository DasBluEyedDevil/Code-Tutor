---
type: "KEY_POINT"
title: "Multiple Interfaces: Java's Alternative to Multiple Inheritance"
---

Java doesn't allow:
class Dog extends Animal, Robot { }  // ERROR!

But DOES allow:
class RobotDog extends Robot implements Walkable, Barkable { }

Real example:
interface Swimmable { void swim(); }
interface Flyable { void fly(); }
interface Walkable { void walk(); }

class Duck extends Animal implements Flyable, Swimmable, Walkable {
    public void fly() { System.out.println("Flying"); }
    public void swim() { System.out.println("Swimming"); }
    public void walk() { System.out.println("Walking"); }
}

Now Duck can be used as:
- Animal (inheritance)
- Flyable (interface)
- Swimmable (interface)
- Walkable (interface)

This is POWERFUL for flexible design!