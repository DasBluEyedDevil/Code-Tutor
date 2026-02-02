---
type: "THEORY"
title: "Version 4: Organizing with Functions"
---


Our code is getting messy. Let's use functions to organize it:


**Much better!** Each function has one job:
- `generateSecretNumber()` - creates random number
- `checkGuess()` - compares guess to secret
- `printHeader()` - shows game title
- `printSummary()` - shows final stats



```dart
import 'dart:math';

// Function to generate random number
int generateSecretNumber() {
  var random = Random();
  return random.nextInt(100) + 1;
}

// Function to check a guess
String checkGuess(int guess, int secret) {
  if (guess == secret) {
    return 'correct';
  } else if (guess > secret) {
    return 'high';
  } else {
    return 'low';
  }
}

// Function to print game header
void printHeader() {
  print('=== Number Guessing Game ===');
  print('I\'m thinking of a number between 1 and 100...\n');
}

// Function to print game summary
void printSummary(int attempts, List<int> history) {
  print('\n🎉 You win!');
  print('It took you $attempts attempts.');
  print('Your guesses: $history');
}

void main() {
  var secretNumber = generateSecretNumber();
  var guesses = [50, 30, 70, 60, 55, 52, 51];  // Simulated
  var attemptCount = 0;
  List<int> guessHistory = [];

  printHeader();

  for (var guess in guesses) {
    attemptCount++;
    guessHistory.add(guess);

    print('Attempt $attemptCount: You guessed $guess');

    var result = checkGuess(guess, secretNumber);

    if (result == 'correct') {
      printSummary(attemptCount, guessHistory);
      break;
    } else if (result == 'high') {
      print('📉 Too high! Try again.\n');
    } else {
      print('📈 Too low! Try again.\n');
    }
  }
}
```
