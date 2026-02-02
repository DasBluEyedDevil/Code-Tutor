---
type: "EXAMPLE"
title: "json_serializable Package: Code Generation Setup"
---

The `json_serializable` package automatically generates the `fromJson` and `toJson` code for you. You write the model class with annotations, and the code generator creates the serialization logic.

**Step 1: Add Dependencies**

Add these packages to your `pubspec.yaml`:

```yaml
dependencies:
  flutter:
    sdk: flutter
  json_annotation: ^4.8.1

dev_dependencies:
  flutter_test:
    sdk: flutter
  build_runner: ^2.4.8
  json_serializable: ^6.7.1
```

Run `flutter pub get` to install the packages.

**Step 2: Create a Model Class with Annotations**

Create a file `lib/models/user.dart`:

```dart
import 'package:json_annotation/json_annotation.dart';

// This line connects this file to the generated code
part 'user.g.dart';

// Annotation that tells json_serializable to generate code for this class
@JsonSerializable()
class User {
  final int id;
  final String name;
  final String email;
  
  // @JsonKey customizes how a field is serialized
  @JsonKey(name: 'created_at')
  final DateTime createdAt;
  
  @JsonKey(name: 'is_premium', defaultValue: false)
  final bool isPremium;
  
  // Constructor - all fields required
  User({
    required this.id,
    required this.name,
    required this.email,
    required this.createdAt,
    required this.isPremium,
  });
  
  // These methods delegate to generated code
  factory User.fromJson(Map<String, dynamic> json) => _$UserFromJson(json);
  
  Map<String, dynamic> toJson() => _$UserToJson(this);
}
```

**Step 3: Generate the Code**

Run the build_runner command to generate the serialization code:

```bash
# Generate once
flutter pub run build_runner build

# Or watch for changes and regenerate automatically
flutter pub run build_runner watch

# If you get conflicts, delete old generated files first
flutter pub run build_runner build --delete-conflicting-outputs
```

This creates a file `lib/models/user.g.dart` containing:

```dart
// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'user.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

User _$UserFromJson(Map<String, dynamic> json) => User(
      id: json['id'] as int,
      name: json['name'] as String,
      email: json['email'] as String,
      createdAt: DateTime.parse(json['created_at'] as String),
      isPremium: json['is_premium'] as bool? ?? false,
    );

Map<String, dynamic> _$UserToJson(User instance) => <String, dynamic>{
      'id': instance.id,
      'name': instance.name,
      'email': instance.email,
      'created_at': instance.createdAt.toIso8601String(),
      'is_premium': instance.isPremium,
    };
```

**Step 4: Use the Model**

```dart
import 'dart:convert';
import 'package:my_app/models/user.dart';

void main() {
  final jsonString = '{"id": 1, "name": "Alice", "email": "alice@example.com", "created_at": "2024-01-15T10:30:00Z", "is_premium": true}';
  
  final user = User.fromJson(jsonDecode(jsonString));
  print(user.name);      // Alice
  print(user.createdAt); // 2024-01-15 10:30:00.000Z
  
  // Convert back to JSON
  final json = jsonEncode(user.toJson());
  print(json);
}
```

**Common @JsonSerializable Options**

```dart
@JsonSerializable(
  // Include fields with null values in JSON output
  includeIfNull: false,
  
  // Automatically convert field names from camelCase to snake_case
  fieldRename: FieldRename.snake,
  
  // Throw exception if JSON has unknown keys
  disallowUnrecognizedKeys: true,
  
  // Generate toJson method (default: true)
  createToJson: true,
  
  // Generate fromJson factory (default: true)
  createFactory: true,
  
  // Make all fields explicit (no implicit conversions)
  explicitToJson: true,
)
class User {
  // ...
}
```

**Using fieldRename for Automatic Case Conversion**

When your API uses snake_case but you want camelCase in Dart:

```dart
@JsonSerializable(fieldRename: FieldRename.snake)
class UserProfile {
  final int userId;         // JSON: user_id
  final String firstName;   // JSON: first_name
  final String lastName;    // JSON: last_name
  final bool isActive;      // JSON: is_active
  
  UserProfile({
    required this.userId,
    required this.firstName,
    required this.lastName,
    required this.isActive,
  });
  
  factory UserProfile.fromJson(Map<String, dynamic> json) => 
      _$UserProfileFromJson(json);
  Map<String, dynamic> toJson() => _$UserProfileToJson(this);
}
```

