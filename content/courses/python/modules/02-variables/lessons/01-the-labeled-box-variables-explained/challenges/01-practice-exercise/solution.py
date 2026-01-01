# Personal Profile Card - SOLUTION

# Create your variables
first_name = "Alex"
last_name = "Johnson"
age = 25
favorite_number = 7
likes_pizza = True
city = "Portland"

# Calculate age in months
age_in_months = age * 12

# Display your profile
print("=" * 40)
print("       PERSONAL PROFILE CARD")
print("=" * 40)
print(f"Name: {first_name} {last_name}")
print(f"Age: {age} years ({age_in_months} months)")
print(f"City: {city}")
print(f"Favorite Number: {favorite_number}")
print(f"Likes Pizza: {likes_pizza}")
print("=" * 40)

# Update one variable
age = 26  # Birthday!
age_in_months = age * 12  # Recalculate
print(f"\nUpdated Age: {age} years ({age_in_months} months)")