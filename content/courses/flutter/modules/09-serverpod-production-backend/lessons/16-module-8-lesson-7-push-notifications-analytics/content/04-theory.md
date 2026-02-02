---
type: "THEORY"
title: "Part 1: Firebase Cloud Messaging (FCM)"
---


### How Push Notifications Work




```dart
1. App requests permission
   ↓
2. FCM generates unique token for device
   ↓
3. App sends token to your server (or Firestore)
   ↓
4. Server sends notification to FCM
   ↓
5. FCM delivers to device
   ↓
6. Notification appears on user's screen
```
