# Movie Theater Ticket Pricing Calculator - SOLUTION
# Determine price based on age using if-else

print("=== Movie Theater Ticket Pricing ===")
print()

# Get user's age
age = int(input("Enter your age: "))

print()

# Use if-else to determine ticket price
if age < 13:
    category = "Child"
    price = 8.00
else:
    category = "Adult"
    price = 15.00

# Display results
print(f"Ticket Category: {category}")
print(f"Ticket Price: ${price:.2f}")

print()

# Calculate total for multiple tickets
quantity = int(input("How many tickets? "))
total = price * quantity

print(f"Total: ${total:.2f}")
print("\nEnjoy the show!")