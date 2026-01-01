# Personal Finance Tracker - Monthly Budget Structure

# TODO: Create nested budget dictionary
budget = {
    'january': {
        'food': {'limit': 500, 'spent': 450},
        'transport': {'limit': 200, 'spent': 175}
    },
    ???  # Add february
}

# TODO: Access January's food limit
jan_food_limit = ???
print(f"January food limit: ${jan_food_limit}")

# TODO: Update February's transport spent to 180
???

# TODO: Print budget summary
print("\nBudget Summary:")
for month, categories in budget.items():
    print(f"\n{month.title()}:")
    for category, data in ???:
        remaining = ???
        print(f"  {category}: ${data['spent']}/${data['limit']} (${remaining} remaining)")