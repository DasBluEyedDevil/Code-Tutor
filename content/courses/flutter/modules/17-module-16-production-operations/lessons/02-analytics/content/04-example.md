---
type: "EXAMPLE"
title: "Creating an Analytics Service"
---


Wrap Firebase Analytics in a service for cleaner code and easier testing:



```dart
// lib/services/analytics_service.dart
import 'package:firebase_analytics/firebase_analytics.dart';

/// Centralized analytics service for tracking user behavior
class AnalyticsService {
  final FirebaseAnalytics _analytics;
  
  // Singleton pattern
  static final AnalyticsService _instance = AnalyticsService._internal();
  factory AnalyticsService() => _instance;
  AnalyticsService._internal() : _analytics = FirebaseAnalytics.instance;
  
  // For testing - allow injecting a mock
  AnalyticsService.withAnalytics(this._analytics);
  
  /// Get the observer for automatic screen tracking
  FirebaseAnalyticsObserver get observer => 
      FirebaseAnalyticsObserver(analytics: _analytics);
  
  /// Log a custom event
  Future<void> logEvent({
    required String name,
    Map<String, Object>? parameters,
  }) async {
    await _analytics.logEvent(
      name: name,
      parameters: parameters,
    );
  }
  
  /// Log when user views a screen
  Future<void> logScreenView({
    required String screenName,
    String? screenClass,
  }) async {
    await _analytics.logScreenView(
      screenName: screenName,
      screenClass: screenClass ?? screenName,
    );
  }
  
  /// Set user property for segmentation
  Future<void> setUserProperty({
    required String name,
    required String? value,
  }) async {
    await _analytics.setUserProperty(
      name: name,
      value: value,
    );
  }
  
  /// Set user ID for cross-device tracking
  Future<void> setUserId(String? userId) async {
    await _analytics.setUserId(id: userId);
  }
}
```
