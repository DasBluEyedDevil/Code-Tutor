---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
What's your name? Jordan
What city are you from? Seattle
What's your favorite hobby? hiking

========================================
       GETTING TO KNOW YOU
========================================
Name: Jordan
City: Seattle
Hobby: hiking
========================================

Welcome, Jordan from {city}!
I hope you enjoy hiking today!
```

```python
# A smooth, conversational program

# Ask questions with the prompt inside input()
name = input("What's your name? ")
city = input("What city are you from? ")
hobby = input("What's your favorite hobby? ")

# Now let's use all that information
print("\n" + "=" * 40)  # Print a line of equal signs
print("       GETTING TO KNOW YOU")
print("=" * 40)
print(f"Name: {name}")
print(f"City: {city}")
print(f"Hobby: {hobby}")
print("=" * 40)
print(f"\nWelcome, {name} from {city}!")
print(f"I hope you enjoy {hobby} today!")
```
