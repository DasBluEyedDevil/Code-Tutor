---
type: "THEORY"
title: "Setting Up Your Development Environment"
---


### Required Tools

For Compose Multiplatform development, you'll need:

**Required for All Platforms**:
- **Android Studio** (latest stable version) - Primary IDE. Download from [developer.android.com/studio](https://developer.android.com/studio)
- **Kotlin Multiplatform plugin** - Installed via Android Studio
- **JDK 17+** - Usually bundled with Android Studio
- To verify your Kotlin version: Settings → Languages & Frameworks → Kotlin
- **Important**: Kotlin 2.1.0+ is required for Compose Multiplatform 1.8+

**Required for iOS Development (macOS only)**:
- **Xcode** (latest stable version from Mac App Store) - For iOS builds and simulator
- **Xcode Command Line Tools** - Run `xcode-select --install`
- **CocoaPods** - Dependency manager for iOS

> **Note**: KMP is a rapidly evolving ecosystem - version requirements may change. Check [kotlinlang.org/docs/multiplatform.html](https://kotlinlang.org/docs/multiplatform.html) for current requirements.

### Installing Android Studio

1. Go to [developer.android.com/studio](https://developer.android.com/studio)
2. Download the **latest stable version** of Android Studio
3. Run the installer for your platform
4. Choose **Standard** installation type
5. Wait for SDK components to download (~3 GB)

### Installing Kotlin Multiplatform Plugin

1. Open Android Studio
2. Go to **Settings/Preferences** → **Plugins**
3. Search for **Kotlin Multiplatform**
4. Click **Install** and restart Android Studio

### macOS: Setting Up iOS Development


### Verifying Your Setup

After installation, verify with the KDoctor tool:

---



```bash
# Install Xcode from App Store first, then:

# Install Xcode Command Line Tools
xcode-select --install

# Install Homebrew (if not installed)
/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"

# Install CocoaPods
brew install cocoapods

# Install KDoctor (optional, checks your setup)
brew install kdoctor
kdoctor
```
