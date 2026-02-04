---
type: WARNING
---

**iOS notification permissions are one-shot.** Unlike Android (which grants notification permission by default on API < 33), iOS prompts the user exactly once. If the user denies permission, your app cannot ask again -- the user must manually enable notifications in Settings.

Best practices for permission requests:
- Delay the permission prompt until the user understands the value (e.g., after they send their first message, not at app launch)
- Show a pre-permission screen explaining what notifications they will receive before triggering the system dialog
- If permission was previously denied, show a button that deep-links to the app's Settings page using `openAppSettings()`

Also remember that FCM device tokens rotate periodically. Always update the stored token on the backend when `FirebaseMessaging.instance.onTokenRefresh` fires, or push notifications will silently stop reaching devices with stale tokens.
