# Personal Finance Tracker - Transaction Manager

# TODO: Create empty transactions dictionary
transactions = ???

def add_transaction(transactions, id, category, amount, tags=None):
    """Add a transaction. Returns True if successful, False if ID exists."""
    # TODO: Check if ID already exists
    # TODO: Add transaction with category, amount, and tags (as a set)
    pass

def get_transaction(transactions, id):
    """Get a transaction by ID. Returns None if not found."""
    # TODO: Safely return transaction or None
    pass

def get_all_tags(transactions):
    """Get all unique tags across all transactions."""
    # TODO: Collect all tags from all transactions into a set
    pass

# TODO: Add sample transactions
add_transaction(transactions, 'T001', 'food', 25.50, ['groceries', 'essential'])
# Add more...

# TODO: Print summary
print("All Transactions:")
for id, t in transactions.items():
    print(f"  {id}: {t['category']} - ${t['amount']} - Tags: {t['tags']}")

print(f"\nAll unique tags: {get_all_tags(transactions)}")