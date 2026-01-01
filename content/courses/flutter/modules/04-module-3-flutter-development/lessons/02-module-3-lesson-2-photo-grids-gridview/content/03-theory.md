---
type: "THEORY"
title: "GridView.extent - Maximum Item Size"
---


Instead of specifying columns, specify max width per item:


Automatically adjusts columns based on screen size - **responsive**!



```dart
GridView.extent(
  maxCrossAxisExtent: 150,  // Max 150px per item
  children: [
    // Items adjust to fit screen width
  ],
)
```
