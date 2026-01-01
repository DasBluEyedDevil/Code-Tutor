---
type: "THEORY"
title: "📅 Custom Serializers for Special Types"
---


### Problem: Dates and Times

`LocalDateTime` is not supported by default:


### Solution: Custom Serializer

**Step 1: Create the serializer**


**Step 2: Use it in your data class**


**JSON result:**

### Simplified: Using @Contextual

For types you use frequently, register them globally:


---



```kotlin
// In your Application.kt
install(ContentNegotiation) {
    json(Json {
        serializersModule = SerializersModule {
            contextual(LocalDateTime::class, LocalDateTimeSerializer)
        }
    })
}

// In your data class
@Serializable
data class Event(
    val id: Int,
    val name: String,
    @Contextual
    val date: LocalDateTime  // No need to specify serializer
)
```
