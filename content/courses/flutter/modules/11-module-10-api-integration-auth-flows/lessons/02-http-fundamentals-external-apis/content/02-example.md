---
type: "EXAMPLE"
title: "Dio Package Setup"
---


Let us set up Dio in your Flutter project and configure it properly for production use.

**Step 1: Add Dependencies**

Add Dio to your `pubspec.yaml`:

```yaml
dependencies:
  flutter:
    sdk: flutter
  dio: ^5.4.0
```

Run `flutter pub get` to install.

**Step 2: Create a Dio Instance with BaseOptions**

Never create Dio instances inline. Instead, configure a properly set up instance:



```dart
// lib/core/network/dio_client.dart
import 'package:dio/dio.dart';

/// Factory class for creating configured Dio instances.
/// 
/// This centralized approach ensures consistent configuration
/// across all API calls in your app.
class DioClient {
  /// Creates a Dio instance configured for a specific API.
  /// 
  /// [baseUrl] - The base URL for all requests (e.g., 'https://api.openweathermap.org/data/2.5')
  /// [connectTimeout] - How long to wait for connection (default: 30 seconds)
  /// [receiveTimeout] - How long to wait for response (default: 30 seconds)
  /// [sendTimeout] - How long to wait for request upload (default: 30 seconds)
  static Dio create({
    required String baseUrl,
    Duration connectTimeout = const Duration(seconds: 30),
    Duration receiveTimeout = const Duration(seconds: 30),
    Duration sendTimeout = const Duration(seconds: 30),
    Map<String, dynamic>? defaultHeaders,
    Map<String, dynamic>? defaultQueryParameters,
  }) {
    final options = BaseOptions(
      // The base URL is prepended to all request paths
      baseUrl: baseUrl,
      
      // Timeout configurations
      connectTimeout: connectTimeout,
      receiveTimeout: receiveTimeout,
      sendTimeout: sendTimeout,
      
      // Default headers sent with every request
      headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        ...?defaultHeaders,
      },
      
      // Query parameters added to every request
      queryParameters: defaultQueryParameters,
      
      // How to handle response data
      responseType: ResponseType.json,
      
      // Which status codes are considered successful
      // By default, 2xx codes are successful, others throw DioException
      validateStatus: (status) => status != null && status >= 200 && status < 300,
    );
    
    return Dio(options);
  }
  
  /// Creates a Dio instance for OpenWeatherMap API.
  /// 
  /// Usage:
  /// ```dart
  /// final weatherDio = DioClient.openWeatherMap(apiKey: 'your_api_key');
  /// final response = await weatherDio.get('/weather', queryParameters: {'q': 'London'});
  /// ```
  static Dio openWeatherMap({required String apiKey}) {
    return create(
      baseUrl: 'https://api.openweathermap.org/data/2.5',
      defaultQueryParameters: {
        'appid': apiKey,
        'units': 'metric', // Use Celsius by default
      },
    );
  }
  
  /// Creates a Dio instance for JSONPlaceholder (testing API).
  /// 
  /// JSONPlaceholder is a free fake REST API for testing and prototyping.
  /// Perfect for learning HTTP concepts without needing an API key.
  static Dio jsonPlaceholder() {
    return create(
      baseUrl: 'https://jsonplaceholder.typicode.com',
    );
  }
  
  /// Creates a Dio instance for GitHub API.
  /// 
  /// [token] is optional. Without it, you get lower rate limits.
  static Dio gitHub({String? token}) {
    return create(
      baseUrl: 'https://api.github.com',
      defaultHeaders: {
        'Accept': 'application/vnd.github+json',
        'X-GitHub-Api-Version': '2022-11-28',
        if (token != null) 'Authorization': 'Bearer $token',
      },
    );
  }
}
```
