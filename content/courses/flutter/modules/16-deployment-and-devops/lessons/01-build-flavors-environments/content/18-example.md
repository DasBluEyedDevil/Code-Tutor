---
type: "EXAMPLE"
title: "IDE Configuration - Android Studio"
---


Set up Run Configurations in Android Studio:

**Steps:**
1. Run -> Edit Configurations
2. Click "+" -> Flutter
3. Create configurations for each flavor:



```dart
// Configuration: Dev
// Name: Dev
// Dart entrypoint: lib/main.dart
// Additional run args: --flavor dev --dart-define-from-file=config/dev.json

// Configuration: Staging  
// Name: Staging
// Dart entrypoint: lib/main.dart
// Additional run args: --flavor staging --dart-define-from-file=config/staging.json

// Configuration: Prod
// Name: Prod
// Dart entrypoint: lib/main.dart
// Additional run args: --flavor prod --dart-define-from-file=config/prod.json
```
