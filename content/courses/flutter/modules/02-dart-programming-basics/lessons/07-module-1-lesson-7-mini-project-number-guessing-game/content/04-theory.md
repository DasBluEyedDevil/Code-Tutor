---
type: "THEORY"
title: "Version 1: The Foundation"
---


Let's start simple - player tries to guess a specific number.

Create a file called `guessing_game.dart`:


**Run it!** You should see:


**What's happening**:
- We have a secret number (42)
- We loop through guesses
- For each guess, we give feedback
- When correct, we celebrate and exit



```dart
=== Number Guessing Game ===
I'm thinking of a number between 1 and 100...

You guessed: 50
📉 Too high! Try again.

You guessed: 30
📈 Too low! Try again.

You guessed: 40
📈 Too low! Try again.

You guessed: 42
🎉 Correct! You win!
```
