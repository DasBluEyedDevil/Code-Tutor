---
type: "KEY_POINT"
title: "Cross-Platform Considerations"
---


**Platform Detection:**

Flutter provides multiple ways to detect and adapt to different platforms:

```dart
import 'dart:io' show Platform;
import 'package:flutter/foundation.dart' show kIsWeb;

// Check if running on web
if (kIsWeb) {
  // Web-specific code
}

// Check specific platforms (not available on web)
if (!kIsWeb) {
  if (Platform.isWindows) { /* Windows */ }
  if (Platform.isMacOS) { /* macOS */ }
  if (Platform.isLinux) { /* Linux */ }
  if (Platform.isAndroid) { /* Android */ }
  if (Platform.isIOS) { /* iOS */ }
}
```

**Conditional Imports:**

Use conditional imports for platform-specific implementations:

```dart
// file_picker_stub.dart (fallback)
FilePicker createFilePicker() => throw UnsupportedError('No implementation');

// file_picker_mobile.dart
import 'package:file_picker/file_picker.dart';
FilePicker createFilePicker() => FilePicker.platform;

// file_picker_web.dart
import 'package:file_picker/file_picker.dart';
FilePicker createFilePicker() => FilePicker.platform;

// main import
import 'file_picker_stub.dart'
    if (dart.library.io) 'file_picker_mobile.dart'
    if (dart.library.html) 'file_picker_web.dart';
```

**Platform-Specific Packages:**

| Capability | Mobile | Web | Desktop |
|------------|--------|-----|----------|
| File picker | file_picker | file_picker | file_picker |
| Local storage | shared_preferences | shared_preferences | shared_preferences |
| URL launcher | url_launcher | url_launcher | url_launcher |
| Path provider | path_provider | N/A (web storage) | path_provider |
| Window management | N/A | N/A | window_manager |
| System tray | N/A | N/A | system_tray |
| Native menus | N/A | N/A | menubar |

**Responsive Design Best Practices:**

- Use `LayoutBuilder` and `MediaQuery` for responsive layouts
- Implement mouse hover states for desktop
- Add keyboard shortcuts for desktop users
- Consider scroll wheel behavior differences
- Test window resizing on desktop
- Handle portrait/landscape on all platforms

**Web-Specific Considerations:**

- Initial load time (bundle size matters)
- SEO limitations with client-side rendering
- Browser back button behavior
- URL routing and deep links
- PWA capabilities (service workers, offline)

**Desktop-Specific Considerations:**

- Window minimum/maximum sizes
- Multi-window support
- Native file dialogs
- System tray icons
- Global keyboard shortcuts
- Auto-update mechanisms

