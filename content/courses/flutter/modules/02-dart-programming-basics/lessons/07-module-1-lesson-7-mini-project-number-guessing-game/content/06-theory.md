---
type: "THEORY"
title: "Version 3: Tracking Attempts and History"
---


Let's count how many guesses it takes and remember all guesses:


**New features**:
- `attemptCount` tracks number of tries
- `guessHistory` remembers all guesses
- We show a summary at the end



```dart
import 'dart:math';

void main() {
  var random = Random();
  var secretNumber = random.nextInt(100) + 1;
  var guesses = [50, 30, 40, 45, 42];  // Simulated guesses
  var attemptCount = 0;
  List<int> guessHistory = [];

  print('=== Number Guessing Game ===');
  print('I\'m thinking of a number between 1 and 100...');

  for (var guess in guesses) {
    attemptCount++;
    guessHistory.add(guess);

    print('\n--- Attempt $attemptCount ---');
    print('You guessed: $guess');

    if (guess == secretNumber) {
      print('🎉 Correct! You win!');
      print('It took you $attemptCount attempts.');
      print('Your guesses: $guessHistory');
      break;
    } else if (guess > secretNumber) {
      print('📉 Too high! Try again.');
    } else {
      print('📈 Too low! Try again.');
    }
  }
}
```
