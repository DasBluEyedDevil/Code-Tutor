---
type: "THEORY"
title: "Adding an \"Otherwise\": else"
---


What if we want to do something when the condition is false?


**Output**: `You are a minor.` (because 15 is not >= 18)

Think of it like:
- **IF** the condition is true, do the first thing
- **OTHERWISE** (else), do the second thing



```dart
void main() {
  var age = 15;

  if (age >= 18) {
    print('You are an adult!');
  } else {
    print('You are a minor.');
  }
}
```
