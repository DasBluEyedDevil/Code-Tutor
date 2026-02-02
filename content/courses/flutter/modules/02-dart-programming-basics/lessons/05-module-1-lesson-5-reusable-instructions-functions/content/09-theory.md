---
type: "THEORY"
title: "Optional Parameters"
---


Sometimes you want parameters to be **optional**:


**Square brackets `[]`** make a parameter optional with a default value.



```dart
void greet(String name, [String greeting = 'Hello']) {
  print('$greeting, $name!');
}

void main() {
  greet('Alice');              // Output: Hello, Alice!
  greet('Bob', 'Hi');          // Output: Hi, Bob!
  greet('Charlie', 'Hey');     // Output: Hey, Charlie!
}
```
