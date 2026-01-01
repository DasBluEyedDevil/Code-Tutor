---
type: "THEORY"
title: "Solution 2"
---



---



```kotlin
import io.sentry.Sentry
import io.sentry.SentryEvent
import io.sentry.SentryLevel
import io.sentry.protocol.User

object SentryManager {
    fun init(dsn: String, environment: String) {
        Sentry.init { options ->
            options.dsn = dsn
            options.environment = environment
            options.release = System.getenv("VERSION") ?: "dev"
            options.tracesSampleRate = 1.0

            options.setBeforeSend { event, hint ->
                // Add custom tags to all events
                event.setTag("server_region", System.getenv("REGION") ?: "us-east-1")
                event.setTag("deployment", System.getenv("DEPLOYMENT_ID") ?: "local")
                event
            }
        }
    }

    fun captureException(
        exception: Throwable,
        userId: String? = null,
        extras: Map<String, Any> = emptyMap(),
        tags: Map<String, String> = emptyMap()
    ) {
        Sentry.captureException(exception) { scope ->
            // Set user
            userId?.let {
                scope.user = User().apply {
                    id = it
                }
            }

            // Add extras
            extras.forEach { (key, value) ->
                scope.setExtra(key, value)
            }

            // Add tags
            tags.forEach { (key, value) ->
                scope.setTag(key, value)
            }

            // Add breadcrumbs
            scope.addBreadcrumb("Exception captured: ${exception.message}")
        }
    }

    fun captureMessage(
        message: String,
        level: SentryLevel = SentryLevel.INFO,
        extras: Map<String, Any> = emptyMap()
    ) {
        Sentry.captureMessage(message, level) { scope ->
            extras.forEach { (key, value) ->
                scope.setExtra(key, value)
            }
        }
    }

    fun addBreadcrumb(message: String, category: String = "default") {
        Sentry.addBreadcrumb(message, category)
    }
}

// Usage in service
class PaymentService {
    fun processPayment(orderId: String, amount: Double): PaymentResult {
        SentryManager.addBreadcrumb("Starting payment processing", "payment")

        try {
            val result = stripeClient.charge(amount)

            SentryManager.addBreadcrumb("Payment successful", "payment")

            return result
        } catch (e: StripeException) {
            SentryManager.captureException(
                exception = e,
                extras = mapOf(
                    "order_id" to orderId,
                    "amount" to amount,
                    "stripe_error_code" to e.code
                ),
                tags = mapOf(
                    "payment_provider" to "stripe",
                    "error_type" to "payment_failed"
                )
            )
            throw PaymentException("Payment failed", e)
        }
    }
}
```
