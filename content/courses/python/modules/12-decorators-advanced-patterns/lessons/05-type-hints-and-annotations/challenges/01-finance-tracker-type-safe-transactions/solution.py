from typing import TypedDict

# Personal Finance Tracker - Type-Safe Transactions
# Demonstrates TypedDict, modern syntax, and practical typing

# Step 1: Define TypedDict for transactions
class TransactionDict(TypedDict):
    """A financial transaction with strict types."""
    id: int
    amount: float
    category: str
    is_expense: bool

# Step 2: Type-safe expense filter
def get_expenses(
    transactions: list[TransactionDict]
) -> list[TransactionDict]:
    """Return only expense transactions.
    
    Args:
        transactions: List of all transactions
        
    Returns:
        List of expense-only transactions
    """
    return [t for t in transactions if t['is_expense']]

# Step 3: Category totals with full type hints
def total_by_category(
    transactions: list[TransactionDict]
) -> dict[str, float]:
    """Calculate total spending per category.
    
    Args:
        transactions: List of transactions to analyze
        
    Returns:
        Dictionary mapping category names to total amounts
    """
    totals: dict[str, float] = {}
    for t in transactions:
        if t['is_expense']:
            cat = t['category']
            totals[cat] = totals.get(cat, 0.0) + t['amount']
    return totals

# Bonus: Find largest expense with Optional return
def find_largest_expense(
    transactions: list[TransactionDict]
) -> TransactionDict | None:
    """Find the largest expense, or None if no expenses."""
    expenses = get_expenses(transactions)
    if not expenses:
        return None
    return max(expenses, key=lambda t: t['amount'])

# Test data with type annotation
transactions: list[TransactionDict] = [
    {'id': 1, 'amount': 5000.00, 'category': 'Income', 'is_expense': False},
    {'id': 2, 'amount': 150.00, 'category': 'Food', 'is_expense': True},
    {'id': 3, 'amount': 50.00, 'category': 'Transport', 'is_expense': True},
    {'id': 4, 'amount': 200.00, 'category': 'Food', 'is_expense': True},
]

# Test functions
print("=== Finance Tracker Type Demo ===")

expenses = get_expenses(transactions)
print(f"\nTotal expenses: {len(expenses)}")
for e in expenses:
    print(f"  ${e['amount']:.2f} - {e['category']}")

category_totals = total_by_category(transactions)
print(f"\nSpending by category:")
for cat, total in category_totals.items():
    print(f"  {cat}: ${total:.2f}")

largest = find_largest_expense(transactions)
if largest:
    print(f"\nLargest expense: ${largest['amount']:.2f} ({largest['category']})")