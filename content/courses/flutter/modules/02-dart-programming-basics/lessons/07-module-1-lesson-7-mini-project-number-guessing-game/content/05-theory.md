---
type: "THEORY"
title: "Version 2: Adding Random Numbers"
---


Instead of always guessing 42, let's make it random!

**First, import the Random library** at the top of your file:


**Understanding `random.nextInt(100) + 1`**:
- `random.nextInt(100)` gives 0-99
- `+ 1` shifts it to 1-100

**Try running it multiple times** - you'll get different numbers!



```dart
import 'dart:math';

void main() {
  // Generate random number between 1 and 100
  var random = Random();
  var secretNumber = random.nextInt(100) + 1;

  print('=== Number Guessing Game ===');
  print('I\'m thinking of a number between 1 and 100...');
  print('(Psst... it\'s $secretNumber - but pretend you don\'t know!)');

  // Rest of code...
}
```
