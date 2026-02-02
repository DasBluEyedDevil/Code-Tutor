import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:firebase_performance/firebase_performance.dart';

class MonitoredApiClient {
  final http.Client _client;
  final FirebasePerformance _performance;
  
  // TODO: Add fields to track statistics
  
  MonitoredApiClient({
    http.Client? client,
    FirebasePerformance? performance,
  })  : _client = client ?? http.Client(),
        _performance = performance ?? FirebasePerformance.instance;
  
  /// Perform a monitored GET request
  Future<http.Response> get(
    Uri url, {
    Map<String, String>? headers,
  }) async {
    // TODO: Create HTTP metric, perform request, log results
    throw UnimplementedError();
  }
  
  /// Perform a monitored POST request
  Future<http.Response> post(
    Uri url, {
    Map<String, String>? headers,
    Object? body,
  }) async {
    // TODO: Create HTTP metric, perform request, log results
    throw UnimplementedError();
  }
  
  /// Get aggregated request statistics
  RequestStats getRequestStats() {
    // TODO: Return stats based on tracked data
    throw UnimplementedError();
  }
  
  void close() {
    _client.close();
  }
}

class RequestStats {
  final int totalRequests;
  final int successfulRequests;
  final double averageResponseTimeMs;
  
  RequestStats({
    required this.totalRequests,
    required this.successfulRequests,
    required this.averageResponseTimeMs,
  });
  
  double get successRate => 
      totalRequests > 0 ? (successfulRequests / totalRequests) * 100 : 0;
}

void main() {
  print('MonitoredApiClient created');
}