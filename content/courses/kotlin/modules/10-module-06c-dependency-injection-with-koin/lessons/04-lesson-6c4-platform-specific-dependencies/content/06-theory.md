---
type: "THEORY"
title: "Pattern 4: Conditional Binding (Debug vs Release)"
---

Different implementations for debug and release builds:

```kotlin
// commonMain
interface ApiConfig {
    val baseUrl: String
    val timeout: Long
    val loggingEnabled: Boolean
}

// Debug implementation
class DebugApiConfig : ApiConfig {
    override val baseUrl = "https://api-staging.example.com"
    override val timeout = 60_000L
    override val loggingEnabled = true
}

// Release implementation
class ReleaseApiConfig : ApiConfig {
    override val baseUrl = "https://api.example.com"
    override val timeout = 30_000L
    override val loggingEnabled = false
}
```

### Using BuildConfig or Flags

```kotlin
// Android approach
val configModule = module {
    single<ApiConfig> {
        if (BuildConfig.DEBUG) {
            DebugApiConfig()
        } else {
            ReleaseApiConfig()
        }
    }
}

// KMP approach - pass flag during init
fun initKoin(isDebug: Boolean) {
    startKoin {
        modules(
            module {
                single<ApiConfig> {
                    if (isDebug) DebugApiConfig() else ReleaseApiConfig()
                }
            },
            commonModule,
            platformModule
        )
    }
}
```