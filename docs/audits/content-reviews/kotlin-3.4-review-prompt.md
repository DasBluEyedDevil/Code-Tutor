# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Kotlin Multiplatform Complete Course (kotlin)
- **Module:** Object-Oriented Programming
- **Lesson:** Lesson 2.4: Interfaces and Abstract Classes (ID: 3.4)
- **Difficulty:** beginner
- **Estimated Time:** 65 minutes

## Current Lesson Content

{
    "id":  "3.4",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "Introduction",
                                "content":  "**Estimated Time**: 65 minutes\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Topic Introduction",
                                "content":  "\nYou\u0027ve learned about inheritance and abstract classes. Now let\u0027s explore **interfaces**—one of OOP\u0027s most powerful tools for designing flexible, maintainable systems.\n\nAn **interface** defines a contract: \"Any class that implements me must provide these capabilities.\" Unlike abstract classes (which you can only inherit from one), a class can implement multiple interfaces, enabling composition of behaviors.\n\nThis lesson will teach you:\n- How to define and implement interfaces\n- The difference between interfaces and abstract classes\n- When to use each\n- Default interface methods\n- Real-world design patterns\n\n---\n\n"
                            },
                            {
                                "type":  "ANALOGY",
                                "title":  "The Concept",
                                "content":  "\n### What is an Interface?\n\nAn **interface** is a contract that defines what a class can do, without specifying how it does it.\n\n**Real-World Analogy: Power Outlets**\n\nA power outlet is an interface:\n- **Contract**: \"I provide electricity through these two/three holes\"\n- **Devices** (implementations): Phone chargers, laptops, lamps all plug into the same outlet\n- **Different implementations**: Each device uses the electricity differently, but all follow the outlet interface\n\n\n### Why Interfaces?\n\n**Problems interfaces solve**:\n1. **Multiple inheritance**: A class can implement multiple interfaces\n2. **Loose coupling**: Code depends on contracts, not implementations\n3. **Testability**: Easy to create mock implementations for testing\n4. **Flexibility**: Swap implementations without changing client code\n\n---\n\n",
                                "code":  "  Interface: PowerSource\n       ↓\n  ┌───────────────────────────┐\n  │ fun provideElectricity()  │\n  └───────────────────────────┘\n           ↓         ↓         ↓\n     PhoneCharger  Laptop   Lamp",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Defining Interfaces",
                                "content":  "\n**Syntax**:\n\n\n**Example: Simple Interface**\n\n\n**Output**:\n\n---\n\n",
                                "code":  "Drawing a circle\nDrawing a square",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Implementing Multiple Interfaces",
                                "content":  "\nUnlike classes (single inheritance), you can implement multiple interfaces!\n\n\n**Output**:\n\n---\n\n",
                                "code":  "Duck is flying\nDuck is swimming\nDuck is walking\n\nFish is swimming\n\nBird is flying\nBird is walking",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Interface Properties",
                                "content":  "\nInterfaces can declare properties, but they can\u0027t have backing fields.\n\n\n---\n\n",
                                "code":  "interface Vehicle {\n    val maxSpeed: Int  // Must be overridden\n    val type: String\n        get() = \"Generic Vehicle\"  // Can provide default\n\n    fun start()\n    fun stop()\n}\n\nclass Car(override val maxSpeed: Int) : Vehicle {\n    override val type: String\n        get() = \"Car\"\n\n    override fun start() {\n        println(\"Car starting with key\")\n    }\n\n    override fun stop() {\n        println(\"Car stopping\")\n    }\n}\n\nclass Motorcycle(override val maxSpeed: Int) : Vehicle {\n    override val type: String = \"Motorcycle\"  // Can also initialize directly\n\n    override fun start() {\n        println(\"Motorcycle starting with button\")\n    }\n\n    override fun stop() {\n        println(\"Motorcycle stopping\")\n    }\n}\n\nfun main() {\n    val car = Car(180)\n    println(\"${car.type} - Max Speed: ${car.maxSpeed} km/h\")\n    car.start()\n\n    val bike = Motorcycle(220)\n    println(\"${bike.type} - Max Speed: ${bike.maxSpeed} km/h\")\n    bike.start()\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Default Interface Methods",
                                "content":  "\nKotlin interfaces can have default implementations (unlike Java pre-8):\n\n\n---\n\n",
                                "code":  "interface Logger {\n    fun log(message: String) {\n        println(\"[LOG] $message\")  // Default implementation\n    }\n\n    fun error(message: String) {\n        println(\"[ERROR] $message\")  // Default implementation\n    }\n\n    fun debug(message: String)  // Must be implemented\n}\n\nclass ConsoleLogger : Logger {\n    override fun debug(message: String) {\n        println(\"[DEBUG] $message\")\n    }\n    // log() and error() use default implementations\n}\n\nclass FileLogger : Logger {\n    override fun log(message: String) {\n        println(\"[FILE LOG] Writing to file: $message\")\n    }\n\n    override fun error(message: String) {\n        println(\"[FILE ERROR] Writing error to file: $message\")\n    }\n\n    override fun debug(message: String) {\n        println(\"[FILE DEBUG] Writing debug to file: $message\")\n    }\n}\n\nfun main() {\n    val console = ConsoleLogger()\n    console.log(\"Application started\")\n    console.error(\"Connection failed\")\n    console.debug(\"Variable value: 42\")\n\n    println()\n\n    val file = FileLogger()\n    file.log(\"Application started\")\n    file.error(\"Connection failed\")\n    file.debug(\"Variable value: 42\")\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Abstract Classes vs Interfaces",
                                "content":  "\n### When to Use Abstract Classes\n\nUse **abstract classes** when:\n- You have shared **state** (properties with backing fields)\n- You want to provide **common implementation** for subclasses\n- You have a clear \"is-a\" relationship\n- You need **constructors with parameters**\n\n\n### When to Use Interfaces\n\nUse **interfaces** when:\n- You want to define **capabilities** or **behaviors**\n- You need **multiple inheritance** of type\n- You don\u0027t need shared state\n- You want loose coupling\n\n\n### Comparison Table\n\n| Feature | Abstract Class | Interface |\n|---------|---------------|-----------|\n| State (backing fields) | ✅ Yes | ❌ No |\n| Constructor | ✅ Yes | ❌ No |\n| Multiple inheritance | ❌ No (single only) | ✅ Yes (multiple) |\n| Default implementations | ✅ Yes | ✅ Yes (since Kotlin 1.0) |\n| Access modifiers | ✅ Yes (public, protected, private) | ✅ Limited (public only) |\n| When to use | \"is-a\" relationship | \"can-do\" capability |\n\n---\n\n",
                                "code":  "interface Flyable {\n    fun fly()\n}\n\ninterface Swimmable {\n    fun swim()\n}\n\n// A class can implement multiple interfaces\nclass Duck : Flyable, Swimmable {\n    override fun fly() = println(\"Duck flying\")\n    override fun swim() = println(\"Duck swimming\")\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Real-World Example: E-Commerce System",
                                "content":  "\n\n---\n\n",
                                "code":  "// Interface for payment processing\ninterface PaymentProcessor {\n    fun processPayment(amount: Double): Boolean\n    fun refund(transactionId: String): Boolean\n\n    fun validatePayment(amount: Double): Boolean {\n        return amount \u003e 0  // Default implementation\n    }\n}\n\n// Interface for notification\ninterface Notifiable {\n    fun sendNotification(message: String)\n}\n\n// Credit card payment\nclass CreditCardProcessor : PaymentProcessor {\n    override fun processPayment(amount: Double): Boolean {\n        if (!validatePayment(amount)) return false\n        println(\"Processing credit card payment: $amount\")\n        println(\"Payment successful!\")\n        return true\n    }\n\n    override fun refund(transactionId: String): Boolean {\n        println(\"Refunding transaction: $transactionId\")\n        return true\n    }\n}\n\n// PayPal payment\nclass PayPalProcessor : PaymentProcessor, Notifiable {\n    override fun processPayment(amount: Double): Boolean {\n        if (!validatePayment(amount)) return false\n        println(\"Processing PayPal payment: $amount\")\n        sendNotification(\"Payment processed via PayPal\")\n        return true\n    }\n\n    override fun refund(transactionId: String): Boolean {\n        println(\"Refunding PayPal transaction: $transactionId\")\n        sendNotification(\"Refund processed\")\n        return true\n    }\n\n    override fun sendNotification(message: String) {\n        println(\"📧 Email sent: $message\")\n    }\n}\n\n// Bitcoin payment\nclass BitcoinProcessor : PaymentProcessor, Notifiable {\n    override fun processPayment(amount: Double): Boolean {\n        if (!validatePayment(amount)) return false\n        println(\"Processing Bitcoin payment: $amount\")\n        println(\"Waiting for blockchain confirmation...\")\n        sendNotification(\"Bitcoin payment received\")\n        return true\n    }\n\n    override fun refund(transactionId: String): Boolean {\n        println(\"Bitcoin refunds take 24-48 hours\")\n        return false\n    }\n\n    override fun sendNotification(message: String) {\n        println(\"📱 Push notification: $message\")\n    }\n}\n\nfun checkout(processor: PaymentProcessor, amount: Double) {\n    println(\"\\n=== Checkout ===\")\n    val success = processor.processPayment(amount)\n\n    if (success) {\n        println(\"Order confirmed!\")\n    } else {\n        println(\"Payment failed!\")\n    }\n}\n\nfun main() {\n    val creditCard = CreditCardProcessor()\n    val paypal = PayPalProcessor()\n    val bitcoin = BitcoinProcessor()\n\n    checkout(creditCard, 99.99)\n    checkout(paypal, 149.99)\n    checkout(bitcoin, 299.99)\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Exercise 1: Media Player System",
                                "content":  "\n**Goal**: Create a flexible media player system using interfaces.\n\n**Requirements**:\n1. Interface `Playable` with methods: `play()`, `pause()`, `stop()`\n2. Interface `Downloadable` with method: `download()`\n3. Class `Song` implements `Playable` and `Downloadable`\n4. Class `Podcast` implements `Playable` and `Downloadable`\n5. Class `LiveStream` implements only `Playable` (can\u0027t download)\n6. Create a playlist that can hold any `Playable` item\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Solution: Media Player System",
                                "content":  "\n\n---\n\n",
                                "code":  "interface Playable {\n    val title: String\n    var isPlaying: Boolean\n\n    fun play() {\n        isPlaying = true\n        println(\"▶️  Playing: $title\")\n    }\n\n    fun pause() {\n        isPlaying = false\n        println(\"⏸️  Paused: $title\")\n    }\n\n    fun stop() {\n        isPlaying = false\n        println(\"⏹️  Stopped: $title\")\n    }\n}\n\ninterface Downloadable {\n    val sizeInMB: Double\n\n    fun download() {\n        println(\"⬇️  Downloading... ($sizeInMB MB)\")\n        println(\"✅ Download complete!\")\n    }\n}\n\nclass Song(\n    override val title: String,\n    val artist: String,\n    override val sizeInMB: Double\n) : Playable, Downloadable {\n    override var isPlaying: Boolean = false\n\n    override fun play() {\n        println(\"🎵 Song\")\n        super.play()\n        println(\"   Artist: $artist\")\n    }\n}\n\nclass Podcast(\n    override val title: String,\n    val host: String,\n    val episode: Int,\n    override val sizeInMB: Double\n) : Playable, Downloadable {\n    override var isPlaying: Boolean = false\n\n    override fun play() {\n        println(\"🎙️  Podcast\")\n        super.play()\n        println(\"   Host: $host, Episode: $episode\")\n    }\n}\n\nclass LiveStream(\n    override val title: String,\n    val streamer: String\n) : Playable {\n    override var isPlaying: Boolean = false\n\n    override fun play() {\n        println(\"📡 Live Stream\")\n        super.play()\n        println(\"   Streamer: $streamer\")\n    }\n}\n\nclass MediaPlayer {\n    private val playlist = mutableListOf\u003cPlayable\u003e()\n    private var currentIndex = 0\n\n    fun addToPlaylist(item: Playable) {\n        playlist.add(item)\n        println(\"Added to playlist: ${item.title}\")\n    }\n\n    fun playAll() {\n        println(\"\\n=== Playing All ===\")\n        playlist.forEach { it.play() }\n    }\n\n    fun downloadAll() {\n        println(\"\\n=== Downloading All (if possible) ===\")\n        playlist.forEach { item -\u003e\n            if (item is Downloadable) {\n                item.download()\n            } else {\n                println(\"⚠️  ${item.title} cannot be downloaded (live stream)\")\n            }\n        }\n    }\n}\n\nfun main() {\n    val player = MediaPlayer()\n\n    val song1 = Song(\"Bohemian Rhapsody\", \"Queen\", 5.8)\n    val song2 = Song(\"Imagine\", \"John Lennon\", 3.2)\n    val podcast = Podcast(\"Tech Talk Daily\", \"Jane Doe\", 42, 25.5)\n    val stream = LiveStream(\"Gaming Night\", \"ProGamer123\")\n\n    player.addToPlaylist(song1)\n    player.addToPlaylist(song2)\n    player.addToPlaylist(podcast)\n    player.addToPlaylist(stream)\n\n    player.playAll()\n    player.downloadAll()\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Exercise 2: Smart Home System",
                                "content":  "\n**Goal**: Create a smart home system with different device types.\n\n**Requirements**:\n1. Interface `SmartDevice` with properties: `name`, `isOn`, methods: `turnOn()`, `turnOff()`\n2. Interface `Schedulable` with method: `schedule(time: String)`\n3. Interface `VoiceControllable` with method: `respondToVoice(command: String)`\n4. Class `SmartLight` implements all three interfaces\n5. Class `SmartThermostat` implements `SmartDevice` and `Schedulable`\n6. Class `SmartSpeaker` implements `SmartDevice` and `VoiceControllable`\n7. Create a home controller that manages all devices\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Solution: Smart Home System",
                                "content":  "\n\n---\n\n",
                                "code":  "interface SmartDevice {\n    val name: String\n    var isOn: Boolean\n\n    fun turnOn() {\n        isOn = true\n        println(\"✅ $name is now ON\")\n    }\n\n    fun turnOff() {\n        isOn = false\n        println(\"❌ $name is now OFF\")\n    }\n\n    fun getStatus(): String {\n        return \"$name: ${if (isOn) \"ON\" else \"OFF\"}\"\n    }\n}\n\ninterface Schedulable {\n    fun schedule(time: String)\n}\n\ninterface VoiceControllable {\n    fun respondToVoice(command: String)\n}\n\nclass SmartLight(\n    override val name: String,\n    var brightness: Int = 100\n) : SmartDevice, Schedulable, VoiceControllable {\n    override var isOn: Boolean = false\n\n    fun setBrightness(level: Int) {\n        require(level in 0..100) { \"Brightness must be 0-100\" }\n        brightness = level\n        println(\"💡 $name brightness set to $level%\")\n    }\n\n    override fun schedule(time: String) {\n        println(\"⏰ $name scheduled to turn on at $time\")\n    }\n\n    override fun respondToVoice(command: String) {\n        when {\n            \"on\" in command.lowercase() -\u003e turnOn()\n            \"off\" in command.lowercase() -\u003e turnOff()\n            \"brightness\" in command.lowercase() -\u003e {\n                val level = command.filter { it.isDigit() }.toIntOrNull() ?: 50\n                setBrightness(level)\n            }\n            else -\u003e println(\"🔊 $name: Command not understood\")\n        }\n    }\n}\n\nclass SmartThermostat(\n    override val name: String,\n    var temperature: Int = 72\n) : SmartDevice, Schedulable {\n    override var isOn: Boolean = false\n\n    fun setTemperature(temp: Int) {\n        require(temp in 60..85) { \"Temperature must be 60-85°F\" }\n        temperature = temp\n        println(\"🌡️  $name temperature set to $temp°F\")\n    }\n\n    override fun schedule(time: String) {\n        println(\"⏰ $name scheduled to set temperature at $time\")\n    }\n}\n\nclass SmartSpeaker(\n    override val name: String,\n    var volume: Int = 50\n) : SmartDevice, VoiceControllable {\n    override var isOn: Boolean = false\n\n    fun setVolume(level: Int) {\n        require(level in 0..100) { \"Volume must be 0-100\" }\n        volume = level\n        println(\"🔊 $name volume set to $level\")\n    }\n\n    override fun respondToVoice(command: String) {\n        when {\n            \"play music\" in command.lowercase() -\u003e {\n                if (isOn) println(\"🎵 Playing music...\")\n                else println(\"❌ Turn me on first!\")\n            }\n            \"volume\" in command.lowercase() -\u003e {\n                val level = command.filter { it.isDigit() }.toIntOrNull() ?: 50\n                setVolume(level)\n            }\n            else -\u003e println(\"🔊 $name: I can play music or adjust volume\")\n        }\n    }\n}\n\nclass HomeController {\n    private val devices = mutableListOf\u003cSmartDevice\u003e()\n\n    fun addDevice(device: SmartDevice) {\n        devices.add(device)\n        println(\"➕ Added ${device.name} to home system\")\n    }\n\n    fun turnAllOn() {\n        println(\"\\n=== Turning All Devices ON ===\")\n        devices.forEach { it.turnOn() }\n    }\n\n    fun turnAllOff() {\n        println(\"\\n=== Turning All Devices OFF ===\")\n        devices.forEach { it.turnOff() }\n    }\n\n    fun showStatus() {\n        println(\"\\n=== Home Status ===\")\n        devices.forEach { device -\u003e\n            println(device.getStatus())\n        }\n    }\n\n    fun scheduleAll(time: String) {\n        println(\"\\n=== Scheduling Devices ===\")\n        devices.forEach { device -\u003e\n            if (device is Schedulable) {\n                device.schedule(time)\n            }\n        }\n    }\n\n    fun voiceCommand(command: String) {\n        println(\"\\n=== Voice Command: \u0027$command\u0027 ===\")\n        devices.forEach { device -\u003e\n            if (device is VoiceControllable) {\n                device.respondToVoice(command)\n            }\n        }\n    }\n}\n\nfun main() {\n    val home = HomeController()\n\n    val livingRoomLight = SmartLight(\"Living Room Light\")\n    val bedroomLight = SmartLight(\"Bedroom Light\")\n    val thermostat = SmartThermostat(\"Main Thermostat\")\n    val speaker = SmartSpeaker(\"Kitchen Speaker\")\n\n    home.addDevice(livingRoomLight)\n    home.addDevice(bedroomLight)\n    home.addDevice(thermostat)\n    home.addDevice(speaker)\n\n    home.turnAllOn()\n    home.showStatus()\n\n    home.scheduleAll(\"7:00 AM\")\n\n    home.voiceCommand(\"turn on\")\n    home.voiceCommand(\"set brightness to 75\")\n    home.voiceCommand(\"play music\")\n\n    home.turnAllOff()\n    home.showStatus()\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Exercise 3: Plugin System",
                                "content":  "\n**Goal**: Create an extensible plugin system.\n\n**Requirements**:\n1. Interface `Plugin` with properties: `name`, `version`, methods: `initialize()`, `execute()`, `shutdown()`\n2. Interface `Configurable` with method: `configure(settings: Map\u003cString, String\u003e)`\n3. Create 3 different plugin types\n4. Create a `PluginManager` that loads and manages plugins\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Solution: Plugin System",
                                "content":  "\n\n---\n\n",
                                "code":  "interface Plugin {\n    val name: String\n    val version: String\n\n    fun initialize()\n    fun execute()\n    fun shutdown()\n}\n\ninterface Configurable {\n    fun configure(settings: Map\u003cString, String\u003e)\n}\n\nclass LoggerPlugin : Plugin, Configurable {\n    override val name = \"Logger\"\n    override val version = \"1.0.0\"\n    private var logLevel = \"INFO\"\n\n    override fun initialize() {\n        println(\"[$name] Initializing logger plugin...\")\n    }\n\n    override fun execute() {\n        println(\"[$name] Logging at level: $logLevel\")\n        println(\"[$name] Log entry: Application running smoothly\")\n    }\n\n    override fun shutdown() {\n        println(\"[$name] Shutting down logger...\")\n    }\n\n    override fun configure(settings: Map\u003cString, String\u003e) {\n        logLevel = settings[\"logLevel\"] ?: \"INFO\"\n        println(\"[$name] Configured with log level: $logLevel\")\n    }\n}\n\nclass DatabasePlugin : Plugin, Configurable {\n    override val name = \"Database\"\n    override val version = \"2.1.0\"\n    private var connectionString = \"\"\n\n    override fun initialize() {\n        println(\"[$name] Connecting to database...\")\n    }\n\n    override fun execute() {\n        println(\"[$name] Querying database at: $connectionString\")\n        println(\"[$name] Query result: 42 records found\")\n    }\n\n    override fun shutdown() {\n        println(\"[$name] Closing database connection...\")\n    }\n\n    override fun configure(settings: Map\u003cString, String\u003e) {\n        connectionString = settings[\"connectionString\"] ?: \"localhost:5432\"\n        println(\"[$name] Configured to connect to: $connectionString\")\n    }\n}\n\nclass CachePlugin : Plugin {\n    override val name = \"Cache\"\n    override val version = \"1.5.2\"\n    private val cache = mutableMapOf\u003cString, String\u003e()\n\n    override fun initialize() {\n        println(\"[$name] Initializing cache system...\")\n    }\n\n    override fun execute() {\n        cache[\"user:1\"] = \"Alice\"\n        cache[\"user:2\"] = \"Bob\"\n        println(\"[$name] Cache populated with ${cache.size} items\")\n    }\n\n    override fun shutdown() {\n        cache.clear()\n        println(\"[$name] Cache cleared and shutdown\")\n    }\n}\n\nclass PluginManager {\n    private val plugins = mutableListOf\u003cPlugin\u003e()\n\n    fun registerPlugin(plugin: Plugin) {\n        plugins.add(plugin)\n        println(\"\\n✅ Registered plugin: ${plugin.name} v${plugin.version}\")\n    }\n\n    fun configurePlugin(pluginName: String, settings: Map\u003cString, String\u003e) {\n        val plugin = plugins.find { it.name == pluginName }\n        if (plugin is Configurable) {\n            plugin.configure(settings)\n        } else {\n            println(\"⚠️  Plugin \u0027$pluginName\u0027 is not configurable\")\n        }\n    }\n\n    fun initializeAll() {\n        println(\"\\n=== Initializing All Plugins ===\")\n        plugins.forEach { it.initialize() }\n    }\n\n    fun executeAll() {\n        println(\"\\n=== Executing All Plugins ===\")\n        plugins.forEach { it.execute() }\n    }\n\n    fun shutdownAll() {\n        println(\"\\n=== Shutting Down All Plugins ===\")\n        plugins.forEach { it.shutdown() }\n    }\n\n    fun listPlugins() {\n        println(\"\\n=== Installed Plugins ===\")\n        plugins.forEach { plugin -\u003e\n            val configurable = if (plugin is Configurable) \"(Configurable)\" else \"\"\n            println(\"${plugin.name} v${plugin.version} $configurable\")\n        }\n    }\n}\n\nfun main() {\n    val manager = PluginManager()\n\n    // Register plugins\n    val logger = LoggerPlugin()\n    val database = DatabasePlugin()\n    val cache = CachePlugin()\n\n    manager.registerPlugin(logger)\n    manager.registerPlugin(database)\n    manager.registerPlugin(cache)\n\n    manager.listPlugins()\n\n    // Configure\n    manager.configurePlugin(\"Logger\", mapOf(\"logLevel\" to \"DEBUG\"))\n    manager.configurePlugin(\"Database\", mapOf(\"connectionString\" to \"prod-db.example.com:5432\"))\n\n    // Run lifecycle\n    manager.initializeAll()\n    manager.executeAll()\n    manager.shutdownAll()\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Checkpoint Quiz",
                                "content":  "\n### Question 1\nWhat is the main difference between an interface and an abstract class?\n\nA) Interfaces can\u0027t have methods\nB) A class can implement multiple interfaces but inherit from only one abstract class\nC) Abstract classes are faster\nD) There is no difference\n\n### Question 2\nCan interfaces have properties with backing fields?\n\nA) Yes, always\nB) No, never\nC) Only if marked `open`\nD) Only if they\u0027re `lateinit`\n\n### Question 3\nCan interface methods have default implementations in Kotlin?\n\nA) No, never\nB) Yes, always\nC) Yes, but not in Java\nD) Yes, since Kotlin 1.0\n\n### Question 4\nWhen should you use an interface instead of an abstract class?\n\nA) When you need constructors\nB) When you need to define capabilities without shared state\nC) When you need multiple inheritance of type\nD) Both B and C\n\n### Question 5\nWhat\u0027s required for a class property declared in an interface?\n\nA) It must have a backing field\nB) It must be overridden by implementing classes (unless it has a default getter)\nC) It must be mutable\nD) It must be private\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Quiz Answers",
                                "content":  "\n**Question 1: B) A class can implement multiple interfaces but inherit from only one abstract class**\n\nThis is one of the key differences and a major reason to use interfaces.\n\n\n---\n\n**Question 2: B) No, never**\n\nInterfaces can\u0027t have backing fields. Properties must either be abstract or have custom getters.\n\n\n---\n\n**Question 3: D) Yes, since Kotlin 1.0**\n\nKotlin interfaces can have default method implementations from the start.\n\n\n---\n\n**Question 4: D) Both B and C**\n\nUse interfaces when you want to define capabilities (\"can-do\") without shared state, and when you need multiple inheritance.\n\n\n---\n\n**Question 5: B) It must be overridden by implementing classes (unless it has a default getter)**\n\nInterface properties without default getters must be overridden.\n\n\n---\n\n",
                                "code":  "interface Vehicle {\n    val speed: Int  // Must override\n    val type: String\n        get() = \"Generic\"  // Has default, override optional\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "KEY_POINT",
                                "title":  "Platform Contracts: The Expect/Actual Pattern",
                                "content":  "\nSince this course teaches **Kotlin Multiplatform (KMP)** from day one, let\u0027s explore a powerful pattern that extends the interface concept to work across platforms: the `expect/actual` mechanism.\n\n### The Problem\n\nWhen writing code that runs on both Android and iOS, some things work differently on each platform:\n- Getting the current time\n- Reading files\n- Logging messages\n- Accessing device features\n\n### The Solution: Expect/Actual\n\nThe `expect/actual` pattern is like an **interface for platforms**:\n- `expect` declares **what** you need (the contract)\n- `actual` provides **how** each platform implements it\n\n### Basic Example\n\n```kotlin\n// In commonMain (shared code)\n// Declares: \"I need a way to get the platform name\"\nexpect fun getPlatformName(): String\n\n// In androidMain\nactual fun getPlatformName(): String = \"Android\"\n\n// In iosMain\nactual fun getPlatformName(): String = \"iOS\"\n```\n\n### Using in Shared Code\n\n```kotlin\n// This works on ALL platforms!\nfun greet(): String {\n    return \"Hello from ${getPlatformName()}!\"\n}\n\n// Android: \"Hello from Android!\"\n// iOS: \"Hello from iOS!\"\n```\n\n### Compare to Interfaces\n\n| Concept | Interface | Expect/Actual |\n|---------|-----------|---------------|\n| Purpose | Define class contracts | Define platform contracts |\n| Declares | What a class must implement | What each platform must provide |\n| Scope | Within same codebase | Across platform source sets |\n| Keyword | `interface` / `override` | `expect` / `actual` |\n\n### When to Use\n\n**Use expect/actual when:**\n- You need platform-specific implementations\n- Accessing platform APIs (storage, sensors, etc.)\n- Different libraries per platform\n\n**Example: Platform Logger**\n\n```kotlin\n// commonMain - declare what we need\nexpect fun log(message: String)\n\n// androidMain - use Android\u0027s Log\nactual fun log(message: String) {\n    android.util.Log.d(\"App\", message)\n}\n\n// iosMain - use println (or NSLog)\nactual fun log(message: String) {\n    println(message)\n}\n```\n\nYou\u0027ll explore this pattern in depth in Part 7 when building advanced cross-platform features!\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "What You\u0027ve Learned",
                                "content":  "\n✅ Defining and implementing interfaces\n✅ Multiple interface implementation\n✅ Interface properties (without backing fields)\n✅ Default interface methods\n✅ Interfaces vs abstract classes\n✅ Real-world design patterns with interfaces\n✅ The expect/actual pattern for cross-platform contracts (KMP)\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Next Steps",
                                "content":  "\nIn **Lesson 2.5: Data Classes and Sealed Classes**, you\u0027ll learn:\n- Data classes for holding data\n- Automatic `equals()`, `hashCode()`, `toString()`, `copy()`\n- Destructuring declarations\n- Sealed classes for restricted hierarchies\n- When to use each special class type\n\nYou\u0027re building a complete OOP toolkit!\n\n---\n\n**Congratulations on completing Lesson 2.4!** 🎉\n\nInterfaces are essential for designing flexible, maintainable systems. You now understand when to use interfaces vs abstract classes!\n\n"
                            }
                        ],
    "challenges":  [
                       {
                           "type":  "FREE_CODING",
                           "id":  "3.4.1",
                           "title":  "Data Classes",
                           "description":  "Create a data class `Person` with properties name, age, and email. Create two instances with the same data and compare them using ==.",
                           "instructions":  "Create a data class `Person` with properties name, age, and email. Create two instances with the same data and compare them using ==.",
                           "starterCode":  "// Create Person data class\n\nfun main() {\n    val person1 = Person(\"Alice\", 30, \"alice@example.com\")\n    val person2 = Person(\"Alice\", 30, \"alice@example.com\")\n    println(\"Are they equal? ${person1 == person2}\")\n    println(person1)\n}",
                           "solution":  "data class Person(val name: String, val age: Int, val email: String)\n\nfun main() {\n    val person1 = Person(\"Alice\", 30, \"alice@example.com\")\n    val person2 = Person(\"Alice\", 30, \"alice@example.com\")\n    println(\"Are they equal? ${person1 == person2}\")\n    println(person1)\n}",
                           "language":  "kotlin",
                           "testCases":  [
                                             {
                                                 "id":  "test-1",
                                                 "description":  "Data classes with same values should be equal",
                                                 "expectedOutput":  "Are they equal? true",
                                                 "isVisible":  true
                                             },
                                             {
                                                 "id":  "test-2",
                                                 "description":  "toString should show all properties",
                                                 "expectedOutput":  "Person(name=Alice, age=30, email=alice@example.com)",
                                                 "isVisible":  true
                                             }
                                         ],
                           "hints":  [
                                         {
                                             "level":  1,
                                             "text":  "Use \u0027data class\u0027 instead of \u0027class\u0027"
                                         },
                                         {
                                             "level":  2,
                                             "text":  "Data classes automatically generate equals(), hashCode(), toString()"
                                         },
                                         {
                                             "level":  3,
                                             "text":  "Two data classes with same property values are equal"
                                         }
                                     ],
                           "commonMistakes":  [
                                                  {
                                                      "mistake":  "Forgetting null safety operators",
                                                      "consequence":  "NullPointerException",
                                                      "correction":  "Use ?. for safe calls, ?: for elvis operator"
                                                  },
                                                  {
                                                      "mistake":  "Using var when val would suffice",
                                                      "consequence":  "Unnecessary mutability",
                                                      "correction":  "Prefer val for immutable values"
                                                  },
                                                  {
                                                      "mistake":  "Incorrect string interpolation",
                                                      "consequence":  "Syntax error",
                                                      "correction":  "Use $variable or ${expression}"
                                                  }
                                              ],
                           "difficulty":  "intermediate"
                       }
                   ],
    "difficulty":  "beginner",
    "title":  "Lesson 2.4: Interfaces and Abstract Classes",
    "estimatedMinutes":  65
}

## Review Instructions

Perform the following analysis:

### 1. Accuracy Check
- Verify all code examples are syntactically correct
- Confirm technical explanations match current kotlin documentation
- Search the web for the latest kotlin version and verify examples work with it
- Flag any deprecated APIs, syntax, or patterns

### 2. Completeness Check
- Does the lesson cover all concepts needed for a beginner to understand this topic?
- Are there missing explanations between concepts that would confuse a learner?
- Does the lesson have:
  - [ ] A clear analogy or real-world example (ANALOGY section)
  - [ ] Theoretical explanation (THEORY section)
  - [ ] Working code example (EXAMPLE section)
  - [ ] Common mistakes to avoid (WARNING section)
  - [ ] At least one practice challenge

### 3. Freshness Check
- Search for "kotlin Lesson 2.4: Interfaces and Abstract Classes 2024 2025" to find latest practices
- Compare lesson content against current best practices
- Identify any outdated patterns or recommendations

### 4. Pedagogical Gap Analysis
- What prerequisite knowledge is assumed but not explained?
- What follow-up questions would a learner likely have?
- What practical applications or use cases are missing?
- Are the challenges appropriately scaffolded for the difficulty level?

## Output Format

Provide your review as structured JSON:

```json
{
  "lessonId": "3.4",
  "reviewDate": "YYYY-MM-DD",
  "overallScore": 1-10,
  "accuracy": {
    "score": 1-10,
    "issues": ["issue 1", "issue 2"],
    "recommendations": ["fix 1", "fix 2"]
  },
  "completeness": {
    "score": 1-10,
    "missingSections": ["section type needed"],
    "gaps": ["gap 1", "gap 2"],
    "recommendations": ["add X", "expand Y"]
  },
  "freshness": {
    "score": 1-10,
    "outdatedItems": ["item 1"],
    "currentVersion": "language version checked",
    "recommendations": ["update X to Y"]
  },
  "pedagogicalGaps": {
    "score": 1-10,
    "missingPrerequisites": ["concept 1"],
    "unansweredQuestions": ["question learner would have"],
    "missingUseCases": ["practical application"],
    "recommendations": ["add section on X"]
  },
  "contentLengthIssues": {
    "shortSections": [
      {"sectionTitle": "title", "currentLength": 42, "recommendation": "expand to explain X"}
    ]
  },
  "suggestedNewContent": [
    {
      "sectionType": "THEORY|EXAMPLE|WARNING|etc",
      "title": "suggested title",
      "contentOutline": "what this section should cover"
    }
  ],
  "priority": "HIGH|MEDIUM|LOW"
}
```

