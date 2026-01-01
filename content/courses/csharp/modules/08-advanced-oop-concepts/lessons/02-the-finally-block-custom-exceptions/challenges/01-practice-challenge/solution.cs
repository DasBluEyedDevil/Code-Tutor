class InsufficientFundsException : Exception
{
    public InsufficientFundsException(string message) : base(message)
    {
    }
}

class BankAccount
{
    public decimal Balance;
    
    public void Withdraw(decimal amount)
    {
        if (amount > Balance)
        {
            throw new InsufficientFundsException("Insufficient funds for withdrawal!");
        }
        Balance -= amount;
    }
}

try
{
    BankAccount account = new BankAccount();
    account.Balance = 100;
    
    Console.WriteLine("Balance: $" + account.Balance);
    account.Withdraw(150);
    Console.WriteLine("Withdrawal successful!");
}
catch (InsufficientFundsException ex)
{
    Console.WriteLine("Error: " + ex.Message);
}
finally
{
    Console.WriteLine("Transaction complete.");
}