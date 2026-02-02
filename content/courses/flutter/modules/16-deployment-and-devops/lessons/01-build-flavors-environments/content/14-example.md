---
type: "EXAMPLE"
title: "Using Config in Your App"
---


Integrate the config into your app:



```dart
// lib/main.dart
import 'package:flutter/material.dart';
import 'config/app_config.dart';
import 'services/api_client.dart';

void main() {
  // Initialize config first
  AppConfig.initialize();
  
  // Log environment info
  final config = AppConfig.instance;
  if (config.enableLogging) {
    print('Starting ${config.appName}');
    print('Environment: ${config.flavor.name}');
    print('API URL: ${config.apiBaseUrl}');
  }
  
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});
  
  @override
  Widget build(BuildContext context) {
    final config = AppConfig.instance;
    
    return MaterialApp(
      title: config.appName,
      debugShowCheckedModeBanner: config.showDebugBanner,
      theme: ThemeData(
        colorScheme: ColorScheme.fromSeed(
          seedColor: config.isDev ? Colors.green : 
                     config.isStaging ? Colors.orange : 
                     Colors.blue,
        ),
      ),
      home: const HomeScreen(),
    );
  }
}

// lib/services/api_client.dart
class ApiClient {
  final String baseUrl;
  
  ApiClient() : baseUrl = AppConfig.instance.apiBaseUrl;
  
  Future<Response> get(String path) {
    final url = '$baseUrl$path';
    if (AppConfig.instance.enableLogging) {
      print('GET $url');
    }
    // ... make request
  }
}
```
