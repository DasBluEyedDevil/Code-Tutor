from typing import TypedDict

# TODO: Define TransactionDict TypedDict
# Fields: id (int), amount (float), category (str), is_expense (bool)

def get_expenses(transactions):
    """Return only expense transactions."""
    # TODO: Add type hints
    return [t for t in transactions if t['is_expense']]

def total_by_category(transactions):
    """Calculate total spending per category."""
    # TODO: Add type hints
    totals = {}
    for t in transactions:
        if t['is_expense']:
            cat = t['category']
            totals[cat] = totals.get(cat, 0.0) + t['amount']
    return totals

# Test data
transactions = [
    {'id': 1, 'amount': 5000.00, 'category': 'Income', 'is_expense': False},
    {'id': 2, 'amount': 150.00, 'category': 'Food', 'is_expense': True},
    {'id': 3, 'amount': 50.00, 'category': 'Transport', 'is_expense': True},
    {'id': 4, 'amount': 200.00, 'category': 'Food', 'is_expense': True},
]

expenses = get_expenses(transactions)
category_totals = total_by_category(transactions)

print(f"Expenses: {len(expenses)}")
print(f"By category: {category_totals}")