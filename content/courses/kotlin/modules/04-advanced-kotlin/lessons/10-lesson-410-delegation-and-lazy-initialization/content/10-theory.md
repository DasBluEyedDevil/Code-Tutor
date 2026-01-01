---
type: "THEORY"
title: "Map-Based Delegation"
---


Delegate properties to a map:


### Mutable Map Delegation


### Practical Example: JSON-like Configuration


---



```kotlin
class Config(private val properties: MutableMap<String, Any?> = mutableMapOf()) {
    var serverUrl: String by properties
    var port: Int by properties
    var timeout: Long by properties
    var enableLogging: Boolean by properties

    fun toMap(): Map<String, Any?> = properties.toMap()
}

fun main() {
    val config = Config()

    config.serverUrl = "https://api.example.com"
    config.port = 8080
    config.timeout = 5000L
    config.enableLogging = true

    println(config.toMap())
    // {serverUrl=https://api.example.com, port=8080, timeout=5000, enableLogging=true}
}
```
