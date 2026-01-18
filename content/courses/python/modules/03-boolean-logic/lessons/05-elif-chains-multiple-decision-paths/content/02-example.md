---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
--- Grade Calculator ---
Enter your score (0-100): 85
Score 85 = Grade B

--- Main Menu ---
1. Start Game
2. Load Save
3. Settings
4. Quit
Enter choice (1-4): 2
Loading saved game...
```

```python
# elif Chains: Multiple Decision Paths

# Example 1: Interactive Grade Calculator
print("--- Grade Calculator ---")
# Use int() to convert the string input to an integer
score = int(input("Enter your score (0-100): "))

if score >= 90:
    grade = "A"
elif score >= 80:
    grade = "B"
elif score >= 70:
    grade = "C"
elif score >= 60:
    grade = "D"
else:
    grade = "F"

print(f"Score {score} = Grade {grade}")
print()

# Example 2: Menu System
print("--- Main Menu ---")
print("1. Start Game")
print("2. Load Save")
print("3. Settings")
print("4. Quit")

choice = input("Enter choice (1-4): ")

# Note: input() returns a string, so we compare with strings "1", "2", etc.
if choice == "1":
    print("Starting new game...")
elif choice == "2":
    print("Loading saved game...")
elif choice == "3":
    print("Opening settings...")
elif choice == "4":
    print("Goodbye!")
else:
    print("Invalid choice! Please enter 1-4.")
```
