# Personal Finance Tracker - Unique Categories

# Transaction categories for each month (with duplicates)
january_expenses = ['food', 'transport', 'food', 'entertainment', 'utilities', 'food']
february_expenses = ['food', 'transport', 'healthcare', 'utilities', 'transport']

# Convert to sets
jan_categories = set(january_expenses)
feb_categories = set(february_expenses)

print(f"January categories: {jan_categories}")
print(f"February categories: {feb_categories}")

# Find categories in BOTH months
common = jan_categories & feb_categories
print(f"\nCommon categories: {common}")

# Find ALL categories across both months
all_categories = jan_categories | feb_categories
print(f"All categories: {all_categories}")

# Find categories ONLY in January
january_only = jan_categories - feb_categories
print(f"January only: {january_only}")