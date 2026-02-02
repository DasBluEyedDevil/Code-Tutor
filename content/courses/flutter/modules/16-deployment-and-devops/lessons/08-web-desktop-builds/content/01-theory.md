---
type: "THEORY"
title: "Introduction"
---


**Flutter's Multi-Platform Vision**

Flutter enables true cross-platform development from a single codebase. Beyond mobile, Flutter supports web browsers, Windows, macOS, and Linux desktops, allowing you to reach users on virtually any platform.

**Supported Platforms:**

| Platform | Status | Use Cases |
|----------|--------|----------|
| Web | Stable | Progressive web apps, dashboards, internal tools |
| Windows | Stable | Enterprise apps, utilities, creative tools |
| macOS | Stable | Native Mac apps, developer tools, productivity |
| Linux | Stable | System utilities, specialized applications |

**Build Targets Overview:**

```
flutter build web      # Web deployment
flutter build windows  # Windows executable
flutter build macos    # macOS application
flutter build linux    # Linux binary
```

**Platform Support Check:**

```bash
# Enable desktop support (one-time setup)
flutter config --enable-windows-desktop
flutter config --enable-macos-desktop
flutter config --enable-linux-desktop

# Verify platform support
flutter doctor
flutter devices
```

**Key Considerations:**

- **Screen sizes**: Desktop and web have larger, resizable windows
- **Input methods**: Mouse, keyboard, touch, and trackpad support
- **Platform conventions**: Native menus, window controls, keyboard shortcuts
- **Distribution**: Different packaging and signing requirements per platform

