# Personal Finance Tracker - Monthly Budget Structure

# Create nested budget dictionary
budget = {
    'january': {
        'food': {'limit': 500, 'spent': 450},
        'transport': {'limit': 200, 'spent': 175}
    },
    'february': {
        'food': {'limit': 500, 'spent': 380},
        'transport': {'limit': 200, 'spent': 150}
    }
}

# Access January's food limit
jan_food_limit = budget['january']['food']['limit']
print(f"January food limit: ${jan_food_limit}")

# Update February's transport spent to 180
budget['february']['transport']['spent'] = 180

# Print budget summary
print("\nBudget Summary:")
for month, categories in budget.items():
    print(f"\n{month.title()}:")
    for category, data in categories.items():
        remaining = data['limit'] - data['spent']
        print(f"  {category}: ${data['spent']}/${data['limit']} (${remaining} remaining)")