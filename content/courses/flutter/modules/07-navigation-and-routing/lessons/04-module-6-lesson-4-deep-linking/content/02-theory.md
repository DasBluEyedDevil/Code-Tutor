---
type: "THEORY"
title: "Types of Deep Links"
---


### 1. Custom URL Schemes (Old Way)

**Problems:**
- Any app can register the same scheme (security risk!)
- No fallback if app isn't installed
- Doesn't work on web

### 2. App Links (Android) & Universal Links (iOS) (Modern Way)

**Benefits:**
- ✅ Secure (verified with your website)
- ✅ Fallback to website if app not installed
- ✅ Works on mobile, web, and desktop
- ✅ Better user experience

**We'll focus on the modern way!**



```dart
https://mycompany.com/product/123
```
