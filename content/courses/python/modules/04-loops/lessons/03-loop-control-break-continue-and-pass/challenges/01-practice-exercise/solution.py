# Number Search and Statistics Program - SOLUTION
# Uses break, continue, and loop else

print("=== Number Search and Statistics ===")
print()

# The data
numbers = [23, -5, 42, 17, -3, 8, 56, 12, -9, 34]

# Get search target
target = int(input("Enter a number to search for: "))

print("\nSearching...")

# Initialize variables for statistics
total = 0
count = 0
processed = []  # Track which positive numbers we processed

# Loop through the list with enumeration (position tracking)
for position, num in enumerate(numbers):
    
    # Skip negative numbers (continue)
    if num < 0:
        print(f"Skipping negative: {num}")
        continue  # Skip to next iteration
    
    # Add to running total for positive numbers
    total = total + num
    count = count + 1
    processed.append(num)
    
    # Check if this is the target (break)
    if num == target:
        print(f"Found {target} at position {position}!")
        break  # Exit loop early

else:
    # Runs only if loop completed without break
    print(f"{target} not found in the list.")

# Calculate and display average
if count > 0:
    average = total / count
    print(f"\nAverage of positive numbers encountered: {average:.2f}")
    print(f"(Processed: {', '.join(map(str, processed))})")
else:
    print("\nNo positive numbers encountered.")