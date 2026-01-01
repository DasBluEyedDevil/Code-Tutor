# Pattern Generator - SOLUTION
# Create visual patterns using for loops

print("=== Pattern Generator ===")
print()

# Get user input
rows = int(input("How many rows? "))

print()

# Pattern 1: Right Triangle (growing)
print("Pattern 1 - Right Triangle:")

for i in range(1, rows + 1):  # 1, 2, 3, ..., rows
    for j in range(i):  # Print i stars (row 1 = 1 star, row 2 = 2 stars, etc.)
        print("*", end="")  # Print star without newline
    print()  # Move to next line after each row

print()

# Pattern 2: Square (same width each row)
print("Pattern 2 - Square:")

for i in range(rows):  # Repeat rows times
    for j in range(rows):  # Print rows stars per row
        print("*", end="")
    print()  # Move to next line

print()

# Pattern 3: Numbers (print row number repeatedly)
print("Pattern 3 - Numbers:")

for i in range(1, rows + 1):  # 1, 2, 3, ..., rows
    for j in range(i):  # Print number i times
        print(i, end="")  # Print the row number
    print()  # Move to next line