---
type: "THEORY"
title: "Flexible Columns with GridView"
---



On 400px screen: 2 columns
On 800px screen: 4 columns
Auto-responsive!



```dart
GridView.extent(
  maxCrossAxisExtent: 200,  // Adjusts columns automatically!
  children: items,
)
```
