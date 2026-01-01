---
type: "THEORY"
title: "Built-in Annotations"
---


Kotlin provides several useful annotations.

### @Deprecated

Mark code as deprecated with migration hints:


**Deprecation Levels**:
- `WARNING` - shows warning (default)
- `ERROR` - compilation error
- `HIDDEN` - not visible to code

### @Suppress

Suppress compiler warnings:


### JVM Interoperability Annotations

#### @JvmName

Change the JVM name of a function:


#### @JvmStatic

Generate static method for companion object:


#### @JvmField

Expose property as public field (no getter/setter):


#### @JvmOverloads

Generate overloaded methods for default parameters:


### @Throws

Declare checked exceptions (for Java interop):


---



```kotlin
import java.io.IOException

@Throws(IOException::class)
fun readFile(path: String): String {
    throw IOException("File not found")
}

// In Java, this is a checked exception
```
