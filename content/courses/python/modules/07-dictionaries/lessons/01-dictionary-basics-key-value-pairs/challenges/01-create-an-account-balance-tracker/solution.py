# Personal Finance Tracker - Account Balances

# Create accounts dictionary with at least 3 accounts
accounts = {
    "checking": 2500.00,
    "savings": 10000.00,
    "credit_card": -450.00
}

# Safely get savings balance
savings_balance = accounts.get("savings")

# Get investment balance with default 0
investment_balance = accounts.get("investment", 0)

# Add emergency_fund account with balance 1000
accounts["emergency_fund"] = 1000

# Print results
print(f"Savings balance: ${savings_balance}")
print(f"Investment balance: ${investment_balance}")
print(f"All accounts: {accounts}")