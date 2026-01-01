---
type: "EXAMPLE"
title: "Working with Option"
---


Option for explicit optionality:



```kotlin
import arrow.core.*

// Creating Options
val some: Option<Int> = Some(42)
val none: Option<Int> = None

// From nullable
val fromNullable: Option<String> = "hello".toOption()  // Some("hello")
val fromNull: Option<String> = null.toOption()         // None

// Extracting values
val value1: Int? = some.getOrNull()        // 42
val value2: Int = some.getOrElse { 0 }     // 42
val value3: Int = none.getOrElse { 0 }     // 0

// Transforming
val doubled: Option<Int> = some.map { it * 2 }  // Some(84)

// Chaining
fun findUser(id: Long): Option<User> = /* ... */
fun getAddress(user: User): Option<Address> = /* ... */

val address: Option<Address> = findUser(1)
    .flatMap { user -> getAddress(user) }

// With either { } builder
fun getUserAddress(userId: Long): Either<String, Address> = either {
    val user = findUser(userId).toEither { "User not found" }.bind()
    val address = getAddress(user).toEither { "No address" }.bind()
    address
}
```
