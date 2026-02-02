---
type: "THEORY"
title: "Combining Conditions: AND / OR"
---


Sometimes you need to check multiple things at once.

### AND (&&) - Both must be true

```dart
void main() {
  var age = 25;
  var hasLicense = true;

  if (age >= 18 && hasLicense) {
    print('You can drive!');
  } else {
    print('You cannot drive.');
  }
}
```

### OR (||) - At least one must be true

```dart
void main() {
  var isWeekend = true;
  var isHoliday = false;

  if (isWeekend || isHoliday) {
    print('Day off! Time to relax.');
  } else {
    print('Time to work.');
  }
}
```

### NOT (!) - Flips true/false




```dart
void main() {
  var isRaining = false;

  if (!isRaining) {
    print('It\'s not raining. Let\'s go outside!');
  }
}
```
