class BankAccount
{
    private decimal _balance = 0;
    
    public decimal Balance
    {
        get { return _balance; }
        set
        {
            if (value >= 0)
                _balance = value;
            else
                Console.WriteLine("Balance cannot be negative!");
        }
    }
    
    public string AccountHolder { get; set; }
    
    public bool IsOverdrawn => _balance < 0;
}

BankAccount account = new BankAccount();
account.AccountHolder = "Alice";
account.Balance = 100;
Console.WriteLine("Balance: $" + account.Balance);

account.Balance = -50;  // Rejected!
Console.WriteLine("Balance: $" + account.Balance);
Console.WriteLine("Overdrawn: " + account.IsOverdrawn);