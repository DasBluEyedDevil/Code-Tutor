---
type: "EXAMPLE"
title: "Destructuring Lists and Maps"
---

Pattern matching works on lists and maps too. Use [...rest] to capture remaining elements, [...] to skip middle elements, and {'key': variable} for maps. Patterns can be nested for complex structures.

```dart
void main() {
  // List destructuring
  var numbers = [1, 2, 3, 4, 5];
  var [first, second, ...rest] = numbers;
  print('First: $first');       // 1
  print('Second: $second');     // 2
  print('Rest: $rest');         // [3, 4, 5]
  
  // Get first and last
  var [head, ..., tail] = numbers;
  print('Head: $head, Tail: $tail');  // 1, 5
  
  // Map destructuring
  var person = {'name': 'Alice', 'age': 30};
  var {'name': userName, 'age': userAge} = person;
  print('$userName is $userAge');
  
  // Nested destructuring
  var nested = [1, [2, 3], 4];
  var [a, [b, c], d] = nested;
  print('$a, $b, $c, $d');  // 1, 2, 3, 4
}
```
