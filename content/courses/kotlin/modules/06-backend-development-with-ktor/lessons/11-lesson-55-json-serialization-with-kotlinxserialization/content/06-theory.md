---
type: "THEORY"
title: "🔄 Handling Nullable and Optional Fields"
---


### Nullable Fields


**JSON examples:**

### Default Values


**JSON examples:**

### Required vs Optional


---



```kotlin
@Serializable
data class CreateBookRequest(
    val title: String,           // REQUIRED (no default, not nullable)
    val author: String,          // REQUIRED
    val year: Int? = null,       // OPTIONAL (nullable with default)
    val isbn: String? = null     // OPTIONAL
)
```
