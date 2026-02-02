import 'package:firebase_remote_config/firebase_remote_config.dart';

class FeatureFlagService {
  // Singleton pattern
  static final FeatureFlagService _instance = FeatureFlagService._internal();
  factory FeatureFlagService() => _instance;
  FeatureFlagService._internal();
  
  final FirebaseRemoteConfig _remoteConfig = FirebaseRemoteConfig.instance;
  
  // Default values for all flags
  static const Map<String, dynamic> _defaults = {
    'feature_new_checkout': false,
    'feature_dark_mode': true,
    'max_cart_items': 50,
  };
  
  /// Initialize the service with settings and defaults
  Future<void> initialize() async {
    // Set config settings
    await _remoteConfig.setConfigSettings(RemoteConfigSettings(
      fetchTimeout: const Duration(minutes: 1),
      minimumFetchInterval: const Duration(hours: 1),
    ));
    
    // Set defaults
    await _remoteConfig.setDefaults(_defaults);
    
    // Fetch and activate with error handling
    try {
      await _remoteConfig.fetchAndActivate();
    } catch (e) {
      print('Feature flag fetch failed: $e');
      // Continue with defaults
    }
  }
  
  // Typed flag getters
  bool get isNewCheckoutEnabled => _remoteConfig.getBool('feature_new_checkout');
  
  bool get isDarkModeEnabled => _remoteConfig.getBool('feature_dark_mode');
  
  int get maxCartItems => _remoteConfig.getInt('max_cart_items');
  
  // Generic accessors
  bool getBool(String key) => _remoteConfig.getBool(key);
  
  String getString(String key) => _remoteConfig.getString(key);
  
  int getInt(String key) => _remoteConfig.getInt(key);
  
  /// Refresh flags from server
  Future<bool> refresh() async {
    try {
      final updated = await _remoteConfig.fetchAndActivate();
      return updated;
    } catch (e) {
      print('Feature flag refresh failed: $e');
      return false;
    }
  }
}

void main() {
  print('FeatureFlagService created');
  final service = FeatureFlagService();
  print('Singleton works: ${identical(service, FeatureFlagService())}');
}