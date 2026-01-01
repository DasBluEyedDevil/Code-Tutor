---
type: "THEORY"
title: "Step 5: Add a Divider"
---

A thin line helps separate your header from your contact info. Use the `SizedBox` and `Divider` widgets.

```dart
// After the Title Text widget:
SizedBox(
  height: 20,
  width: 150,
  child: Divider(
    color: Colors.teal.shade100,
  ),
),
```