---
type: "THEORY"
title: "The Basic Pattern: if"
---


Here's the simplest decision:


**Conceptual Explanation**:
- We check a condition: "Is age greater than or equal to 18?"
- If the answer is YES (true), we run the code inside the `{ }`
- If the answer is NO (false), we skip that code

**Output**: `You are an adult!` (because 20 is >= 18)



```dart
void main() {
  var age = 20;

  if (age >= 18) {
    print('You are an adult!');
  }
}
```
