---
type: "WARNING"
title: "Common Abstract Classes and Interfaces Pitfalls"
---

1. CONFUSING WHEN TO USE EACH:
- Use ABSTRACT CLASS for IS-A with shared state/code
- Use INTERFACE for CAN-DO behavior contracts

2. FORGETTING abstract KEYWORD:
class Shape { abstract void draw(); }  // ERROR! Class must be abstract

3. INTERFACE DEFAULT METHODS (Java 8+):
Interfaces CAN have default implementations now:
interface Drawable {
    default void draw() { System.out.println("Drawing..."); }
}
But use sparingly - can cause diamond problem!

4. SEALED TYPES (Java 17+):
For controlled hierarchies, consider sealed classes/interfaces:
sealed interface Shape permits Circle, Rectangle, Triangle {}

5. FUNCTIONAL INTERFACES:
Interfaces with ONE abstract method can be used with lambdas:
@FunctionalInterface
interface Runnable { void run(); }
Runnable r = () -> System.out.println("Running!");