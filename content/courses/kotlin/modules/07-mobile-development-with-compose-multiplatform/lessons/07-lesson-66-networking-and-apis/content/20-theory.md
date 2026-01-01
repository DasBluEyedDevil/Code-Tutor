---
type: "THEORY"
title: "Cross-Platform Networking"
---


### Ktor for Multiplatform

For Compose Multiplatform apps, use Ktor instead of Retrofit:

```kotlin
// In commonMain - works on Android AND iOS!
val httpClient = HttpClient {
    install(ContentNegotiation) {
        json(Json {
            ignoreUnknownKeys = true
        })
    }
}

suspend fun getUsers(): List<User> {
    return httpClient.get("https://api.example.com/users").body()
}
```

### Platform-Specific Engines

| Platform | HTTP Engine |
|----------|-------------|
| Android | `ktor-client-android` or `ktor-client-okhttp` |
| iOS | `ktor-client-darwin` |
| JVM | `ktor-client-cio` |

### Running on iOS

1. Build and run on iOS Simulator
2. Make network calls - they work identically!
3. Test error handling on both platforms
4. Verify loading states appear correctly

### iOS Networking Permissions

iOS requires network permissions in Info.plist for non-HTTPS URLs:

```xml
<key>NSAppTransportSecurity</key>
<dict>
    <key>NSAllowsArbitraryLoads</key>
    <true/>
</dict>
```

Note: For production, always use HTTPS!

---

