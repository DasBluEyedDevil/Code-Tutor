---
type: "EXAMPLE"
title: "Combining All Three Features"
---

Records, pattern matching, and sealed classes work beautifully together. Use sealed classes for operation types, records for results, and pattern matching for exhaustive handling with guard clauses.

```dart
import 'dart:math';

// Sealed class for operations
sealed class MathOperation {}

class Add extends MathOperation {
  final num a, b;
  Add(this.a, this.b);
}

class Subtract extends MathOperation {
  final num a, b;
  Subtract(this.a, this.b);
}

class Multiply extends MathOperation {
  final num a, b;
  Multiply(this.a, this.b);
}

class Divide extends MathOperation {
  final num a, b;
  Divide(this.a, this.b);
}

// Record for results
typedef CalcResult = ({num result, String description});

// Pattern matching with sealed classes
CalcResult calculate(MathOperation op) {
  return switch (op) {
    Add(a: var x, b: var y) => (
      result: x + y,
      description: '$x + $y',
    ),
    Subtract(a: var x, b: var y) => (
      result: x - y,
      description: '$x - $y',
    ),
    Multiply(a: var x, b: var y) => (
      result: x * y,
      description: '$x * $y',
    ),
    Divide(a: var x, b: var y) when y != 0 => (
      result: x / y,
      description: '$x / $y',
    ),
    Divide(a: var x, b: _) => (
      result: double.nan,
      description: '$x / 0 (undefined)',
    ),
  };
}

void main() {
  var operations = [
    Add(10, 5),
    Subtract(10, 3),
    Multiply(4, 7),
    Divide(20, 4),
    Divide(10, 0),  // Edge case!
  ];
  
  for (var op in operations) {
    // Destructure the result record
    var (:result, :description) = calculate(op);
    print('$description = $result');
  }
}
```
