// NOTE: Requires Kotlin 2.2+ with -Xcontext-parameters compiler flag
// This exercise simulates context parameters using interface-based dependency passing.
// When context parameters are stable, replace with: context(logger: Logger)

interface Logger {
    fun info(message: String)
    fun error(message: String)
}

interface Transaction {
    fun addOperation(op: String)
    fun commit()
    fun rollback()
}

// TODO: Implement SimpleLogger
// class SimpleLogger : Logger { ... }

// TODO: Implement SimpleTransaction
// class SimpleTransaction : Transaction {
//   Track operations in a list
//   commit() prints "Transaction committed with N operations"
//   rollback() prints "Transaction rolled back"
// }

// TODO: Implement functions that use context parameters
// When context parameters are stable:
//   context(logger: Logger)
//   fun processOrder(orderId: String) { logger.info("Processing order $orderId") }
//
// For now, simulate with explicit parameters:
//   fun processOrder(logger: Logger, orderId: String) { ... }
//
// Similarly:
//   fun saveUser(logger: Logger, tx: Transaction, name: String) { ... }
//   fun executeInTransaction(logger: Logger, tx: Transaction, block: (Logger, Transaction) -> Unit) { ... }

fun main() {
    val logger = SimpleLogger()
    val tx = SimpleTransaction()

    // Simulated context parameter usage
    processOrder(logger, "123")
    saveUser(logger, tx, "Alice")
    tx.commit()
}
