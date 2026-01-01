---
type: "KEY_POINT"
title: "Record Patterns (Java 21+)"
---

Java 21 introduced RECORD PATTERNS - automatically extract record components in pattern matching!

BASIC RECORD PATTERN:
record Point(int x, int y) {}

Object obj = new Point(3, 4);
if (obj instanceof Point(int x, int y)) {
    // x and y are extracted automatically!
    System.out.println("x=" + x + ", y=" + y);
}

IN SWITCH EXPRESSIONS:
record Circle(double radius) {}
record Rectangle(double width, double height) {}

double getArea(Object shape) {
    return switch (shape) {
        case Circle(double r) -> Math.PI * r * r;
        case Rectangle(double w, double h) -> w * h;
        default -> 0;
    };
}

NESTED RECORD PATTERNS:
record Line(Point start, Point end) {}

// Extract nested components in one pattern!
if (line instanceof Line(Point(int x1, int y1), Point(int x2, int y2))) {
    double length = Math.sqrt(Math.pow(x2-x1, 2) + Math.pow(y2-y1, 2));
}

WHY USE RECORD PATTERNS?
- Eliminates manual field extraction
- Type-safe deconstruction
- Works with sealed classes for exhaustive matching
- Makes code more declarative and readable