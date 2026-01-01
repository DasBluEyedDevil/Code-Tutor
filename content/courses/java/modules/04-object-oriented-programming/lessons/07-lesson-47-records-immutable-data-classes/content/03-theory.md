---
type: "THEORY"
title: "Record Syntax and Features"
---

BASIC RECORD:
public record Point(int x, int y) {}

RECORD WITH COMPACT CONSTRUCTOR:
Records can have a compact constructor for validation:

public record Person(String name, int age) {
    public Person {
        // Compact constructor - no parameters needed
        if (age < 0) {
            throw new IllegalArgumentException("Age cannot be negative");
        }
        if (name == null || name.isBlank()) {
            throw new IllegalArgumentException("Name is required");
        }
        // Fields are automatically assigned after this block
    }
}

RECORD WITH METHODS:
public record Rectangle(int width, int height) {
    // You can add methods to records!
    public int area() {
        return width * height;
    }
    
    public int perimeter() {
        return 2 * (width + height);
    }
}

RECORD WITH STATIC MEMBERS:
public record Temperature(double celsius) {
    public static Temperature freezing() {
        return new Temperature(0);
    }
    
    public static Temperature boiling() {
        return new Temperature(100);
    }
    
    public double fahrenheit() {
        return celsius * 9/5 + 32;
    }
}