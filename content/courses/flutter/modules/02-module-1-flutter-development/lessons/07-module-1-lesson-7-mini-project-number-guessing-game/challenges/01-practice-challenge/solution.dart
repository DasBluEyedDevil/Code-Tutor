// Solution: Number Guessing Game - Range Tracker

import 'dart:math';

void main() {
  final random = Random();
  final secretNumber = random.nextInt(100) + 1;  // 1-100
  
  // Track the valid range
  var lowBound = 1;
  var highBound = 100;
  
  // Simulate guesses
  void makeGuess(int guess) {
    print('You guessed: $guess');
    
    if (guess > secretNumber) {
      highBound = guess - 1;  // Number must be lower
      print('Too high! The number is between $lowBound and $highBound');
    } else if (guess < secretNumber) {
      lowBound = guess + 1;  // Number must be higher
      print('Too low! The number is between $lowBound and $highBound');
    } else {
      print('Correct! The number was $secretNumber');
    }
  }
  
  // Test with some guesses
  makeGuess(50);
  makeGuess(25);
  makeGuess(37);
}