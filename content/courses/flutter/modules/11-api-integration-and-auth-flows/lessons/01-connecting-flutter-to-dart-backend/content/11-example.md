---
type: "EXAMPLE"
title: "Building a Robust API Call Wrapper"
---


Here is a utility class that wraps API calls with consistent error handling and retry logic:



```dart
// lib/services/api_result.dart

/// Represents the result of an API call - either success with data or failure with error.
/// This pattern avoids throwing exceptions for expected error cases.
sealed class ApiResult<T> {
  const ApiResult();
}

class ApiSuccess<T> extends ApiResult<T> {
  final T data;
  const ApiSuccess(this.data);
}

class ApiFailure<T> extends ApiResult<T> {
  final ApiError error;
  const ApiFailure(this.error);
}

/// Categorized API errors for different handling strategies
enum ApiErrorType {
  network,      // No internet, server unreachable
  timeout,      // Request took too long
  unauthorized, // Need to log in or refresh token
  forbidden,    // Logged in but not allowed
  notFound,     // Resource does not exist
  validation,   // Invalid input data
  server,       // Server-side error (500)
  unknown,      // Unexpected error
}

class ApiError {
  final ApiErrorType type;
  final String message;
  final int? statusCode;
  final dynamic originalError;
  
  const ApiError({
    required this.type,
    required this.message,
    this.statusCode,
    this.originalError,
  });
  
  /// Check if this error might be resolved by retrying
  bool get isRetryable => type == ApiErrorType.network || type == ApiErrorType.timeout;
}

// lib/services/api_caller.dart
import 'dart:async';
import 'dart:io';
import 'package:serverpod_flutter/serverpod_flutter.dart';

/// Utility class that wraps API calls with error handling and optional retry logic.
class ApiCaller {
  final int maxRetries;
  final Duration retryDelay;
  
  const ApiCaller({
    this.maxRetries = 3,
    this.retryDelay = const Duration(seconds: 1),
  });
  
  /// Execute an API call with automatic error categorization.
  Future<ApiResult<T>> call<T>(Future<T> Function() apiCall) async {
    try {
      final result = await apiCall();
      return ApiSuccess(result);
    } on ServerpodClientException catch (e) {
      return ApiFailure(_categorizeServerpodError(e));
    } on SocketException catch (e) {
      return ApiFailure(ApiError(
        type: ApiErrorType.network,
        message: 'Cannot connect to server. Check your internet connection.',
        originalError: e,
      ));
    } on TimeoutException catch (e) {
      return ApiFailure(ApiError(
        type: ApiErrorType.timeout,
        message: 'Request timed out. Please try again.',
        originalError: e,
      ));
    } catch (e) {
      return ApiFailure(ApiError(
        type: ApiErrorType.unknown,
        message: 'An unexpected error occurred.',
        originalError: e,
      ));
    }
  }
  
  /// Execute an API call with automatic retry for transient failures.
  Future<ApiResult<T>> callWithRetry<T>(Future<T> Function() apiCall) async {
    ApiResult<T>? lastResult;
    
    for (int attempt = 0; attempt < maxRetries; attempt++) {
      lastResult = await call(apiCall);
      
      // If successful, return immediately
      if (lastResult is ApiSuccess<T>) {
        return lastResult;
      }
      
      // If not retryable, return the failure
      final failure = lastResult as ApiFailure<T>;
      if (!failure.error.isRetryable) {
        return failure;
      }
      
      // Wait before retrying (exponential backoff)
      if (attempt < maxRetries - 1) {
        await Future.delayed(retryDelay * (attempt + 1));
      }
    }
    
    return lastResult!;
  }
  
  ApiError _categorizeServerpodError(ServerpodClientException e) {
    final statusCode = e.statusCode;
    
    if (statusCode == 401) {
      return ApiError(
        type: ApiErrorType.unauthorized,
        message: 'Please log in to continue.',
        statusCode: statusCode,
        originalError: e,
      );
    } else if (statusCode == 403) {
      return ApiError(
        type: ApiErrorType.forbidden,
        message: 'You do not have permission to perform this action.',
        statusCode: statusCode,
        originalError: e,
      );
    } else if (statusCode == 404) {
      return ApiError(
        type: ApiErrorType.notFound,
        message: 'The requested resource was not found.',
        statusCode: statusCode,
        originalError: e,
      );
    } else if (statusCode == 400 || statusCode == 422) {
      return ApiError(
        type: ApiErrorType.validation,
        message: e.message,
        statusCode: statusCode,
        originalError: e,
      );
    } else if (statusCode >= 500) {
      return ApiError(
        type: ApiErrorType.server,
        message: 'Server error. Please try again later.',
        statusCode: statusCode,
        originalError: e,
      );
    } else {
      return ApiError(
        type: ApiErrorType.unknown,
        message: e.message,
        statusCode: statusCode,
        originalError: e,
      );
    }
  }
}
```
