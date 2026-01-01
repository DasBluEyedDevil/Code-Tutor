---
type: "THEORY"
title: "Lazy Initialization"
---


`lazy` creates a property that's initialized only when first accessed.

### Basic Lazy


### Lazy Thread Safety


### Practical Example: Database Connection


---



```kotlin
class DatabaseConnection {
    init {
        println("Connecting to database...")
        Thread.sleep(1000)
        println("Connected!")
    }

    fun query(sql: String): String {
        return "Result for: $sql"
    }
}

class Repository {
    private val db: DatabaseConnection by lazy {
        println("Lazy initialization triggered")
        DatabaseConnection()
    }

    fun getData(): String {
        return db.query("SELECT * FROM users")
    }
}

fun main() {
    println("Creating repository")
    val repo = Repository()

    println("Repository created (DB not connected yet)")

    println("\nFetching data...")
    println(repo.getData())
}
// Output:
// Creating repository
// Repository created (DB not connected yet)
//
// Fetching data...
// Lazy initialization triggered
// Connecting to database...
// Connected!
// Result for: SELECT * FROM users
```
