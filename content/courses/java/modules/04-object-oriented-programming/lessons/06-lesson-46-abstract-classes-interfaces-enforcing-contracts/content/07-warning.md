---
type: "WARNING"
title: "Common Abstract Classes and Interfaces Pitfalls"
---

1. CONFUSING WHEN TO USE EACH:
- Use ABSTRACT CLASS for IS-A with shared state/code
- Use INTERFACE for CAN-DO behavior contracts

2. FORGETTING abstract KEYWORD:
class Shape { abstract void draw(); }  // ERROR! Class must be abstract

3. INTERFACE DEFAULT METHODS:
Interfaces CAN have default implementations:
interface Drawable {
    default void draw() { IO.println("Drawing..."); }
}
But use sparingly - can cause diamond problem!

4. SEALED TYPES:
For controlled hierarchies, consider sealed classes/interfaces:
sealed interface Shape permits Circle, Rectangle, Triangle {}

5. FUNCTIONAL INTERFACES:
Interfaces with ONE abstract method can be used with lambdas:
@FunctionalInterface
interface Runnable { void run(); }
Runnable r = () -> IO.println("Running!");