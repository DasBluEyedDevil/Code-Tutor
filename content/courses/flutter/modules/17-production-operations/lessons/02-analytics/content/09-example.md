---
type: "EXAMPLE"
title: "Custom Navigation Observer"
---


For more control, create a custom navigation observer:



```dart
// lib/navigation/analytics_navigator_observer.dart
import 'package:flutter/material.dart';
import '../services/analytics_service.dart';

/// Custom navigator observer for detailed screen tracking
class AnalyticsNavigatorObserver extends NavigatorObserver {
  final AnalyticsService _analytics;
  
  AnalyticsNavigatorObserver({AnalyticsService? analytics})
      : _analytics = analytics ?? AnalyticsService();
  
  @override
  void didPush(Route<dynamic> route, Route<dynamic>? previousRoute) {
    super.didPush(route, previousRoute);
    _logScreenView(route);
  }
  
  @override
  void didReplace({Route<dynamic>? newRoute, Route<dynamic>? oldRoute}) {
    super.didReplace(newRoute: newRoute, oldRoute: oldRoute);
    if (newRoute != null) {
      _logScreenView(newRoute);
    }
  }
  
  @override
  void didPop(Route<dynamic> route, Route<dynamic>? previousRoute) {
    super.didPop(route, previousRoute);
    // Log the screen we're returning to
    if (previousRoute != null) {
      _logScreenView(previousRoute);
    }
  }
  
  void _logScreenView(Route<dynamic> route) {
    final screenName = _getScreenName(route);
    if (screenName != null) {
      _analytics.logScreenView(
        screenName: screenName,
        screenClass: route.settings.name ?? 'Unknown',
      );
    }
  }
  
  String? _getScreenName(Route<dynamic> route) {
    // Get screen name from route settings
    final routeName = route.settings.name;
    
    if (routeName == null || routeName.isEmpty) {
      return null;
    }
    
    // Convert route name to readable screen name
    // '/product/123' -> 'Product'
    // '/user-settings' -> 'User Settings'
    final segments = routeName.split('/');
    final lastSegment = segments.lastWhere(
      (s) => s.isNotEmpty && !_isParameter(s),
      orElse: () => '',
    );
    
    if (lastSegment.isEmpty) {
      return routeName == '/' ? 'Home' : null;
    }
    
    return _formatScreenName(lastSegment);
  }
  
  bool _isParameter(String segment) {
    // Check if segment looks like a parameter (numeric ID, UUID, etc.)
    return int.tryParse(segment) != null ||
        RegExp(r'^[a-f0-9-]{36}$').hasMatch(segment);
  }
  
  String _formatScreenName(String name) {
    // 'user-settings' -> 'User Settings'
    return name
        .replaceAll('-', ' ')
        .replaceAll('_', ' ')
        .split(' ')
        .map((word) => word.isEmpty 
            ? '' 
            : '${word[0].toUpperCase()}${word.substring(1)}')
        .join(' ');
  }
}

// Usage in main.dart:
class MyApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      navigatorObservers: [
        AnalyticsNavigatorObserver(),
      ],
      // ...
    );
  }
}
```
