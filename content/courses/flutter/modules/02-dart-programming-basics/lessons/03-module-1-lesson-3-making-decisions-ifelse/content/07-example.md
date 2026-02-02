---
type: "EXAMPLE"
title: "Real-World Examples"
---


### Example 1: Login Check

```dart
void main() {
  var username = 'admin';
  var password = 'secret123';

  if (username == 'admin' && password == 'secret123') {
    print('Login successful! Welcome, $username');
  } else {
    print('Invalid username or password');
  }
}
```

### Example 2: Weather Advice

```dart
void main() {
  var temperature = 75;
  var isRaining = true;

  if (isRaining) {
    print('Don\'t forget your umbrella!');
  } else if (temperature > 80) {
    print('It\'s hot! Stay hydrated.');
  } else if (temperature < 50) {
    print('It\'s cold! Wear a jacket.');
  } else {
    print('Nice weather for a walk!');
  }
}
```

**Note**: `\'` lets you put an apostrophe inside a single-quoted string.

### Example 3: Shopping Cart


**Output**: `You need $5.0 more.`



```dart
void main() {
  var itemPrice = 50.00;
  var walletMoney = 45.00;

  if (walletMoney >= itemPrice) {
    print('Purchase successful!');
  } else {
    var shortage = itemPrice - walletMoney;
    print('You need \$$shortage more.');
  }
}
```
