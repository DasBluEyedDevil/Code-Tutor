---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
=== Counting to 5 ===
Count: 1
Count: 2
Count: 3
Count: 4
Count: 5
Done counting!

=== Countdown ===
5
4
3
2
1
Blastoff! 🚀

=== Password Checker ===
Enter password: wrong
❌ Incorrect! Try again.
Enter password: python123
✓ Access granted!

=== Sum Calculator ===
Adding 1 to total (0)
Adding 2 to total (1)
Adding 3 to total (3)
Adding 4 to total (6)
Adding 5 to total (10)
Final total: 15

=== Age Input with Validation ===
Enter your age (0-120): -5
Invalid age! Try again.
Enter your age (0-120): 25
Thank you! Your age: 25

=== Menu System ===
1. Say Hello
2. Say Goodbye
3. Quit
Enter choice: 1
Hello there!

1. Say Hello
2. Say Goodbye
3. Quit
Enter choice: 3
Exiting...
Program ended.
```

```python
# while Loops: Repeating Code Based on a Condition

# Example 1: Basic Counting Loop
print("=== Counting to 5 ===")
count = 1  # Start value

while count <= 5:  # Condition: keep going while True
    print(f"Count: {count}")
    count = count + 1  # CRITICAL: Update the variable!

print("Done counting!")
print()

# Example 2: Countdown
print("=== Countdown ===")
countdown = 5

while countdown > 0:
    print(countdown)
    countdown = countdown - 1  # Decrease each time

print("Blastoff! 🚀")
print()

# Example 3: User Input Loop (Sentinel Pattern)
print("=== Password Checker ===")
password = ""
correct_password = "python123"

while password != correct_password:
    password = input("Enter password: ")
    
    if password != correct_password:
        print("❌ Incorrect! Try again.")

print("✓ Access granted!")
print()

# Example 4: Accumulator Pattern (Sum)
print("=== Sum Calculator ===")
total = 0
number = 1

while number <= 5:
    print(f"Adding {number} to total ({total})")
    total = total + number  # Accumulate sum
    number = number + 1

print(f"Final total: {total}")  # 1+2+3+4+5 = 15
print()

# Example 5: Input Validation Loop
print("=== Age Input with Validation ===")
age = -1  # Invalid starting value

while age < 0 or age > 120:  # Keep looping while invalid
    age = int(input("Enter your age (0-120): "))
    
    if age < 0 or age > 120:
        print("Invalid age! Try again.")

print(f"Thank you! Your age: {age}")
print()

# Example 6: Flag-Controlled Loop
print("=== Menu System ===")
running = True

while running:
    print("\n1. Say Hello")
    print("2. Say Goodbye")
    print("3. Quit")
    
    choice = int(input("Enter choice: "))
    
    if choice == 1:
        print("Hello there!")
    elif choice == 2:
        print("Goodbye!")
    elif choice == 3:
        print("Exiting...")
        running = False  # Change flag to stop loop
    else:
        print("Invalid choice!")

print("Program ended.")
```
