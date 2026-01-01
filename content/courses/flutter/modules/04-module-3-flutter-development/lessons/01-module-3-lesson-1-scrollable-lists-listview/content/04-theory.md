---
type: "THEORY"
title: "ListView.builder - For Dynamic Lists"
---


When you have many items (especially from data), use `ListView.builder`:


**Why builder?** It only creates widgets for visible items - much more efficient!



```dart
ListView.builder(
  itemCount: 100,  // Number of items
  itemBuilder: (context, index) {
    return ListTile(
      title: Text('Item $index'),
    );
  },
)
```
