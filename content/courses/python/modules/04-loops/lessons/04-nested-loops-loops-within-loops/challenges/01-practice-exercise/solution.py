# Pattern Generator Program - SOLUTION
# Create visual patterns using nested loops

print("=== Pattern Generator ===")
print()

size = int(input("Enter size: "))

print()

# Pattern 1: Diamond
print("Pattern 1 - Diamond:")

# Top half (growing pyramid)
for row in range(1, size + 1):
    # Print leading spaces
    for space in range(size - row):
        print(" ", end="")
    # Print stars
    for star in range(2 * row - 1):
        print("*", end="")
    print()

# Bottom half (shrinking pyramid)
for row in range(size - 1, 0, -1):
    # Print leading spaces
    for space in range(size - row):
        print(" ", end="")
    # Print stars
    for star in range(2 * row - 1):
        print("*", end="")
    print()

print()

# Pattern 2: Checkerboard
print("Pattern 2 - Checkerboard:")

for row in range(size):
    for col in range(size):
        # Alternate based on position sum
        if (row + col) % 2 == 0:
            print("*", end=" ")
        else:
            print("-", end=" ")
    print()

print()

# Pattern 3: Times Table
print(f"Pattern 3 - Times Table ({size}x{size}):")

for row in range(1, size + 1):
    for col in range(1, size + 1):
        product = row * col
        print(f"{product:3}", end=" ")  # :3 pads to 3 characters
    print()