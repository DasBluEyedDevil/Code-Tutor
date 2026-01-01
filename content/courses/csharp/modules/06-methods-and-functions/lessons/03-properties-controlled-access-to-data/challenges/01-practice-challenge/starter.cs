class BankAccount
{
    private decimal _balance = 0;
    
    // Add Balance property with validation
    
    // Add AccountHolder auto-property
    
    // Add IsOverdrawn read-only property
}

// Create account and test
BankAccount account = new BankAccount();
account.AccountHolder = "Alice";
// Try setting balance to 100, then try -50
// Display balance and IsOverdrawn status