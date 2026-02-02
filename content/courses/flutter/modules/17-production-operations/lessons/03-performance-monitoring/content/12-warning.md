---
type: "WARNING"
title: "Performance Monitoring Best Practices and Pitfalls"
---


**Best Practices**

1. **Don't Over-Instrument**

Too many traces create overhead and noise:
```dart
// BAD - Trace every small operation
performance.newTrace('button_tap'); // Too granular!

// GOOD - Trace meaningful operations
performance.newTrace('checkout_flow'); // Business-critical
```

2. **Use Meaningful Names**

```dart
// BAD - Generic names
'trace_1', 'operation', 'api_call'

// GOOD - Descriptive names
'user_registration', 'product_search', 'order_creation'
```

3. **Add Context with Attributes**

```dart
// BAD - No context
trace.start();
await fetchProducts();
trace.stop();

// GOOD - With context for filtering
trace.putAttribute('category', selectedCategory);
trace.putAttribute('page_size', pageSize.toString());
trace.start();
await fetchProducts();
trace.stop();
```

4. **Monitor in Production, Not Just Development**

Development devices are not representative of user devices.

**Common Pitfalls**

1. **Forgetting to Stop Traces**

```dart
// BAD - Trace never stopped on error
trace.start();
await riskyOperation(); // If this throws, trace leaks
trace.stop();

// GOOD - Always stop in finally block
trace.start();
try {
  await riskyOperation();
} finally {
  trace.stop();
}
```

2. **Starting Traces Multiple Times**

```dart
// BAD - Can cause issues
if (condition) {
  trace.start(); // May already be started
}

// GOOD - Track state
if (!_isTraceActive) {
  trace.start();
  _isTraceActive = true;
}
```

3. **Blocking UI with Monitoring**

Performance monitoring itself should not impact performance. The Firebase SDK handles this well, but be careful with custom logic.

4. **Ignoring the Data**

Data is useless if you don't act on it:
- Set up regular reviews of performance dashboards
- Create alerts for regressions
- Make performance part of code review
- Set and track performance budgets

5. **Not Testing Monitoring Code**

```dart
// Create a testable service
class PerformanceService {
  PerformanceService.withPerformance(this._performance);
  
  // In tests, inject a mock
}
```

