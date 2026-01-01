# Comprehensions for Transaction Analysis
# This solution demonstrates dict, set, and list comprehensions

transactions = [
    {'amount': 45.50, 'category': 'food', 'desc': 'Grocery shopping'},
    {'amount': 12.00, 'category': 'transport', 'desc': 'Bus fare'},
    {'amount': 78.90, 'category': 'food', 'desc': 'Restaurant'},
    {'amount': 100.00, 'category': 'entertainment', 'desc': 'Concert'},
    {'amount': 25.00, 'category': 'transport', 'desc': 'Taxi'},
    {'amount': 55.00, 'category': 'food', 'desc': 'Takeout'}
]

# Step 1: Get unique categories first (set comprehension)
categories = {t['category'] for t in transactions}

# Step 2: Dict of total spending per category (dict comprehension)
# Sum amounts for each category
totals_by_category = {
    cat: sum(t['amount'] for t in transactions if t['category'] == cat)
    for cat in categories
}

# Step 3: List of descriptions for amounts > 50 (list comprehension)
large_transactions = [
    f"{t['desc']} (${t['amount']:.2f})"
    for t in transactions
    if t['amount'] > 50
]

# Display results
print("=== Transaction Analysis ===")

print("\n1. Unique Categories (set comprehension):")
print(f"   {categories}")

print("\n2. Totals by Category (dict comprehension):")
for cat, total in totals_by_category.items():
    print(f"   {cat}: ${total:.2f}")

print("\n3. Large Transactions > $50 (list comprehension):")
for desc in large_transactions:
    print(f"   - {desc}")

# Bonus: Combined generator expression
print("\n4. Total spending (generator expression):")
total_spent = sum(t['amount'] for t in transactions)
print(f"   ${total_spent:.2f}")