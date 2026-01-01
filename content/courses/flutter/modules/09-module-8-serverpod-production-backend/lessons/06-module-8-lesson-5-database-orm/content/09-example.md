---
type: "EXAMPLE"
title: "Generated Dart Class"
---

After running `serverpod generate`, the YAML above produces this Dart class (simplified):



```dart
// This is AUTO-GENERATED - do not edit!
// lib/src/generated/user.dart

class User extends TableRow {
  /// The database id, set if the object has been inserted into the database
  /// or loaded from the database.
  @override
  int? id;
  
  String email;
  String username;
  String? displayName;
  String? avatarUrl;
  String? bio;
  DateTime createdAt;
  DateTime? lastLoginAt;
  bool isActive;
  bool isVerified;
  int postCount;
  int followerCount;
  
  User({
    this.id,
    required this.email,
    required this.username,
    this.displayName,
    this.avatarUrl,
    this.bio,
    required this.createdAt,
    this.lastLoginAt,
    required this.isActive,
    required this.isVerified,
    required this.postCount,
    required this.followerCount,
  });
  
  /// Access to database operations
  static final db = UserRepository._();
  
  // Plus: toJson(), copyWith(), serialization methods, etc.
}
```
