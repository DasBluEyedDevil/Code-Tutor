# Pattern Generator - SOLUTION
# Create visual patterns using for loops

print("=== Pattern Generator ===")
print()

# Get user input
try:
    rows = int(input("How many rows? "))
except ValueError:
    print("Invalid input! Defaulting to 4 rows.")
    rows = 4

print()

# Pattern 1: Right Triangle (growing)
print("Pattern 1 - Right Triangle:")

for i in range(1, rows + 1):  # 1, 2, 3, ..., rows
    for j in range(i):  # Print i stars
        print("*", end="")
    print()  # Move to next line

print()

# Pattern 2: Square (same width each row)
print("Pattern 2 - Square:")

for i in range(rows):  # Repeat 'rows' times
    for j in range(rows):  # Print 'rows' stars per row
        print("*", end="")
    print()

print()

# Pattern 3: Numbers (print row number repeatedly)
print("Pattern 3 - Numbers:")

for i in range(1, rows + 1):
    for j in range(i):
        print(i, end="")  # Print the row number
    print()
