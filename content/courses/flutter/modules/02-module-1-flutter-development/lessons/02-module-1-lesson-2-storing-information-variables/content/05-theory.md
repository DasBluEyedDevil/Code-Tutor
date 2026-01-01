---
type: "THEORY"
title: "Using Variables"
---


Once you create a variable, you can use it anywhere in your code:

```dart
void main() {
  var name = 'Alex';
  var age = 28;
  var city = 'New York';

  print('My name is $name');
  print('I am $age years old');
  print('I live in $city');
}
```

**Output**:

Notice the `$` symbol? That's how we insert variables into strings. It's called **string interpolation**.



```dart
My name is Alex
I am 28 years old
I live in New York
```
