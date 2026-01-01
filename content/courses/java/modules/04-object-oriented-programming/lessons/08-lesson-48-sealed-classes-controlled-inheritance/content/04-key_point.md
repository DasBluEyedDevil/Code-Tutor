---
type: "KEY_POINT"
title: "Sealed Classes + Pattern Matching = Power"
---

The REAL power of sealed classes is EXHAUSTIVE PATTERN MATCHING!

sealed interface Shape permits Circle, Rectangle, Triangle {}
record Circle(double radius) implements Shape {}
record Rectangle(double w, double h) implements Shape {}
record Triangle(double b, double h) implements Shape {}

// Compiler KNOWS all cases - no default needed!
double calculateArea(Shape shape) {
    return switch (shape) {
        case Circle(double r) -> Math.PI * r * r;
        case Rectangle(double w, double h) -> w * h;
        case Triangle(double b, double h) -> 0.5 * b * h;
        // No default needed! Compiler knows these are ALL possibilities
    };
}

IF YOU FORGET A CASE:
double calculateArea(Shape shape) {
    return switch (shape) {
        case Circle(double r) -> Math.PI * r * r;
        case Rectangle(double w, double h) -> w * h;
        // COMPILE ERROR: 'switch' expression does not cover all possible input values
    };
}

This makes your code SAFER:
- Add new shape? Compiler tells you everywhere to update
- No runtime surprises from unexpected subtypes
- Self-documenting code structure