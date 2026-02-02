---
type: "WARNING"
title: "Common Mistakes"
---


❌ **Mistake 1**: Forgetting android:autoVerify="true"

✅ **Fix**:

❌ **Mistake 2**: Wrong file location

✅ **Fix**:

❌ **Mistake 3**: Not handling initial link

✅ **Fix**:



```dart
// Handle both cases
_appLinks.uriLinkStream.listen((uri) { ... });
final initialUri = await _appLinks.getInitialLink();
if (initialUri != null) { ... }
```
