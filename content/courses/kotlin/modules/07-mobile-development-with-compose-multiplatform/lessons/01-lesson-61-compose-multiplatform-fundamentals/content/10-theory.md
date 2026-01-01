---
type: "THEORY"
title: "Running Your App"
---


### Running on Android

#### Option A: Android Emulator

1. Click **Device Manager** (phone icon in toolbar)
2. Click **Create Device** → Select **Pixel 8** → **Next**
3. Select **VanillaIceCream** (API 35) → **Download** if needed → **Next**
4. Click **Finish**
5. Select the run configuration: **composeApp** (Android)
6. Select your emulator from the device dropdown
7. Click **Run** (green play button)

#### Option B: Physical Android Device

1. Enable **Developer Options**: Settings → About phone → Tap Build number 7 times
2. Enable **USB Debugging** in Developer options
3. Connect phone via USB, allow debugging prompt
4. Select your device from the dropdown
5. Click **Run**

### Running on iOS (macOS only)

#### Option A: iOS Simulator

1. Open the **iosApp** folder in Xcode:
   - In Android Studio: Right-click `iosApp` → **Open in** → **Xcode**
   - Or in Terminal: `open iosApp/iosApp.xcodeproj`
2. In Xcode, select a simulator (e.g., **iPhone 15**)
3. Click **Run** (play button) or press **Cmd + R**
4. The app builds and launches in the iOS Simulator

**Alternative from Android Studio**:
1. With the Kotlin Multiplatform plugin installed
2. Select the **iosApp** run configuration
3. Select an iOS Simulator from the device dropdown
4. Click **Run**

#### Option B: Physical iOS Device

1. Connect your iPhone via USB
2. In Xcode, select your device as the target
3. You'll need an Apple Developer account for device testing
4. Trust the developer certificate on your iPhone
5. Click **Run**

### The Magic: Same Code, Both Platforms!

Make a change in `commonMain/kotlin/App.kt` and run on both platforms - the same UI appears on Android AND iOS!

---

