---
type: "THEORY"
title: "Understanding the Magic (Just a Peek)"
---


Open the file `lib/main.dart`. You'll see about 110 lines of code. Don't worry - we're not going to understand all of it yet.

But notice around line 94, you'll see:


This is the line that displays the counter! When you press the + button, the number `_counter` changes, and the screen updates.

**Don't try to understand this code yet.** We'll learn every single piece in the upcoming lessons. For now, just know: *this is what makes the number appear*.



```dart
Text(
  '$_counter',
  style: Theme.of(context).textTheme.headlineMedium,
),
```
