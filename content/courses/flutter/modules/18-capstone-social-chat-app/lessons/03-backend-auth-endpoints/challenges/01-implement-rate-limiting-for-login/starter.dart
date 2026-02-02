// server/lib/src/protocol/login_attempt.yaml
# TODO: Define the LoginAttempt model
# Fields needed:
# - email (indexed)
# - attemptedAt (timestamp)
# - ipAddress (optional)
# - wasSuccessful (bool)

---

// server/lib/src/services/rate_limiter.dart
import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

class RateLimiter {
  static const int maxAttempts = 5;
  static const Duration cooldownPeriod = Duration(minutes: 15);
  
  /// Check if login is allowed for this email
  /// Returns null if allowed, or RateLimitInfo if blocked
  Future<RateLimitInfo?> checkLoginAllowed(
    Session session,
    String email,
  ) async {
    // TODO: Implement this method
    // 1. Query recent failed attempts for this email
    // 2. Count attempts within cooldownPeriod
    // 3. If >= maxAttempts, return RateLimitInfo with details
    // 4. If allowed, return null
    throw UnimplementedError();
  }
  
  /// Record a login attempt
  Future<void> recordAttempt(
    Session session,
    String email,
    bool wasSuccessful, {
    String? ipAddress,
  }) async {
    // TODO: Implement this method
    // 1. Insert new LoginAttempt record
    // 2. If successful, optionally clear recent failures
    throw UnimplementedError();
  }
  
  /// Get remaining attempts before lockout
  Future<int> getRemainingAttempts(
    Session session,
    String email,
  ) async {
    // TODO: Implement this method
    throw UnimplementedError();
  }
}

/// Rate limit information returned when blocked
class RateLimitInfo {
  final int attemptsCount;
  final DateTime blockedUntil;
  final Duration remainingTime;
  
  RateLimitInfo({
    required this.attemptsCount,
    required this.blockedUntil,
    required this.remainingTime,
  });
}