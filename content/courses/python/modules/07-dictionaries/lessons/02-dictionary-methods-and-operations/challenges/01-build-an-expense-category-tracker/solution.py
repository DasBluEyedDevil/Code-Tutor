# Personal Finance Tracker - Expense Categories

# List of expenses as (category, amount) tuples
expenses = [
    ('food', 25),
    ('transport', 15),
    ('food', 30),
    ('entertainment', 50),
    ('transport', 20)
]

# Create empty category_totals dictionary
category_totals = {}

# Loop through expenses and accumulate totals
for category, amount in expenses:
    # Use get() with default 0 to add amounts
    category_totals[category] = category_totals.get(category, 0) + amount

# Print each category total
print("Expense Summary:")
for category, total in category_totals.items():
    print(f"  {category}: ${total}")

# Calculate and print grand total
grand_total = sum(category_totals.values())
print(f"\nGrand Total: ${grand_total}")