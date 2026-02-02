// app/lib/core/config/app_config.dart

/// Application configuration based on environment
class AppConfig {
  AppConfig._();
  
  /// Current environment (development, staging, production)
  static const String environment = String.fromEnvironment(
    'ENVIRONMENT',
    defaultValue: 'development',
  );
  
  /// Whether we're running in production mode
  static bool get isProduction => environment == 'production';
  
  /// Whether we're running in development mode
  static bool get isDevelopment => environment == 'development';
  
  /// Server URL based on environment
  static String get serverUrl {
    switch (environment) {
      case 'production':
        return 'https://api.socialchat.app/';
      case 'staging':
        return 'https://staging-api.socialchat.app/';
      case 'development':
      default:
        return 'http://localhost:8080/';
    }
  }
  
  /// WebSocket URL for real-time features
  static String get websocketUrl {
    final baseUrl = serverUrl.replaceFirst('http', 'ws');
    return '${baseUrl}websocket';
  }
  
  /// App version string
  static const String appVersion = '1.0.0';
  
  /// Build number (set during CI/CD)
  static const String buildNumber = String.fromEnvironment(
    'BUILD_NUMBER',
    defaultValue: '1',
  );
  
  /// Full version string including build number
  static String get fullVersion => '$appVersion+$buildNumber';
  
  /// Print configuration for debugging
  static void printDebugInfo() {
    print('=== App Configuration ===');
    print('Environment: $environment');
    print('Is Production: $isProduction');
    print('Server URL: $serverUrl');
    print('WebSocket URL: $websocketUrl');
    print('Version: $fullVersion');
    print('========================');
  }
}

void main() {
  AppConfig.printDebugInfo();
  
  // Verify the configuration
  assert(AppConfig.environment == 'development');
  assert(AppConfig.serverUrl == 'http://localhost:8080/');
  assert(!AppConfig.isProduction);
  print('\nAll assertions passed!');
}