---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
=== Finding a Number (break) ===
Checking 1
Checking 2
Checking 3
Checking 4
Checking 5
Found 5! Stopping search.
Loop ended

=== Printing Odd Numbers (continue) ===
1
3
5
7
9

=== Password System (3 attempts) ===
Enter password: wrong
❌ Incorrect. 2 attempts remaining.
Enter password: nope
❌ Incorrect. 1 attempts remaining.
Enter password: python123
✓ Access granted!

=== Processing Valid Scores ===
Skipping invalid score: -1
Processing valid score: 85
Processing valid score: 92
Skipping invalid score: 150
Processing valid score: 78
Skipping invalid score: -5
Processing valid score: 88

Average of valid scores: 85.75

=== Searching for a Value (with else) ===
6 not found in the list

=== Using pass ===
Processing 0
Processing 1
Processing 3
Processing 4

=== Prime Number Checker ===
17 is prime!

=== Simple Menu ===
1. Say Hello
2. Say Goodbye
3. Exit
Choose an option: 1
Hello!

1. Say Hello
2. Say Goodbye
3. Exit
Choose an option: 3
Exiting program...
Program ended.
```

```python
# Loop Control: break, continue, and pass

# Example 1: break - Exit Loop Early
print("=== Finding a Number (break) ===")

for num in range(1, 11):
    print(f"Checking {num}")
    
    if num == 5:
        print("Found 5! Stopping search.")
        break  # Exit the loop immediately
    
print("Loop ended\n")
# Output: Checks 1-5, then stops

# Example 2: continue - Skip Current Iteration
print("=== Printing Odd Numbers (continue) ===")

for num in range(1, 11):
    if num % 2 == 0:  # If even
        continue  # Skip to next iteration
    
    print(num)  # Only prints odd numbers

print()

# Example 3: Practical break - Password System
print("=== Password System (3 attempts) ===")

correct_password = "python123"
attempts = 0
max_attempts = 3

while attempts < max_attempts:
    password = input("Enter password: ")
    attempts = attempts + 1
    
    if password == correct_password:
        print("✓ Access granted!")
        break  # Stop asking for password
    else:
        remaining = max_attempts - attempts
        if remaining > 0:
            print(f"❌ Incorrect. {remaining} attempts remaining.")

if attempts == max_attempts and password != correct_password:
    print("🔒 Account locked!")

print()

# Example 4: continue for Data Filtering
print("=== Processing Valid Scores ===")

scores = [85, -1, 92, 150, 78, -5, 88]  # Some invalid scores

total = 0
valid_count = 0

for score in scores:
    # Skip invalid scores (negative or > 100)
    if score < 0 or score > 100:
        print(f"Skipping invalid score: {score}")
        continue  # Skip to next iteration
    
    # Process valid score
    print(f"Processing valid score: {score}")
    total = total + score
    valid_count = valid_count + 1

average = total / valid_count
print(f"\nAverage of valid scores: {average}")
print()

# Example 5: Loop with else Clause
print("=== Searching for a Value (with else) ===")

numbers = [1, 3, 5, 7, 9]
search_for = 6

for num in numbers:
    if num == search_for:
        print(f"Found {search_for}!")
        break
else:
    # Runs only if loop completed without break
    print(f"{search_for} not found in the list")

print()

# Example 6: pass - Placeholder
print("=== Using pass ===")

for i in range(5):
    if i == 2:
        pass  # TODO: Add special handling later
    else:
        print(f"Processing {i}")

print()

# Example 7: Prime Number Checker (break with else)
print("=== Prime Number Checker ===")

number = 17

if number < 2:
    print(f"{number} is not prime")
else:
    for i in range(2, number):
        if number % i == 0:
            print(f"{number} is not prime (divisible by {i})")
            break
    else:
        # Only runs if loop completed without break
        print(f"{number} is prime!")

print()

# Example 8: Menu System with break
print("=== Simple Menu ===")

while True:  # Infinite loop
    print("\n1. Say Hello")
    print("2. Say Goodbye")
    print("3. Exit")
    
    choice = input("Choose an option: ")
    
    if choice == "1":
        print("Hello!")
    elif choice == "2":
        print("Goodbye!")
    elif choice == "3":
        print("Exiting program...")
        break  # Exit the infinite loop
    else:
        print("Invalid choice!")

print("Program ended.")
```
