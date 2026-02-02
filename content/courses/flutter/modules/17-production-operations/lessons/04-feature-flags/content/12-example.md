---
type: "EXAMPLE"
title: "Implementing Kill Switches"
---


Create a robust kill switch system with fallback behavior:



```dart
// lib/features/kill_switch_service.dart
import 'dart:async';
import 'package:flutter/material.dart';
import 'package:firebase_remote_config/firebase_remote_config.dart';

/// Service for managing emergency kill switches
class KillSwitchService {
  final FirebaseRemoteConfig _remoteConfig;
  StreamSubscription? _realTimeSubscription;
  final List<VoidCallback> _listeners = [];
  
  // Singleton
  static final KillSwitchService _instance = KillSwitchService._internal();
  factory KillSwitchService() => _instance;
  KillSwitchService._internal()
      : _remoteConfig = FirebaseRemoteConfig.instance {
    _setupRealTimeUpdates();
  }
  
  void _setupRealTimeUpdates() {
    _realTimeSubscription = _remoteConfig.onConfigUpdated.listen(
      (event) async {
        await _remoteConfig.activate();
        _notifyListeners();
        print('Kill switch status updated');
      },
    );
  }
  
  void addListener(VoidCallback listener) {
    _listeners.add(listener);
  }
  
  void removeListener(VoidCallback listener) {
    _listeners.remove(listener);
  }
  
  void _notifyListeners() {
    for (final listener in _listeners) {
      listener();
    }
  }
  
  /// Check if a feature is killed (disabled)
  bool isKilled(String feature) {
    return _remoteConfig.getBool('kill_switch_$feature');
  }
  
  /// Check if app is in maintenance mode
  bool get isMaintenanceMode => _remoteConfig.getBool('maintenance_mode');
  
  /// Get maintenance message
  String get maintenanceMessage => 
      _remoteConfig.getString('maintenance_message');
  
  /// Common kill switches
  bool get isPaymentsKilled => isKilled('payments');
  bool get isRegistrationKilled => isKilled('registration');
  bool get isSocialLoginKilled => isKilled('social_login');
  bool get isPushNotificationsKilled => isKilled('push_notifications');
  
  void dispose() {
    _realTimeSubscription?.cancel();
    _listeners.clear();
  }
}

/// Widget that handles kill switch state
class KillSwitchGate extends StatefulWidget {
  final String feature;
  final Widget child;
  final Widget? fallback;
  final String? killedMessage;
  
  const KillSwitchGate({
    super.key,
    required this.feature,
    required this.child,
    this.fallback,
    this.killedMessage,
  });
  
  @override
  State<KillSwitchGate> createState() => _KillSwitchGateState();
}

class _KillSwitchGateState extends State<KillSwitchGate> {
  final KillSwitchService _killSwitch = KillSwitchService();
  
  @override
  void initState() {
    super.initState();
    _killSwitch.addListener(_onKillSwitchChanged);
  }
  
  @override
  void dispose() {
    _killSwitch.removeListener(_onKillSwitchChanged);
    super.dispose();
  }
  
  void _onKillSwitchChanged() {
    if (mounted) setState(() {});
  }
  
  @override
  Widget build(BuildContext context) {
    if (_killSwitch.isKilled(widget.feature)) {
      return widget.fallback ?? _buildDefaultFallback();
    }
    return widget.child;
  }
  
  Widget _buildDefaultFallback() {
    return Center(
      child: Padding(
        padding: const EdgeInsets.all(24),
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            const Icon(
              Icons.construction,
              size: 64,
              color: Colors.orange,
            ),
            const SizedBox(height: 16),
            Text(
              widget.killedMessage ?? 'This feature is temporarily unavailable',
              textAlign: TextAlign.center,
              style: Theme.of(context).textTheme.titleMedium,
            ),
            const SizedBox(height: 8),
            const Text(
              'Please try again later',
              textAlign: TextAlign.center,
            ),
          ],
        ),
      ),
    );
  }
}

/// Maintenance mode wrapper for entire app
class MaintenanceGate extends StatefulWidget {
  final Widget child;
  
  const MaintenanceGate({super.key, required this.child});
  
  @override
  State<MaintenanceGate> createState() => _MaintenanceGateState();
}

class _MaintenanceGateState extends State<MaintenanceGate> {
  final KillSwitchService _killSwitch = KillSwitchService();
  
  @override
  void initState() {
    super.initState();
    _killSwitch.addListener(_onStatusChanged);
  }
  
  @override
  void dispose() {
    _killSwitch.removeListener(_onStatusChanged);
    super.dispose();
  }
  
  void _onStatusChanged() {
    if (mounted) setState(() {});
  }
  
  @override
  Widget build(BuildContext context) {
    if (_killSwitch.isMaintenanceMode) {
      return MaterialApp(
        home: MaintenanceScreen(
          message: _killSwitch.maintenanceMessage,
        ),
      );
    }
    return widget.child;
  }
}

/// Full-screen maintenance mode display
class MaintenanceScreen extends StatelessWidget {
  final String message;
  
  const MaintenanceScreen({super.key, required this.message});
  
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SafeArea(
        child: Center(
          child: Padding(
            padding: const EdgeInsets.all(32),
            child: Column(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                const Icon(
                  Icons.build_circle,
                  size: 100,
                  color: Colors.blue,
                ),
                const SizedBox(height: 32),
                const Text(
                  'Under Maintenance',
                  style: TextStyle(
                    fontSize: 28,
                    fontWeight: FontWeight.bold,
                  ),
                ),
                const SizedBox(height: 16),
                Text(
                  message.isNotEmpty
                      ? message
                      : 'We are performing scheduled maintenance. Please check back soon.',
                  textAlign: TextAlign.center,
                  style: const TextStyle(fontSize: 16),
                ),
                const SizedBox(height: 32),
                OutlinedButton.icon(
                  onPressed: () {
                    // Trigger a refresh
                    KillSwitchService();
                  },
                  icon: const Icon(Icons.refresh),
                  label: const Text('Check Again'),
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}

// Usage in main.dart:
// void main() async {
//   await Firebase.initializeApp();
//   runApp(
//     MaintenanceGate(
//       child: MyApp(),
//     ),
//   );
// }

// Usage for specific features:
// KillSwitchGate(
//   feature: 'payments',
//   killedMessage: 'Payments are temporarily disabled for maintenance',
//   child: PaymentButton(),
//   fallback: DisabledPaymentMessage(),
// )
```
