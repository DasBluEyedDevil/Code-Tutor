import 'dart:async';
import 'package:firebase_remote_config/firebase_remote_config.dart';

class KillSwitchService {
  // Singleton pattern
  static final KillSwitchService _instance = KillSwitchService._internal();
  factory KillSwitchService() => _instance;
  
  KillSwitchService._internal() {
    _setupRealTimeUpdates();
  }
  
  final FirebaseRemoteConfig _remoteConfig = FirebaseRemoteConfig.instance;
  final List<void Function()> _listeners = [];
  StreamSubscription? _realTimeSubscription;
  
  /// Check if a specific feature is killed
  bool isKilled(String feature) {
    return _remoteConfig.getBool('kill_switch_$feature');
  }
  
  // Specific kill switches
  bool get isPaymentsKilled => isKilled('payments');
  
  bool get isRegistrationKilled => isKilled('registration');
  
  // Maintenance mode
  bool get isMaintenanceMode => _remoteConfig.getBool('maintenance_mode');
  
  String get maintenanceMessage => _remoteConfig.getString('maintenance_message');
  
  /// Add a listener for kill switch changes
  void addListener(void Function() listener) {
    _listeners.add(listener);
  }
  
  /// Remove a listener
  void removeListener(void Function() listener) {
    _listeners.remove(listener);
  }
  
  void _notifyListeners() {
    for (final listener in _listeners) {
      listener();
    }
  }
  
  /// Set up real-time updates and notify listeners on change
  void _setupRealTimeUpdates() {
    _realTimeSubscription = _remoteConfig.onConfigUpdated.listen(
      (event) async {
        await _remoteConfig.activate();
        _notifyListeners();
      },
      onError: (error) {
        print('Real-time config error: $error');
      },
    );
  }
  
  void dispose() {
    _realTimeSubscription?.cancel();
    _listeners.clear();
  }
}

void main() {
  print('KillSwitchService created');
  final service = KillSwitchService();
  
  // Test listener
  void onKillSwitchChange() {
    print('Kill switch status changed!');
  }
  
  service.addListener(onKillSwitchChange);
  print('Listener added');
  
  service.removeListener(onKillSwitchChange);
  print('Listener removed');
}