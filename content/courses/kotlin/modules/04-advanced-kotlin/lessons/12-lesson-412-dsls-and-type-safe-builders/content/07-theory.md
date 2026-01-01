---
type: "THEORY"
title: "Configuration DSL"
---


Create a type-safe configuration DSL:


---



```kotlin
class Server {
    var host: String = "localhost"
    var port: Int = 8080
    var ssl: Boolean = false
}

class Database {
    var url: String = ""
    var username: String = ""
    var password: String = ""
    var maxConnections: Int = 10
}

class AppConfig {
    private var serverConfig: Server? = null
    private var databaseConfig: Database? = null

    fun server(action: Server.() -> Unit) {
        serverConfig = Server().apply(action)
    }

    fun database(action: Database.() -> Unit) {
        databaseConfig = Database().apply(action)
    }

    fun getServer(): Server = serverConfig ?: Server()
    fun getDatabase(): Database = databaseConfig ?: Database()

    override fun toString(): String {
        return """
            Server: ${getServer().host}:${getServer().port} (SSL: ${getServer().ssl})
            Database: ${getDatabase().url} (Max connections: ${getDatabase().maxConnections})
        """.trimIndent()
    }
}

fun config(action: AppConfig.() -> Unit): AppConfig {
    return AppConfig().apply(action)
}

fun main() {
    val appConfig = config {
        server {
            host = "0.0.0.0"
            port = 3000
            ssl = true
        }

        database {
            url = "jdbc:postgresql://localhost:5432/mydb"
            username = "admin"
            password = "secret"
            maxConnections = 20
        }
    }

    println(appConfig)
}
```
