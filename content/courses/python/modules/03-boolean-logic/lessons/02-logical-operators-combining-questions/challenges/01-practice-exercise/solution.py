# Amusement Park Ride Safety Checker - SOLUTION
# Determine ride eligibility using logical operators

print("=== Amusement Park Ride Checker ===")
print()

# Get user information
height = int(input("Enter your height (inches): "))
age = int(input("Enter your age: "))
pregnant_input = input("Are you pregnant? (yes/no): ")
with_adult_input = input("Are you with an adult? (yes/no): ")

# Convert yes/no to Boolean
is_pregnant = pregnant_input.lower() == "yes"
with_adult = with_adult_input.lower() == "yes"

print("\nRide Access:")

# Check each ride's requirements using logical operators

# Roller Coaster: Height >= 48 AND not pregnant
can_ride_roller_coaster = height >= 48 and not is_pregnant

# Bumper Cars: Age >= 5 OR (age >= 3 AND with adult)
can_ride_bumper_cars = age >= 5 or (age >= 3 and with_adult)

# Ferris Wheel: Everyone can ride
can_ride_ferris_wheel = True

# Display results
print(f"✓ Roller Coaster: {can_ride_roller_coaster}")
print(f"✓ Bumper Cars: {can_ride_bumper_cars}")
print(f"✓ Ferris Wheel: {can_ride_ferris_wheel}")

# Count accessible rides (True = 1, False = 0)
total_rides = can_ride_roller_coaster + can_ride_bumper_cars + can_ride_ferris_wheel
print(f"\nYou can ride {total_rides} out of 3 attractions!")