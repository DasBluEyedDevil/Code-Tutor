---
type: "THEORY"
title: "Syntax Breakdown"
---

### Program Architecture Patterns:
#### 1. Number Guessing Game Structure
```
# Core pattern: While loop with attempt limit
attempts = 0
max_attempts = 7

while attempts < max_attempts:
    # Get and validate input
    try:
        guess = int(input("Your guess: "))
    except ValueError:
        continue  # Invalid input - don't count attempt
    
    attempts = attempts + 1  # Count valid attempt
    
    # Check condition
    if guess == target:
        break  # Success - exit loop
    # Give feedback

else:
    # This runs if loop completes without break
    print("You lose!")

```
**Key techniques:**

- **try/except**: Catches ValueError when user enters non-numeric input
- **continue**: Skip invalid input without counting as attempt
- **break**: Exit on win
- **loop else**: Detects loss (loop finished without break)

#### 2. Menu System Pattern
```
# Core pattern: Infinite loop with exit condition
while True:
    # Display menu
    print("1. Option A")
    print("2. Option B")
    print("3. Exit")
    
    choice = input("Choose: ")
    
    # Exit condition
    if choice == "3":
        break  # Exit the infinite loop
    
    # Validate
    if choice not in ["1", "2"]:
        continue  # Invalid - show menu again
    
    # Process valid choice
    if choice == "1":
        do_option_a()
    elif choice == "2":
        do_option_b()

```
**Key techniques:**

- **while True**: Infinite loop (runs until break)
- **Strategic break**: Exit only when user chooses
- **continue**: Skip to next iteration for invalid input
- **elif chains**: Handle multiple menu options

#### 3. Data Analyzer Pattern
```
# Core pattern: Accumulator variables + for loop
# Initialize accumulators
total = 0
count = 0
highest = float('-inf')  # Start very low
lowest = float('inf')    # Start very high
passing = 0

# Process each item
for i in range(num_items):
    value = get_value()
    
    # Update accumulators
    total = total + value
    count = count + 1
    
    if value > highest:
        highest = value
    if value < lowest:
        lowest = value
    
    if value >= threshold:
        passing = passing + 1

# Calculate statistics
average = total / count

```
**Key accumulator patterns:**

<table border='1' cellpadding='5' style='border-collapse: collapse;'><tr><th>Purpose</th><th>Initialization</th><th>Update</th></tr><tr><td>Sum/Total</td><td>0</td><td>total = total + value</td></tr><tr><td>Count</td><td>0</td><td>count = count + 1</td></tr><tr><td>Maximum</td><td>0 or -inf</td><td>if value > max: max = value</td></tr><tr><td>Minimum</td><td>inf or large number</td><td>if value < min: min = value</td></tr><tr><td>Conditional count</td><td>0</td><td>if condition: count += 1</td></tr></table>#### 4. Nested Loop Patterns
```
# Pattern 1: Fixed rectangle
for row in range(height):
    for col in range(width):
        print("*", end=" ")
    print()  # Newline after each row

# Pattern 2: Growing triangle
for row in range(1, size + 1):
    for col in range(row):  # Columns = row number
        print("*", end=" ")
    print()

# Pattern 3: Pyramid (spaces + stars)
for row in range(1, size + 1):
    # Leading spaces
    for space in range(size - row):
        print(" ", end="")
    # Stars
    for star in range(2 * row - 1):
        print("*", end="")
    print()

# Pattern 4: Multiplication table
for row in range(1, size + 1):
    for col in range(1, size + 1):
        print(row * col, end=" ")
    print()

```
### Input Validation Techniques:
#### Method 1: try/except (Type Validation)
```
while True:
    try:
        number = int(input("Enter number: "))
        break  # Success - exit validation loop
    except ValueError:
        print("Invalid! Please enter a number.")

```
#### Method 2: Range Validation
```
while True:
    number = int(input("Enter 1-10: "))
    if 1 <= number <= 10:
        break  # Valid range
    else:
        print("Must be between 1 and 10!")

```
#### Method 3: Choice Validation
```
while True:
    choice = input("Enter A, B, or C: ")
    if choice in ["A", "B", "C"]:
        break  # Valid choice
    else:
        print("Invalid choice!")

```
#### Method 4: Combined Validation
```
while True:
    try:
        age = int(input("Enter age: "))
        if 0 <= age <= 120:  # Reasonable range
            break
        else:
            print("Age must be 0-120!")
    except ValueError:
        print("Please enter a number!")

```
### Loop Control Strategy:
<table border='1' cellpadding='5' style='border-collapse: collapse;'><tr><th>Situation</th><th>Use</th><th>Example</th></tr><tr><td>Invalid input, don't count it</td><td>continue</td><td>Bad guess in game</td></tr><tr><td>Goal achieved, exit early</td><td>break</td><td>Found search target</td></tr><tr><td>User wants to quit</td><td>break</td><td>Exit menu choice</td></tr><tr><td>Maximum attempts reached</td><td>Loop condition</td><td>while attempts < max</td></tr><tr><td>Keep running indefinitely</td><td>while True</td><td>Menu system</td></tr></table>### Common Patterns Summary:
```
# Pattern 1: Limited attempts
attempts = 0
max_attempts = 5
while attempts < max_attempts:
    attempts += 1
    if success:
        break
else:
    handle_failure()

# Pattern 2: Infinite menu
while True:
    show_menu()
    if choice == "exit":
        break
    process(choice)

# Pattern 3: Data accumulation
total = 0
for item in items:
    total += process(item)
average = total / len(items)

# Pattern 4: Input validation
while True:
    try:
        value = int(input("Number: "))
        if valid(value):
            break
    except ValueError:
        pass

# Pattern 5: Search with early exit
for item in collection:
    if matches(item):
        found = item
        break
else:
    found = None

```