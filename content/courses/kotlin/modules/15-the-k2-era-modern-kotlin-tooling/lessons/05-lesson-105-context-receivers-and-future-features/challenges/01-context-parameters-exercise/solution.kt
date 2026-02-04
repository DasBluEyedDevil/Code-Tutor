// NOTE: Requires Kotlin 2.2+ with -Xcontext-parameters compiler flag
// This solution simulates context parameters using explicit parameter passing.
// When context parameters become stable, the signatures change from:
//   fun processOrder(logger: Logger, orderId: String)
// to:
//   context(logger: Logger)
//   fun processOrder(orderId: String)

interface Logger {
    fun info(message: String)
    fun error(message: String)
}

interface Transaction {
    fun addOperation(op: String)
    fun commit()
    fun rollback()
}

class SimpleLogger : Logger {
    override fun info(message: String) {
        println("[INFO] $message")
    }

    override fun error(message: String) {
        println("[ERROR] $message")
    }
}

class SimpleTransaction : Transaction {
    private val operations = mutableListOf<String>()

    override fun addOperation(op: String) {
        operations.add(op)
    }

    override fun commit() {
        println("Transaction committed with ${operations.size} operations")
        operations.clear()
    }

    override fun rollback() {
        println("Transaction rolled back (${operations.size} operations discarded)")
        operations.clear()
    }
}

// Simulated context parameter functions
// With real context parameters (Kotlin 2.2+ -Xcontext-parameters):
//   context(logger: Logger)
//   fun processOrder(orderId: String) { logger.info("Processing order $orderId") }

fun processOrder(logger: Logger, orderId: String) {
    logger.info("Processing order $orderId")
}

fun saveUser(logger: Logger, tx: Transaction, name: String) {
    logger.info("Saving user $name")
    tx.addOperation("INSERT user $name")
}

fun executeInTransaction(
    logger: Logger,
    tx: Transaction,
    block: (Logger, Transaction) -> Unit
) {
    try {
        block(logger, tx)
        tx.commit()
    } catch (e: Exception) {
        logger.error("Failed: ${e.message}")
        tx.rollback()
    }
}

fun main() {
    val logger = SimpleLogger()
    val tx = SimpleTransaction()

    // Simulated context parameter usage
    processOrder(logger, "123")

    // Context propagation through executeInTransaction
    executeInTransaction(logger, tx) { log, transaction ->
        saveUser(log, transaction, "Alice")
    }
}
