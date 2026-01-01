// app/lib/core/config/app_config.dart

/// Application configuration based on environment
class AppConfig {
  AppConfig._();
  
  // TODO: Read environment from String.fromEnvironment
  // Default to 'development' if not set
  static const String environment = 'development';
  
  // TODO: Return true if environment is 'production'
  static bool get isProduction => throw UnimplementedError();
  
  // TODO: Return correct server URL based on environment
  // Development: 'http://localhost:8080/'
  // Production: 'https://api.socialchat.app/'
  static String get serverUrl => throw UnimplementedError();
  
  // TODO: App version
  static const String appVersion = '1.0.0';
  
  // TODO: Add a debug info method that prints all config values
  static void printDebugInfo() {
    // Print environment, serverUrl, isProduction, appVersion
  }
}

// Test your configuration
void main() {
  AppConfig.printDebugInfo();
}