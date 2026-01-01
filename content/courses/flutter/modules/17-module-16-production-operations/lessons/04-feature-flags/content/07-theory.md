---
type: "THEORY"
title: "Feature Flag Patterns"
---


**Pattern 1: Boolean Feature Toggle**

The simplest pattern - feature is on or off:

```dart
if (featureFlags.isNewCheckoutEnabled) {
  return NewCheckoutScreen();
} else {
  return OldCheckoutScreen();
}
```

**Pattern 2: Gradual Rollout with Percentiles**

In Firebase Console, use "User in random percentile" condition:

- 0-5%: Initial rollout (5% of users)
- 0-25%: Expanded rollout (25% of users)
- 0-100%: Full rollout (all users)

The percentile is sticky per user - same user always gets same experience.

**Pattern 3: User Segment Targeting**

Target specific users based on properties:

```dart
// Set user property in Analytics
await FirebaseAnalytics.instance.setUserProperty(
  name: 'subscription_tier',
  value: 'premium',
);

// In Firebase Console, create condition:
// User property 'subscription_tier' equals 'premium'
```

**Pattern 4: Platform-Specific Features**

Different values for iOS vs Android:

```
// Firebase Console condition:
// Platform equals 'iOS' -> value: true
// Platform equals 'Android' -> value: false
```

**Pattern 5: App Version Gating**

Enable features only for newer app versions:

```
// Firebase Console condition:
// App version >= 2.0.0 -> value: true
// Default -> value: false
```

**Pattern 6: Multi-Variant Flags**

More than two options for A/B/C testing:

```dart
final variant = featureFlags.getString('checkout_variant');

switch (variant) {
  case 'control':
    return OriginalCheckout();
  case 'variant_a':
    return SimplifiedCheckout();
  case 'variant_b':
    return OnePageCheckout();
  default:
    return OriginalCheckout();
}
```

**Pattern 7: JSON Configuration**

Complex configuration as JSON string:

```dart
final configJson = featureFlags.getString('promo_banner_config');
final config = json.decode(configJson);
// {
//   "enabled": true,
//   "title": "Summer Sale!",
//   "discount": 20,
//   "end_date": "2024-08-31"
// }
```

