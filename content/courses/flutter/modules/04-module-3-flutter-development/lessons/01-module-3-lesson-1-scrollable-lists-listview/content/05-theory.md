---
type: "THEORY"
title: "ListView.separated - With Dividers"
---


Add dividers between items:




```dart
ListView.separated(
  itemCount: contacts.length,
  itemBuilder: (context, index) {
    return ListTile(title: Text(contacts[index]));
  },
  separatorBuilder: (context, index) {
    return Divider();
  },
)
```
