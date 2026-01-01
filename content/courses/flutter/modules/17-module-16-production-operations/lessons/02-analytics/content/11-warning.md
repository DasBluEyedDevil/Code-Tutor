---
type: "WARNING"
title: "Analytics Best Practices and Pitfalls"
---


**Best Practices**

1. **Create an Event Naming Convention**

Document your naming scheme:
```
[action]_[object]
Examples: view_product, add_to_cart, complete_purchase
```

2. **Use a Tracking Plan**

Before implementing, create a spreadsheet:

| Event Name | When Fired | Parameters | Purpose |
|------------|------------|------------|----------|
| view_product | Product screen opens | item_id, category, price | Track product interest |

3. **Test in DebugView**

Firebase has a real-time debug view:
```bash
# Enable debug mode
adb shell setprop debug.firebase.analytics.app your.package.name
```

4. **Set Up Conversion Events**

Mark important events as conversions in Firebase Console for:
- Optimization targets
- Audience triggers
- Attribution

**Common Pitfalls**

1. **Not Waiting for Data**

Firebase batches events and may delay up to 1 hour in production. Don't panic if events don't appear immediately.

2. **Exceeding Limits**

- Max 500 distinct event types
- Max 25 parameters per event
- Max 25 user properties
- String values max 100 characters

3. **Tracking Sensitive Data**

Never put PII in events:
```dart
// BAD - Don't do this!
logEvent(name: 'purchase', parameters: {
  'email': user.email, // Privacy violation!
  'credit_card': '****1234', // Dangerous!
});

// GOOD - Use anonymized identifiers
logEvent(name: 'purchase', parameters: {
  'user_tier': 'premium',
  'payment_method': 'credit_card',
});
```

4. **Forgetting to Clear User ID on Logout**

```dart
// Always clear on logout
await analytics.setUserId(id: null);
```

