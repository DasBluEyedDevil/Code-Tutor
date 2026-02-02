---
type: "EXAMPLE"
title: "Network Request Monitoring"
---


Firebase automatically monitors network requests, but you can add custom HTTP metrics for more control:



```dart
// lib/services/monitored_http_client.dart
import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:firebase_performance/firebase_performance.dart';

/// HTTP client wrapper that automatically tracks performance metrics
class MonitoredHttpClient {
  final http.Client _client;
  final FirebasePerformance _performance;
  
  MonitoredHttpClient({
    http.Client? client,
    FirebasePerformance? performance,
  })  : _client = client ?? http.Client(),
        _performance = performance ?? FirebasePerformance.instance;
  
  /// Perform a GET request with performance monitoring
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
  
  /// Perform a POST request with performance monitoring
  Future<http.Response> post(
    Uri url, {
    Map<String, String>? headers,
    Object? body,
    Encoding? encoding,
  }) async {
    return _monitoredRequest(
      url: url,
      method: HttpMethod.Post,
      requestPayloadSize: _calculatePayloadSize(body),
      request: () => _client.post(
        url,
        headers: headers,
        body: body,
        encoding: encoding,
      ),
    );
  }
  
  /// Perform a PUT request with performance monitoring
  Future<http.Response> put(
    Uri url, {
    Map<String, String>? headers,
    Object? body,
    Encoding? encoding,
  }) async {
    return _monitoredRequest(
      url: url,
      method: HttpMethod.Put,
      requestPayloadSize: _calculatePayloadSize(body),
      request: () => _client.put(
        url,
        headers: headers,
        body: body,
        encoding: encoding,
      ),
    );
  }
  
  /// Perform a DELETE request with performance monitoring
  Future<http.Response> delete(
    Uri url, {
    Map<String, String>? headers,
  }) async {
    return _monitoredRequest(
      url: url,
      method: HttpMethod.Delete,
      request: () => _client.delete(url, headers: headers),
    );
  }
  
  Future<http.Response> _monitoredRequest({
    required Uri url,
    required HttpMethod method,
    required Future<http.Response> Function() request,
    int? requestPayloadSize,
  }) async {
    // Create HTTP metric
    final metric = _performance.newHttpMetric(
      url.toString(),
      method,
    );
    
    // Set request payload size if known
    if (requestPayloadSize != null) {
      metric.requestPayloadSize = requestPayloadSize;
    }
    
    await metric.start();
    
    try {
      final response = await request();
      
      // Record response details
      metric.responsePayloadSize = response.contentLength;
      metric.responseContentType = response.headers['content-type'];
      metric.httpResponseCode = response.statusCode;
      
      return response;
    } catch (e) {
      // Record failure
      metric.httpResponseCode = 0; // Indicates network failure
      rethrow;
    } finally {
      await metric.stop();
    }
  }
  
  int? _calculatePayloadSize(Object? body) {
    if (body == null) return null;
    if (body is String) return utf8.encode(body).length;
    if (body is List<int>) return body.length;
    if (body is Map) return utf8.encode(json.encode(body)).length;
    return null;
  }
  
  void close() {
    _client.close();
  }
}

// Usage:
class ApiService {
  final MonitoredHttpClient _client = MonitoredHttpClient();
  final String _baseUrl = 'https://api.example.com';
  
  Future<List<Product>> getProducts() async {
    final response = await _client.get(
      Uri.parse('$_baseUrl/products'),
    );
    
    if (response.statusCode == 200) {
      final List<dynamic> data = json.decode(response.body);
      return data.map((json) => Product.fromJson(json)).toList();
    }
    throw Exception('Failed to load products');
  }
  
  Future<Order> createOrder(Map<String, dynamic> orderData) async {
    final response = await _client.post(
      Uri.parse('$_baseUrl/orders'),
      headers: {'Content-Type': 'application/json'},
      body: json.encode(orderData),
    );
    
    if (response.statusCode == 201) {
      return Order.fromJson(json.decode(response.body));
    }
    throw Exception('Failed to create order');
  }
}
```
