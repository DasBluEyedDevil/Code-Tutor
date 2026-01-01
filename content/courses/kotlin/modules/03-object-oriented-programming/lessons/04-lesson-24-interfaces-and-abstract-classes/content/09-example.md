---
type: "EXAMPLE"
title: "Real-World Example: E-Commerce System"
---



---



```kotlin
// Interface for payment processing
interface PaymentProcessor {
    fun processPayment(amount: Double): Boolean
    fun refund(transactionId: String): Boolean

    fun validatePayment(amount: Double): Boolean {
        return amount > 0  // Default implementation
    }
}

// Interface for notification
interface Notifiable {
    fun sendNotification(message: String)
}

// Credit card payment
class CreditCardProcessor : PaymentProcessor {
    override fun processPayment(amount: Double): Boolean {
        if (!validatePayment(amount)) return false
        println("Processing credit card payment: $$amount")
        println("Payment successful!")
        return true
    }

    override fun refund(transactionId: String): Boolean {
        println("Refunding transaction: $transactionId")
        return true
    }
}

// PayPal payment
class PayPalProcessor : PaymentProcessor, Notifiable {
    override fun processPayment(amount: Double): Boolean {
        if (!validatePayment(amount)) return false
        println("Processing PayPal payment: $$amount")
        sendNotification("Payment processed via PayPal")
        return true
    }

    override fun refund(transactionId: String): Boolean {
        println("Refunding PayPal transaction: $transactionId")
        sendNotification("Refund processed")
        return true
    }

    override fun sendNotification(message: String) {
        println("📧 Email sent: $message")
    }
}

// Bitcoin payment
class BitcoinProcessor : PaymentProcessor, Notifiable {
    override fun processPayment(amount: Double): Boolean {
        if (!validatePayment(amount)) return false
        println("Processing Bitcoin payment: $$amount")
        println("Waiting for blockchain confirmation...")
        sendNotification("Bitcoin payment received")
        return true
    }

    override fun refund(transactionId: String): Boolean {
        println("Bitcoin refunds take 24-48 hours")
        return false
    }

    override fun sendNotification(message: String) {
        println("📱 Push notification: $message")
    }
}

fun checkout(processor: PaymentProcessor, amount: Double) {
    println("\n=== Checkout ===")
    val success = processor.processPayment(amount)

    if (success) {
        println("Order confirmed!")
    } else {
        println("Payment failed!")
    }
}

fun main() {
    val creditCard = CreditCardProcessor()
    val paypal = PayPalProcessor()
    val bitcoin = BitcoinProcessor()

    checkout(creditCard, 99.99)
    checkout(paypal, 149.99)
    checkout(bitcoin, 299.99)
}
```
