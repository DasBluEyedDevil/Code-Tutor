# Movie Ticket Pricing System
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

# YOUR CODE HERE:
# Use nested conditionals to determine price

if :  # Child (under 13)
    category = "Child"
    
    if is_weekend:
        price = 
        day_type = "Weekend"
    else:
        price = 
        day_type = "Weekday"
        
elif :  # Adult (13-64)
    category = "Adult"
    
    if is_student:
        price =   # Student price (same any day)
        day_type = "Weekday" if not is_weekend else "Weekend"
    else:
        if is_weekend:
            price = 
            day_type = "Weekend"
        else:
            price = 
            day_type = "Weekday"
            
else:  # Senior (65+)
    category = 
    
    if :
        price = 
        day_type = 
    else:
        price = 
        day_type = 

# Display results
print(f"Age Category: {category}")
print(f"Day Type: {day_type}")
if category == "Adult" and is_student:
    print("Student Discount: Yes")
print(f"Ticket Price: ${price:.2f}")