---
type: "EXAMPLE"
title: "Using --dart-define"
---


The simplest way to pass environment values at build time:

**Running with dart-define:**
```bash
# Development
flutter run --dart-define=ENVIRONMENT=dev --dart-define=API_URL=http://localhost:3000

# Staging  
flutter run --dart-define=ENVIRONMENT=staging --dart-define=API_URL=https://staging-api.myapp.com

# Production
flutter run --dart-define=ENVIRONMENT=prod --dart-define=API_URL=https://api.myapp.com
```

**Accessing in Dart:**



```dart
// lib/config/environment.dart

class Environment {
  // Read compile-time constants
  static const String name = String.fromEnvironment(
    'ENVIRONMENT',
    defaultValue: 'dev',
  );
  
  static const String apiUrl = String.fromEnvironment(
    'API_URL',
    defaultValue: 'http://localhost:3000',
  );
  
  static const bool enableLogging = bool.fromEnvironment(
    'ENABLE_LOGGING',
    defaultValue: true,
  );
  
  // Computed properties
  static bool get isDev => name == 'dev';
  static bool get isStaging => name == 'staging';
  static bool get isProd => name == 'prod';
}

// Usage anywhere in your app:
void main() {
  print('Running in ${Environment.name} mode');
  print('API URL: ${Environment.apiUrl}');
  
  if (Environment.isDev) {
    // Enable dev-only features
  }
}
```
