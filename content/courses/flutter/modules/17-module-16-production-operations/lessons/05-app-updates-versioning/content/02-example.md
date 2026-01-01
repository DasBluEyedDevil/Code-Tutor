---
type: "EXAMPLE"
title: "Version Management in pubspec.yaml"
---


**Basic Version Configuration**

The version field in pubspec.yaml controls both version name and build number:



```dart
# pubspec.yaml
name: my_flutter_app
description: A sample Flutter application
version: 2.4.1+45

# The version format is: VERSION_NAME+BUILD_NUMBER
# - VERSION_NAME (2.4.1): Displayed to users, follows SemVer
# - BUILD_NUMBER (45): Used by app stores, must always increment

# Platform mapping:
# iOS:
#   - CFBundleShortVersionString = 2.4.1 (version name)
#   - CFBundleVersion = 45 (build number)
# Android:
#   - versionName = "2.4.1"
#   - versionCode = 45

# Reading version at runtime:
# lib/utils/version_info.dart
import 'package:package_info_plus/package_info_plus.dart';

class VersionInfo {
  static PackageInfo? _packageInfo;
  
  /// Initialize version info (call in main)
  static Future<void> initialize() async {
    _packageInfo = await PackageInfo.fromPlatform();
  }
  
  /// Get version name (e.g., "2.4.1")
  static String get versionName => _packageInfo?.version ?? 'unknown';
  
  /// Get build number (e.g., "45")
  static String get buildNumber => _packageInfo?.buildNumber ?? '0';
  
  /// Get full version string (e.g., "2.4.1 (45)")
  static String get fullVersion => '$versionName ($buildNumber)';
  
  /// Get app name
  static String get appName => _packageInfo?.appName ?? 'App';
  
  /// Get package name (bundle ID / application ID)
  static String get packageName => _packageInfo?.packageName ?? '';
  
  /// Parse version into components
  static List<int> get versionComponents {
    return versionName
        .split('.')
        .map((s) => int.tryParse(s) ?? 0)
        .toList();
  }
  
  static int get majorVersion => versionComponents.isNotEmpty 
      ? versionComponents[0] : 0;
  static int get minorVersion => versionComponents.length > 1 
      ? versionComponents[1] : 0;
  static int get patchVersion => versionComponents.length > 2 
      ? versionComponents[2] : 0;
}

// Usage in main.dart:
void main() async {
  WidgetsFlutterBinding.ensureInitialized();
  await VersionInfo.initialize();
  
  print('App: ${VersionInfo.appName}');
  print('Version: ${VersionInfo.fullVersion}');
  print('Major: ${VersionInfo.majorVersion}');
  
  runApp(const MyApp());
}
```
