---
type: "THEORY"
title: "Understanding the Build System"
---


### Gradle for Multiplatform

**Gradle** manages the entire build process for all platforms:
- Compiles Kotlin for each target platform
- Generates Android APK/AAB
- Creates iOS framework for Xcode
- Handles platform-specific dependencies

### Multiplatform Gradle Tasks

Common tasks for Compose Multiplatform:


### Build Outputs

**Android**:
- APK: `composeApp/build/outputs/apk/debug/`
- AAB: `composeApp/build/outputs/bundle/release/`

**iOS**:
- Framework: `composeApp/build/bin/iosArm64/` or `iosSimulatorArm64/`
- The framework is embedded in the Xcode project automatically

### Sync Project

After modifying `build.gradle.kts`, click **Sync Now** or:
- **File** → **Sync Project with Gradle Files**

This downloads dependencies and updates project configuration.

---



```bash
# Build Android debug APK
./gradlew :composeApp:assembleDebug

# Build Android release APK
./gradlew :composeApp:assembleRelease

# Install on connected Android device
./gradlew :composeApp:installDebug

# Build iOS framework (macOS only)
./gradlew :composeApp:linkDebugFrameworkIosSimulatorArm64

# Run all tests
./gradlew allTests

# Run common tests only
./gradlew :composeApp:testDebugUnitTest

# Clean build
./gradlew clean
```
