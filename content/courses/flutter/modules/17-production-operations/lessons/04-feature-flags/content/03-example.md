---
type: "EXAMPLE"
title: "Firebase Remote Config Setup"
---


**Step 1: Add Dependencies**

```yaml
# pubspec.yaml
dependencies:
  firebase_core: ^3.9.0
  firebase_remote_config: ^5.3.0
  firebase_analytics: ^11.4.0  # Optional, for user targeting
```

**Step 2: Initialize Remote Config**



```dart
// lib/main.dart
import 'package:flutter/material.dart';
import 'package:firebase_core/firebase_core.dart';
import 'package:firebase_remote_config/firebase_remote_config.dart';
import 'firebase_options.dart';

void main() async {
  WidgetsFlutterBinding.ensureInitialized();
  await Firebase.initializeApp(
    options: DefaultFirebaseOptions.currentPlatform,
  );
  
  // Initialize Remote Config
  await initializeRemoteConfig();
  
  runApp(const MyApp());
}

Future<void> initializeRemoteConfig() async {
  final remoteConfig = FirebaseRemoteConfig.instance;
  
  // Set configuration settings
  await remoteConfig.setConfigSettings(RemoteConfigSettings(
    fetchTimeout: const Duration(minutes: 1),
    minimumFetchInterval: const Duration(hours: 1),
  ));
  
  // Set default values (used if fetch fails or params not set)
  await remoteConfig.setDefaults(const {
    'new_checkout_enabled': false,
    'max_cart_items': 50,
    'welcome_message': 'Welcome to our app!',
    'maintenance_mode': false,
    'feature_dark_mode': true,
  });
  
  // Fetch and activate latest values
  try {
    await remoteConfig.fetchAndActivate();
    print('Remote Config fetched and activated');
  } catch (e) {
    print('Remote Config fetch failed: $e');
    // App continues with default values
  }
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});
  
  @override
  Widget build(BuildContext context) {
    final remoteConfig = FirebaseRemoteConfig.instance;
    final welcomeMessage = remoteConfig.getString('welcome_message');
    
    return MaterialApp(
      title: 'Feature Flags Demo',
      home: Scaffold(
        appBar: AppBar(title: const Text('Feature Flags')),
        body: Center(
          child: Text(welcomeMessage),
        ),
      ),
    );
  }
}
```
