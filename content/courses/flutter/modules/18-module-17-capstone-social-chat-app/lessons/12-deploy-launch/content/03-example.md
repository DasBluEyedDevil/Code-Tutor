---
type: "EXAMPLE"
title: "Mobile App Release"
---


**Building Release Artifacts for Android and iOS**

This section covers the complete release build process including code signing, version management, release notes, and staged rollout strategies. These configurations integrate with the deployment module (Module 16) patterns.



```kotlin
# ============================================================
# Android Release Configuration
# ============================================================
# android/app/build.gradle
plugins {
    id "com.android.application"
    id "kotlin-android"
    id "dev.flutter.flutter-gradle-plugin"
}

def keystoreProperties = new Properties()
def keystorePropertiesFile = rootProject.file('key.properties')
if (keystorePropertiesFile.exists()) {
    keystoreProperties.load(new FileInputStream(keystorePropertiesFile))
}

// Read version from pubspec.yaml
def flutterVersionCode = localProperties.getProperty('flutter.versionCode')
def flutterVersionName = localProperties.getProperty('flutter.versionName')

android {
    namespace "com.yourcompany.yourapp"
    compileSdk 34

    defaultConfig {
        applicationId "com.yourcompany.yourapp"
        minSdk 24
        targetSdk 34
        versionCode flutterVersionCode.toInteger()
        versionName flutterVersionName
        
        // Enable multidex for large apps
        multiDexEnabled true
    }

    signingConfigs {
        release {
            keyAlias keystoreProperties['keyAlias']
            keyPassword keystoreProperties['keyPassword']
            storeFile keystoreProperties['storeFile'] ? file(keystoreProperties['storeFile']) : null
            storePassword keystoreProperties['storePassword']
        }
    }

    buildTypes {
        release {
            signingConfig signingConfigs.release
            minifyEnabled true
            shrinkResources true
            proguardFiles getDefaultProguardFile('proguard-android-optimize.txt'), 'proguard-rules.pro'
            
            // Enable R8 full mode for better optimization
            ndk {
                debugSymbolLevel 'FULL'
            }
        }
    }

    // Split APKs by ABI for smaller downloads
    splits {
        abi {
            enable true
            reset()
            include "armeabi-v7a", "arm64-v8a", "x86_64"
            universalApk true
        }
    }

    // Generate version code per ABI
    applicationVariants.all { variant ->
        variant.outputs.each { output ->
            def abiVersionCode = project.ext.abiCodes.get(output.getFilter(com.android.build.OutputFile.ABI))
            if (abiVersionCode != null) {
                output.versionCodeOverride = abiVersionCode * 1000 + variant.versionCode
            }
        }
    }
}

ext.abiCodes = ['armeabi-v7a': 1, 'arm64-v8a': 2, 'x86_64': 3]

---

# android/key.properties (DO NOT COMMIT - add to .gitignore)
storePassword=your_keystore_password
keyPassword=your_key_password
keyAlias=your_key_alias
storeFile=/path/to/your/keystore.jks

---

# ============================================================
# iOS Release Configuration
# ============================================================
# ios/ExportOptions.plist
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<dict>
    <key>method</key>
    <string>app-store</string>
    <key>teamID</key>
    <string>YOUR_TEAM_ID</string>
    <key>signingStyle</key>
    <string>automatic</string>
    <key>uploadBitcode</key>
    <false/>
    <key>uploadSymbols</key>
    <true/>
    <key>compileBitcode</key>
    <false/>
    <key>destination</key>
    <string>upload</string>
</dict>
</plist>

---

# ============================================================
# Version Bumping Script
# ============================================================
#!/bin/bash
# scripts/bump_version.sh

set -e

BUMP_TYPE=${1:-patch}  # major, minor, or patch

# Read current version from pubspec.yaml
CURRENT_VERSION=$(grep '^version:' pubspec.yaml | sed 's/version: //' | cut -d'+' -f1)
CURRENT_BUILD=$(grep '^version:' pubspec.yaml | sed 's/version: //' | cut -d'+' -f2)

IFS='.' read -r MAJOR MINOR PATCH <<< "$CURRENT_VERSION"

case $BUMP_TYPE in
    major)
        NEW_VERSION="$((MAJOR + 1)).0.0"
        ;;
    minor)
        NEW_VERSION="$MAJOR.$((MINOR + 1)).0"
        ;;
    patch)
        NEW_VERSION="$MAJOR.$MINOR.$((PATCH + 1))"
        ;;
    *)
        echo "Invalid bump type. Use: major, minor, or patch"
        exit 1
        ;;
esac

NEW_BUILD=$((CURRENT_BUILD + 1))

echo "Bumping version: $CURRENT_VERSION+$CURRENT_BUILD -> $NEW_VERSION+$NEW_BUILD"

# Update pubspec.yaml
sed -i "s/^version: .*/version: $NEW_VERSION+$NEW_BUILD/" pubspec.yaml

# Update iOS Info.plist
/usr/libexec/PlistBuddy -c "Set :CFBundleShortVersionString $NEW_VERSION" ios/Runner/Info.plist
/usr/libexec/PlistBuddy -c "Set :CFBundleVersion $NEW_BUILD" ios/Runner/Info.plist

echo "Version updated successfully!"
echo "New version: $NEW_VERSION+$NEW_BUILD"

# Create git tag
read -p "Create git tag v$NEW_VERSION? (y/n) " -n 1 -r
echo
if [[ $REPLY =~ ^[Yy]$ ]]; then
    git add pubspec.yaml ios/Runner/Info.plist
    git commit -m "chore: bump version to $NEW_VERSION"
    git tag -a "v$NEW_VERSION" -m "Release version $NEW_VERSION"
    echo "Tag v$NEW_VERSION created"
fi

---

# ============================================================
# Release Build Script
# ============================================================
#!/bin/bash
# scripts/build_release.sh

set -e

PLATFORM=${1:-all}  # android, ios, or all
BUILD_NUMBER=${BUILD_NUMBER:-$(date +%Y%m%d%H%M)}

echo "Building release artifacts..."
echo "Platform: $PLATFORM"
echo "Build number: $BUILD_NUMBER"

# Clean previous builds
flutter clean
flutter pub get

# Generate code
flutter pub run build_runner build --delete-conflicting-outputs

if [[ "$PLATFORM" == "android" || "$PLATFORM" == "all" ]]; then
    echo "\n=== Building Android ==="
    
    # Build App Bundle for Play Store
    flutter build appbundle \
        --release \
        --build-number=$BUILD_NUMBER \
        --obfuscate \
        --split-debug-info=build/debug-info/android
    
    echo "App Bundle created: build/app/outputs/bundle/release/app-release.aab"
    
    # Also build APK for direct distribution
    flutter build apk \
        --release \
        --build-number=$BUILD_NUMBER \
        --split-per-abi \
        --obfuscate \
        --split-debug-info=build/debug-info/android-apk
    
    echo "APKs created in: build/app/outputs/flutter-apk/"
    
    # List artifact sizes
    echo "\nAndroid artifact sizes:"
    ls -lh build/app/outputs/bundle/release/
    ls -lh build/app/outputs/flutter-apk/*.apk
fi

if [[ "$PLATFORM" == "ios" || "$PLATFORM" == "all" ]]; then
    echo "\n=== Building iOS ==="
    
    # Build iOS archive
    flutter build ipa \
        --release \
        --build-number=$BUILD_NUMBER \
        --obfuscate \
        --split-debug-info=build/debug-info/ios \
        --export-options-plist=ios/ExportOptions.plist
    
    echo "IPA created: build/ios/ipa/*.ipa"
    
    # List artifact size
    echo "\niOS artifact sizes:"
    ls -lh build/ios/ipa/
fi

echo "\n=== Build Complete ==="
echo "Debug symbols saved to: build/debug-info/"
echo "Upload debug symbols to Crashlytics for symbolicated crash reports."

---

# ============================================================
# Release Notes Generator
# ============================================================
#!/bin/bash
# scripts/generate_release_notes.sh

set -e

VERSION=$1
PREVIOUS_TAG=$(git describe --tags --abbrev=0 HEAD^ 2>/dev/null || echo "")

if [ -z "$VERSION" ]; then
    VERSION=$(grep '^version:' pubspec.yaml | sed 's/version: //' | cut -d'+' -f1)
fi

echo "# Release Notes v$VERSION"
echo ""
echo "## What's New"
echo ""

if [ -n "$PREVIOUS_TAG" ]; then
    # Features
    FEATURES=$(git log $PREVIOUS_TAG..HEAD --pretty=format:"%s" | grep -E "^feat" | sed 's/feat[:(]/- /' | sed 's/):/:/g' || true)
    if [ -n "$FEATURES" ]; then
        echo "### Features"
        echo "$FEATURES"
        echo ""
    fi
    
    # Bug Fixes
    FIXES=$(git log $PREVIOUS_TAG..HEAD --pretty=format:"%s" | grep -E "^fix" | sed 's/fix[:(]/- /' | sed 's/):/:/g' || true)
    if [ -n "$FIXES" ]; then
        echo "### Bug Fixes"
        echo "$FIXES"
        echo ""
    fi
    
    # Performance
    PERF=$(git log $PREVIOUS_TAG..HEAD --pretty=format:"%s" | grep -E "^perf" | sed 's/perf[:(]/- /' | sed 's/):/:/g' || true)
    if [ -n "$PERF" ]; then
        echo "### Performance Improvements"
        echo "$PERF"
        echo ""
    fi
else
    echo "- Initial release"
    echo ""
fi

echo "## Installation"
echo ""
echo "Download from:"
echo "- [App Store](https://apps.apple.com/app/yourapp)"
echo "- [Google Play](https://play.google.com/store/apps/details?id=com.yourcompany.yourapp)"

---

# ============================================================
# Staged Rollout Configuration
# ============================================================
// lib/core/config/rollout_config.dart
import 'package:firebase_remote_config/firebase_remote_config.dart';

/// Manages staged rollout percentages using Firebase Remote Config
class RolloutConfig {
  final FirebaseRemoteConfig _remoteConfig;

  RolloutConfig(this._remoteConfig);

  /// Check if a feature is enabled for this user
  bool isFeatureEnabled(String featureKey) {
    final rolloutPercentage = _remoteConfig.getInt('${featureKey}_rollout');
    final userBucket = _getUserBucket();
    return userBucket < rolloutPercentage;
  }

  /// Get user's consistent bucket (0-99) based on user ID
  int _getUserBucket() {
    final userId = _getUserId();
    final hash = userId.hashCode.abs();
    return hash % 100;
  }

  String _getUserId() {
    // Return consistent user identifier
    return 'user_id_from_auth';
  }

  /// Check if app update is available and required
  Future<UpdateInfo> checkForUpdate() async {
    await _remoteConfig.fetchAndActivate();

    final minVersion = _remoteConfig.getString('min_supported_version');
    final latestVersion = _remoteConfig.getString('latest_version');
    final forceUpdate = _remoteConfig.getBool('force_update');

    final currentVersion = await _getCurrentVersion();

    return UpdateInfo(
      currentVersion: currentVersion,
      latestVersion: latestVersion,
      minSupportedVersion: minVersion,
      isUpdateAvailable: _isNewerVersion(latestVersion, currentVersion),
      isUpdateRequired: forceUpdate || _isNewerVersion(minVersion, currentVersion),
    );
  }

  bool _isNewerVersion(String v1, String v2) {
    final parts1 = v1.split('.').map(int.parse).toList();
    final parts2 = v2.split('.').map(int.parse).toList();

    for (var i = 0; i < 3; i++) {
      if (parts1[i] > parts2[i]) return true;
      if (parts1[i] < parts2[i]) return false;
    }
    return false;
  }

  Future<String> _getCurrentVersion() async {
    final info = await PackageInfo.fromPlatform();
    return info.version;
  }
}

class UpdateInfo {
  final String currentVersion;
  final String latestVersion;
  final String minSupportedVersion;
  final bool isUpdateAvailable;
  final bool isUpdateRequired;

  UpdateInfo({
    required this.currentVersion,
    required this.latestVersion,
    required this.minSupportedVersion,
    required this.isUpdateAvailable,
    required this.isUpdateRequired,
  });
}
```
