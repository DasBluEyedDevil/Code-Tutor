---
type: "EXAMPLE"
title: "Defining the Annotation"
---


Start with a simple annotation:



```kotlin
// annotations/src/main/kotlin/com/example/AutoBuilder.kt
package com.example

/**
 * Generates a builder class for the annotated data class.
 * 
 * Usage:
 * ```
 * @AutoBuilder
 * data class User(val id: Long, val name: String)
 * ```
 * 
 * Generates:
 * ```
 * class UserBuilder {
 *     var id: Long? = null
 *     var name: String? = null
 *     
 *     fun id(value: Long) = apply { id = value }
 *     fun name(value: String) = apply { name = value }
 *     fun build(): User = User(id = id!!, name = name!!)
 * }
 * ```
 */
@Target(AnnotationTarget.CLASS)
@Retention(AnnotationRetention.SOURCE)  // Only needed at compile time
annotation class AutoBuilder
```
