// Solution: Create Encapsulated BankAccount
// This demonstrates encapsulation with private fields and public methods

public class BankAccount {
    // Private field - cannot be accessed directly from outside
    private double balance;
    
    // Constructor to set initial balance
    public BankAccount(double initialBalance) {
        this.balance = initialBalance;
    }
    
    // Getter method - controlled read access
    public double getBalance() {
        return balance;
    }
    
    // Deposit method with validation
    public void deposit(double amount) {
        // Only deposit if amount is positive
        if (amount > 0) {
            balance += amount;
        }
    }
    
    // Withdraw method with validation
    public void withdraw(double amount) {
        // Only withdraw if amount is positive AND sufficient balance
        if (amount > 0 && balance >= amount) {
            balance -= amount;
        }
    }
}