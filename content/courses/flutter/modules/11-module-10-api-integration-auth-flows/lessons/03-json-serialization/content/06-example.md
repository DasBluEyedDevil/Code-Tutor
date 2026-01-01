---
type: "EXAMPLE"
title: "freezed Package: Immutable Models with Superpowers"
---

The `freezed` package takes code generation further by creating immutable data classes with features like:
- Automatic `==` operator and `hashCode`
- `copyWith` method for creating modified copies
- `toString` for debugging
- Union types (sealed classes) for state management
- Pattern matching support

**Step 1: Add Dependencies**

```yaml
dependencies:
  flutter:
    sdk: flutter
  freezed_annotation: ^2.4.1
  json_annotation: ^4.8.1

dev_dependencies:
  flutter_test:
    sdk: flutter
  build_runner: ^2.4.8
  freezed: ^2.4.6
  json_serializable: ^6.7.1
```

Run `flutter pub get`.

**Step 2: Create a freezed Model**

Create `lib/models/user.dart`:

```dart
import 'package:freezed_annotation/freezed_annotation.dart';

// Required: connects to generated files
part 'user.freezed.dart';
part 'user.g.dart';

@freezed
class User with _$User {
  const factory User({
    required int id,
    required String name,
    required String email,
    @JsonKey(name: 'created_at') required DateTime createdAt,
    @JsonKey(name: 'is_premium') @Default(false) bool isPremium,
    String? avatarUrl,
  }) = _User;
  
  factory User.fromJson(Map<String, dynamic> json) => _$UserFromJson(json);
}
```

**Step 3: Generate the Code**

```bash
flutter pub run build_runner build --delete-conflicting-outputs
```

This generates two files:
- `user.freezed.dart` - immutability, copyWith, ==, hashCode, toString
- `user.g.dart` - JSON serialization

**Step 4: Use the freezed Model**

```dart
void main() {
  // Create a user
  final user = User(
    id: 1,
    name: 'Alice',
    email: 'alice@example.com',
    createdAt: DateTime.now(),
  );
  
  // Automatic toString
  print(user);
  // User(id: 1, name: Alice, email: alice@example.com, ...)
  
  // Immutable - cannot modify fields directly
  // user.name = 'Bob';  // Compile error!
  
  // Create a modified copy with copyWith
  final updatedUser = user.copyWith(name: 'Alice Johnson');
  print(updatedUser.name);  // Alice Johnson
  print(user.name);         // Alice (original unchanged)
  
  // Only change specific fields
  final premiumUser = user.copyWith(isPremium: true);
  
  // Automatic equality comparison
  final user1 = User(id: 1, name: 'Alice', email: 'a@b.com', createdAt: DateTime(2024));
  final user2 = User(id: 1, name: 'Alice', email: 'a@b.com', createdAt: DateTime(2024));
  print(user1 == user2);  // true (value equality, not reference)
  
  // Can use in Sets and as Map keys
  final userSet = {user1, user2};
  print(userSet.length);  // 1 (duplicates removed)
}
```

**Adding Custom Methods and Getters**

You can add methods to freezed classes:

```dart
@freezed
class User with _$User {
  // Private constructor enables adding methods
  const User._();
  
  const factory User({
    required int id,
    required String name,
    required String email,
    @JsonKey(name: 'first_name') String? firstName,
    @JsonKey(name: 'last_name') String? lastName,
    @JsonKey(name: 'created_at') required DateTime createdAt,
    @Default(false) bool isPremium,
  }) = _User;
  
  factory User.fromJson(Map<String, dynamic> json) => _$UserFromJson(json);
  
  // Custom getter
  String get displayName {
    if (firstName != null && lastName != null) {
      return '$firstName $lastName';
    }
    return name;
  }
  
  // Custom method
  bool get isNewUser {
    final daysSinceCreation = DateTime.now().difference(createdAt).inDays;
    return daysSinceCreation < 30;
  }
  
  // Validation method
  bool get hasValidEmail => email.contains('@') && email.contains('.');
}
```

**Default Values**

```dart
@freezed
class Settings with _$Settings {
  const factory Settings({
    @Default(true) bool darkMode,
    @Default(false) bool notifications,
    @Default('en') String language,
    @Default(14) int fontSize,
    @Default([]) List<String> favoriteCategories,
  }) = _Settings;
  
  factory Settings.fromJson(Map<String, dynamic> json) => _$SettingsFromJson(json);
}

// Usage
final defaultSettings = Settings();  // All defaults applied
final customSettings = Settings(darkMode: false, language: 'es');
```

