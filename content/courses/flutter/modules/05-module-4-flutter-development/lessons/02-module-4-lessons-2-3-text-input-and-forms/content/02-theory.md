---
type: "THEORY"
title: "The Basic TextField"
---

The `TextField` widget allows users to type text. You can customize its appearance using the `decoration` property.

```dart
TextField(
  decoration: InputDecoration(
    labelText: 'Enter your name',
    hintText: 'e.g. John Doe',
    prefixIcon: Icon(Icons.person),
    border: OutlineInputBorder(), // Adds a box border
  ),
)
```

Common input types:
- **Email**: `keyboardType: TextInputType.emailAddress`
- **Password**: `obscureText: true` (hides characters)
- **Numbers**: `keyboardType: TextInputType.number`
- **Multi-line**: `maxLines: 3`