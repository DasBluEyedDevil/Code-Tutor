---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
Water is boiling!
Be careful!
Temperature check complete.

This always prints

Enter your age: 22
✓ You are eligible to vote!
✓ You can register at vote.gov
✓ You can purchase alcohol (in the US)
Age verification complete.

Excellent work!
You earned an A grade.
Bonus points awarded: 5
Grading complete.

✓ Login successful!
Welcome back, Alice!

✓ Ticket verified
✓ You may enter the venue
Enjoy the show!
```

```python
# if Statements: Making Your Programs Decide

# Example 1: Basic if statement
temperature = 105

if temperature > 100:
    print("Water is boiling!")
    print("Be careful!")

print("Temperature check complete.")
# Output:
# Water is boiling!
# Be careful!
# Temperature check complete.

print()

# Example 2: Condition is False - code doesn't run
temperature = 75

if temperature > 100:
    print("This won't print")  # Skipped! Condition is False

print("This always prints")  # Not indented, so always runs
# Output:
# This always prints

print()

# Example 3: User input validation
age = int(input("Enter your age: "))

if age >= 18:
    print("✓ You are eligible to vote!")
    print("✓ You can register at vote.gov")

if age >= 21:
    print("✓ You can purchase alcohol (in the US)")

if age >= 25:
    print("✓ You can rent a car without extra fees")

print("\nAge verification complete.")

print()

# Example 4: Multiple statements in the if block
score = 95

if score >= 90:
    print("Excellent work!")
    print("You earned an A grade.")
    grade = "A"
    bonus_points = 5
    print(f"Bonus points awarded: {bonus_points}")

print("Grading complete.")

print()

# Example 5: Combining with logical operators
password = "secret123"
username = "alice"

if username == "alice" and password == "secret123":
    print("✓ Login successful!")
    print("Welcome back, Alice!")
    is_logged_in = True

print()

# Example 6: Using Boolean variables
has_ticket = True
has_id = True

if has_ticket:
    print("✓ Ticket verified")

if has_ticket and has_id:
    print("✓ You may enter the venue")
    print("Enjoy the show!")
```