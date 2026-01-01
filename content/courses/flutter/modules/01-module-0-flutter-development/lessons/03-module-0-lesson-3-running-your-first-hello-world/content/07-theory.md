---
type: "THEORY"
title: "Understanding the Magic (Just a Peek)"
---


### The Building Blocks of Your App

Even though we haven't learned Dart yet, you can see some familiar patterns. Every Flutter app is built using **Widgets**. Think of widgets like Lego blocks.

Here are the main blocks in your current app:

1.  **`MaterialApp`**: This is the "root" of your app. It tells Flutter you're building an app that follows the Material Design style (Google's design language).
2.  **`Scaffold`**: Think of this as the "skeleton" of a page. It provides the standard layout structure for an app bar at the top, a body in the middle, and a button at the bottom.
3.  **`AppBar`**: The blue bar at the top that contains your app's title.
4.  **`Center` and `Column`**: These are "layout widgets". They tell Flutter where to put other widgets (like the text and the counter) on the screen.
5.  **`FloatingActionButton`**: The circular button in the bottom-right corner.

**The Magic Line**:
Around line 94, you'll see:

```dart
Text(
  '$_counter',
  style: Theme.of(context).textTheme.headlineMedium,
),
```

This is the line that displays the counter! When you press the + button, the number `_counter` changes, and the screen updates automatically. We'll learn how this "automatic update" works very soon!
