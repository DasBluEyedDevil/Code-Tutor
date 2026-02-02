---
type: "THEORY"
title: "Part 2: Firebase Analytics"
---


### Setup Analytics

Firebase Analytics is included with `firebase_core` - no extra package needed!

### Track Events


### Track Navigation


### Usage Example




```dart
// In your screens
final analytics = AnalyticsService();

// Track screen view
@override
void initState() {
  super.initState();
  analytics.logScreenView('Home Screen');
}

// Track button clicks
ElevatedButton(
  onPressed: () {
    analytics.logButtonClick('create_post_button');
    // ... button action
  },
  child: const Text('Create Post'),
)

// Track signup
await authService.register(...);
analytics.logSignUp('email');

// Track login
await authService.login(...);
analytics.logLogin('google');
```
