---
type: "EXAMPLE"
title: "Shared Code in Action"
---


Here's the killer feature of full-stack Dart: **shared models**.

Imagine you have a `User` model. In a traditional setup, you'd write it twice:

**JavaScript Backend (Node.js)**:
```javascript
// backend/models/user.js
class User {
  constructor(id, name, email) {
    this.id = id;
    this.name = name;
    this.email = email;
  }
}
```

**Dart Frontend (Flutter)**:
```dart
// flutter_app/lib/models/user.dart
class User {
  final String id;
  final String name;
  final String email;
  
  User({required this.id, required this.name, required this.email});
}
```

Two files, two languages, two places to update when `User` changes. Recipe for bugs.

**With Full-Stack Dart**:



```dart
// shared/lib/models/user.dart
// This SINGLE file is used by BOTH Flutter and Backend!

class User {
  final String id;
  final String name;
  final String email;
  
  User({
    required this.id, 
    required this.name, 
    required this.email,
  });
  
  // JSON serialization - works everywhere
  factory User.fromJson(Map<String, dynamic> json) => User(
    id: json['id'] as String,
    name: json['name'] as String,
    email: json['email'] as String,
  );
  
  Map<String, dynamic> toJson() => {
    'id': id,
    'name': name,
    'email': email,
  };
  
  @override
  String toString() => 'User(id: $id, name: $name, email: $email)';
}

// In your Dart Frog backend:
// import 'package:shared/models/user.dart';
// final user = User.fromJson(requestBody);

// In your Flutter app:
// import 'package:shared/models/user.dart';
// final user = User.fromJson(apiResponse);
```
