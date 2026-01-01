---
type: "THEORY"
title: "Understanding Build Outputs"
---


### Android Build Outputs

**APK (Android Package)**:
- Installable file for Android devices
- Located at: `composeApp/build/outputs/apk/debug/`
- Used for testing and direct installation

**AAB (Android App Bundle)**:
- Publishing format for Google Play
- Located at: `composeApp/build/outputs/bundle/release/`
- Google Play generates optimized APKs per device

### iOS Build Output

**iOS Framework**:
- Kotlin code is compiled to a native iOS framework
- Located at: `composeApp/build/bin/iosArm64/` (device) or `iosSimulatorArm64/` (simulator)
- Automatically embedded in the Xcode project

**iOS App**:
- Built by Xcode, not Gradle
- IPA file for App Store distribution
- Located in Xcode's derived data folder

### Publishing Your App

| Platform | Store | Format | Tool |
|----------|-------|--------|------|
| Android | Google Play | AAB | Android Studio / Play Console |
| iOS | App Store | IPA | Xcode / App Store Connect |

---

