---
type: "EXAMPLE"
title: "Combining Interceptors in a Production Setup"
---


Here is how to wire all interceptors together for a production-ready HTTP client:



```dart
// lib/core/network/api_client.dart
import 'package:dio/dio.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'interceptors/logging_interceptor.dart';
import 'interceptors/auth_interceptor.dart';
import 'interceptors/retry_interceptor.dart';
import 'interceptors/error_interceptor.dart';

/// Centralized API client with all interceptors configured.
/// 
/// Usage:
/// ```dart
/// final apiClient = ApiClient();
/// await apiClient.initialize();
/// 
/// final response = await apiClient.dio.get('/users');
/// ```
class ApiClient {
  static const String _baseUrl = 'https://api.yourapp.com/v1';
  
  late final Dio dio;
  final FlutterSecureStorage _secureStorage;
  
  // Callback for handling 401 errors (e.g., navigate to login)
  void Function()? onUnauthorized;
  
  ApiClient({
    FlutterSecureStorage? secureStorage,
    this.onUnauthorized,
  }) : _secureStorage = secureStorage ?? const FlutterSecureStorage();
  
  /// Initialize the client. Must be called before making requests.
  void initialize({bool enableLogging = true}) {
    dio = Dio(BaseOptions(
      baseUrl: _baseUrl,
      connectTimeout: const Duration(seconds: 30),
      receiveTimeout: const Duration(seconds: 30),
      headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
      },
    ));
    
    // Add interceptors in order
    // 1. Logging (see all traffic)
    if (enableLogging) {
      dio.interceptors.add(LoggingInterceptor(
        logRequestBody: true,
        logResponseBody: false, // Set to true for debugging
      ));
    }
    
    // 2. Authentication (add tokens)
    dio.interceptors.add(AuthInterceptor(
      getToken: _getAuthToken,
      onUnauthorized: onUnauthorized,
    ));
    
    // 3. Retry logic (handle transient failures)
    dio.interceptors.add(RetryInterceptor(
      dio: dio,
      maxRetries: 3,
      retryDelay: const Duration(seconds: 1),
    ));
    
    // 4. Error transformation (convert to app errors)
    dio.interceptors.add(ErrorInterceptor());
  }
  
  Future<String?> _getAuthToken() async {
    return await _secureStorage.read(key: 'auth_token');
  }
  
  Future<void> setAuthToken(String token) async {
    await _secureStorage.write(key: 'auth_token', value: token);
  }
  
  Future<void> clearAuthToken() async {
    await _secureStorage.delete(key: 'auth_token');
  }
}

// lib/core/di/service_locator.dart (using get_it for dependency injection)
import 'package:get_it/get_it.dart';
import '../network/api_client.dart';

final getIt = GetIt.instance;

void setupServiceLocator() {
  // Register API client as singleton
  getIt.registerLazySingleton<ApiClient>(() {
    final client = ApiClient(
      onUnauthorized: () {
        // Navigate to login screen
        // navigationService.navigateTo('/login');
      },
    );
    client.initialize();
    return client;
  });
}

// Usage in your app:
void main() {
  setupServiceLocator();
  runApp(const MyApp());
}

// In any service:
class UserService {
  final ApiClient _apiClient = getIt<ApiClient>();
  
  Future<User> getProfile() async {
    try {
      final response = await _apiClient.dio.get('/me');
      return User.fromJson(response.data);
    } on DioException catch (e) {
      final apiError = e.error as ApiException;
      throw apiError; // Rethrow as ApiException for UI handling
    }
  }
}
```
