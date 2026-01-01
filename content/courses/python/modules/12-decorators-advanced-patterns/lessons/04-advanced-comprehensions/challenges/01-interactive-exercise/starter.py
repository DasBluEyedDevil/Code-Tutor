transactions = [
    {'amount': 45.50, 'category': 'food', 'desc': 'Grocery shopping'},
    {'amount': 12.00, 'category': 'transport', 'desc': 'Bus fare'},
    {'amount': 78.90, 'category': 'food', 'desc': 'Restaurant'},
    {'amount': 100.00, 'category': 'entertainment', 'desc': 'Concert'},
    {'amount': 25.00, 'category': 'transport', 'desc': 'Taxi'},
    {'amount': 55.00, 'category': 'food', 'desc': 'Takeout'}
]

# TODO: Create dict of total per category
totals_by_category = {}

# TODO: Create set of unique categories
categories = set()

# TODO: Create list of descriptions for amounts > 50
large_transactions = []

print(f"Totals: {totals_by_category}")
print(f"Categories: {categories}")
print(f"Large: {large_transactions}")