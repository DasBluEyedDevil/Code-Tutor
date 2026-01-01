---
type: "WARNING"
title: "Common Flutter App Errors"
---


**Missing Import Statement**

If you see `Undefined name 'MaterialApp'` or similar:
```dart
// ❌ Missing import
void main() {
  runApp(MaterialApp(...));  // Error!
}

// ✅ Add this at the top of your file
import 'package:flutter/material.dart';
```

**Mismatched Parentheses**

Flutter code has many nested parentheses. Count them carefully:
```dart
// ❌ Missing closing parenthesis
MaterialApp(
  home: Center(
    child: Text('Hello')
  )
// Where's the closing )?

// ✅ Every ( needs a )
MaterialApp(
  home: Center(
    child: Text('Hello'),
  ),
)
```

**Missing Trailing Commas**

Add commas after properties for better formatting:
```dart
// ✅ Good practice - trailing commas help formatting
Text(
  'Hello',
  style: TextStyle(
    fontSize: 24,  // <-- trailing comma
  ),  // <-- trailing comma
)
```

**Red Screen of Death**

If you see a red error screen, don't panic! Read the error message - it tells you exactly what's wrong and which line caused it.

