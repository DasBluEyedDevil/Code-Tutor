---
type: "EXAMPLE"
title: "If-Case Pattern Matching"
---

If-case statements combine pattern matching with conditional execution. The pattern must match AND the optional 'when' guard must be true for the block to execute. Great for handling specific data shapes.

```dart
void processData(Object data) {
  // If-case for conditional pattern matching
  if (data case int n when n > 0) {
    print('Positive integer: $n');
  }
  
  if (data case String s when s.startsWith('Hello')) {
    print('Greeting: $s');
  }
  
  // Match and extract from records
  if (data case (String name, int age) when age >= 18) {
    print('$name is an adult');
  }
  
  // Match list patterns
  if (data case [var first, _, var last]) {
    print('Three elements: first=$first, last=$last');
  }
}

void main() {
  processData(42);                    // Positive integer: 42
  processData('Hello, World!');       // Greeting: Hello, World!
  processData(('Alice', 25));         // Alice is an adult
  processData([1, 2, 3]);             // Three elements: first=1, last=3
  processData(-5);                    // (no output - doesn't match)
}
```
