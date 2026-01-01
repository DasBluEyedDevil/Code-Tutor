---
type: "THEORY"
title: "Publishing Android Apps"
---

### Signing Configuration

Configure release signing in your Android module:

```kotlin
// app/build.gradle.kts
android {
    signingConfigs {
        create("release") {
            storeFile = file(System.getenv("KEYSTORE_PATH") ?: "keystore.jks")
            storePassword = System.getenv("KEYSTORE_PASSWORD") ?: ""
            keyAlias = System.getenv("KEY_ALIAS") ?: ""
            keyPassword = System.getenv("KEY_PASSWORD") ?: ""
        }
    }
    
    buildTypes {
        release {
            isMinifyEnabled = true
            isShrinkResources = true
            proguardFiles(
                getDefaultProguardFile("proguard-android-optimize.txt"),
                "proguard-rules.pro"
            )
            signingConfig = signingConfigs.getByName("release")
        }
    }
}
```

### Generate Keystore

```bash
keytool -genkey -v -keystore release-key.jks \
  -keyalg RSA -keysize 2048 -validity 10000 \
  -alias my-key-alias
```

### Prepare for Play Store

1. **Version Code & Name**: Update in `build.gradle.kts`:
```kotlin
defaultConfig {
    versionCode = 1
    versionName = "1.0.0"
}
```

2. **Build App Bundle**:
Output: `app/build/outputs/bundle/release/app-release.aab`

3. **Upload to Play Console**:
   - Create app listing
   - Upload app bundle
   - Fill store listing (title, description, screenshots)
   - Set pricing & distribution
   - Submit for review

```bash
# Generate release App Bundle for Play Store
./gradlew bundleRelease

# Or generate APK for direct distribution
./gradlew assembleRelease

# Verify the bundle
bundletool validate --bundle=app/build/outputs/bundle/release/app-release.aab
```
