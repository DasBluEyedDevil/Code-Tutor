---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
You are an adult
You are a minor

Enter a number: 7
7 is ODD

Enter password: secret123
✓ Login successful!
Welcome to your dashboard.

Temperature is comfortable.
AC status: False

Using if-else (mutually exclusive):
Grade: Not an A

Using multiple if statements (independent):
Grade: Not an A

Would you like to continue? (yes/no): no
Stopping...
```

```python
# if-else Statements: Handling Both Paths

# Example 1: Basic if-else
age = 20

if age >= 18:
    print("You are an adult")
else:
    print("You are a minor")
# Output: You are an adult

# Try with different age:
age = 15
if age >= 18:
    print("You are an adult")
else:
    print("You are a minor")
# Output: You are a minor

print()

# Example 2: Even or Odd Checker
number = int(input("Enter a number: "))

if number % 2 == 0:
    print(f"{number} is EVEN")
else:
    print(f"{number} is ODD")

print()

# Example 3: Login Authentication
entered_password = input("Enter password: ")
correct_password = "secret123"

if entered_password == correct_password:
    print("✓ Login successful!")
    print("Welcome to your dashboard.")
    is_logged_in = True
else:
    print("❌ Incorrect password!")
    print("Please try again.")
    is_logged_in = False

print()

# Example 4: Temperature Response
temperature = 75

if temperature > 85:
    print("It's hot! Turn on the AC.")
    ac_on = True
else:
    print("Temperature is comfortable.")
    ac_on = False

print(f"AC status: {ac_on}")

print()

# Example 5: Comparing if-else vs Multiple if statements
score = 85

print("Using if-else (mutually exclusive):")
if score >= 90:
    print("Grade: A")
else:
    print("Grade: Not an A")
# Output: Grade: Not an A (only ONE prints)

print()

print("Using multiple if statements (independent):")
if score >= 90:
    print("Grade: A")

if score < 90:
    print("Grade: Not an A")
# Also outputs: Grade: Not an A
# But required TWO separate checks!

print()

# Example 6: Input Validation
user_input = input("Would you like to continue? (yes/no): ")

if user_input.lower() == "yes":
    print("Continuing...")
    should_continue = True
else:
    print("Stopping...")
    should_continue = False
```
