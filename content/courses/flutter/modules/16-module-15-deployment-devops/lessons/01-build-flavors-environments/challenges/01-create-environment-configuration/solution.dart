// lib/config/app_config.dart

enum Flavor { dev, staging, prod }

class AppConfig {
  final Flavor flavor;
  final String apiBaseUrl;
  final String appName;
  final bool enableLogging;
  final bool enableAnalytics;
  final bool enableNewDashboard;
  
  const AppConfig._internal({
    required this.flavor,
    required this.apiBaseUrl,
    required this.appName,
    required this.enableLogging,
    required this.enableAnalytics,
    required this.enableNewDashboard,
  });
  
  static late AppConfig _instance;
  static AppConfig get instance => _instance;
  
  static void initialize() {
    const envName = String.fromEnvironment('ENVIRONMENT', defaultValue: 'dev');
    const apiUrl = String.fromEnvironment('API_URL');
    const logging = bool.fromEnvironment('ENABLE_LOGGING', defaultValue: true);
    const analytics = bool.fromEnvironment('ENABLE_ANALYTICS', defaultValue: false);
    const newDashboard = bool.fromEnvironment('FEATURE_NEW_DASHBOARD', defaultValue: false);
    
    switch (envName) {
      case 'prod':
        _instance = AppConfig._internal(
          flavor: Flavor.prod,
          apiBaseUrl: apiUrl.isNotEmpty ? apiUrl : 'https://api.myapp.com',
          appName: 'MyApp',
          enableLogging: logging,
          enableAnalytics: analytics,
          enableNewDashboard: newDashboard,
        );
        break;
      case 'staging':
        _instance = AppConfig._internal(
          flavor: Flavor.staging,
          apiBaseUrl: apiUrl.isNotEmpty ? apiUrl : 'https://staging-api.myapp.com',
          appName: 'MyApp Staging',
          enableLogging: logging,
          enableAnalytics: analytics,
          enableNewDashboard: newDashboard,
        );
        break;
      case 'dev':
      default:
        _instance = AppConfig._internal(
          flavor: Flavor.dev,
          apiBaseUrl: apiUrl.isNotEmpty ? apiUrl : 'http://localhost:3000',
          appName: 'MyApp Dev',
          enableLogging: logging,
          enableAnalytics: analytics,
          enableNewDashboard: true, // Always on in dev
        );
    }
  }
  
  bool get isDev => flavor == Flavor.dev;
  bool get isStaging => flavor == Flavor.staging;
  bool get isProd => flavor == Flavor.prod;
}

void main() {
  AppConfig.initialize();
  print('Flavor: ${AppConfig.instance.flavor}');
  print('API: ${AppConfig.instance.apiBaseUrl}');
  print('Logging: ${AppConfig.instance.enableLogging}');
  print('Analytics: ${AppConfig.instance.enableAnalytics}');
  print('New Dashboard: ${AppConfig.instance.enableNewDashboard}');
}