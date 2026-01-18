---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
=== Countdown ===
Enter starting number: 3
3
2
1
Blastoff! 🚀

=== Password Checker ===
Enter password: wrong
❌ Incorrect! Try again.
Enter password: python123
✓ Access granted!

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

# Example 1: Interactive Countdown
print("=== Countdown ===")
countdown = int(input("Enter starting number: "))

while countdown > 0:
    print(countdown)
    countdown = countdown - 1  # Decrease each time

print("Blastoff! 🚀")
print()

# Example 2: Sentinel Loop (Password Checker)
print("=== Password Checker ===")
password = ""
correct_password = "python123"

while password != correct_password:
    password = input("Enter password: ")
    
    if password != correct_password:
        print("❌ Incorrect! Try again.")

print("✓ Access granted!")
print()

# Example 3: Flag-Controlled Loop (Menu)
print("=== Menu System ===")
running = True

while running:
    print("\n1. Say Hello")
    print("2. Say Goodbye")
    print("3. Quit")
    
    # Check if input is digit to avoid errors
    choice_input = input("Enter choice: ")
    
    if choice_input == "1":
        print("Hello there!")
    elif choice_input == "2":
        print("Goodbye!")
    elif choice_input == "3":
        print("Exiting...")
        running = False  # Change flag to stop loop
    else:
        print("Invalid choice!")

print("Program ended.")
```
