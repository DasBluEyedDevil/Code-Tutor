---
type: "THEORY"
title: "Common Patterns"
---


### Pattern 1: Range Checking

```dart
void main() {
  var score = 85;

  if (score >= 90) {
    print('Grade: A');
  } else if (score >= 80) {
    print('Grade: B');
  } else if (score >= 70) {
    print('Grade: C');
  } else if (score >= 60) {
    print('Grade: D');
  } else {
    print('Grade: F');
  }
}
```

### Pattern 2: Eligibility Checking

```dart
void main() {
  var age = 20;
  var isCitizen = true;
  var isRegistered = true;

  if (age >= 18 && isCitizen && isRegistered) {
    print('You are eligible to vote!');
  } else {
    print('You are not eligible to vote.');
  }
}
```

### Pattern 3: Validation




```dart
var username = '';

if (username == '') {
  print('Error: Username cannot be empty');
} else {
  print('Username: $username');
}
```
