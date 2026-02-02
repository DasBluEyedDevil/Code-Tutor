---
type: "THEORY"
title: "Introduction"
---


**Debug vs Release Builds**

When you run `flutter run`, Flutter creates a debug build. Debug builds are:
- Large in size (include debugging symbols)
- Slower (no optimizations, JIT compilation)
- Show debug banners and enable hot reload
- Not suitable for distribution

Release builds are:
- Optimized and minified (AOT compilation)
- Significantly smaller
- Much faster performance
- Ready for store submission

**What is App Signing?**

Android requires all apps to be digitally signed before installation. Signing:
- Proves you are the app's author
- Ensures the APK hasn't been tampered with
- Enables Play Store to verify updates come from you
- Is required for Play Store submission

**Debug vs Release Signing:**
- Debug builds use an auto-generated debug keystore (for development only)
- Release builds must use your own keystore that you keep forever
- Losing your release keystore = can never update your app

**What You'll Learn:**
- Creating and securing a release keystore
- Configuring Gradle for signed builds
- Enabling ProGuard/R8 code shrinking
- Building APK and App Bundle formats
- Testing release builds before submission

