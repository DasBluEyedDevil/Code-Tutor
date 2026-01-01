import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:firebase_performance/firebase_performance.dart';

class MonitoredApiClient {
  final http.Client _client;
  final FirebasePerformance _performance;
  
  // Statistics tracking
  int _totalRequests = 0;
  int _successfulRequests = 0;
  int _totalResponseTimeMs = 0;
  
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
    return _monitoredRequest(
      url: url,
      method: HttpMethod.Get,
      request: () => _client.get(url, headers: headers),
    );
  }
  
  /// Perform a monitored POST request
  Future<http.Response> post(
    Uri url, {
    Map<String, String>? headers,
    Object? body,
  }) async {
    return _monitoredRequest(
      url: url,
      method: HttpMethod.Post,
      requestSize: _calculateBodySize(body),
      request: () => _client.post(
        url,
        headers: headers,
        body: body,
      ),
    );
  }
  
  Future<http.Response> _monitoredRequest({
    required Uri url,
    required HttpMethod method,
    required Future<http.Response> Function() request,
    int? requestSize,
  }) async {
    final metric = _performance.newHttpMetric(url.toString(), method);
    
    if (requestSize != null) {
      metric.requestPayloadSize = requestSize;
    }
    
    final stopwatch = Stopwatch()..start();
    await metric.start();
    
    try {
      final response = await request();
      
      stopwatch.stop();
      
      // Update Firebase metric
      metric.httpResponseCode = response.statusCode;
      metric.responsePayloadSize = response.contentLength;
      metric.responseContentType = response.headers['content-type'];
      
      // Update local statistics
      _totalRequests++;
      _totalResponseTimeMs += stopwatch.elapsedMilliseconds;
      if (response.statusCode >= 200 && response.statusCode < 300) {
        _successfulRequests++;
      }
      
      return response;
    } catch (e) {
      stopwatch.stop();
      _totalRequests++;
      _totalResponseTimeMs += stopwatch.elapsedMilliseconds;
      metric.httpResponseCode = 0;
      rethrow;
    } finally {
      await metric.stop();
    }
  }
  
  int? _calculateBodySize(Object? body) {
    if (body == null) return null;
    if (body is String) return utf8.encode(body).length;
    if (body is List<int>) return body.length;
    if (body is Map) return utf8.encode(json.encode(body)).length;
    return null;
  }
  
  /// Get aggregated request statistics
  RequestStats getRequestStats() {
    return RequestStats(
      totalRequests: _totalRequests,
      successfulRequests: _successfulRequests,
      averageResponseTimeMs: _totalRequests > 0
          ? _totalResponseTimeMs / _totalRequests
          : 0,
    );
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
  final client = MonitoredApiClient();
  final stats = client.getRequestStats();
  print('Initial stats: ${stats.totalRequests} requests');
}