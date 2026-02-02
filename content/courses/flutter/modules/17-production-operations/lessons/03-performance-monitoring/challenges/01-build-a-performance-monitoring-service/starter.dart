import 'package:firebase_performance/firebase_performance.dart';

class PerformanceMonitor {
  // TODO: Implement singleton pattern with FirebasePerformance.instance
  
  // TODO: Store active traces in a Map
  
  /// Start a named trace
  Future<void> startTrace(String name) async {
    // TODO: Create and start a trace, store in map
  }
  
  /// Stop a named trace
  Future<void> stopTrace(String name) async {
    // TODO: Get trace from map, stop it, remove from map
  }
  
  /// Add attribute to an active trace
  void setTraceAttribute(String traceName, String key, String value) {
    // TODO: Get trace from map and set attribute
  }
  
  /// Measure an async operation
  Future<T> measureAsync<T>(String name, Future<T> Function() operation) async {
    // TODO: Create trace, run operation, stop trace, return result
    throw UnimplementedError();
  }
  
  /// Track a network request
  Future<void> trackNetworkRequest({
    required String url,
    required HttpMethod method,
    required int statusCode,
    required int responseSize,
    required int durationMs,
  }) async {
    // TODO: Create HTTP metric, set properties, start and stop
  }
}

void main() {
  print('PerformanceMonitor created');
}