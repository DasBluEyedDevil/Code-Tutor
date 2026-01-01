# Pattern Generator Program
# Create visual patterns using nested loops

print("=== Pattern Generator ===")
print()

size = int(input("Enter size: "))

print()

# YOUR CODE HERE:

# Pattern 1: Diamond
print("Pattern 1 - Diamond:")

# Top half (growing pyramid)
for row in range(1, size + 1):
    # Print spaces
    for space in range(size - row):
        print(" ", end="")
    # Print stars
    for star in range(2 * row - 1):
        print("*", end="")
    print()

# Bottom half (shrinking)
for row in range(size - 1, 0, -1):
    # Your code for bottom half
    pass

print()

# Pattern 2: Checkerboard
print("Pattern 2 - Checkerboard:")

for row in range(size):
    for col in range(size):
        # Use (row + col) % 2 to alternate
        if :  # Even sum
            print("*", end=" ")
        else:  # Odd sum
            print("-", end=" ")
    print()

print()

# Pattern 3: Times Table
print(f"Pattern 3 - Times Table ({size}x{size}):")

for row in range(1, size + 1):
    for col in range(1, size + 1):
        product = 
        print(f"{product:3}", end=" ")  # :3 pads to 3 characters
    print()
