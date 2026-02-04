class AppConfig {
    val databaseUrl: String by lazy {
        println("Loading config...")
        "jdbc:postgresql://localhost/mydb"
    }
}

fun main() {
    val config = AppConfig()
    println(config.databaseUrl)
    println(config.databaseUrl)
}
