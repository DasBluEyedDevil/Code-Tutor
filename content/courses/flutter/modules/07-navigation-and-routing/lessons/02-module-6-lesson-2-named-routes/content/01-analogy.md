---
type: "ANALOGY"
title: "The Problem with Basic Navigation"
---


With basic navigation, you write this EVERYWHERE:


**Problems:**
- Repetitive code
- Hard to change transitions
- No central route management
- Typos cause runtime errors

**Solution: Named Routes!**



```dart
Navigator.push(
  context,
  MaterialPageRoute(builder: (context) => ProductDetail(product: product)),
);

Navigator.push(
  context,
  MaterialPageRoute(builder: (context) => UserProfile(userId: userId)),
);

Navigator.push(
  context,
  MaterialPageRoute(builder: (context) => SettingsScreen()),
);
```
