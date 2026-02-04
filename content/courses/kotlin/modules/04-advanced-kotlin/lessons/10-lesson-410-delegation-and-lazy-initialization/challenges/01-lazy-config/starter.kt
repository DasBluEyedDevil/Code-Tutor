class AppConfig {
    val databaseUrl: String by lazy {
        // Print "Loading config..." then return the URL
        TODO("Implement lazy initialization")
    }
}

fun main() {
    val config = AppConfig()
    println(config.databaseUrl)
    println(config.databaseUrl)
}
