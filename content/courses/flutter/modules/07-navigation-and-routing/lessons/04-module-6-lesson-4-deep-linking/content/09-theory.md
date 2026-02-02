---
type: "THEORY"
title: "Firebase Dynamic Links Alternative"
---


For more advanced features (analytics, short links, campaign tracking):



**Dynamic Links can:**
- Survive app installation (remember where user came from)
- Track campaign performance
- Create short links for sharing



```dart
// Handle Firebase Dynamic Links
FirebaseDynamicLinks.instance.onLink.listen((dynamicLinkData) {
  final Uri deepLink = dynamicLinkData.link;
  // Handle the link
  _router.go(deepLink.path);
});
```
