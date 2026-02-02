---
type: "THEORY"
title: "Test File Organization"
---


**Project Structure:**
```
lib/
  features/
    auth/
      login_screen.dart
      auth_notifier.dart
test/
  features/
    auth/
      login_screen_test.dart
      auth_notifier_test.dart
integration_test/
  app_test.dart
```

**Naming Convention:**
- Mirror `lib/` structure in `test/`
- Suffix with `_test.dart`
- Integration tests go in `integration_test/`

