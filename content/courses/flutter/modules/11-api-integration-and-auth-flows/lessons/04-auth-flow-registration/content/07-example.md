---
type: "EXAMPLE"
title: "Comprehensive Error Handling"
---

Production-quality apps need robust error handling that provides helpful feedback to users while logging issues for developers. Here is a comprehensive error handling strategy.

**Create a Custom Exception Hierarchy**

Create `lib/exceptions/auth_exceptions.dart`:

```dart
/// Base exception for all authentication-related errors.
abstract class AuthException implements Exception {
  final String message;
  final String code;
  final dynamic originalError;
  
  const AuthException(this.message, this.code, [this.originalError]);
  
  @override
  String toString() => 'AuthException($code): $message';
}

/// Thrown when user tries to register with an existing email.
class EmailAlreadyExistsException extends AuthException {
  const EmailAlreadyExistsException()
      : super(
          'An account with this email already exists.',
          'email_exists',
        );
}

/// Thrown when email format is invalid.
class InvalidEmailException extends AuthException {
  const InvalidEmailException()
      : super(
          'Please enter a valid email address.',
          'invalid_email',
        );
}

/// Thrown when password does not meet requirements.
class WeakPasswordException extends AuthException {
  final List<String> requirements;
  
  const WeakPasswordException({this.requirements = const []})
      : super(
          'Password does not meet security requirements.',
          'weak_password',
        );
}

/// Thrown when network connection fails.
class NetworkException extends AuthException {
  const NetworkException([dynamic originalError])
      : super(
          'No internet connection. Please check your network.',
          'network_error',
          originalError,
        );
}

/// Thrown when server returns an error.
class ServerException extends AuthException {
  final int? statusCode;
  
  const ServerException({this.statusCode, dynamic originalError})
      : super(
          'Server error. Please try again later.',
          'server_error',
          originalError,
        );
}

/// Thrown when too many requests are made.
class RateLimitException extends AuthException {
  final Duration? retryAfter;
  
  const RateLimitException({this.retryAfter})
      : super(
          'Too many attempts. Please wait before trying again.',
          'rate_limited',
        );
}

/// Thrown when an unknown error occurs.
class UnknownAuthException extends AuthException {
  const UnknownAuthException([dynamic originalError])
      : super(
          'An unexpected error occurred. Please try again.',
          'unknown',
          originalError,
        );
}
```

**Create an Error Handler Utility**

Create `lib/utils/error_handler.dart`:

```dart
import 'dart:io';
import 'package:flutter/foundation.dart';
import 'package:serverpod_flutter/serverpod_flutter.dart';
import '../exceptions/auth_exceptions.dart';

/// Utility class for handling and converting errors to user-friendly messages.
class ErrorHandler {
  /// Converts any exception to an AuthException for consistent handling.
  static AuthException handleError(dynamic error) {
    // Log the error for debugging (in debug mode only)
    if (kDebugMode) {
      print('AuthError: $error');
      if (error is Error) {
        print('Stack trace: ${error.stackTrace}');
      }
    }
    
    // Handle specific exception types
    if (error is AuthException) {
      return error;
    }
    
    if (error is SocketException) {
      return NetworkException(error);
    }
    
    if (error is ServerpodClientException) {
      return _handleServerpodError(error);
    }
    
    // Handle timeout errors
    if (error.toString().contains('TimeoutException')) {
      return const NetworkException();
    }
    
    return UnknownAuthException(error);
  }
  
  static AuthException _handleServerpodError(ServerpodClientException error) {
    switch (error.statusCode) {
      case 400:
        return const InvalidEmailException();
      case 401:
        return const UnknownAuthException();
      case 409:
        return const EmailAlreadyExistsException();
      case 422:
        return const WeakPasswordException();
      case 429:
        final retryAfter = error.response?.headers['retry-after'];
        final duration = retryAfter != null
            ? Duration(seconds: int.tryParse(retryAfter) ?? 60)
            : null;
        return RateLimitException(retryAfter: duration);
      case 500:
      case 502:
      case 503:
        return ServerException(statusCode: error.statusCode, originalError: error);
      default:
        return UnknownAuthException(error);
    }
  }
  
  /// Returns a user-friendly error message based on the exception.
  static String getUserFriendlyMessage(AuthException error) {
    // You can customize messages here based on context
    return error.message;
  }
  
  /// Returns an appropriate icon for the error type.
  static IconData getErrorIcon(AuthException error) {
    switch (error.code) {
      case 'network_error':
        return Icons.wifi_off;
      case 'server_error':
        return Icons.cloud_off;
      case 'rate_limited':
        return Icons.timer_off;
      case 'email_exists':
        return Icons.person;
      default:
        return Icons.error_outline;
    }
  }
}
```

**Display Errors with Context-Aware UI**

Create a reusable error display widget. Create `lib/widgets/error_display.dart`:

```dart
import 'package:flutter/material.dart';
import '../exceptions/auth_exceptions.dart';
import '../utils/error_handler.dart';

/// A context-aware error display widget that shows appropriate UI based on error type.
class ErrorDisplay extends StatelessWidget {
  final AuthException error;
  final VoidCallback? onRetry;
  final VoidCallback? onDismiss;
  
  const ErrorDisplay({
    super.key,
    required this.error,
    this.onRetry,
    this.onDismiss,
  });
  
  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    final isRetryable = _isRetryableError(error);
    
    return Container(
      padding: const EdgeInsets.all(16),
      decoration: BoxDecoration(
        color: theme.colorScheme.errorContainer,
        borderRadius: BorderRadius.circular(12),
        border: Border.all(
          color: theme.colorScheme.error.withOpacity(0.3),
        ),
      ),
      child: Column(
        mainAxisSize: MainAxisSize.min,
        children: [
          Row(
            children: [
              Icon(
                ErrorHandler.getErrorIcon(error),
                color: theme.colorScheme.error,
                size: 24,
              ),
              const SizedBox(width: 12),
              Expanded(
                child: Text(
                  ErrorHandler.getUserFriendlyMessage(error),
                  style: TextStyle(
                    color: theme.colorScheme.onErrorContainer,
                  ),
                ),
              ),
              if (onDismiss != null)
                IconButton(
                  icon: const Icon(Icons.close),
                  onPressed: onDismiss,
                  color: theme.colorScheme.onErrorContainer,
                  iconSize: 20,
                ),
            ],
          ),
          if (isRetryable && onRetry != null) ...[
            const SizedBox(height: 12),
            SizedBox(
              width: double.infinity,
              child: OutlinedButton.icon(
                onPressed: onRetry,
                icon: const Icon(Icons.refresh),
                label: const Text('Try Again'),
                style: OutlinedButton.styleFrom(
                  foregroundColor: theme.colorScheme.error,
                ),
              ),
            ),
          ],
          // Special handling for email exists error
          if (error is EmailAlreadyExistsException) ...[
            const SizedBox(height: 8),
            TextButton(
              onPressed: () {
                Navigator.of(context).pushReplacementNamed('/login');
              },
              child: const Text('Sign in instead'),
            ),
          ],
          // Special handling for rate limiting
          if (error is RateLimitException) ...[
            const SizedBox(height: 8),
            Text(
              'Please wait ${(error as RateLimitException).retryAfter?.inSeconds ?? 60} seconds',
              style: theme.textTheme.bodySmall?.copyWith(
                color: theme.colorScheme.onErrorContainer.withOpacity(0.7),
              ),
            ),
          ],
        ],
      ),
    );
  }
  
  bool _isRetryableError(AuthException error) {
    return error is NetworkException ||
           error is ServerException ||
           error is UnknownAuthException;
  }
}
```

This comprehensive error handling system provides:
- Type-safe exception handling with a custom hierarchy
- Context-aware error messages
- Retryable error detection
- Special UI for specific error types (like "sign in instead" for existing emails)
- Rate limit countdown display

