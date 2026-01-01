---
type: "THEORY"
title: "FCM Setup"
---

Firebase Cloud Messaging (FCM) is the industry-standard solution for push notifications on both Android and iOS. Understanding its architecture is essential for implementing reliable real-time notifications in your Flutter apps.

**FCM Architecture:**

FCM operates through a client-server model where your app registers with Firebase, receives a unique device token, and your backend uses this token to target specific devices for notifications.

**How It Works:**

1. App initializes Firebase and requests notification permissions
2. FCM generates a unique registration token for the device
3. App sends this token to your Serverpod backend
4. Backend stores token associated with user account
5. When an event occurs, backend sends notification via FCM API
6. FCM delivers to the specific device(s)

**APNs for iOS:**

Apple Push Notification service (APNs) is required for iOS devices. FCM acts as a wrapper around APNs, but you need:

- Apple Developer account with push notification capability
- APNs authentication key or certificate uploaded to Firebase
- Proper entitlements in your iOS project

**Platform Differences:**

| Feature | Android | iOS |
|---------|---------|-----|
| Background delivery | Automatic | Requires background modes |
| Permissions | Auto-granted (Android 12-) | Must request explicitly |
| Token refresh | Automatic | Automatic |
| Priority levels | Normal/High | Silent/Alert |

**Notification Types:**

- **Notification messages**: Display automatically when app is backgrounded
- **Data messages**: Silent, handled by app code
- **Mixed**: Notification + data payload

