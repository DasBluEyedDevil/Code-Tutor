---
type: "EXAMPLE"
title: "Practical Examples"
---


### Generic Repository Pattern


### Generic Result Type


---



```kotlin
sealed class Result<out T> {
    data class Success<T>(val data: T) : Result<T>()
    data class Error(val message: String) : Result<Nothing>()
    object Loading : Result<Nothing>()

    fun <R> map(transform: (T) -> R): Result<R> = when (this) {
        is Success -> Success(transform(data))
        is Error -> this
        is Loading -> this
    }

    fun getOrNull(): T? = when (this) {
        is Success -> data
        else -> null
    }
}

fun fetchUser(id: Int): Result<String> {
    return if (id > 0) {
        Result.Success("User $id")
    } else {
        Result.Error("Invalid user ID")
    }
}

fun main() {
    val result1 = fetchUser(42)
    println(result1.getOrNull())  // User 42

    val result2 = fetchUser(-1)
    println(result2.getOrNull())  // null

    val mapped = result1.map { it.uppercase() }
    println(mapped.getOrNull())  // USER 42
}
```
