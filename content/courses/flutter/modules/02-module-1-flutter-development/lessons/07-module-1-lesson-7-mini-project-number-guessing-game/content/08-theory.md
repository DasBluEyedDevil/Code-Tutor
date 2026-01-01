---
type: "THEORY"
title: "Version 5: Adding Difficulty Levels"
---


Let's add difficulty levels with different ranges:




```dart
import 'dart:math';

int generateSecretNumber(String difficulty) {
  var random = Random();

  if (difficulty == 'easy') {
    return random.nextInt(50) + 1;  // 1-50
  } else if (difficulty == 'medium') {
    return random.nextInt(100) + 1;  // 1-100
  } else {  // hard
    return random.nextInt(500) + 1;  // 1-500
  }
}

void printHeader(String difficulty) {
  print('=== Number Guessing Game ===');
  print('Difficulty: ${difficulty.toUpperCase()}');

  if (difficulty == 'easy') {
    print('I\'m thinking of a number between 1 and 50...\n');
  } else if (difficulty == 'medium') {
    print('I\'m thinking of a number between 1 and 100...\n');
  } else {
    print('I\'m thinking of a number between 1 and 500...\n');
  }
}

void main() {
  var difficulty = 'easy';  // Try: 'easy', 'medium', 'hard'
  var secretNumber = generateSecretNumber(difficulty);

  printHeader(difficulty);

  // Rest of game logic...
}
```
