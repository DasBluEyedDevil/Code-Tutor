---
type: "EXAMPLE"
title: "Platform-Specific Version Configuration"
---


**iOS Version Configuration**

For iOS, versions are also set in Info.plist (Flutter handles this automatically):



```dart
// iOS/Runner/Info.plist is auto-generated from pubspec.yaml
// But you can override if needed:

// For iOS-specific version handling, check in native code:
// iOS/Runner/AppDelegate.swift
import UIKit
import Flutter

@UIApplicationMain
@objc class AppDelegate: FlutterAppDelegate {
  override func application(
    _ application: UIApplication,
    didFinishLaunchingWithOptions launchOptions: [UIApplication.LaunchOptionsKey: Any]?
  ) -> Bool {
    // Access version info
    let version = Bundle.main.infoDictionary?["CFBundleShortVersionString"] as? String ?? "1.0.0"
    let build = Bundle.main.infoDictionary?["CFBundleVersion"] as? String ?? "1"
    print("iOS App Version: \(version) (\(build))")
    
    GeneratedPluginRegistrant.register(with: self)
    return super.application(application, didFinishLaunchingWithOptions: launchOptions)
  }
}

// Android Version Configuration
// android/app/build.gradle is auto-configured from pubspec.yaml
// The following shows what Flutter generates:

// android/app/build.gradle
// android {
//     defaultConfig {
//         applicationId "com.example.myapp"
//         minSdkVersion 21
//         targetSdkVersion 34
//         // These are set from pubspec.yaml:
//         versionCode flutterVersionCode.toInteger()
//         versionName flutterVersionName
//     }
// }

// For Android-specific version handling in native code:
// android/app/src/main/kotlin/.../MainActivity.kt
// package com.example.myapp
// 
// import io.flutter.embedding.android.FlutterActivity
// import android.os.Bundle
// 
// class MainActivity: FlutterActivity() {
//     override fun onCreate(savedInstanceState: Bundle?) {
//         super.onCreate(savedInstanceState)
//         
//         val versionName = packageManager
//             .getPackageInfo(packageName, 0).versionName
//         val versionCode = packageManager
//             .getPackageInfo(packageName, 0).longVersionCode
//         println("Android App Version: $versionName ($versionCode)")
//     }
// }

// Dart code for platform-aware version display:
import 'dart:io';
import 'package:package_info_plus/package_info_plus.dart';

class PlatformVersionInfo {
  final PackageInfo _packageInfo;
  
  PlatformVersionInfo._(this._packageInfo);
  
  static Future<PlatformVersionInfo> load() async {
    final info = await PackageInfo.fromPlatform();
    return PlatformVersionInfo._(info);
  }
  
  String get version => _packageInfo.version;
  String get buildNumber => _packageInfo.buildNumber;
  
  /// Get platform-specific display string
  String get displayString {
    if (Platform.isIOS) {
      return 'Version $version (Build $buildNumber)';
    } else if (Platform.isAndroid) {
      return 'v$version ($buildNumber)';
    }
    return '$version+$buildNumber';
  }
  
  /// Get store-specific version format
  String get storeFormat {
    if (Platform.isIOS) {
      // App Store format
      return 'Version $version';
    } else if (Platform.isAndroid) {
      // Play Store format
      return '$version';
    }
    return version;
  }
}
```
