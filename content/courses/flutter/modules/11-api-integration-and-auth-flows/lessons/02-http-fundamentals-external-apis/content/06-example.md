---
type: "EXAMPLE"
title: "Implementing Essential Interceptors"
---


Here are production-ready interceptors that you will use in real apps:



```dart
// lib/core/network/interceptors/logging_interceptor.dart
import 'package:dio/dio.dart';

/// Logs all HTTP requests and responses for debugging.
/// 
/// In production, you would use a proper logging framework
/// and only log in debug mode.
class LoggingInterceptor extends Interceptor {
  final bool logRequestBody;
  final bool logResponseBody;
  
  LoggingInterceptor({
    this.logRequestBody = true,
    this.logResponseBody = false, // Response bodies can be large
  });

  @override
  void onRequest(RequestOptions options, RequestInterceptorHandler handler) {
    final method = options.method;
    final uri = options.uri;
    
    print('\n--> $method $uri');
    print('Headers: ${options.headers}');
    
    if (logRequestBody && options.data != null) {
      print('Body: ${options.data}');
    }
    
    // IMPORTANT: Always call handler.next() to continue the request
    handler.next(options);
  }

  @override
  void onResponse(Response response, ResponseInterceptorHandler handler) {
    final method = response.requestOptions.method;
    final uri = response.requestOptions.uri;
    final statusCode = response.statusCode;
    
    print('<-- $statusCode $method $uri');
    
    if (logResponseBody) {
      print('Response: ${response.data}');
    }
    
    print('');
    handler.next(response);
  }

  @override
  void onError(DioException err, ErrorInterceptorHandler handler) {
    final method = err.requestOptions.method;
    final uri = err.requestOptions.uri;
    
    print('<-- ERROR $method $uri');
    print('Error: ${err.message}');
    
    if (err.response != null) {
      print('Status: ${err.response?.statusCode}');
      print('Data: ${err.response?.data}');
    }
    
    print('');
    handler.next(err); // Pass error to next handler
  }
}

// lib/core/network/interceptors/auth_interceptor.dart
import 'package:dio/dio.dart';

/// Automatically adds authentication headers to requests.
/// 
/// This interceptor retrieves the auth token from a token provider
/// (like secure storage) and adds it to every request.
class AuthInterceptor extends Interceptor {
  final Future<String?> Function() getToken;
  final void Function()? onUnauthorized;
  
  AuthInterceptor({
    required this.getToken,
    this.onUnauthorized,
  });

  @override
  void onRequest(
    RequestOptions options,
    RequestInterceptorHandler handler,
  ) async {
    // Skip auth for certain endpoints
    if (options.extra['skipAuth'] == true) {
      return handler.next(options);
    }
    
    // Get the current token
    final token = await getToken();
    
    if (token != null && token.isNotEmpty) {
      options.headers['Authorization'] = 'Bearer $token';
    }
    
    handler.next(options);
  }

  @override
  void onError(DioException err, ErrorInterceptorHandler handler) {
    // If we get a 401, the token might be expired
    if (err.response?.statusCode == 401) {
      onUnauthorized?.call();
    }
    
    handler.next(err);
  }
}

// lib/core/network/interceptors/retry_interceptor.dart
import 'dart:async';
import 'package:dio/dio.dart';

/// Automatically retries failed requests for transient errors.
/// 
/// Retries on:
/// - Network errors (no internet, timeout)
/// - Server errors (503 Service Unavailable)
/// 
/// Does NOT retry on:
/// - Client errors (400, 401, 403, 404)
/// - Request cancelled
class RetryInterceptor extends Interceptor {
  final Dio dio;
  final int maxRetries;
  final Duration retryDelay;
  
  RetryInterceptor({
    required this.dio,
    this.maxRetries = 3,
    this.retryDelay = const Duration(seconds: 1),
  });

  @override
  void onError(DioException err, ErrorInterceptorHandler handler) async {
    // Check if we should retry
    if (!_shouldRetry(err)) {
      return handler.next(err);
    }
    
    // Get current retry count from extras
    final retryCount = err.requestOptions.extra['retryCount'] ?? 0;
    
    if (retryCount >= maxRetries) {
      // Max retries reached, pass the error
      return handler.next(err);
    }
    
    // Wait before retrying (exponential backoff)
    final delay = retryDelay * (retryCount + 1);
    await Future.delayed(delay);
    
    // Increment retry count
    err.requestOptions.extra['retryCount'] = retryCount + 1;
    
    print('Retrying request (attempt ${retryCount + 1} of $maxRetries)...');
    
    try {
      // Retry the request
      final response = await dio.fetch(err.requestOptions);
      return handler.resolve(response);
    } on DioException catch (e) {
      // Retry failed, pass to next error handler
      return handler.next(e);
    }
  }
  
  bool _shouldRetry(DioException err) {
    // Retry on connection errors
    if (err.type == DioExceptionType.connectionError ||
        err.type == DioExceptionType.connectionTimeout ||
        err.type == DioExceptionType.receiveTimeout ||
        err.type == DioExceptionType.sendTimeout) {
      return true;
    }
    
    // Retry on certain server errors
    final statusCode = err.response?.statusCode;
    if (statusCode == 503 || statusCode == 502 || statusCode == 500) {
      return true;
    }
    
    return false;
  }
}

// lib/core/network/interceptors/error_interceptor.dart
import 'package:dio/dio.dart';

/// Transforms DioExceptions into app-specific error types.
/// 
/// This provides a consistent error handling experience throughout
/// the app, regardless of the underlying HTTP error.
class ErrorInterceptor extends Interceptor {
  @override
  void onError(DioException err, ErrorInterceptorHandler handler) {
    final apiError = ApiException.fromDioException(err);
    
    // Create a new DioException with our ApiException as the error
    handler.next(DioException(
      requestOptions: err.requestOptions,
      error: apiError,
      response: err.response,
      type: err.type,
    ));
  }
}

/// Custom exception type for API errors.
/// 
/// Provides user-friendly messages and categorizes errors
/// for appropriate handling in the UI.
class ApiException implements Exception {
  final String message;
  final String? userMessage;
  final int? statusCode;
  final ApiErrorType type;
  final dynamic originalError;

  ApiException({
    required this.message,
    this.userMessage,
    this.statusCode,
    required this.type,
    this.originalError,
  });

  factory ApiException.fromDioException(DioException err) {
    switch (err.type) {
      case DioExceptionType.connectionTimeout:
      case DioExceptionType.sendTimeout:
      case DioExceptionType.receiveTimeout:
        return ApiException(
          message: 'Request timed out: ${err.message}',
          userMessage: 'The request took too long. Please try again.',
          type: ApiErrorType.timeout,
          originalError: err,
        );
        
      case DioExceptionType.connectionError:
        return ApiException(
          message: 'Connection error: ${err.message}',
          userMessage: 'Unable to connect. Please check your internet connection.',
          type: ApiErrorType.network,
          originalError: err,
        );
        
      case DioExceptionType.badResponse:
        return _fromStatusCode(
          err.response?.statusCode,
          err.response?.data,
          err,
        );
        
      case DioExceptionType.cancel:
        return ApiException(
          message: 'Request cancelled',
          userMessage: 'Request was cancelled.',
          type: ApiErrorType.cancelled,
          originalError: err,
        );
        
      default:
        return ApiException(
          message: 'Unknown error: ${err.message}',
          userMessage: 'An unexpected error occurred. Please try again.',
          type: ApiErrorType.unknown,
          originalError: err,
        );
    }
  }

  static ApiException _fromStatusCode(
    int? statusCode,
    dynamic responseData,
    DioException err,
  ) {
    // Try to extract error message from response
    String? serverMessage;
    if (responseData is Map) {
      serverMessage = responseData['message'] ??
          responseData['error'] ??
          responseData['error_description'];
    }

    switch (statusCode) {
      case 400:
        return ApiException(
          message: serverMessage ?? 'Bad request',
          userMessage: serverMessage ?? 'Invalid request. Please check your input.',
          statusCode: 400,
          type: ApiErrorType.validation,
          originalError: err,
        );
        
      case 401:
        return ApiException(
          message: 'Unauthorized',
          userMessage: 'Please log in to continue.',
          statusCode: 401,
          type: ApiErrorType.unauthorized,
          originalError: err,
        );
        
      case 403:
        return ApiException(
          message: 'Forbidden',
          userMessage: 'You do not have permission to perform this action.',
          statusCode: 403,
          type: ApiErrorType.forbidden,
          originalError: err,
        );
        
      case 404:
        return ApiException(
          message: 'Not found',
          userMessage: 'The requested resource was not found.',
          statusCode: 404,
          type: ApiErrorType.notFound,
          originalError: err,
        );
        
      case 422:
        return ApiException(
          message: serverMessage ?? 'Validation error',
          userMessage: serverMessage ?? 'Please check your input.',
          statusCode: 422,
          type: ApiErrorType.validation,
          originalError: err,
        );
        
      case 429:
        return ApiException(
          message: 'Rate limited',
          userMessage: 'Too many requests. Please wait a moment and try again.',
          statusCode: 429,
          type: ApiErrorType.rateLimited,
          originalError: err,
        );
        
      case 500:
      case 502:
      case 503:
        return ApiException(
          message: 'Server error: $statusCode',
          userMessage: 'Server error. Please try again later.',
          statusCode: statusCode,
          type: ApiErrorType.server,
          originalError: err,
        );
        
      default:
        return ApiException(
          message: 'HTTP $statusCode: ${serverMessage ?? 'Unknown error'}',
          userMessage: serverMessage ?? 'An error occurred. Please try again.',
          statusCode: statusCode,
          type: ApiErrorType.unknown,
          originalError: err,
        );
    }
  }

  @override
  String toString() => 'ApiException($type): $message';
}

enum ApiErrorType {
  network,
  timeout,
  unauthorized,
  forbidden,
  notFound,
  validation,
  rateLimited,
  server,
  cancelled,
  unknown,
}
```
