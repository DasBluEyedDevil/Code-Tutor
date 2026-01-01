---
type: "THEORY"
title: "Step 3: Validation Pipeline"
---


Create data validation functions.


---



```kotlin
// Validation functions
typealias Validator<T> = (T) -> Boolean

object Validators {
    val validQuantity: Validator<SalesRecord> = { it.quantity > 0 }
    val validPrice: Validator<SalesRecord> = { it.price > 0 }
    val validCustomer: Validator<SalesRecord> = { it.customer.isNotBlank() }
    val validProduct: Validator<SalesRecord> = { it.product.isNotBlank() }

    fun validateRecord(record: SalesRecord): Boolean {
        return listOf(
            validQuantity,
            validPrice,
            validCustomer,
            validProduct
        ).all { it(record) }
    }
}

// Extension function for validation
fun List<SalesRecord>.validated(): List<SalesRecord> {
    return this.filter(Validators::validateRecord)
}
```
