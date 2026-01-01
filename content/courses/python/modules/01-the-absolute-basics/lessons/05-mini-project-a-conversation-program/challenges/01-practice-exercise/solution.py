# Future Year Calculator - SAMPLE SOLUTION
# Created by: The Python Team

print("="*50)
print("    FUTURE YEAR CALCULATOR")
print("    Discover Your Future Milestones!")
print("="*50)

# Get user information
name = input("\nWhat's your name? ")
print(f"\nHello, {name}! Let's explore your future together.")

age = input("How old are you right now? ")
current_year = input("What year is it? ")

# Ask about goals
milestone_age = input("At what age would you like to achieve a major goal? ")
goal = input("What goal do you want to achieve? ")
favorite_place = input("What's your dream travel destination? ")

# Calculate future milestones
age_100 = int(current_year) + (100 - int(age))
years_to_milestone = int(milestone_age) - int(age)
milestone_year = int(current_year) + years_to_milestone

# Create an exciting summary
print("\n" + "="*50)
print("        YOUR FUTURE MILESTONES")
print("="*50)
print(f"Name: {name}")
print(f"Current Age: {age} years old")
print(f"Current Year: {current_year}")
print("\n--- EXCITING PREDICTIONS ---")
print(f"🎂 You'll turn 100 in the year {age_100}!")
print(f"🎯 In {years_to_milestone} years (year {milestone_year}),")
print(f"   you'll be {milestone_age} and achieve: {goal}")
print(f"✈️  Don't forget to visit {favorite_place}!")
print("="*50)

print(f"\nAmazing, {name}! The future is bright! 🌟")
print(f"Start working towards {goal} today!")
print("\nThank you for using the Future Year Calculator!")
print("Good luck on your journey! 🚀")