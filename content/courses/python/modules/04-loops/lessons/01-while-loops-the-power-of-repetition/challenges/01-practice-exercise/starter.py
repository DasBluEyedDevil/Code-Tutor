# Number Guessing Game
# User tries to guess a random number

import random

print("=== Number Guessing Game ===")
print("I'm thinking of a number between 1 and 20...")
print()

# YOUR CODE HERE:
# 1. Pick a random number
secret_number = random.randint(1, 20)

# 2. Initialize variables
guess = 0  # User's guess (start with impossible value)
attempts = 0  # Counter for number of guesses

# 3. Loop until correct guess
while :  # What condition keeps the loop going?
    
    # Get user's guess
    guess = 
    
    # Increment attempt counter
    attempts = 
    
    # Check if guess is correct, too high, or too low
    if guess < secret_number:
        print("Too low! Try again.")
        print()
    elif guess > secret_number:
        print("Too high! Try again.")
        print()
    else:
        # Correct guess! Loop will end
        print(f"🎉 Correct! You got it in {attempts} guesses!")