---
type: "THEORY"
title: "Solution: Smart Home System"
---



---



```kotlin
interface SmartDevice {
    val name: String
    var isOn: Boolean

    fun turnOn() {
        isOn = true
        println("✅ $name is now ON")
    }

    fun turnOff() {
        isOn = false
        println("❌ $name is now OFF")
    }

    fun getStatus(): String {
        return "$name: ${if (isOn) "ON" else "OFF"}"
    }
}

interface Schedulable {
    fun schedule(time: String)
}

interface VoiceControllable {
    fun respondToVoice(command: String)
}

class SmartLight(
    override val name: String,
    var brightness: Int = 100
) : SmartDevice, Schedulable, VoiceControllable {
    override var isOn: Boolean = false

    fun setBrightness(level: Int) {
        require(level in 0..100) { "Brightness must be 0-100" }
        brightness = level
        println("💡 $name brightness set to $level%")
    }

    override fun schedule(time: String) {
        println("⏰ $name scheduled to turn on at $time")
    }

    override fun respondToVoice(command: String) {
        when {
            "on" in command.lowercase() -> turnOn()
            "off" in command.lowercase() -> turnOff()
            "brightness" in command.lowercase() -> {
                val level = command.filter { it.isDigit() }.toIntOrNull() ?: 50
                setBrightness(level)
            }
            else -> println("🔊 $name: Command not understood")
        }
    }
}

class SmartThermostat(
    override val name: String,
    var temperature: Int = 72
) : SmartDevice, Schedulable {
    override var isOn: Boolean = false

    fun setTemperature(temp: Int) {
        require(temp in 60..85) { "Temperature must be 60-85°F" }
        temperature = temp
        println("🌡️  $name temperature set to $temp°F")
    }

    override fun schedule(time: String) {
        println("⏰ $name scheduled to set temperature at $time")
    }
}

class SmartSpeaker(
    override val name: String,
    var volume: Int = 50
) : SmartDevice, VoiceControllable {
    override var isOn: Boolean = false

    fun setVolume(level: Int) {
        require(level in 0..100) { "Volume must be 0-100" }
        volume = level
        println("🔊 $name volume set to $level")
    }

    override fun respondToVoice(command: String) {
        when {
            "play music" in command.lowercase() -> {
                if (isOn) println("🎵 Playing music...")
                else println("❌ Turn me on first!")
            }
            "volume" in command.lowercase() -> {
                val level = command.filter { it.isDigit() }.toIntOrNull() ?: 50
                setVolume(level)
            }
            else -> println("🔊 $name: I can play music or adjust volume")
        }
    }
}

class HomeController {
    private val devices = mutableListOf<SmartDevice>()

    fun addDevice(device: SmartDevice) {
        devices.add(device)
        println("➕ Added ${device.name} to home system")
    }

    fun turnAllOn() {
        println("\n=== Turning All Devices ON ===")
        devices.forEach { it.turnOn() }
    }

    fun turnAllOff() {
        println("\n=== Turning All Devices OFF ===")
        devices.forEach { it.turnOff() }
    }

    fun showStatus() {
        println("\n=== Home Status ===")
        devices.forEach { device ->
            println(device.getStatus())
        }
    }

    fun scheduleAll(time: String) {
        println("\n=== Scheduling Devices ===")
        devices.forEach { device ->
            if (device is Schedulable) {
                device.schedule(time)
            }
        }
    }

    fun voiceCommand(command: String) {
        println("\n=== Voice Command: '$command' ===")
        devices.forEach { device ->
            if (device is VoiceControllable) {
                device.respondToVoice(command)
            }
        }
    }
}

fun main() {
    val home = HomeController()

    val livingRoomLight = SmartLight("Living Room Light")
    val bedroomLight = SmartLight("Bedroom Light")
    val thermostat = SmartThermostat("Main Thermostat")
    val speaker = SmartSpeaker("Kitchen Speaker")

    home.addDevice(livingRoomLight)
    home.addDevice(bedroomLight)
    home.addDevice(thermostat)
    home.addDevice(speaker)

    home.turnAllOn()
    home.showStatus()

    home.scheduleAll("7:00 AM")

    home.voiceCommand("turn on")
    home.voiceCommand("set brightness to 75")
    home.voiceCommand("play music")

    home.turnAllOff()
    home.showStatus()
}
```
