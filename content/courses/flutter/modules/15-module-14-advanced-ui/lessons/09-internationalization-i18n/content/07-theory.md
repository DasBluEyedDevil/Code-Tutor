---
type: "THEORY"
title: "Using Translations - AppLocalizations"
---


**Accessing Localized Strings**

After running `flutter gen-l10n`, Flutter generates `AppLocalizations`:

```dart
// Access in any widget
final l10n = AppLocalizations.of(context);

// Use generated methods
Text(l10n.appTitle);
Text(l10n.welcomeUser('John'));
Text(l10n.taskCount(5));
```

**Generated Code Structure:**

```dart
// .dart_tool/flutter_gen/gen_l10n/app_localizations.dart
abstract class AppLocalizations {
  static AppLocalizations of(BuildContext context) { ... }
  
  String get appTitle;  // Simple string
  String welcomeUser(String userName);  // With placeholder
  String taskCount(int count);  // With pluralization
}
```

**Extension for Cleaner Access:**

```dart
// lib/extensions/l10n_extension.dart
import 'package:flutter/widgets.dart';
import 'package:flutter_gen/gen_l10n/app_localizations.dart';

extension L10nExtension on BuildContext {
  AppLocalizations get l10n => AppLocalizations.of(this);
}

// Usage: much cleaner!
Text(context.l10n.appTitle);
```

**Regenerating Localizations:**

```bash
# Regenerate after changing ARB files
flutter gen-l10n

# Or it runs automatically with flutter run/build
```

