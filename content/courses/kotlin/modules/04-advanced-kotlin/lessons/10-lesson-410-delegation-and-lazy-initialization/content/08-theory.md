---
type: "THEORY"
title: "Delegates.notNull"
---


For non-null properties that can't be initialized immediately:


---



```kotlin
import kotlin.properties.Delegates

class Configuration {
    var apiKey: String by Delegates.notNull()
    var apiSecret: String by Delegates.notNull()

    fun initialize(key: String, secret: String) {
        apiKey = key
        apiSecret = secret
    }
}

fun main() {
    val config = Configuration()

    // println(config.apiKey)  // ❌ Throws IllegalStateException

    config.initialize("key123", "secret456")

    println(config.apiKey)     // ✅ Works: key123
    println(config.apiSecret)  // ✅ Works: secret456
}
```
