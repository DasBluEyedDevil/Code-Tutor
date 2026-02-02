---
type: "EXAMPLE"
title: "Creating a Feature Flag Service"
---


Wrap Remote Config in a service for cleaner code and easier testing:



```dart
// lib/services/feature_flag_service.dart
import 'package:firebase_remote_config/firebase_remote_config.dart';

/// Centralized service for feature flag management
class FeatureFlagService {
  final FirebaseRemoteConfig _remoteConfig;
  
  // Singleton pattern
  static final FeatureFlagService _instance = FeatureFlagService._internal();
  factory FeatureFlagService() => _instance;
  FeatureFlagService._internal()
      : _remoteConfig = FirebaseRemoteConfig.instance;
  
  // For testing - allow injecting a mock
  FeatureFlagService.withConfig(this._remoteConfig);
  
  /// Initialize with default values
  Future<void> initialize() async {
    await _remoteConfig.setConfigSettings(RemoteConfigSettings(
      fetchTimeout: const Duration(minutes: 1),
      minimumFetchInterval: const Duration(hours: 1),
    ));
    
    await _remoteConfig.setDefaults(_defaultValues);
    
    try {
      await _remoteConfig.fetchAndActivate();
    } catch (e) {
      // Log error but continue with defaults
      print('Feature flag fetch failed: $e');
    }
  }
  
  /// Default values for all feature flags
  static const Map<String, dynamic> _defaultValues = {
    // Feature rollout flags
    'feature_new_checkout': false,
    'feature_dark_mode': true,
    'feature_social_sharing': false,
    'feature_push_notifications': true,
    
    // A/B test variants
    'experiment_onboarding_variant': 'control',
    'experiment_pricing_page': 'original',
    
    // Operational flags
    'maintenance_mode': false,
    'force_update_required': false,
    'min_supported_version': '1.0.0',
    
    // Configuration values
    'max_cart_items': 50,
    'api_timeout_seconds': 30,
    'cache_duration_hours': 24,
  };
  
  // ============ Feature Flags ============
  
  bool get isNewCheckoutEnabled => 
      _remoteConfig.getBool('feature_new_checkout');
  
  bool get isDarkModeEnabled => 
      _remoteConfig.getBool('feature_dark_mode');
  
  bool get isSocialSharingEnabled => 
      _remoteConfig.getBool('feature_social_sharing');
  
  bool get isPushNotificationsEnabled => 
      _remoteConfig.getBool('feature_push_notifications');
  
  // ============ A/B Tests ============
  
  String get onboardingVariant => 
      _remoteConfig.getString('experiment_onboarding_variant');
  
  String get pricingPageVariant => 
      _remoteConfig.getString('experiment_pricing_page');
  
  // ============ Operational Flags ============
  
  bool get isMaintenanceMode => 
      _remoteConfig.getBool('maintenance_mode');
  
  bool get isForceUpdateRequired => 
      _remoteConfig.getBool('force_update_required');
  
  String get minSupportedVersion => 
      _remoteConfig.getString('min_supported_version');
  
  // ============ Configuration ============
  
  int get maxCartItems => 
      _remoteConfig.getInt('max_cart_items');
  
  int get apiTimeoutSeconds => 
      _remoteConfig.getInt('api_timeout_seconds');
  
  int get cacheDurationHours => 
      _remoteConfig.getInt('cache_duration_hours');
  
  // ============ Generic Access ============
  
  /// Get any boolean flag by key
  bool getBool(String key) => _remoteConfig.getBool(key);
  
  /// Get any string value by key
  String getString(String key) => _remoteConfig.getString(key);
  
  /// Get any integer value by key
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
  
  /// Get last fetch status
  RemoteConfigFetchStatus get lastFetchStatus => 
      _remoteConfig.lastFetchStatus;
  
  /// Get last fetch time
  DateTime get lastFetchTime => _remoteConfig.lastFetchTime;
}
```
