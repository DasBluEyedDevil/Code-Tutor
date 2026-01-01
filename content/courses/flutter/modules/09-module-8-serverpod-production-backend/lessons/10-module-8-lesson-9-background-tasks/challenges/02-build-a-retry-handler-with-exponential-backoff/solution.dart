import 'dart:async';
import 'dart:math';

/// Result of a retry operation.
class RetryResult<T> {
  final bool success;
  final T? value;
  final int attempts;
  final String? errorMessage;
  
  RetryResult.success(this.value, this.attempts)
      : success = true,
        errorMessage = null;
  
  RetryResult.failure(this.errorMessage, this.attempts)
      : success = false,
        value = null;
  
  @override
  String toString() {
    if (success) {
      return 'Success after $attempts attempt(s): $value';
    }
    return 'Failed after $attempts attempt(s): $errorMessage';
  }
}

/// Configuration for retry behavior.
class RetryConfig {
  final int maxAttempts;
  final Duration baseDelay;
  final Duration maxDelay;
  final bool useJitter;
  
  const RetryConfig({
    this.maxAttempts = 5,
    this.baseDelay = const Duration(milliseconds: 100),
    this.maxDelay = const Duration(seconds: 30),
    this.useJitter = true,
  });
}

/// Custom exception types for testing.
class TransientError implements Exception {
  final String message;
  TransientError(this.message);
  @override
  String toString() => 'TransientError: $message';
}

class PermanentError implements Exception {
  final String message;
  PermanentError(this.message);
  @override
  String toString() => 'PermanentError: $message';
}

/// Executes operations with retry logic.
class RetryExecutor {
  final RetryConfig config;
  final Random _random = Random();
  
  RetryExecutor({this.config = const RetryConfig()});
  
  /// Determine if an error should trigger a retry.
  bool shouldRetry(Object error) {
    if (error is TransientError) return true;
    if (error is PermanentError) return false;
    
    final message = error.toString().toLowerCase();
    if (message.contains('invalid') ||
        message.contains('unauthorized') ||
        message.contains('forbidden') ||
        message.contains('not found') ||
        message.contains('bad request')) {
      return false;
    }
    if (message.contains('timeout') ||
        message.contains('connection') ||
        message.contains('unavailable') ||
        message.contains('rate limit')) {
      return true;
    }
    return true;
  }
  
  /// Calculate delay for a given attempt number.
  Duration calculateDelay(int attemptNumber) {
    final multiplier = _pow(2, attemptNumber);
    var delayMs = config.baseDelay.inMilliseconds * multiplier;
    delayMs = delayMs.clamp(0, config.maxDelay.inMilliseconds);
    
    if (config.useJitter && delayMs > 0) {
      final maxJitter = (delayMs * 0.25).toInt();
      if (maxJitter > 0) {
        delayMs += _random.nextInt(maxJitter);
      }
    }
    
    return Duration(milliseconds: delayMs);
  }
  
  int _pow(int base, int exponent) {
    int result = 1;
    for (int i = 0; i < exponent; i++) {
      result *= base;
    }
    return result;
  }
  
  /// Execute an operation with retry logic.
  Future<RetryResult<T>> execute<T>(Future<T> Function() operation) async {
    Object? lastError;
    
    for (int attempt = 1; attempt <= config.maxAttempts; attempt++) {
      try {
        final result = await operation();
        return RetryResult.success(result, attempt);
      } catch (e) {
        lastError = e;
        
        if (!shouldRetry(e)) {
          return RetryResult.failure(e.toString(), attempt);
        }
        
        if (attempt >= config.maxAttempts) {
          return RetryResult.failure(e.toString(), attempt);
        }
        
        final delay = calculateDelay(attempt - 1);
        await Future.delayed(delay);
      }
    }
    
    return RetryResult.failure(
      lastError?.toString() ?? 'Unknown error',
      config.maxAttempts,
    );
  }
}

void main() async {
  print('=== Testing RetryExecutor ===\n');
  
  final executor = RetryExecutor(
    config: RetryConfig(
      maxAttempts: 5,
      baseDelay: Duration(milliseconds: 100),
      maxDelay: Duration(seconds: 10),
      useJitter: false,
    ),
  );
  
  print('Test 1: Immediate success');
  var result = await executor.execute(() async {
    return 42;
  });
  print(result);
  print('Expected: Success after 1 attempt(s): 42\n');
  
  print('Test 2: Success after transient failures');
  var attempts = 0;
  result = await executor.execute(() async {
    attempts++;
    if (attempts < 3) {
      throw TransientError('Network timeout');
    }
    return 'recovered';
  });
  print(result);
  print('Expected: Success after 3 attempt(s): recovered\n');
  
  print('Test 3: Permanent failure (should not retry)');
  result = await executor.execute(() async {
    throw PermanentError('Invalid credentials');
  });
  print(result);
  print('Expected: Failed after 1 attempt(s): ...\n');
  
  print('Test 4: Exhaust all retries');
  result = await executor.execute(() async {
    throw TransientError('Service unavailable');
  });
  print(result);
  print('Expected: Failed after 5 attempt(s): ...\n');
  
  print('Test 5: Delay calculation (without jitter)');
  print('Attempt 0: ${executor.calculateDelay(0).inMilliseconds}ms (expected: 100)');
  print('Attempt 1: ${executor.calculateDelay(1).inMilliseconds}ms (expected: 200)');
  print('Attempt 2: ${executor.calculateDelay(2).inMilliseconds}ms (expected: 400)');
  print('Attempt 3: ${executor.calculateDelay(3).inMilliseconds}ms (expected: 800)');
}