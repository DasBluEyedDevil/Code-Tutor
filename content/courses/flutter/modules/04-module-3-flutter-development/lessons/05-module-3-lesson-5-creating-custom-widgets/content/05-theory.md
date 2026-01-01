---
type: "THEORY"
title: "Widget Organization"
---

As your app grows, keep your custom widgets in a separate folder:

```text
lib/
  main.dart
  widgets/
    my_button.dart
    user_card.dart
    navigation_bar.dart
```

In `user_card.dart`:
```dart
import 'package:flutter/material.dart';

class UserCard extends StatelessWidget { ... }
```

In `main.dart`:
```dart
import 'widgets/user_card.dart';

// Now use UserCard anywhere
```