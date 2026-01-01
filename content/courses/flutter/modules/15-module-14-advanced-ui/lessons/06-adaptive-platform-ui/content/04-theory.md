---
type: "THEORY"
title: "Cupertino Widgets - iOS-Native Look and Feel"
---


**Flutter includes a complete Cupertino (iOS-style) widget library:**

| Material Widget | Cupertino Equivalent | iOS Behavior |
|-----------------|---------------------|---------------|
| `MaterialApp` | `CupertinoApp` | iOS navigation, themes |
| `Scaffold` | `CupertinoPageScaffold` | iOS page structure |
| `AppBar` | `CupertinoNavigationBar` | iOS nav bar with large titles |
| `ElevatedButton` | `CupertinoButton` | iOS button with highlight |
| `TextField` | `CupertinoTextField` | iOS text input style |
| `Switch` | `CupertinoSwitch` | iOS toggle switch |
| `AlertDialog` | `CupertinoAlertDialog` | iOS alert style |
| `CircularProgressIndicator` | `CupertinoActivityIndicator` | iOS spinner |
| `Slider` | `CupertinoSlider` | iOS slider |
| `BottomNavigationBar` | `CupertinoTabBar` | iOS tab bar |

**Key Differences:**

- **Navigation:** iOS uses sliding transitions, Android uses fade/scale
- **Buttons:** iOS buttons have no elevation, just color changes on press
- **Typography:** iOS uses San Francisco font, Android uses Roboto
- **Colors:** iOS uses system blue (#007AFF), Android uses Material purple

**Import:**
```dart
import 'package:flutter/cupertino.dart';
```

