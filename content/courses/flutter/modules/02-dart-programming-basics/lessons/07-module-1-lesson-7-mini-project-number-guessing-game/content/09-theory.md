---
type: "THEORY"
title: "Full Game with All Features"
---


Here's the complete, polished version:




```dart
import 'dart:math';

// ========== GAME CONFIGURATION ==========

class GameConfig {
  static const Map<String, int> ranges = {
    'easy': 50,
    'medium': 100,
    'hard': 500,
  };

  static const Map<String, int> maxAttempts = {
    'easy': 10,
    'medium': 7,
    'hard': 12,
  };
}

// ========== GAME FUNCTIONS ==========

int generateSecretNumber(String difficulty) {
  var random = Random();
  var range = GameConfig.ranges[difficulty] ?? 100;
  return random.nextInt(range) + 1;
}

void printHeader(String difficulty) {
  print('\n' + '=' * 40);
  print('   NUMBER GUESSING GAME');
  print('=' * 40);
  print('Difficulty: ${difficulty.toUpperCase()}');
  var range = GameConfig.ranges[difficulty] ?? 100;
  print('Guess a number between 1 and $range');
  var maxAttempts = GameConfig.maxAttempts[difficulty] ?? 7;
  print('You have $maxAttempts attempts. Good luck!\n');
}

String checkGuess(int guess, int secret) {
  if (guess == secret) return 'correct';
  if (guess > secret) return 'high';
  return 'low';
}

void printAttempt(int attemptNum, int guess, String result) {
  print('--- Attempt $attemptNum ---');
  print('You guessed: $guess');

  if (result == 'correct') {
    print('🎉 CORRECT! You found it!');
  } else if (result == 'high') {
    var diff = guess - (guess * 0.1).toInt();  // Give a hint
    print('📉 Too high! Try something lower...');
  } else {
    print('📈 Too low! Try something higher...');
  }
  print('');
}

void printWinSummary(int attempts, List<int> history, String difficulty) {
  print('=' * 40);
  print('   🎊 VICTORY! 🎊');
  print('=' * 40);
  print('Difficulty: ${difficulty.toUpperCase()}');
  print('Attempts used: $attempts');
  print('Your guessing strategy: $history');

  if (attempts <= 3) {
    print('Rating: ⭐⭐⭐ Amazing! Lucky or skilled?');
  } else if (attempts <= 5) {
    print('Rating: ⭐⭐ Great job!');
  } else {
    print('Rating: ⭐ You made it!');
  }
  print('=' * 40 + '\n');
}

void printLossSummary(int secret, List<int> history) {
  print('=' * 40);
  print('   😢 GAME OVER');
  print('=' * 40);
  print('The number was: $secret');
  print('Your guesses: $history');
  print('Better luck next time!');
  print('=' * 40 + '\n');
}

// ========== MAIN GAME LOGIC ==========

void playGame(String difficulty) {
  var secretNumber = generateSecretNumber(difficulty);
  var maxAttempts = GameConfig.maxAttempts[difficulty] ?? 7;
  var attemptCount = 0;
  List<int> guessHistory = [];

  printHeader(difficulty);

  // Simulate guesses (in real game, this would be user input)
  var simulatedGuesses = [50, 25, 37, 31, 28, 29, 30];

  for (var guess in simulatedGuesses) {
    if (attemptCount >= maxAttempts) {
      printLossSummary(secretNumber, guessHistory);
      return;
    }

    attemptCount++;
    guessHistory.add(guess);

    var result = checkGuess(guess, secretNumber);
    printAttempt(attemptCount, guess, result);

    if (result == 'correct') {
      printWinSummary(attemptCount, guessHistory, difficulty);
      return;
    }
  }

  // If loop ends without finding number
  printLossSummary(secretNumber, guessHistory);
}

void main() {
  print('\n🎮 Welcome to the Number Guessing Game! 🎮\n');

  // Play different difficulties
  playGame('easy');
  playGame('medium');
  playGame('hard');

  print('Thanks for playing! 👋');
}
```
