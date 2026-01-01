---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
Score 85 = Grade B

SLOW DOWN

Age: 8
Category: Child
Price: $8

Temperature: 45°F
Status: Cold
Action: Close windows

Checking score: 95
You passed!

Main Menu:
1. Start Game
2. Load Save
3. Settings
4. Quit
Enter choice (1-4): 2
Loading saved game...
```

```python
# elif Chains: Multiple Decision Paths

# Example 1: Letter Grade Calculator
score = 85

if score >= 90:
    grade = "A"
    print(f"Score {score} = Grade A")
elif score >= 80:
    grade = "B"
    print(f"Score {score} = Grade B")
elif score >= 70:
    grade = "C"
    print(f"Score {score} = Grade C")
elif score >= 60:
    grade = "D"
    print(f"Score {score} = Grade D")
else:
    grade = "F"
    print(f"Score {score} = Grade F")
# Output: Score 85 = Grade B

print()

# Example 2: Traffic Light Response
light_color = "yellow"

if light_color == "green":
    print("GO")
elif light_color == "yellow":
    print("SLOW DOWN")
elif light_color == "red":
    print("STOP")
else:
    print("ERROR: Unknown light color")
# Output: SLOW DOWN

print()

# Example 3: Age-Based Ticket Pricing
age = 8

if age < 5:
    price = 0
    category = "Infant (Free)"
elif age < 13:
    price = 8
    category = "Child"
elif age < 18:
    price = 12
    category = "Teen"
elif age < 65:
    price = 15
    category = "Adult"
else:
    price = 10
    category = "Senior"

print(f"Age: {age}")
print(f"Category: {category}")
print(f"Price: ${price}")
# Output:
# Age: 8
# Category: Child
# Price: $8

print()

# Example 4: Temperature Response System
temperature = 45

if temperature > 90:
    action = "Turn on AC"
    warning = "Very hot!"
elif temperature > 75:
    action = "Open windows"
    warning = "Warm"
elif temperature > 60:
    action = "Do nothing"
    warning = "Comfortable"
elif temperature > 32:
    action = "Close windows"
    warning = "Cold"
else:
    action = "Turn on heater"
    warning = "Freezing!"

print(f"Temperature: {temperature}°F")
print(f"Status: {warning}")
print(f"Action: {action}")
# Output:
# Temperature: 45°F
# Status: Cold
# Action: Close windows

print()

# Example 5: First Match Wins (Order Matters!)
score = 95

print("Checking score: 95")

if score >= 60:
    print("You passed!")  # This matches first!
    # The rest are skipped, even though they would also be True
elif score >= 70:
    print("This never runs")  # Skipped
elif score >= 80:
    print("This never runs")  # Skipped
elif score >= 90:
    print("This never runs")  # Skipped

# Output: You passed!
# (Even though score >= 90 is also True, it never checks it!)

print()

# Example 6: Menu System
print("Main Menu:")
print("1. Start Game")
print("2. Load Save")
print("3. Settings")
print("4. Quit")

choice = int(input("Enter choice (1-4): "))

if choice == 1:
    print("Starting new game...")
elif choice == 2:
    print("Loading saved game...")
elif choice == 3:
    print("Opening settings...")
elif choice == 4:
    print("Goodbye!")
else:
    print("Invalid choice! Please enter 1-4.")
```
