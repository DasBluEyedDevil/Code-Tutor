---
type: "THEORY"
title: "Error Handling and Retry Strategies"
---

Robust background tasks require sophisticated error handling. Different failures need different responses.

**Types of Failures:**

1. **Transient Failures** (Should Retry)
   - Network timeout
   - Database connection lost
   - Rate limited (429 response)
   - Service temporarily unavailable (503)

2. **Permanent Failures** (Should NOT Retry)
   - Invalid data (validation error)
   - Resource not found (404)
   - Authentication failed (401)
   - Bad request (400)

3. **Poisoned Messages** (Quarantine)
   - Tasks that crash the worker
   - Tasks stuck in infinite loops
   - Tasks consuming excessive resources

**Retry Strategies:**

```dart
// 1. Immediate Retry (use sparingly)
await retryOperation(operation, maxAttempts: 3, delay: Duration.zero);

// 2. Fixed Delay (simple but not optimal)
await retryOperation(operation, maxAttempts: 5, delay: Duration(seconds: 5));
// Waits: 5s, 5s, 5s, 5s

// 3. Exponential Backoff (recommended)
final delays = [1, 2, 4, 8, 16]; // seconds
for (var attempt = 0; attempt < delays.length; attempt++) {
  try {
    await operation();
    break;
  } catch (e) {
    if (attempt == delays.length - 1) rethrow;
    await Future.delayed(Duration(seconds: delays[attempt]));
  }
}
// Waits: 1s, 2s, 4s, 8s, 16s

// 4. Exponential Backoff with Jitter (best for distributed systems)
final baseDelay = Duration(seconds: pow(2, attempt).toInt());
final jitter = Duration(milliseconds: Random().nextInt(1000));
await Future.delayed(baseDelay + jitter);
// Prevents thundering herd when many tasks retry simultaneously
```

**Circuit Breaker Pattern:**

When a service is down, stop hammering it:

```dart
class CircuitBreaker {
  int _failureCount = 0;
  DateTime? _openedAt;
  static const maxFailures = 5;
  static const resetTimeout = Duration(minutes: 1);
  
  bool get isOpen {
    if (_openedAt == null) return false;
    if (DateTime.now().difference(_openedAt!) > resetTimeout) {
      _halfOpen();
      return false;
    }
    return true;
  }
  
  Future<T> execute<T>(Future<T> Function() operation) async {
    if (isOpen) {
      throw CircuitOpenException('Circuit breaker is open');
    }
    
    try {
      final result = await operation();
      _recordSuccess();
      return result;
    } catch (e) {
      _recordFailure();
      rethrow;
    }
  }
  
  void _recordSuccess() {
    _failureCount = 0;
    _openedAt = null;
  }
  
  void _recordFailure() {
    _failureCount++;
    if (_failureCount >= maxFailures) {
      _openedAt = DateTime.now();
    }
  }
  
  void _halfOpen() {
    _failureCount = maxFailures - 1; // One more failure reopens
  }
}
```

