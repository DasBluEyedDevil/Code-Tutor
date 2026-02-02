---
type: "EXAMPLE"
title: "Environment Config Class"
---


Create a comprehensive config class that combines dart-define with flavor detection:



```dart
// lib/config/app_config.dart

enum Flavor { dev, staging, prod }

class AppConfig {
  final Flavor flavor;
  final String apiBaseUrl;
  final String appName;
  final bool enableLogging;
  final bool enableAnalytics;
  final String? sentryDsn;
  final bool showDebugBanner;
  
  const AppConfig._internal({
    required this.flavor,
    required this.apiBaseUrl,
    required this.appName,
    required this.enableLogging,
    required this.enableAnalytics,
    this.sentryDsn,
    required this.showDebugBanner,
  });
  
  // Singleton instance
  static late AppConfig _instance;
  static AppConfig get instance => _instance;
  
  // Initialize based on environment
  static void initialize() {
    const envName = String.fromEnvironment('ENVIRONMENT', defaultValue: 'dev');
    
    switch (envName) {
      case 'prod':
        _instance = AppConfig._internal(
          flavor: Flavor.prod,
          apiBaseUrl: 'https://api.myapp.com',
          appName: 'MyApp',
          enableLogging: false,
          enableAnalytics: true,
          sentryDsn: const String.fromEnvironment('SENTRY_DSN'),
          showDebugBanner: false,
        );
        break;
      case 'staging':
        _instance = AppConfig._internal(
          flavor: Flavor.staging,
          apiBaseUrl: 'https://staging-api.myapp.com',
          appName: 'MyApp Staging',
          enableLogging: true,
          enableAnalytics: true,
          sentryDsn: const String.fromEnvironment('SENTRY_DSN'),
          showDebugBanner: true,
        );
        break;
      case 'dev':
      default:
        _instance = AppConfig._internal(
          flavor: Flavor.dev,
          apiBaseUrl: 'http://localhost:3000',
          appName: 'MyApp Dev',
          enableLogging: true,
          enableAnalytics: false,
          sentryDsn: null,
          showDebugBanner: true,
        );
    }
  }
  
  // Convenience getters
  bool get isDev => flavor == Flavor.dev;
  bool get isStaging => flavor == Flavor.staging;
  bool get isProd => flavor == Flavor.prod;
}
```
