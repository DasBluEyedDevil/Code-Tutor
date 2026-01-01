---
type: "THEORY"
title: "Queue Management"
---


**Sync Queue Best Practices:**

**1. Operation Ordering:**
- Process in FIFO order
- Handle dependencies (create parent before child)
- Batch related operations

**2. Retry Strategy:**
- Exponential backoff (1s, 2s, 4s, 8s...)
- Maximum retry count (5-10 attempts)
- Different strategies for different errors

**3. Error Handling:**
- Network errors: Retry later
- 4xx errors: May need user intervention
- 5xx errors: Retry with backoff
- Conflict errors: Apply resolution strategy

**4. Queue Persistence:**
- Store queue in database (survives app restart)
- Process on app launch
- Process when connectivity restored



```dart
class RetryStrategy {
  static Duration getBackoffDuration(int retryCount) {
    // Exponential backoff with jitter
    final baseDelay = Duration(seconds: math.pow(2, retryCount).toInt());
    final jitter = Duration(
      milliseconds: Random().nextInt(1000),
    );
    return baseDelay + jitter;
  }
  
  static bool shouldRetry(int statusCode, int retryCount) {
    if (retryCount >= 5) return false;
    
    // Retry on network errors and server errors
    if (statusCode >= 500) return true;
    if (statusCode == 408) return true; // Timeout
    if (statusCode == 429) return true; // Rate limited
    
    // Don't retry on client errors (except specific ones)
    if (statusCode >= 400 && statusCode < 500) return false;
    
    return true;
  }
}

// Listen for connectivity changes
class ConnectivityAwareSyncQueue extends SyncQueue {
  StreamSubscription? _connectivitySubscription;
  
  void startListening() {
    _connectivitySubscription = Connectivity()
        .onConnectivityChanged
        .listen((result) {
      if (result != ConnectivityResult.none) {
        // Back online - process queue
        processQueue();
      }
    });
  }
  
  void dispose() {
    _connectivitySubscription?.cancel();
  }
}
```
