---
type: "THEORY"
title: "Functions That Return Values"
---


Sometimes you want a function to **give you back** a result.

**Conceptual Explanation**:
Think of a vending machine:
- You put in money and press a button (call the function)
- It **returns** a snack to you (the return value)

**The Pattern**:


**Real Example**:


**Notice**:
- **`int`** instead of `void` - this function returns an integer
- **`return`** keyword sends the value back



```dart
int add(int a, int b) {
  return a + b;
}

void main() {
  var result = add(5, 3);
  print('5 + 3 = $result');  // Output: 5 + 3 = 8

  var another = add(10, 20);
  print('10 + 20 = $another'); // Output: 10 + 20 = 30
}
```
