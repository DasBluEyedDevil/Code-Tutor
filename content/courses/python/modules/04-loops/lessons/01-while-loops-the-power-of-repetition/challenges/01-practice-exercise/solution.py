# Number Guessing Game - SOLUTION
# User tries to guess a random number

import random

# For reproducible results during testing (secret number will be 4)
random.seed(42)

print("=== Number Guessing Game ===")
print("I'm thinking of a number between 1 and 20...")
print()

# Pick a random number between 1 and 20
secret_number = random.randint(1, 20)

# Initialize variables
guess = 0  # User's guess (start with impossible value)
attempts = 0  # Counter for number of guesses

# Loop until user guesses correctly
while guess != secret_number:
    
    # Get user's guess
    try:
        guess = int(input("Enter your guess: "))
    except ValueError:
        print("Please enter a valid number.")
        continue
    
    # Increment attempt counter
    attempts = attempts + 1
    
    # Give feedback
    if guess < secret_number:
        print("Too low! Try again.")
        print()
    elif guess > secret_number:
        print("Too high! Try again.")
        print()
    else:
        # Correct guess!
        print(f"🎉 Correct! You got it in {attempts} guesses!")
