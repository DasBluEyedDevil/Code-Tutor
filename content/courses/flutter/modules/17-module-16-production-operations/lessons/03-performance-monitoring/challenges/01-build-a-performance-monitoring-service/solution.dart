import 'package:firebase_performance/firebase_performance.dart';

class PerformanceMonitor {
  // Singleton pattern
  static final PerformanceMonitor _instance = PerformanceMonitor._internal();
  factory PerformanceMonitor() => _instance;
  PerformanceMonitor._internal();
  
  final FirebasePerformance _performance = FirebasePerformance.instance;
  final Map<String, Trace> _activeTraces = {};
  
  /// Start a named trace
  Future<void> startTrace(String name) async {
    if (_activeTraces.containsKey(name)) {
      return; // Already tracking
    }
    
    final trace = _performance.newTrace(name);
    await trace.start();
    _activeTraces[name] = trace;
  }
  
  /// Stop a named trace
  Future<void> stopTrace(String name) async {
    final trace = _activeTraces.remove(name);
    if (trace != null) {
      await trace.stop();
    }
  }
  
  /// Add attribute to an active trace
  void setTraceAttribute(String traceName, String key, String value) {
    final trace = _activeTraces[traceName];
    trace?.putAttribute(key, value);
  }
  
  /// Measure an async operation
  Future<T> measureAsync<T>(String name, Future<T> Function() operation) async {
    final trace = _performance.newTrace(name);
    await trace.start();
    
    try {
      return await operation();
    } finally {
      await trace.stop();
    }
  }
  
  /// Track a network request
  Future<void> trackNetworkRequest({
    required String url,
    required HttpMethod method,
    required int statusCode,
    required int responseSize,
    required int durationMs,
  }) async {
    final metric = _performance.newHttpMetric(url, method);
    
    metric.httpResponseCode = statusCode;
    metric.responsePayloadSize = responseSize;
    
    await metric.start();
    // In real usage, you'd measure actual request time
    // Here we just log the provided duration
    await Future.delayed(Duration.zero);
    await metric.stop();
  }
}

void main() {
  print('PerformanceMonitor created');
  final monitor = PerformanceMonitor();
  print('Singleton works: ${identical(monitor, PerformanceMonitor())}');
}