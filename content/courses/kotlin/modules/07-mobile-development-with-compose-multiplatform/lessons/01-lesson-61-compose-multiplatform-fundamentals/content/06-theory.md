---
type: "THEORY"
title: "Creating Your First Compose Multiplatform Project"
---


### Step 1: New Project Wizard

1. Open Android Studio
2. Click **New Project**
3. In the left sidebar, select **Kotlin Multiplatform**
4. Choose **Compose Multiplatform Application**
5. Click **Next**

### Step 2: Configure Project

**Name**: `HelloCMP`
**Package name**: `com.example.hellocmp`
**Save location**: Choose a directory
**Minimum SDK**: **API 24 (Android 7.0)**
**iOS Framework Distribution**: **Regular framework**

Click **Finish** and wait for Gradle sync (~1-2 minutes for first time).

### Step 3: What Gets Created

The wizard creates a complete multiplatform project with:
- ✅ Shared code in `composeApp/src/commonMain/`
- ✅ Android-specific code in `composeApp/src/androidMain/`
- ✅ iOS-specific code in `composeApp/src/iosMain/`
- ✅ Xcode project in `iosApp/`
- ✅ Gradle build configuration for all platforms
- ✅ Sample App.kt composable that runs everywhere

---

