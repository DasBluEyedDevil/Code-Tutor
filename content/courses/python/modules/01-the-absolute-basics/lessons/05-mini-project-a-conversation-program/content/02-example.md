---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
==================================================
    WELCOME TO YOUR PERSONAL ASSISTANT
==================================================

Hello! What's your name? Emma

Nice to meet you, Emma!
How are you feeling today, Emma? excited
I'm glad you're feeling excited!

How old are you? 24

Wow! That means you've been alive for approximately:
  🗓️  288 months
  📅  8760 days

What's one goal you have this year, Emma? learn Python
That's awesome! I believe you can achieve learn Python!
What's your favorite food? sushi
What's your favorite color? blue

==================================================
           CONVERSATION SUMMARY
==================================================
Name: Emma
Age: 24 years old (288 months!)
Feeling: excited
Goal: learn Python
Favorite Food: sushi
Favorite Color: blue
==================================================

Thank you for chatting with me, Emma!
I hope you enjoy some blue sushi today!
Good luck with your goals! 🚀
```

```python
# Personal Assistant Chatbot - Example Project

print("="*50)
print("    WELCOME TO YOUR PERSONAL ASSISTANT")
print("="*50)

# Get to know the user
name = input("\nHello! What's your name? ")
print(f"\nNice to meet you, {name}!")

# Ask about their day
feeling = input(f"How are you feeling today, {name}? ")
print(f"I'm glad you're feeling {feeling}!")

# Get their age and calculate fun facts
age = input("\nHow old are you? ")
age_months = int(age) * 12  # Convert age to months
age_days = int(age) * 365   # Approximate days lived

print(f"\nWow! That means you've been alive for approximately:")
print(f"  🗓️  {age_months} months")
print(f"  📅  {age_days} days")

# Future plans
goal = input(f"\nWhat's one goal you have this year, {name}? ")
print(f"That's awesome! I believe you can achieve {goal}!")

# Favorite things
fav_food = input("What's your favorite food? ")
fav_color = input("What's your favorite color? ")

# Summary
print("\n" + "="*50)
print("           CONVERSATION SUMMARY")
print("="*50)
print(f"Name: {name}")
print(f"Age: {age} years old ({age_months} months!)")
print(f"Feeling: {feeling}")
print(f"Goal: {goal}")
print(f"Favorite Food: {fav_food}")
print(f"Favorite Color: {fav_color}")
print("="*50)

print(f"\nThank you for chatting with me, {name}!")
print(f"I hope you enjoy some {fav_color} {fav_food} today!")
print("Good luck with your goals! 🚀")
```
