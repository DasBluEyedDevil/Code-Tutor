// Number Guessing Game - Range Tracker
// TODO: Track and display the valid range after each guess

import 'dart:math';

void main() {
  final random = Random();
  final secretNumber = random.nextInt(100) + 1;  // 1-100
  
  // TODO: Add variables to track the current valid range
  // var lowBound = 1;
  // var highBound = 100;
  
  // Simulate a guess
  var guess = 50;
  print('You guessed: $guess');
  
  // TODO: Check if guess is too high or too low
  // TODO: Update the range bounds
  // TODO: Print the narrowed range
}