---
type: "WARNING"
title: "Common Play Store Issues"
---

### Issue 1: "App signing certificate doesn't match"

- You're using a different key than the one registered with Play Store
- Solution: Use the same keystore you registered, or reset via Play App Signing

### Issue 2: "Version code already exists"

```kotlin
// Always increment versionCode
android {
    defaultConfig {
        versionCode = 2  // Must be higher than previous release
        versionName = "1.0.1"
    }
}
```

### Issue 3: "Target API level not met"

```kotlin
// Update to required API level
android {
    compileSdk = 34
    defaultConfig {
        targetSdk = 34  // Must meet Google's requirements
    }
}
```

### Issue 4: "Data safety form incomplete"

- Go to Policy → App content → Data safety
- Fill out data collection questionnaire
- This is required before publishing