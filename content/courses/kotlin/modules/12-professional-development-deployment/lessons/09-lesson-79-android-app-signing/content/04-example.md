---
type: "EXAMPLE"
title: "Configuring Signing in Gradle"
---

Configure release signing in your `app/build.gradle.kts`:

```kotlin
// app/build.gradle.kts
android {
    signingConfigs {
        create("release") {
            // Load from environment variables (CI/CD)
            storeFile = file(System.getenv("KEYSTORE_PATH") ?: "release.keystore")
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

// Alternative: Load from local.properties (for local development)
// DO NOT commit local.properties to git!
val keystorePropertiesFile = rootProject.file("local.properties")
if (keystorePropertiesFile.exists()) {
    val keystoreProperties = java.util.Properties()
    keystoreProperties.load(keystorePropertiesFile.inputStream())
    
    android.signingConfigs.getByName("release") {
        storeFile = file(keystoreProperties["KEYSTORE_PATH"] as String)
        storePassword = keystoreProperties["KEYSTORE_PASSWORD"] as String
        keyAlias = keystoreProperties["KEY_ALIAS"] as String
        keyPassword = keystoreProperties["KEY_PASSWORD"] as String
    }
}
```
