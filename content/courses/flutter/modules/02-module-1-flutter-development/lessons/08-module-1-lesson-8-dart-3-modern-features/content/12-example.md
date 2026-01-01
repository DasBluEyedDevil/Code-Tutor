---
type: "EXAMPLE"
title: "Sealed Class Basics"
---

Sealed classes define a closed set of subtypes that can only be extended in the same file. The compiler knows all possible subtypes, enabling exhaustive switch statements without a default case.

```dart
// Define a sealed class hierarchy
sealed class Shape {}

class Circle extends Shape {
  final double radius;
  Circle(this.radius);
}

class Rectangle extends Shape {
  final double width;
  final double height;
  Rectangle(this.width, this.height);
}

class Triangle extends Shape {
  final double base;
  final double height;
  Triangle(this.base, this.height);
}

// The compiler KNOWS all possible shapes!
double calculateArea(Shape shape) {
  // Exhaustive switch - compiler ensures all cases covered
  return switch (shape) {
    Circle(radius: var r) => 3.14159 * r * r,
    Rectangle(width: var w, height: var h) => w * h,
    Triangle(base: var b, height: var h) => 0.5 * b * h,
    // No default needed - all cases covered!
  };
}

void main() {
  var shapes = [
    Circle(5),
    Rectangle(4, 6),
    Triangle(3, 4),
  ];
  
  for (var shape in shapes) {
    print('Area: ${calculateArea(shape).toStringAsFixed(2)}');
  }
}
```
