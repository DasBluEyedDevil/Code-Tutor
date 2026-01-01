---
type: "THEORY"
title: "Shared API Client"
---


### Common Network Layer

**API Client (commonMain)**:

**Models (commonMain)**:

This API client works on **all platforms** without modification!

---



```kotlin
package com.example.shared.api

import kotlinx.serialization.Serializable

@Serializable
data class CreateOrderRequest(
    val items: List<OrderItem>,
    val totalAmount: Double
)

@Serializable
data class OrderItem(
    val productId: String,
    val quantity: Int,
    val price: Double
)

@Serializable
data class Order(
    val id: String,
    val items: List<OrderItem>,
    val totalAmount: Double,
    val status: String,
    val createdAt: String
)
```
