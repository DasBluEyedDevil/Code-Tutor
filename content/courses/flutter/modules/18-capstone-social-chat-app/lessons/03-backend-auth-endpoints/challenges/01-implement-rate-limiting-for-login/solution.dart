# server/lib/src/protocol/login_attempt.yaml
class: LoginAttempt
table: login_attempts
fields:
  email: String
  attemptedAt: DateTime
  ipAddress: String?
  wasSuccessful: bool
  userAgent: String?

indexes:
  login_attempt_email_idx:
    fields: email, attemptedAt
  login_attempt_time_idx:
    fields: attemptedAt

---

// server/lib/src/services/rate_limiter.dart
import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

class RateLimiter {
  static const int maxAttempts = 5;
  static const Duration cooldownPeriod = Duration(minutes: 15);
  
  /// Check if login is allowed for this email
  Future<RateLimitInfo?> checkLoginAllowed(
    Session session,
    String email,
  ) async {
    final cutoffTime = DateTime.now().subtract(cooldownPeriod);
    
    // Query recent failed attempts
    final recentAttempts = await LoginAttempt.db.find(
      session,
      where: (t) => t.email.equals(email.toLowerCase()) &
                    t.attemptedAt.greaterThan(cutoffTime) &
                    t.wasSuccessful.equals(false),
      orderBy: (t) => t.attemptedAt,
      orderDescending: true,
    );
    
    if (recentAttempts.length >= maxAttempts) {
      // Find when the oldest attempt in the window occurred
      final oldestAttempt = recentAttempts.last;
      final blockedUntil = oldestAttempt.attemptedAt.add(cooldownPeriod);
      final remainingTime = blockedUntil.difference(DateTime.now());
      
      if (remainingTime.isNegative) {
        // Cooldown has passed
        return null;
      }
      
      return RateLimitInfo(
        attemptsCount: recentAttempts.length,
        blockedUntil: blockedUntil,
        remainingTime: remainingTime,
      );
    }
    
    return null; // Login allowed
  }
  
  /// Record a login attempt
  Future<void> recordAttempt(
    Session session,
    String email,
    bool wasSuccessful, {
    String? ipAddress,
    String? userAgent,
  }) async {
    await LoginAttempt.db.insertRow(
      session,
      LoginAttempt(
        email: email.toLowerCase(),
        attemptedAt: DateTime.now(),
        ipAddress: ipAddress,
        wasSuccessful: wasSuccessful,
        userAgent: userAgent,
      ),
    );
    
    // Optionally clean up old attempts (older than 24 hours)
    final cleanupCutoff = DateTime.now().subtract(Duration(hours: 24));
    await LoginAttempt.db.deleteWhere(
      session,
      where: (t) => t.attemptedAt.lessThan(cleanupCutoff),
    );
  }
  
  /// Get remaining attempts before lockout
  Future<int> getRemainingAttempts(
    Session session,
    String email,
  ) async {
    final cutoffTime = DateTime.now().subtract(cooldownPeriod);
    
    final failedCount = await LoginAttempt.db.count(
      session,
      where: (t) => t.email.equals(email.toLowerCase()) &
                    t.attemptedAt.greaterThan(cutoffTime) &
                    t.wasSuccessful.equals(false),
    );
    
    return (maxAttempts - failedCount).clamp(0, maxAttempts);
  }
  
  /// Clear failed attempts after successful login (optional)
  Future<void> clearFailedAttempts(
    Session session,
    String email,
  ) async {
    final cutoffTime = DateTime.now().subtract(cooldownPeriod);
    
    await LoginAttempt.db.deleteWhere(
      session,
      where: (t) => t.email.equals(email.toLowerCase()) &
                    t.attemptedAt.greaterThan(cutoffTime) &
                    t.wasSuccessful.equals(false),
    );
  }
}

class RateLimitInfo {
  final int attemptsCount;
  final DateTime blockedUntil;
  final Duration remainingTime;
  
  RateLimitInfo({
    required this.attemptsCount,
    required this.blockedUntil,
    required this.remainingTime,
  });
  
  String get message => 
    'Too many failed attempts. Try again in ${remainingTime.inMinutes} minutes.';
}