---
type: "EXAMPLE"
title: "Switch with Patterns"
---

Dart 3 switch expressions combine type checking, value extraction, and conditional logic. Use 'when' for guard clauses. Patterns match types, extract values, and bind them to variables in one concise expression.

```dart
String describeValue(Object value) {
  return switch (value) {
    // Match specific values
    0 => 'zero',
    1 => 'one',
    
    // Match types with binding
    int n when n < 0 => 'negative integer: $n',
    int n when n > 100 => 'large integer: $n',
    int n => 'integer: $n',
    
    // Match strings
    String s when s.isEmpty => 'empty string',
    String s when s.length > 10 => 'long string',
    String s => 'string: $s',
    
    // Match lists
    [] => 'empty list',
    [var single] => 'single element: $single',
    [var first, ...var rest] => 'list starting with $first',
    
    // Match records
    (int x, int y) => 'point at ($x, $y)',
    
    // Catch-all
    _ => 'something else: $value',
  };
}

void main() {
  print(describeValue(0));           // zero
  print(describeValue(-5));          // negative integer: -5
  print(describeValue(150));         // large integer: 150
  print(describeValue('hello'));     // string: hello
  print(describeValue([]));          // empty list
  print(describeValue([1, 2, 3]));   // list starting with 1
  print(describeValue((10, 20)));    // point at (10, 20)
}
```
