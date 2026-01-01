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
    // TODO: Implement logic to determine if error is retryable
    // TransientError should be retried
    // PermanentError should NOT be retried
    return false;
  }
  
  /// Calculate delay for a given attempt number.
  Duration calculateDelay(int attemptNumber) {
    // TODO: Implement exponential backoff with optional jitter
    // Formula: baseDelay * (2 ^ attemptNumber)
    // Add random jitter (0-25% of delay) if useJitter is true
    // Cap at maxDelay
    return Duration.zero;
  }
  
  /// Execute an operation with retry logic.
  Future<RetryResult<T>> execute<T>(Future<T> Function() operation) async {
    // TODO: Implement the retry loop
    // 1. Try to execute the operation
    // 2. On success, return RetryResult.success
    // 3. On failure:
    //    a. Check if error is retryable
    //    b. If not retryable, return RetryResult.failure immediately
    //    c. If retryable and attempts remaining, wait and retry
    //    d. If no attempts remaining, return RetryResult.failure
    
    return RetryResult.failure('Not implemented', 0);
  }
}

// Test the implementation
void main() async {
  print('=== Testing RetryExecutor ===\n');
  
  final executor = RetryExecutor(
    config: RetryConfig(
      maxAttempts: 5,
      baseDelay: Duration(milliseconds: 100),
      maxDelay: Duration(seconds: 10),
      useJitter: false, // Disable for predictable testing
    ),
  );
  
  // Test 1: Immediate success
  print('Test 1: Immediate success');
  var result = await executor.execute(() async {
    return 42;
  });
  print(result);
  print('Expected: Success after 1 attempt(s): 42\n');
  
  // Test 2: Success after retries
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
  
  // Test 3: Permanent failure (no retries)
  print('Test 3: Permanent failure (should not retry)');
  result = await executor.execute(() async {
    throw PermanentError('Invalid credentials');
  });
  print(result);
  print('Expected: Failed after 1 attempt(s): ...\n');
  
  // Test 4: Exhaust all retries
  print('Test 4: Exhaust all retries');
  result = await executor.execute(() async {
    throw TransientError('Service unavailable');
  });
  print(result);
  print('Expected: Failed after 5 attempt(s): ...\n');
  
  // Test 5: Calculate delay
  print('Test 5: Delay calculation (without jitter)');
  print('Attempt 0: ${executor.calculateDelay(0).inMilliseconds}ms (expected: 100)');
  print('Attempt 1: ${executor.calculateDelay(1).inMilliseconds}ms (expected: 200)');
  print('Attempt 2: ${executor.calculateDelay(2).inMilliseconds}ms (expected: 400)');
  print('Attempt 3: ${executor.calculateDelay(3).inMilliseconds}ms (expected: 800)');
}