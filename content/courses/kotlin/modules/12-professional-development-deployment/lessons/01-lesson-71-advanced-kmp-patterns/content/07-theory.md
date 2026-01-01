---
type: "THEORY"
title: "Advanced Expect/Actual Patterns"
---


You learned the basics of expect/actual in Lesson 3.4. Now let's explore advanced patterns.

### Expect Classes with Constructors

```kotlin
// commonMain
expect class SecureStorage(context: PlatformContext) {
    fun store(key: String, value: String)
    fun retrieve(key: String): String?
    fun delete(key: String)
}

// androidMain
actual class SecureStorage actual constructor(
    private val context: PlatformContext
) {
    private val prefs = context.getEncryptedSharedPreferences()
    
    actual fun store(key: String, value: String) {
        prefs.edit().putString(key, value).apply()
    }
    
    actual fun retrieve(key: String): String? {
        return prefs.getString(key, null)
    }
    
    actual fun delete(key: String) {
        prefs.edit().remove(key).apply()
    }
}

// iosMain
actual class SecureStorage actual constructor(
    private val context: PlatformContext
) {
    actual fun store(key: String, value: String) {
        // Use iOS Keychain
        KeychainWrapper.standard.set(value, forKey: key)
    }
    
    actual fun retrieve(key: String): String? {
        return KeychainWrapper.standard.string(forKey: key)
    }
    
    actual fun delete(key: String) {
        KeychainWrapper.standard.removeObject(forKey: key)
    }
}
```

### Type Aliases for Platform Types

```kotlin
// commonMain
expect class PlatformContext

// androidMain
actual typealias PlatformContext = android.content.Context

// iosMain
actual typealias PlatformContext = platform.UIKit.UIViewController
```

### Expect Object (Singleton)

```kotlin
// commonMain
expect object DeviceInfo {
    val osName: String
    val osVersion: String
    val deviceModel: String
}

// androidMain
actual object DeviceInfo {
    actual val osName: String = "Android"
    actual val osVersion: String = Build.VERSION.RELEASE
    actual val deviceModel: String = "${Build.MANUFACTURER} ${Build.MODEL}"
}

// iosMain
actual object DeviceInfo {
    actual val osName: String = "iOS"
    actual val osVersion: String = UIDevice.currentDevice.systemVersion
    actual val deviceModel: String = UIDevice.currentDevice.model
}
```

### Expect with Default Implementation

```kotlin
// commonMain
interface Logger {
    fun log(message: String)
    fun error(message: String)
}

expect fun createLogger(): Logger

// Each platform provides its implementation
// androidMain: Uses Android Log
// iosMain: Uses NSLog
// jvmMain: Uses SLF4J
```

---

