---
type: "EXAMPLE"
title: "Manual JSON Serialization with dart:convert"
---

Let us start with the fundamentals. Dart includes the `dart:convert` library which provides `jsonDecode()` and `jsonEncode()` functions for working with JSON.

**Basic JSON Decoding**

The `jsonDecode()` function converts a JSON string into Dart data structures:

```dart
import 'dart:convert';

void main() {
  // JSON string from an API
  final jsonString = '{"name": "Alice", "age": 30, "isPremium": true}';
  
  // Decode JSON string to a Map
  final Map<String, dynamic> data = jsonDecode(jsonString);
  
  print(data['name']);      // Alice (String)
  print(data['age']);       // 30 (int)
  print(data['isPremium']); // true (bool)
}
```

Notice that `jsonDecode()` returns `dynamic`, which means you lose type safety. This is why we create model classes.

**Creating a Model Class with fromJson/toJson**

A model class represents your data structure with proper types:

```dart
class User {
  final int id;
  final String name;
  final String email;
  final DateTime createdAt;
  final bool isPremium;
  
  // Constructor
  User({
    required this.id,
    required this.name,
    required this.email,
    required this.createdAt,
    required this.isPremium,
  });
  
  // Factory constructor for deserialization (JSON -> Dart)
  factory User.fromJson(Map<String, dynamic> json) {
    return User(
      id: json['id'] as int,
      name: json['name'] as String,
      email: json['email'] as String,
      createdAt: DateTime.parse(json['created_at'] as String),
      isPremium: json['is_premium'] as bool? ?? false,
    );
  }
  
  // Method for serialization (Dart -> JSON)
  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'name': name,
      'email': email,
      'created_at': createdAt.toIso8601String(),
      'is_premium': isPremium,
    };
  }
  
  @override
  String toString() => 'User(id: $id, name: $name, email: $email)';
}
```

**Using the Model Class**

```dart
import 'dart:convert';

void main() {
  // Simulating API response
  final jsonString = '''
  {
    "id": 1,
    "name": "Alice Johnson",
    "email": "alice@example.com",
    "created_at": "2024-01-15T10:30:00Z",
    "is_premium": true
  }
  ''';
  
  // Decode JSON and create User object
  final Map<String, dynamic> jsonMap = jsonDecode(jsonString);
  final user = User.fromJson(jsonMap);
  
  // Now you have full type safety!
  print(user.name);                    // Alice Johnson
  print(user.createdAt.year);          // 2024
  print(user.isPremium ? 'VIP' : '');  // VIP
  
  // Convert back to JSON for sending to API
  final Map<String, dynamic> outputJson = user.toJson();
  final String outputString = jsonEncode(outputJson);
  print(outputString);
}
```

**Handling Lists of Objects**

```dart
class Post {
  final int id;
  final String title;
  final String body;
  final int userId;
  
  Post({
    required this.id,
    required this.title,
    required this.body,
    required this.userId,
  });
  
  factory Post.fromJson(Map<String, dynamic> json) {
    return Post(
      id: json['id'] as int,
      title: json['title'] as String,
      body: json['body'] as String,
      userId: json['userId'] as int,
    );
  }
  
  Map<String, dynamic> toJson() => {
    'id': id,
    'title': title,
    'body': body,
    'userId': userId,
  };
}

// Parsing a list of posts from JSON array
List<Post> parsePostsFromJson(String jsonString) {
  final List<dynamic> jsonList = jsonDecode(jsonString);
  return jsonList
      .map((json) => Post.fromJson(json as Map<String, dynamic>))
      .toList();
}

// Converting list back to JSON
String postsToJson(List<Post> posts) {
  final List<Map<String, dynamic>> jsonList = 
      posts.map((post) => post.toJson()).toList();
  return jsonEncode(jsonList);
}
```

**When to Use Manual Serialization**

- Small projects with few models
- Learning how serialization works
- One-off scripts or simple utilities
- When you want zero dependencies

**Limitations of Manual Serialization**

- Repetitive boilerplate code
- Easy to make typos in field names
- Must manually update both fromJson and toJson
- No compile-time validation of JSON keys

For production apps with many models, we use code generation.

