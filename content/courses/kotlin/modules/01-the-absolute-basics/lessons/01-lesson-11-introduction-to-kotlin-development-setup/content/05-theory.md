---
type: "THEORY"
title: "Setting Up Your Development Environment"
---


### The Multiplatform Setup

In 2025, learning Kotlin means learning **Kotlin Multiplatform (KMP)** from day one. You'll write code once and run it on:
- Android phones and tablets
- iPhones and iPads
- Desktop (Windows, macOS, Linux)
- Web browsers

### Required Tools

**1. Android Studio (Latest Stable Version)**
- Download from [developer.android.com/studio](https://developer.android.com/studio) - always use the latest stable version
- As of late 2025, this is Android Studio Narwhal (2025.1.1) or newer, but version names change frequently
- Includes Kotlin plugin and Android SDK
- To verify your Kotlin version: Settings → Languages & Frameworks → Kotlin
- **Important**: Kotlin 2.1.0+ is required for Compose Multiplatform 1.8+

**2. Xcode (macOS only, for iOS development)**
- Download from Mac App Store
- Required for iOS simulator and building iOS apps
- Windows/Linux users: Use Android-only mode initially

**3. Kotlin Multiplatform Plugin**
- In Android Studio: Settings → Plugins → Search "Kotlin Multiplatform"
- Install and restart
- Check the official KMP documentation at [kotlinlang.org/docs/multiplatform.html](https://kotlinlang.org/docs/multiplatform.html) for current setup instructions

> **Note**: KMP is a rapidly evolving ecosystem - exact wizard names and UI may change between versions. When in doubt, consult the official Kotlin documentation for the most up-to-date instructions.

### Creating Your First KMP Project

1. Open Android Studio
2. Click **New Project**
3. Select **Kotlin Multiplatform App** (or similar KMP template - the exact name may vary by version)
4. Configure:
   - **Name**: HelloMultiplatform
   - **Package**: com.example.hellomultiplatform
   - **iOS framework distribution**: Regular framework
5. Click **Finish**

You now have a project that builds for Android AND iOS!

---

