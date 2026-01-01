---
type: "THEORY"
title: "Solution: Plugin System"
---



---



```kotlin
interface Plugin {
    val name: String
    val version: String

    fun initialize()
    fun execute()
    fun shutdown()
}

interface Configurable {
    fun configure(settings: Map<String, String>)
}

class LoggerPlugin : Plugin, Configurable {
    override val name = "Logger"
    override val version = "1.0.0"
    private var logLevel = "INFO"

    override fun initialize() {
        println("[$name] Initializing logger plugin...")
    }

    override fun execute() {
        println("[$name] Logging at level: $logLevel")
        println("[$name] Log entry: Application running smoothly")
    }

    override fun shutdown() {
        println("[$name] Shutting down logger...")
    }

    override fun configure(settings: Map<String, String>) {
        logLevel = settings["logLevel"] ?: "INFO"
        println("[$name] Configured with log level: $logLevel")
    }
}

class DatabasePlugin : Plugin, Configurable {
    override val name = "Database"
    override val version = "2.1.0"
    private var connectionString = ""

    override fun initialize() {
        println("[$name] Connecting to database...")
    }

    override fun execute() {
        println("[$name] Querying database at: $connectionString")
        println("[$name] Query result: 42 records found")
    }

    override fun shutdown() {
        println("[$name] Closing database connection...")
    }

    override fun configure(settings: Map<String, String>) {
        connectionString = settings["connectionString"] ?: "localhost:5432"
        println("[$name] Configured to connect to: $connectionString")
    }
}

class CachePlugin : Plugin {
    override val name = "Cache"
    override val version = "1.5.2"
    private val cache = mutableMapOf<String, String>()

    override fun initialize() {
        println("[$name] Initializing cache system...")
    }

    override fun execute() {
        cache["user:1"] = "Alice"
        cache["user:2"] = "Bob"
        println("[$name] Cache populated with ${cache.size} items")
    }

    override fun shutdown() {
        cache.clear()
        println("[$name] Cache cleared and shutdown")
    }
}

class PluginManager {
    private val plugins = mutableListOf<Plugin>()

    fun registerPlugin(plugin: Plugin) {
        plugins.add(plugin)
        println("\n✅ Registered plugin: ${plugin.name} v${plugin.version}")
    }

    fun configurePlugin(pluginName: String, settings: Map<String, String>) {
        val plugin = plugins.find { it.name == pluginName }
        if (plugin is Configurable) {
            plugin.configure(settings)
        } else {
            println("⚠️  Plugin '$pluginName' is not configurable")
        }
    }

    fun initializeAll() {
        println("\n=== Initializing All Plugins ===")
        plugins.forEach { it.initialize() }
    }

    fun executeAll() {
        println("\n=== Executing All Plugins ===")
        plugins.forEach { it.execute() }
    }

    fun shutdownAll() {
        println("\n=== Shutting Down All Plugins ===")
        plugins.forEach { it.shutdown() }
    }

    fun listPlugins() {
        println("\n=== Installed Plugins ===")
        plugins.forEach { plugin ->
            val configurable = if (plugin is Configurable) "(Configurable)" else ""
            println("${plugin.name} v${plugin.version} $configurable")
        }
    }
}

fun main() {
    val manager = PluginManager()

    // Register plugins
    val logger = LoggerPlugin()
    val database = DatabasePlugin()
    val cache = CachePlugin()

    manager.registerPlugin(logger)
    manager.registerPlugin(database)
    manager.registerPlugin(cache)

    manager.listPlugins()

    // Configure
    manager.configurePlugin("Logger", mapOf("logLevel" to "DEBUG"))
    manager.configurePlugin("Database", mapOf("connectionString" to "prod-db.example.com:5432"))

    // Run lifecycle
    manager.initializeAll()
    manager.executeAll()
    manager.shutdownAll()
}
```
