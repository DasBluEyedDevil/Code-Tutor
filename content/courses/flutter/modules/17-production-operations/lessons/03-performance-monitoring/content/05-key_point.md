---
type: "KEY_POINT"
title: "Custom Traces for Critical Operations"
---


**When to Use Custom Traces**

Create custom traces for operations you want to optimize:

- **Authentication flows** - Login, signup, password reset
- **Data operations** - Sync, backup, export
- **Complex UI** - Image galleries, heavy lists
- **Business-critical paths** - Checkout, booking, submission

**Trace Naming Conventions**

```dart
// Good names - descriptive and consistent
'user_login'
'checkout_complete'
'image_upload'
'data_sync'

// Bad names - too generic or inconsistent
'trace1'
'operation'
'doStuff'
```

**Adding Metrics to Traces**

Metrics let you track quantities within a trace:

```dart
final trace = performance.newTrace('data_sync');
await trace.start();

// Track how many items were synced
trace.incrementMetric('items_synced', 42);
trace.incrementMetric('bytes_transferred', 102400);
trace.incrementMetric('retries', 2);

await trace.stop();
```

**Adding Attributes for Filtering**

Attributes let you filter traces in the dashboard:

```dart
final trace = performance.newTrace('user_login');

// Add attributes before starting
trace.putAttribute('login_method', 'email');
trace.putAttribute('user_tier', 'premium');
trace.putAttribute('region', 'us-west');

await trace.start();
// ... login logic ...
await trace.stop();
```

**Attribute Limits**

- Max 5 custom attributes per trace
- Attribute names: 1-40 characters
- Attribute values: 1-100 characters
- Only alphanumeric and underscores

