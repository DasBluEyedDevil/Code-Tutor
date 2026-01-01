# Movie Ticket Pricing System - SOLUTION
# Nested conditionals for complex pricing logic

print("=== Movie Ticket Pricing ===")
print()

# Get user input
age = int(input("Enter your age: "))
is_weekend_input = input("Is it the weekend? (yes/no): ")
is_student_input = input("Are you a student? (yes/no): ")

# Convert to boolean
is_weekend = is_weekend_input.lower() == "yes"
is_student = is_student_input.lower() == "yes"

print()

# Determine price using nested conditionals
if age < 13:  # Child
    category = "Child"
    
    if is_weekend:
        price = 10
        day_type = "Weekend"
    else:
        price = 8
        day_type = "Weekday"
        
elif age < 65:  # Adult
    category = "Adult"
    
    if is_student:
        price = 12  # Student price (same any day)
        day_type = "Weekday" if not is_weekend else "Weekend"
    else:
        if is_weekend:
            price = 18
            day_type = "Weekend"
        else:
            price = 15
            day_type = "Weekday"
            
else:  # Senior
    category = "Senior"
    
    if is_weekend:
        price = 12
        day_type = "Weekend"
    else:
        price = 10
        day_type = "Weekday"

# Display results
print(f"Age Category: {category}")
print(f"Day Type: {day_type}")
if category == "Adult" and is_student:
    print("Student Discount: Yes")
print(f"Ticket Price: ${price:.2f}")