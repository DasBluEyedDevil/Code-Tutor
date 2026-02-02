---
type: "THEORY"
title: "Platform Detection - Knowing Where You're Running"
---


**Flutter provides several ways to detect the current platform:**

**1. dart:io Platform Class (Mobile/Desktop)**

```dart
import 'dart:io' show Platform;

if (Platform.isIOS) {
  // iOS-specific code
} else if (Platform.isAndroid) {
  // Android-specific code
} else if (Platform.isMacOS) {
  // macOS-specific code
}
```

**Available properties:**
- `Platform.isAndroid`
- `Platform.isIOS`
- `Platform.isMacOS`
- `Platform.isWindows`
- `Platform.isLinux`
- `Platform.isFuchsia`

**2. kIsWeb Constant (Web Detection)**

```dart
import 'package:flutter/foundation.dart' show kIsWeb;

if (kIsWeb) {
  // Web-specific code (dart:io not available)
}
```

**3. Theme.of(context).platform (Widget Context)**

```dart
final platform = Theme.of(context).platform;
if (platform == TargetPlatform.iOS) {
  // iOS styling from theme
}
```

**Why Use Theme.of?**

- Works in tests (can mock platform)
- Respects `ThemeData(platform: ...)` overrides
- Preferred for widget-level decisions

**Important:** `dart:io` doesn't exist on web, so always check `kIsWeb` first!

