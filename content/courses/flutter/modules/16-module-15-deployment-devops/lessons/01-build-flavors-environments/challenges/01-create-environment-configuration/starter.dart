// lib/config/app_config.dart

enum Flavor { dev, staging, prod }

class AppConfig {
  // TODO: Add properties:
  // - flavor (Flavor)
  // - apiBaseUrl (String)
  // - appName (String)
  // - enableLogging (bool)
  // - enableAnalytics (bool)
  // - enableNewDashboard (bool) - feature flag
  
  // TODO: Create private constructor
  
  // TODO: Add singleton pattern with late static instance
  
  // TODO: Create initialize() method that reads from:
  // - String.fromEnvironment('ENVIRONMENT', defaultValue: 'dev')
  // - String.fromEnvironment('API_URL')
  // - bool.fromEnvironment('ENABLE_LOGGING')
  // - bool.fromEnvironment('ENABLE_ANALYTICS')
  // - bool.fromEnvironment('FEATURE_NEW_DASHBOARD')
  
  // TODO: Add convenience getters: isDev, isStaging, isProd
}

// Test your config:
void main() {
  AppConfig.initialize();
  print('Flavor: ${AppConfig.instance.flavor}');
  print('API: ${AppConfig.instance.apiBaseUrl}');
}