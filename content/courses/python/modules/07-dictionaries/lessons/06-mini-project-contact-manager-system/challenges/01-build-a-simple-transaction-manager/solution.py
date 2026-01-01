# Personal Finance Tracker - Transaction Manager

# Create empty transactions dictionary
transactions = {}

def add_transaction(transactions, id, category, amount, tags=None):
    """Add a transaction. Returns True if successful, False if ID exists."""
    if id in transactions:
        return False
    transactions[id] = {
        'category': category,
        'amount': amount,
        'tags': set(tags) if tags else set()
    }
    return True

def get_transaction(transactions, id):
    """Get a transaction by ID. Returns None if not found."""
    return transactions.get(id)

def get_all_tags(transactions):
    """Get all unique tags across all transactions."""
    all_tags = set()
    for t in transactions.values():
        all_tags.update(t['tags'])
    return all_tags

# Add sample transactions
add_transaction(transactions, 'T001', 'food', 25.50, ['groceries', 'essential'])
add_transaction(transactions, 'T002', 'transport', 45.00, ['gas', 'essential'])
add_transaction(transactions, 'T003', 'entertainment', 15.99, ['streaming', 'subscription'])

# Print summary
print("All Transactions:")
for id, t in transactions.items():
    print(f"  {id}: {t['category']} - ${t['amount']} - Tags: {t['tags']}")

print(f"\nAll unique tags: {get_all_tags(transactions)}")