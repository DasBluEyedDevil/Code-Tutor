---
type: "THEORY"
title: "Exercises"
---


### Exercise 1: Thread-Safe Cache (Medium)

Create a thread-safe caching delegate.

**Requirements**:
- Cache computed values
- Thread-safe access
- Optional expiration time
- Lazy computation

**Solution**:


### Exercise 2: Change Tracking (Medium)

Create a delegate that tracks all changes to a property.

**Requirements**:
- Track value changes with timestamps
- Get change history
- Support any type

**Solution**:


### Exercise 3: Smart Configuration (Hard)

Create a configuration system with validation, defaults, and environment variables.

**Requirements**:
- Type-safe configuration properties
- Default values
- Environment variable override
- Validation

**Solution**:


---



```kotlin
import kotlin.properties.ReadWriteProperty
import kotlin.reflect.KProperty

class ConfigProperty<T>(
    private val default: T,
    private val envVar: String? = null,
    private val validator: (T) -> Boolean = { true }
) : ReadWriteProperty<Any?, T> {
    private var value: T? = null

    override fun getValue(thisRef: Any?, property: KProperty<*>): T {
        if (value == null) {
            // Try environment variable
            value = envVar?.let { getEnvValue(it, default) } ?: default
        }
        return value!!
    }

    override fun setValue(thisRef: Any?, property: KProperty<*>, value: T) {
        if (!validator(value)) {
            throw IllegalArgumentException("Invalid value for ${property.name}: $value")
        }
        this.value = value
    }

    @Suppress("UNCHECKED_CAST")
    private fun getEnvValue(name: String, default: T): T {
        val envValue = System.getenv(name) ?: return default

        return when (default) {
            is String -> envValue as T
            is Int -> envValue.toIntOrNull() as? T ?: default
            is Boolean -> envValue.toBoolean() as T
            is Double -> envValue.toDoubleOrNull() as? T ?: default
            else -> default
        }
    }
}

fun <T> config(
    default: T,
    envVar: String? = null,
    validator: (T) -> Boolean = { true }
) = ConfigProperty(default, envVar, validator)

class AppConfig {
    var host: String by config(
        default = "localhost",
        envVar = "APP_HOST"
    )

    var port: Int by config(
        default = 8080,
        envVar = "APP_PORT",
        validator = { it in 1..65535 }
    )

    var maxConnections: Int by config(
        default = 100,
        validator = { it > 0 }
    )

    var debugMode: Boolean by config(
        default = false,
        envVar = "DEBUG"
    )

    override fun toString(): String {
        return """
            AppConfig(
              host=$host,
              port=$port,
              maxConnections=$maxConnections,
              debugMode=$debugMode
            )
        """.trimIndent()
    }
}

fun main() {
    val config = AppConfig()

    println("Default configuration:")
    println(config)

    // Modify configuration
    config.host = "0.0.0.0"
    config.port = 3000
    config.maxConnections = 500

    println("\nModified configuration:")
    println(config)

    // Validation
    try {
        config.port = 99999  // Invalid
    } catch (e: IllegalArgumentException) {
        println("\n❌ Error: ${e.message}")
    }

    try {
        config.maxConnections = -10  // Invalid
    } catch (e: IllegalArgumentException) {
        println("❌ Error: ${e.message}")
    }
}
```
