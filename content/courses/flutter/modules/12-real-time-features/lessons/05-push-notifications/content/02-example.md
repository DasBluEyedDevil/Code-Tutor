---
type: "EXAMPLE"
title: "Flutter FCM Integration"
---

Setting up Firebase Cloud Messaging in Flutter requires proper initialization, permission handling, and token management. The firebase_messaging package provides a clean API for all these operations.

**Setup Steps:**

1. Add firebase_messaging to pubspec.yaml
2. Configure Firebase for both platforms
3. Initialize messaging in main.dart
4. Request permissions on iOS
5. Get and store the device token



```dart
// pubspec.yaml dependencies:
// firebase_core: ^2.24.0
// firebase_messaging: ^14.7.0

import 'package:firebase_core/firebase_core.dart';
import 'package:firebase_messaging/firebase_messaging.dart';

// Initialize in main.dart
Future<void> main() async {
  WidgetsFlutterBinding.ensureInitialized();
  await Firebase.initializeApp();
  
  // Initialize FCM service
  await PushNotificationService.instance.initialize();
  
  runApp(MyApp());
}

class PushNotificationService {
  static final PushNotificationService instance = PushNotificationService._();
  PushNotificationService._();
  
  final FirebaseMessaging _messaging = FirebaseMessaging.instance;
  String? _deviceToken;
  
  String? get deviceToken => _deviceToken;
  
  Future<void> initialize() async {
    // Request permissions (critical for iOS)
    await _requestPermissions();
    
    // Get the device token
    await _getToken();
    
    // Listen for token refresh
    _messaging.onTokenRefresh.listen(_handleTokenRefresh);
  }
  
  Future<void> _requestPermissions() async {
    final settings = await _messaging.requestPermission(
      alert: true,
      badge: true,
      sound: true,
      provisional: false,
      announcement: true,
      carPlay: false,
      criticalAlert: false,
    );
    
    print('Permission status: ${settings.authorizationStatus}');
    
    if (settings.authorizationStatus == AuthorizationStatus.denied) {
      // Handle denied - maybe show explanation dialog
      print('User denied push notification permissions');
    }
  }
  
  Future<void> _getToken() async {
    try {
      _deviceToken = await _messaging.getToken();
      print('FCM Token: $_deviceToken');
      
      if (_deviceToken != null) {
        await _sendTokenToServer(_deviceToken!);
      }
    } catch (e) {
      print('Failed to get FCM token: $e');
    }
  }
  
  void _handleTokenRefresh(String newToken) {
    print('FCM Token refreshed: $newToken');
    _deviceToken = newToken;
    _sendTokenToServer(newToken);
  }
  
  Future<void> _sendTokenToServer(String token) async {
    // Send to your Serverpod backend
    try {
      await client.notifications.registerDevice(
        token: token,
        platform: Platform.isIOS ? 'ios' : 'android',
      );
    } catch (e) {
      print('Failed to register token with server: $e');
    }
  }
  
  // Unregister when user logs out
  Future<void> unregister() async {
    if (_deviceToken != null) {
      await client.notifications.unregisterDevice(token: _deviceToken!);
    }
    await _messaging.deleteToken();
    _deviceToken = null;
  }
}
```
