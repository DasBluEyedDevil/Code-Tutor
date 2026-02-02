---
type: "THEORY"
title: "What are Named Routes?"
---


Instead of creating MaterialPageRoute everywhere, define routes with string names:


Then navigate with strings:




```dart
Navigator.pushNamed(context, '/detail');
Navigator.pushNamed(context, '/profile');
```
