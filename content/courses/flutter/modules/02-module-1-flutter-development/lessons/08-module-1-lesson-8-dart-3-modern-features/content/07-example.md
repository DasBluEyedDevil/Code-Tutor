---
type: "EXAMPLE"
title: "Destructuring Records"
---

Destructuring extracts record values into individual variables in one step. Use var (a, b) for positional records, or var (:name, :age) shorthand for named records. The underscore (_) ignores unwanted values.

```dart
void main() {
  // Create a record
  var person = ('Alice', 30);
  
  // Destructure into variables - no more $1, $2!
  var (name, age) = person;
  print('Name: $name, Age: $age');
  
  // Named record destructuring
  var user = (name: 'Bob', age: 25, city: 'NYC');
  var (:name, :age, :city) = user;  // Shorthand!
  print('$name ($age) from $city');
  
  // Swap values elegantly
  var a = 1;
  var b = 2;
  (a, b) = (b, a);  // Swap!
  print('a: $a, b: $b');  // a: 2, b: 1
  
  // Ignore values with _
  var data = ('important', 'skip this', 42);
  var (important, _, number) = data;
  print('$important: $number');
}
```
