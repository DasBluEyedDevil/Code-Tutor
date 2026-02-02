---
type: "THEORY"
title: "Setting Up Workmanager"
---


### Installation

**pubspec.yaml:**


### Android Configuration

**android/app/src/main/AndroidManifest.xml:**

### iOS Configuration

**ios/Runner/Info.plist:**

**ios/Runner/AppDelegate.swift:**



```swift
import UIKit
import Flutter
import workmanager

@UIApplicationMain
@objc class AppDelegate: FlutterAppDelegate {
  override func application(
    _ application: UIApplication,
    didFinishLaunchingWithOptions launchOptions: [UIApplication.LaunchOptionsKey: Any]?
  ) -> Bool {
    GeneratedPluginRegistrant.register(with: self)

    WorkmanagerPlugin.setPluginRegistrantCallback { registry in
        GeneratedPluginRegistrant.register(with: registry)
    }

    return super.application(application, didFinishLaunchingWithOptions: launchOptions)
  }
}
```
