---
type: "EXAMPLE"
title: "Lifecycle-Aware Feature Flag Management"
---


Implement proper lifecycle handling for feature flags:



```dart
// lib/services/feature_flag_manager.dart
import 'dart:async';
import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:firebase_remote_config/firebase_remote_config.dart';

/// Manages feature flag lifecycle and real-time updates
class FeatureFlagManager with WidgetsBindingObserver {
  final FirebaseRemoteConfig _remoteConfig;
  final List<VoidCallback> _listeners = [];
  StreamSubscription? _realTimeSubscription;
  bool _isInitialized = false;
  
  // Singleton
  static final FeatureFlagManager _instance = FeatureFlagManager._internal();
  factory FeatureFlagManager() => _instance;
  FeatureFlagManager._internal()
      : _remoteConfig = FirebaseRemoteConfig.instance;
  
  /// Initialize the manager
  Future<void> initialize() async {
    if (_isInitialized) return;
    
    // Configure settings
    await _remoteConfig.setConfigSettings(RemoteConfigSettings(
      fetchTimeout: const Duration(minutes: 1),
      minimumFetchInterval: kDebugMode
          ? Duration.zero
          : const Duration(hours: 1),
    ));
    
    // Set defaults
    await _remoteConfig.setDefaults(FeatureFlagDefaults.all);
    
    // Initial fetch
    await _fetchAndActivate();
    
    // Register lifecycle observer
    WidgetsBinding.instance.addObserver(this);
    
    // Enable real-time updates
    _setupRealTimeListener();
    
    _isInitialized = true;
  }
  
  void _setupRealTimeListener() {
    _realTimeSubscription = _remoteConfig.onConfigUpdated.listen(
      (event) async {
        await _remoteConfig.activate();
        _notifyListeners();
        print('Feature flags updated in real-time');
      },
      onError: (error) {
        print('Real-time config error: $error');
      },
    );
  }
  
  Future<void> _fetchAndActivate() async {
    try {
      final updated = await _remoteConfig.fetchAndActivate();
      if (updated) {
        _notifyListeners();
      }
    } catch (e) {
      print('Feature flag fetch failed: $e');
    }
  }
  
  @override
  void didChangeAppLifecycleState(AppLifecycleState state) {
    if (state == AppLifecycleState.resumed) {
      _fetchAndActivate();
    }
  }
  
  /// Add a listener for flag changes
  void addListener(VoidCallback listener) {
    _listeners.add(listener);
  }
  
  /// Remove a listener
  void removeListener(VoidCallback listener) {
    _listeners.remove(listener);
  }
  
  void _notifyListeners() {
    for (final listener in _listeners) {
      listener();
    }
  }
  
  /// Clean up resources
  void dispose() {
    WidgetsBinding.instance.removeObserver(this);
    _realTimeSubscription?.cancel();
    _listeners.clear();
  }
  
  // Flag accessors
  bool getBool(String key) => _remoteConfig.getBool(key);
  String getString(String key) => _remoteConfig.getString(key);
  int getInt(String key) => _remoteConfig.getInt(key);
  double getDouble(String key) => _remoteConfig.getDouble(key);
}

/// Default values for all feature flags
class FeatureFlagDefaults {
  static const Map<String, dynamic> all = {
    'feature_new_ui': false,
    'feature_offline_mode': true,
    'experiment_checkout': 'control',
    'maintenance_mode': false,
    'kill_switch_payments': false,
  };
}

/// Widget that rebuilds when feature flags change
class FeatureFlagBuilder extends StatefulWidget {
  final Widget Function(BuildContext context) builder;
  
  const FeatureFlagBuilder({super.key, required this.builder});
  
  @override
  State<FeatureFlagBuilder> createState() => _FeatureFlagBuilderState();
}

class _FeatureFlagBuilderState extends State<FeatureFlagBuilder> {
  @override
  void initState() {
    super.initState();
    FeatureFlagManager().addListener(_onFlagsChanged);
  }
  
  @override
  void dispose() {
    FeatureFlagManager().removeListener(_onFlagsChanged);
    super.dispose();
  }
  
  void _onFlagsChanged() {
    if (mounted) {
      setState(() {});
    }
  }
  
  @override
  Widget build(BuildContext context) {
    return widget.builder(context);
  }
}

// Usage:
// FeatureFlagBuilder(
//   builder: (context) {
//     final manager = FeatureFlagManager();
//     if (manager.getBool('feature_new_ui')) {
//       return NewUIWidget();
//     }
//     return OldUIWidget();
//   },
// )
```
