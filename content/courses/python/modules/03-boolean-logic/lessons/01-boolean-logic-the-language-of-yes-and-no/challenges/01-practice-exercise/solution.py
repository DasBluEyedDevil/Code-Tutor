# Movie Ticket Validator - SOLUTION
# Check which movie ratings someone can watch

# Get user's age
age = int(input("Enter your age: "))

print("\nMovie Rating Access:")

# Create Boolean variables for each rating
can_watch_g = True  # G-rated: Everyone can watch

can_watch_pg13 = age >= 13  # PG-13: Age 13 and older

can_watch_r = age >= 17  # R-rated: Age 17 and older

# Display results
print(f"• G-rated movies: {can_watch_g}")
print(f"• PG-13 movies: {can_watch_pg13}")
print(f"• R-rated movies: {can_watch_r}")

# Bonus: Count how many True values
# (Because True == 1 and False == 0, we can add them!)
total_accessible = can_watch_g + can_watch_pg13 + can_watch_r
print(f"\nYou can watch {total_accessible} out of 3 rating categories!")