---
type: "THEORY"
title: "Exercise 3: Safe Config Reader"
---


**Goal**: Create a configuration reader that safely handles missing values.

**Starter Code**:
```kotlin
val config = mutableMapOf<String, String?>()

fun getConfig(key: String, default: String): String = TODO()

fun main() {
    config["appName"] = "MyApp"
    config["port"] = "8080"
    config["debug"] = "true"
    
    // Test your functions here
}
```

**Expected Output**:
```text
=== Reading Config ===
App Name: MyApp
Port: 8080
Debug: true
Timeout: 30 (default used)
```


