// lib/services/auth_analytics_service.dart

import 'package:flutter/foundation.dart';

/// Analytics events for authentication tracking.
enum AuthAnalyticsEvent {
  loginAttempted,
  loginSucceeded,
  loginFailed,
  loginCancelled,
  accountLinked,
  accountUnlinked,
  loggedOut,
}

/// Abstract interface for analytics providers.
abstract class AnalyticsProvider {
  Future<void> logEvent(String name, Map<String, dynamic> parameters);
  Future<void> setUserProperty(String name, String value);
}

/// Service for tracking authentication analytics.
class AuthAnalyticsService {
  final AnalyticsProvider _provider;
  
  AuthAnalyticsService({required AnalyticsProvider provider})
      : _provider = provider;
  
  /// Tracks a login attempt.
  Future<void> trackLoginAttempt(String provider) async {
    await _logEvent('auth_login_attempt', {
      'provider': provider,
      'timestamp': DateTime.now().toIso8601String(),
    });
  }
  
  /// Tracks a successful login.
  Future<void> trackLoginSuccess(
    String provider, {
    bool isNewUser = false,
  }) async {
    await _logEvent('auth_login_success', {
      'provider': provider,
      'is_new_user': isNewUser,
      'timestamp': DateTime.now().toIso8601String(),
    });
    
    // Set user property for preferred login method
    await _provider.setUserProperty('preferred_auth_provider', provider);
  }
  
  /// Tracks a failed login with error details.
  Future<void> trackLoginFailure(String provider, String errorCode) async {
    // Sanitize error code - only log known, safe codes
    final safeErrorCode = _sanitizeErrorCode(errorCode);
    
    await _logEvent('auth_login_failure', {
      'provider': provider,
      'error_code': safeErrorCode,
      'timestamp': DateTime.now().toIso8601String(),
    });
  }
  
  /// Tracks when user cancels login.
  Future<void> trackLoginCancelled(String provider) async {
    await _logEvent('auth_login_cancelled', {
      'provider': provider,
      'timestamp': DateTime.now().toIso8601String(),
    });
  }
  
  /// Tracks account linking.
  Future<void> trackAccountLink(String provider, bool success) async {
    await _logEvent('auth_account_link', {
      'provider': provider,
      'success': success,
      'timestamp': DateTime.now().toIso8601String(),
    });
  }
  
  /// Tracks account unlinking.
  Future<void> trackAccountUnlink(String provider, bool success) async {
    await _logEvent('auth_account_unlink', {
      'provider': provider,
      'success': success,
      'timestamp': DateTime.now().toIso8601String(),
    });
  }
  
  /// Tracks logout.
  Future<void> trackLogout() async {
    await _logEvent('auth_logout', {
      'timestamp': DateTime.now().toIso8601String(),
    });
  }
  
  /// Logs an event with error handling.
  Future<void> _logEvent(
    String name,
    Map<String, dynamic> parameters,
  ) async {
    try {
      await _provider.logEvent(name, parameters);
    } catch (e) {
      // Don't let analytics errors affect the app
      if (kDebugMode) {
        print('Analytics error: $e');
      }
    }
  }
  
  /// Sanitizes error codes to prevent logging sensitive information.
  String _sanitizeErrorCode(String code) {
    // Only allow known, safe error codes
    const safeErrorCodes = {
      'cancelled',
      'network_error',
      'invalid_credentials',
      'user_disabled',
      'account_exists',
      'not_available',
      'timeout',
      'unknown',
    };
    
    final lowerCode = code.toLowerCase();
    return safeErrorCodes.contains(lowerCode) ? lowerCode : 'unknown';
  }
}

// Example Firebase Analytics implementation:
class FirebaseAnalyticsProvider implements AnalyticsProvider {
  @override
  Future<void> logEvent(String name, Map<String, dynamic> parameters) async {
    // await FirebaseAnalytics.instance.logEvent(name: name, parameters: parameters);
  }
  
  @override
  Future<void> setUserProperty(String name, String value) async {
    // await FirebaseAnalytics.instance.setUserProperty(name: name, value: value);
  }
}