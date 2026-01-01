class InsufficientFundsException : Exception
{
    // Constructor
}

class BankAccount
{
    public decimal Balance;
    
    public void Withdraw(decimal amount)
    {
        // Check balance, throw if insufficient
        // Otherwise subtract
    }
}

// Test the system
try
{
    BankAccount account = new BankAccount();
    account.Balance = 100;
    
    Console.WriteLine("Balance: $" + account.Balance);
    account.Withdraw(150);  // Should throw exception
    Console.WriteLine("Withdrawal successful!");
}
catch (InsufficientFundsException ex)
{
    // Handle error
}
finally
{
    // Always runs
}