---
type: "THEORY"
title: "Initialization Strategy"
---

### Shared Initialization Function

```kotlin
// commonMain/kotlin/di/KoinInit.kt
import org.koin.core.context.startKoin
import org.koin.core.module.Module
import org.koin.dsl.KoinAppDeclaration

fun initKoin(
    platformModule: Module,
    appDeclaration: KoinAppDeclaration = {}
) {
    startKoin {
        appDeclaration()
        modules(
            platformModule,
            networkModule,
            commonModule
        )
    }
}

// For iOS (called from Swift)
fun initKoinIOS() {
    initKoin(platformModule) {
        // iOS-specific Koin configuration if needed
    }
}
```

### Android Initialization

```kotlin
// androidMain or androidApp/src/main/kotlin/App.kt
class NotesApplication : Application() {
    override fun onCreate() {
        super.onCreate()
        
        initKoin(platformModule) {
            // Provide Android context
            androidContext(this@NotesApplication)
            
            // Android-specific logging
            androidLogger()
        }
    }
}
```

### iOS Initialization (Swift)

```swift
// iosApp/iosApp/iOSApp.swift
import shared

@main
struct iOSApp: App {
    init() {
        KoinInitKt.initKoinIOS()
    }
    
    var body: some Scene {
        WindowGroup {
            ContentView()
        }
    }
}
```