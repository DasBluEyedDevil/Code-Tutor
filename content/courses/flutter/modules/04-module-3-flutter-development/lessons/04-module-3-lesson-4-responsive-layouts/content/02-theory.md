---
type: "THEORY"
title: "MediaQuery - Screen Information"
---


`MediaQuery` provides information about the device screen and user preferences. Use it to get dimensions, safe areas, text scaling, and more.

The most common use is getting the screen width to decide which layout to show:



```dart
final size = MediaQuery.sizeOf(context);
double screenWidth = size.width;
double screenHeight = size.height;
```
