---
type: "THEORY"
title: "What Are Integration Tests?"
---


Integration tests run your complete app on a real device or emulator, testing full user flows.

**Differences from Widget Tests:**
- Run on actual device/emulator (not headless)
- Test real navigation, animations, gestures
- Can interact with platform services
- Much slower but highest confidence

**Setup:**
```yaml
# pubspec.yaml
dev_dependencies:
  integration_test:
    sdk: flutter
```

**File Structure:**
```
integration_test/
  app_test.dart
  robots/
    login_robot.dart
    checkout_robot.dart
```

