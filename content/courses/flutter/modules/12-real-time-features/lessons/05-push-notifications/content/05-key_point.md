---
type: KEY_POINT
---

- Firebase Cloud Messaging (FCM) delivers push notifications to both Android and iOS using device tokens
- Request notification permission with `FirebaseMessaging.instance.requestPermission()` -- iOS requires explicit opt-in
- Handle three app states separately: foreground (you control display), background (system shows notification), and terminated (system shows notification)
- Store and refresh device tokens on the backend so notifications reach the correct device even after token rotation
- Use notification channels on Android to let users control which notification types they receive
