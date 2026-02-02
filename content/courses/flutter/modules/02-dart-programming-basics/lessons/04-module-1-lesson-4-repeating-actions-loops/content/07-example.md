---
type: "EXAMPLE"
title: "Real-World Examples"
---


### Example 1: Multiplication Table

```dart
void main() {
  var number = 7;

  print('Multiplication table for $number:');
  for (var i = 1; i <= 10; i++) {
    print('$number x $i = ${number * i}');
  }
}
```

**Output**:
```
7 x 1 = 7
7 x 2 = 14
7 x 3 = 21
... (and so on)
```

### Example 2: Password Attempts

```dart
void main() {
  var correctPassword = 'flutter123';
  var maxAttempts = 3;
  var attempts = 0;
  var loggedIn = false;

  // Simulated password attempts
  var userInputs = ['wrong', 'incorrect', 'flutter123'];

  for (var i = 0; i < maxAttempts && !loggedIn; i++) {
    attempts++;
    var input = userInputs[i];

    if (input == correctPassword) {
      print('Login successful on attempt $attempts!');
      loggedIn = true;
    } else {
      print('Attempt $attempts failed. ${maxAttempts - attempts} attempts remaining.');
    }
  }

  if (!loggedIn) {
    print('Account locked after $maxAttempts failed attempts.');
  }
}
```

### Example 3: Sum of Numbers




```dart
void main() {
  var sum = 0;

  for (var i = 1; i <= 10; i++) {
    sum += i;  // Same as: sum = sum + i
  }

  print('Sum of 1 to 10 is: $sum');  // Output: 55
}
```
