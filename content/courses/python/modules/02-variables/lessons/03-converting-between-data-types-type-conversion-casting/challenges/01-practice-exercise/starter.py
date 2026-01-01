# Shopping Cart Calculator

print("=" * 40)
print("    SHOPPING CART CALCULATOR")
print("=" * 40)

# Get user input (all input comes as strings!)
item_name = input("Enter item name: ")
item_price = input("Enter item price: $")       # String like "19.99"
quantity = input("Enter quantity: ")            # String like "3"
tax_rate = input("Enter tax rate (%): ")       # String like "8.5"

# Convert to correct types
price = ____(item_price)      # Convert to float
qty = ____(quantity)          # Convert to integer
tax_percent = ____(tax_rate)  # Convert to float

# Calculate totals
subtotal = price * qty
tax_amount = subtotal * (tax_percent / 100)
total = subtotal + tax_amount

# Display receipt
print("\n" + "=" * 40)
print("           RECEIPT")
print("=" * 40)
print(f"Item: {item_name}")
print(f"Price: ${____}")
print(f"Quantity: {____}")
print(f"Subtotal: ${____}")
print(f"Tax ({tax_percent}%): ${____}")
print("=" * 40)
print(f"TOTAL: ${____}")
print("=" * 40)

# BONUS: Format prices to 2 decimal places
# Use this format: f"{price:.2f}" to show exactly 2 decimals