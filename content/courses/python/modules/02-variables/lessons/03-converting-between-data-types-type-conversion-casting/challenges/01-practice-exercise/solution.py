# Shopping Cart Calculator - SOLUTION

print("=" * 40)
print("    SHOPPING CART CALCULATOR")
print("=" * 40)

# Get user input (all input comes as strings!)
item_name = input("Enter item name: ")
item_price = input("Enter item price: $")
quantity = input("Enter quantity: ")
tax_rate = input("Enter tax rate (%): ")

# Convert to correct types
price = float(item_price)      # "19.99" → 19.99
qty = int(quantity)            # "3" → 3
tax_percent = float(tax_rate)  # "8.5" → 8.5

# Calculate totals
subtotal = price * qty
tax_amount = subtotal * (tax_percent / 100)
total = subtotal + tax_amount

# Display receipt
print("\n" + "=" * 40)
print("           RECEIPT")
print("=" * 40)
print(f"Item: {item_name}")
print(f"Price: ${price:.2f}")           # Format to 2 decimals
print(f"Quantity: {qty}")
print(f"Subtotal: ${subtotal:.2f}")
print(f"Tax ({tax_percent}%): ${tax_amount:.2f}")
print("=" * 40)
print(f"TOTAL: ${total:.2f}")
print("=" * 40)

# Example run:
# Item name: Laptop
# Price: $999.99
# Quantity: 2
# Tax: 8.5%
# Output:
# Subtotal: $1999.98
# Tax (8.5%): $169.998 → $170.00
# TOTAL: $2169.98
