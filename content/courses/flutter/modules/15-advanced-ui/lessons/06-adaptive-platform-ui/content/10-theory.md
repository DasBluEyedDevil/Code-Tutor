---
type: "THEORY"
title: "flutter_platform_widgets Package - Automatic Adaptation"
---


**Writing adaptive widgets manually is tedious.** The `flutter_platform_widgets` package provides ready-made adaptive widgets.

**Installation:**
```yaml
dependencies:
  flutter_platform_widgets: ^6.0.0
```

**Key Widgets:**

| Platform Widget | Material | Cupertino |
|-----------------|----------|----------|
| `PlatformApp` | MaterialApp | CupertinoApp |
| `PlatformScaffold` | Scaffold | CupertinoPageScaffold |
| `PlatformAppBar` | AppBar | CupertinoNavigationBar |
| `PlatformButton` | ElevatedButton | CupertinoButton |
| `PlatformSwitch` | Switch | CupertinoSwitch |
| `PlatformTextField` | TextField | CupertinoTextField |
| `PlatformAlertDialog` | AlertDialog | CupertinoAlertDialog |
| `PlatformCircularProgressIndicator` | CircularProgressIndicator | CupertinoActivityIndicator |

**Platform-Specific Customization:**

```dart
PlatformButton(
  onPressed: () {},
  child: Text('Click me'),
  material: (_, __) => MaterialElevatedButtonData(
    icon: Icon(Icons.add),
  ),
  cupertino: (_, __) => CupertinoButtonData(
    padding: EdgeInsets.all(16),
  ),
)
```

**Benefits:**
- Pre-built adaptive widgets
- Customizable per-platform
- Active maintenance
- Handles edge cases

