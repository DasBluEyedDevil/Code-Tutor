---
type: "EXAMPLE"
title: "Using the Generated Code"
---


The generated builder in action:



```kotlin
// app/src/main/kotlin/com/example/User.kt
package com.example

@AutoBuilder
data class User(
    val id: Long,
    val name: String,
    val email: String,
    val isActive: Boolean
)

// After compilation, this is generated:
// build/generated/ksp/main/kotlin/com/example/UserBuilder.kt
// class UserBuilder {
//     var id: Long? = null
//     var name: String? = null
//     var email: String? = null
//     var isActive: Boolean? = null
//     
//     fun id(value: Long): UserBuilder { this.id = value; return this }
//     fun name(value: String): UserBuilder { this.name = value; return this }
//     fun email(value: String): UserBuilder { this.email = value; return this }
//     fun isActive(value: Boolean): UserBuilder { this.isActive = value; return this }
//     
//     fun build(): User = User(
//         id = id!!,
//         name = name!!,
//         email = email!!,
//         isActive = isActive!!
//     )
// }

// Usage:
fun main() {
    val user = UserBuilder()
        .id(1)
        .name("John Doe")
        .email("john@example.com")
        .isActive(true)
        .build()
    
    println(user)  // User(id=1, name=John Doe, email=john@example.com, isActive=true)
}
```
