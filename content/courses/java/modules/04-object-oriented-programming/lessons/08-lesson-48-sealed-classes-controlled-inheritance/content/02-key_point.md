---
type: "KEY_POINT"
title: "Sealed Classes: You Control Who Extends"
---

A SEALED class explicitly lists which classes can extend it:

public sealed class Shape permits Circle, Rectangle, Triangle {
    public abstract double area();
}

public final class Circle extends Shape {
    private final double radius;
    public Circle(double radius) { this.radius = radius; }
    public double area() { return Math.PI * radius * radius; }
}

public final class Rectangle extends Shape {
    private final double width, height;
    public Rectangle(double w, double h) { width = w; height = h; }
    public double area() { return width * height; }
}

public final class Triangle extends Shape {
    private final double base, height;
    public Triangle(double b, double h) { base = b; height = h; }
    public double area() { return 0.5 * base * height; }
}

// This would NOT compile!
class Pentagon extends Shape { }  // ERROR: Pentagon is not in permits clause

BENEFITS:
- Complete control over class hierarchy
- Compiler knows ALL possible subtypes
- Enables exhaustive pattern matching