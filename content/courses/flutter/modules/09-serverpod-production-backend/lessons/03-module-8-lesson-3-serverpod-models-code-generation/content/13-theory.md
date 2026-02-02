---
type: "THEORY"
title: "Type-Safe Client Generation"
---

The magic of Serverpod is that it generates a complete client library for your Flutter app.

**The Client Package**

When you run `serverpod generate`, it creates/updates:
```
my_project_client/
├── lib/
│   ├── my_project_client.dart      # Main export file
│   └── src/
│       └── protocol/
│           ├── user.dart           # Client User model
│           ├── post.dart           # Client Post model
│           ├── client.dart         # API client class
│           └── protocol.dart       # Protocol definitions
└── pubspec.yaml
```

**Using in Flutter:**

```dart
// In your Flutter app's pubspec.yaml
dependencies:
  my_project_client:
    path: ../my_project_client
```

**Type Safety Across the Stack:**

```dart
// In Flutter - This is EXACTLY the same User class!
import 'package:my_project_client/my_project_client.dart';

void createUser() {
  final user = User(
    name: 'Alice',
    email: 'alice@example.com',
    isActive: true,
    createdAt: DateTime.now(),
  );

  // The IDE knows all the fields and their types!
  print(user.name);     // String
  print(user.age);      // int?
  print(user.isActive); // bool
}
```

**No Runtime Surprises:**

If the server changes a field type from String to int, the client code won't compile. You catch errors at build time, not in production.

