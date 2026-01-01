import random

# Using the random module
# This solution demonstrates common random operations

# Step 1: Generate 5 random numbers between 1-100
numbers = [random.randint(1, 100) for _ in range(5)]

# Step 2: Create and shuffle a list of names
names = ['Alice', 'Bob', 'Carol', 'David']
random.shuffle(names)  # Shuffles the list in place

# Step 3: Pick a random winner
winner = random.choice(names)

# Display results
print(f"Random numbers: {numbers}")
print(f"Shuffled names: {names}")
print(f"Winner: {winner}")

# Bonus: Other useful random functions
print(f"\nBonus examples:")
print(f"Random float 0-1: {random.random():.3f}")
print(f"Random float 1-10: {random.uniform(1, 10):.2f}")
print(f"Random sample of 2: {random.sample(names, 2)}")