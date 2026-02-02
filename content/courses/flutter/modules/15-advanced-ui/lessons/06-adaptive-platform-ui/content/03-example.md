---
type: "EXAMPLE"
title: "Platform Detection Utility"
---


Create a reusable platform detection helper:



```dart
import 'package:flutter/foundation.dart' show kIsWeb, TargetPlatform;
import 'package:flutter/material.dart';

// Platform detection that works everywhere
class PlatformHelper {
  // Check if running on web
  static bool get isWeb => kIsWeb;
  
  // Check platform using context (recommended for widgets)
  static bool isIOS(BuildContext context) {
    return Theme.of(context).platform == TargetPlatform.iOS;
  }
  
  static bool isAndroid(BuildContext context) {
    return Theme.of(context).platform == TargetPlatform.android;
  }
  
  static bool isMacOS(BuildContext context) {
    return Theme.of(context).platform == TargetPlatform.macOS;
  }
  
  static bool isWindows(BuildContext context) {
    return Theme.of(context).platform == TargetPlatform.windows;
  }
  
  static bool isLinux(BuildContext context) {
    return Theme.of(context).platform == TargetPlatform.linux;
  }
  
  // Check if Apple platform (iOS or macOS)
  static bool isApple(BuildContext context) {
    final platform = Theme.of(context).platform;
    return platform == TargetPlatform.iOS || 
           platform == TargetPlatform.macOS;
  }
  
  // Check if desktop platform
  static bool isDesktop(BuildContext context) {
    if (kIsWeb) return false;
    final platform = Theme.of(context).platform;
    return platform == TargetPlatform.macOS ||
           platform == TargetPlatform.windows ||
           platform == TargetPlatform.linux;
  }
  
  // Check if mobile platform
  static bool isMobile(BuildContext context) {
    if (kIsWeb) return false;
    final platform = Theme.of(context).platform;
    return platform == TargetPlatform.iOS ||
           platform == TargetPlatform.android;
  }
}

// Usage example
class PlatformAwareWidget extends StatelessWidget {
  const PlatformAwareWidget({super.key});

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        Text('Running on web: ${PlatformHelper.isWeb}'),
        Text('Is iOS: ${PlatformHelper.isIOS(context)}'),
        Text('Is Android: ${PlatformHelper.isAndroid(context)}'),
        Text('Is Desktop: ${PlatformHelper.isDesktop(context)}'),
        Text('Is Mobile: ${PlatformHelper.isMobile(context)}'),
      ],
    );
  }
}
```
