# Personal Finance Tracker - Comprehension Transformations

transactions = [
    {'id': 'T001', 'type': 'expense', 'category': 'food', 'amount': 25.50, 'merchant': 'Grocery Store'},
    {'id': 'T002', 'type': 'income', 'category': 'salary', 'amount': 3000, 'merchant': 'Employer'},
    {'id': 'T003', 'type': 'expense', 'category': 'transport', 'amount': 45.00, 'merchant': 'Gas Station'},
    {'id': 'T004', 'type': 'expense', 'category': 'food', 'amount': 32.75, 'merchant': 'Restaurant'},
    {'id': 'T005', 'type': 'expense', 'category': 'transport', 'amount': 20.00, 'merchant': 'Gas Station'},
]

# TODO: Create lookup dictionary by ID
# {id: transaction_dict}
by_id = ???
print(f"Transaction T003: {by_id.get('T003')}")

# TODO: Filter only expenses using comprehension
expenses_only = ???
print(f"\nExpense count: {len(expenses_only)}")

# TODO: Get unique merchants using set comprehension
unique_merchants = ???
print(f"\nUnique merchants: {unique_merchants}")

# TODO: Calculate total per category (expenses only)
# Hint: First get expense categories, then sum amounts
category_totals = {}
for t in transactions:
    if t['type'] == 'expense':
        cat = t['category']
        category_totals[cat] = category_totals.get(cat, 0) + t['amount']
print(f"\nCategory totals: {category_totals}")