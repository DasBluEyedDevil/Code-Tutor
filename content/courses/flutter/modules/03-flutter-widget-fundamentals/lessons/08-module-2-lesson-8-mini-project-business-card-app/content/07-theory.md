---
type: "THEORY"
title: "Step 4: Add Name and Title"
---

After the `CircleAvatar`, add two `Text` widgets for your name and professional title.

```dart
// Inside the Column children list:
CircleAvatar(...),
Text(
  'Your Name',
  style: TextStyle(
    fontSize: 40,
    color: Colors.white,
    fontWeight: FontWeight.bold,
  ),
),
Text(
  'FLUTTER DEVELOPER',
  style: TextStyle(
    fontSize: 20,
    color: Colors.teal.shade100,
    letterSpacing: 2.5,
    fontWeight: FontWeight.bold,
  ),
),
```