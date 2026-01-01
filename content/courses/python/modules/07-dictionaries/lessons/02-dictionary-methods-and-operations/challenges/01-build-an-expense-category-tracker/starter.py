# Personal Finance Tracker - Expense Categories

# List of expenses as (category, amount) tuples
expenses = [
    ('food', 25),
    ('transport', 15),
    ('food', 30),
    ('entertainment', 50),
    ('transport', 20)
]

# TODO: Create empty category_totals dictionary
category_totals = ???

# TODO: Loop through expenses and accumulate totals
for category, amount in expenses:
    # Use get() with default 0 to add amounts
    ???

# TODO: Print each category total
print("Expense Summary:")
for category, total in ???:
    print(f"  {category}: ${total}")

# TODO: Calculate and print grand total
grand_total = ???
print(f"\nGrand Total: ${grand_total}")