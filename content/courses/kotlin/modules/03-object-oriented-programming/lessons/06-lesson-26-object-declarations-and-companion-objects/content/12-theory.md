---
type: "THEORY"
title: "Solution: Database Factory"
---



---



```kotlin
abstract class DatabaseConnection(
    protected val host: String,
    protected val port: Int,
    protected val database: String
) {
    abstract fun connect(): Boolean
    abstract fun getConnectionString(): String

    companion object Factory {
        const val DEFAULT_MYSQL_PORT = 3306
        const val DEFAULT_POSTGRES_PORT = 5432
        const val DEFAULT_MONGO_PORT = 27017

        fun createMySql(host: String, database: String, port: Int = DEFAULT_MYSQL_PORT): MySqlConnection {
            return MySqlConnection(host, port, database)
        }

        fun createPostgreSql(host: String, database: String, port: Int = DEFAULT_POSTGRES_PORT): PostgreSqlConnection {
            return PostgreSqlConnection(host, port, database)
        }

        fun createMongo(host: String, database: String, port: Int = DEFAULT_MONGO_PORT): MongoConnection {
            return MongoConnection(host, port, database)
        }

        fun createFromType(type: String, host: String, database: String): DatabaseConnection {
            return when (type.lowercase()) {
                "mysql" -> createMySql(host, database)
                "postgresql", "postgres" -> createPostgreSql(host, database)
                "mongodb", "mongo" -> createMongo(host, database)
                else -> throw IllegalArgumentException("Unknown database type: $type")
            }
        }
    }
}

class MySqlConnection(host: String, port: Int, database: String) : DatabaseConnection(host, port, database) {
    override fun connect(): Boolean {
        println("Connecting to MySQL...")
        println(getConnectionString())
        return true
    }

    override fun getConnectionString(): String {
        return "jdbc:mysql://$host:$port/$database"
    }
}

class PostgreSqlConnection(host: String, port: Int, database: String) : DatabaseConnection(host, port, database) {
    override fun connect(): Boolean {
        println("Connecting to PostgreSQL...")
        println(getConnectionString())
        return true
    }

    override fun getConnectionString(): String {
        return "jdbc:postgresql://$host:$port/$database"
    }
}

class MongoConnection(host: String, port: Int, database: String) : DatabaseConnection(host, port, database) {
    override fun connect(): Boolean {
        println("Connecting to MongoDB...")
        println(getConnectionString())
        return true
    }

    override fun getConnectionString(): String {
        return "mongodb://$host:$port/$database"
    }
}

fun main() {
    println("=== Creating connections using factory methods ===\n")

    val mysql = DatabaseConnection.createMySql("localhost", "myapp")
    mysql.connect()

    println()

    val postgres = DatabaseConnection.createPostgreSql("localhost", "myapp")
    postgres.connect()

    println()

    val mongo = DatabaseConnection.createMongo("localhost", "myapp")
    mongo.connect()

    println("\n=== Creating from type string ===\n")

    val db = DatabaseConnection.createFromType("mysql", "prod-server", "users_db")
    db.connect()
}
```
