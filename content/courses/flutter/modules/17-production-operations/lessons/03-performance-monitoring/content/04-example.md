---
type: "EXAMPLE"
title: "Creating a Performance Service"
---


Wrap Firebase Performance in a service for cleaner code and easier testing:



```dart
// lib/services/performance_service.dart
import 'package:firebase_performance/firebase_performance.dart';

/// Centralized service for performance monitoring
class PerformanceService {
  final FirebasePerformance _performance;
  
  // Track active traces
  final Map<String, Trace> _activeTraces = {};
  
  // Singleton pattern
  static final PerformanceService _instance = PerformanceService._internal();
  factory PerformanceService() => _instance;
  PerformanceService._internal() 
      : _performance = FirebasePerformance.instance;
  
  // For testing - allow injecting a mock
  PerformanceService.withPerformance(this._performance);
  
  /// Enable or disable performance collection
  Future<void> setEnabled(bool enabled) async {
    await _performance.setPerformanceCollectionEnabled(enabled);
  }
  
  /// Start a custom trace
  Future<void> startTrace(String name) async {
    if (_activeTraces.containsKey(name)) {
      // Trace already running
      return;
    }
    
    final trace = _performance.newTrace(name);
    await trace.start();
    _activeTraces[name] = trace;
  }
  
  /// Stop a custom trace
  Future<void> stopTrace(String name) async {
    final trace = _activeTraces.remove(name);
    if (trace != null) {
      await trace.stop();
    }
  }
  
  /// Add a metric to an active trace
  void incrementTraceMetric(String traceName, String metric, int value) {
    final trace = _activeTraces[traceName];
    trace?.incrementMetric(metric, value);
  }
  
  /// Add an attribute to an active trace
  void setTraceAttribute(String traceName, String key, String value) {
    final trace = _activeTraces[traceName];
    trace?.putAttribute(key, value);
  }
  
  /// Create an HTTP metric for network monitoring
  HttpMetric newHttpMetric(String url, HttpMethod method) {
    return _performance.newHttpMetric(url, method);
  }
  
  /// Measure a function's execution time
  Future<T> measureAsync<T>(
    String traceName,
    Future<T> Function() operation, {
    Map<String, String>? attributes,
  }) async {
    final trace = _performance.newTrace(traceName);
    
    // Add any attributes before starting
    attributes?.forEach((key, value) {
      trace.putAttribute(key, value);
    });
    
    await trace.start();
    try {
      return await operation();
    } finally {
      await trace.stop();
    }
  }
  
  /// Measure a synchronous function's execution time
  T measureSync<T>(
    String traceName,
    T Function() operation, {
    Map<String, String>? attributes,
  }) {
    final trace = _performance.newTrace(traceName);
    
    attributes?.forEach((key, value) {
      trace.putAttribute(key, value);
    });
    
    trace.start();
    try {
      return operation();
    } finally {
      trace.stop();
    }
  }
}
```
