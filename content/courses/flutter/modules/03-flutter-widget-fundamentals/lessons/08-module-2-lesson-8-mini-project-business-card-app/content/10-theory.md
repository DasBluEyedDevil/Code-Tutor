---
type: "THEORY"
title: "Step 7: Use Contact Cards"
---

Now use your new `ContactCard` widget in the main Column.

```dart
// Inside the Column children list, after the Divider:
ContactCard(
  icon: Icons.phone,
  text: '+1 234 567 8900',
),
ContactCard(
  icon: Icons.email,
  text: 'your.email@example.com',
),
```