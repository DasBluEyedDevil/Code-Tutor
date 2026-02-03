# Movie Ticket Validator
# Check which movie ratings someone can watch

# Get user's age
age = int(input("Enter your age: "))

print("\nMovie Rating Access:")

# YOUR CODE HERE:
# Create Boolean variables for each rating
can_watch_g = True  # G-rated: Everyone can watch

can_watch_pg13 =   # PG-13: Age 13 and older

can_watch_r =      # R-rated: Age 17 and older

# Display results
print(f"• G-rated movies: {can_watch_g}")
print(f"• PG-13 movies: {can_watch_pg13}")
print(f"• R-rated movies: {can_watch_r}")

# Bonus: Count how many True values
# (Hint: In Python, True == 1 and False == 0, so you can add them!)
total_accessible = can_watch_g + can_watch_pg13 + can_watch_r
print(f"\nYou can watch {total_accessible} out of 3 rating categories!")
