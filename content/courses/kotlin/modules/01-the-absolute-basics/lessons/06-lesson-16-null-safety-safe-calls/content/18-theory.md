---
type: "THEORY"
title: "Solution 3: Safe Config Reader"
---



**Solution Code**:

```kotlin
val config = mutableMapOf<String, String?>(
    "appName" to "MyApp",
    "version" to "1.0.0",
    "port" to "8080",
    "debug" to "true",
    "timeout" to null,
    "apiKey" to null
)

fun getConfig(key: String, default: String): String {
    return config[key] ?: default
}

fun getIntConfig(key: String, default: Int): Int {
    return config[key]?.toIntOrNull() ?: default
}

fun getBoolConfig(key: String, default: Boolean): Boolean {
    return config[key]?.toBoolean() ?: default
}

fun main() {
    println("=== Reading Config ===")
    println("App Name: ${getConfig("appName", "Unknown")}")
    println("Version: ${getConfig("version", "0.0.1")}")
    println("Port: ${getIntConfig("port", 3000)}")
    println("Debug: ${getBoolConfig("debug", false)}")
    println("Timeout: ${getIntConfig("timeout", 30)}")
    println("API Key: ${getConfig("apiKey", "default-key")}")
}
```

**Sample Output**:
