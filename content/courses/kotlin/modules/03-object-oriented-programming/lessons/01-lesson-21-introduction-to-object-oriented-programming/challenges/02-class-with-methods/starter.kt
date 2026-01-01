class BankAccount(val accountNumber: String, var balance: Double) {
    // Add deposit method
    
    // Add withdraw method
    
}

fun main() {
    val account = BankAccount("12345", 1000.0)
    println("Initial balance: ${account.balance}")
    account.deposit(500.0)
    println("After deposit: ${account.balance}")
    account.withdraw(200.0)
    println("After withdrawal: ${account.balance}")
}