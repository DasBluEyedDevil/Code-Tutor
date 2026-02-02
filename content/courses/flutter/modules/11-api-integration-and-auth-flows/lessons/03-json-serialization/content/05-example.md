---
type: "EXAMPLE"
title: "Handling Complex Types: Nested Objects and Lists"
---

Real-world APIs often have nested objects and arrays. Here is how to handle them with json_serializable.

**Nested Objects**

When one model contains another, both must have fromJson/toJson:

```dart
import 'package:json_annotation/json_annotation.dart';

part 'models.g.dart';

// Address model
@JsonSerializable()
class Address {
  final String street;
  final String city;
  final String state;
  
  @JsonKey(name: 'zip_code')
  final String zipCode;
  
  final String country;
  
  Address({
    required this.street,
    required this.city,
    required this.state,
    required this.zipCode,
    required this.country,
  });
  
  factory Address.fromJson(Map<String, dynamic> json) => _$AddressFromJson(json);
  Map<String, dynamic> toJson() => _$AddressToJson(this);
}

// Company model with nested Address
@JsonSerializable(explicitToJson: true)
class Company {
  final String name;
  
  @JsonKey(name: 'catch_phrase')
  final String catchPhrase;
  
  // Nested object - json_serializable handles this automatically
  final Address address;
  
  Company({
    required this.name,
    required this.catchPhrase,
    required this.address,
  });
  
  factory Company.fromJson(Map<String, dynamic> json) => _$CompanyFromJson(json);
  Map<String, dynamic> toJson() => _$CompanyToJson(this);
}

// User model with nested Company
@JsonSerializable(explicitToJson: true)
class User {
  final int id;
  final String name;
  final String email;
  
  // Nullable nested object
  final Company? company;
  
  User({
    required this.id,
    required this.name,
    required this.email,
    this.company,
  });
  
  factory User.fromJson(Map<String, dynamic> json) => _$UserFromJson(json);
  Map<String, dynamic> toJson() => _$UserToJson(this);
}
```

**Important**: Use `explicitToJson: true` when you have nested objects, otherwise `toJson()` will not serialize nested objects properly.

**Lists of Objects**

```dart
@JsonSerializable(explicitToJson: true)
class BlogPost {
  final int id;
  final String title;
  final String content;
  
  @JsonKey(name: 'published_at')
  final DateTime publishedAt;
  
  // List of nested objects
  final List<Comment> comments;
  
  // List of primitive types
  final List<String> tags;
  
  BlogPost({
    required this.id,
    required this.title,
    required this.content,
    required this.publishedAt,
    required this.comments,
    required this.tags,
  });
  
  factory BlogPost.fromJson(Map<String, dynamic> json) => _$BlogPostFromJson(json);
  Map<String, dynamic> toJson() => _$BlogPostToJson(this);
}

@JsonSerializable()
class Comment {
  final int id;
  final String body;
  
  @JsonKey(name: 'author_name')
  final String authorName;
  
  @JsonKey(name: 'created_at')
  final DateTime createdAt;
  
  Comment({
    required this.id,
    required this.body,
    required this.authorName,
    required this.createdAt,
  });
  
  factory Comment.fromJson(Map<String, dynamic> json) => _$CommentFromJson(json);
  Map<String, dynamic> toJson() => _$CommentToJson(this);
}
```

**Handling Maps**

```dart
@JsonSerializable()
class GameProgress {
  final String oderId;
  
  // Map of primitive types
  final Map<String, int> levelScores;
  
  // Map with complex values
  @JsonKey(name: 'achievements')
  final Map<String, Achievement> achievements;
  
  GameProgress({
    required this.oderId,
    required this.levelScores,
    required this.achievements,
  });
  
  factory GameProgress.fromJson(Map<String, dynamic> json) => 
      _$GameProgressFromJson(json);
  Map<String, dynamic> toJson() => _$GameProgressToJson(this);
}

@JsonSerializable()
class Achievement {
  final String name;
  final String description;
  
  @JsonKey(name: 'unlocked_at')
  final DateTime? unlockedAt;
  
  Achievement({
    required this.name,
    required this.description,
    this.unlockedAt,
  });
  
  factory Achievement.fromJson(Map<String, dynamic> json) => 
      _$AchievementFromJson(json);
  Map<String, dynamic> toJson() => _$AchievementToJson(this);
}
```

**Generic Classes**

For paginated API responses, use generic classes:

```dart
import 'package:json_annotation/json_annotation.dart';

part 'pagination.g.dart';

// Generic wrapper for paginated responses
@JsonSerializable(genericArgumentFactories: true)
class PaginatedResponse<T> {
  final List<T> data;
  
  @JsonKey(name: 'current_page')
  final int currentPage;
  
  @JsonKey(name: 'last_page')
  final int lastPage;
  
  @JsonKey(name: 'per_page')
  final int perPage;
  
  final int total;
  
  PaginatedResponse({
    required this.data,
    required this.currentPage,
    required this.lastPage,
    required this.perPage,
    required this.total,
  });
  
  // For generics, we need to pass the fromJson function
  factory PaginatedResponse.fromJson(
    Map<String, dynamic> json,
    T Function(Object? json) fromJsonT,
  ) => _$PaginatedResponseFromJson(json, fromJsonT);
  
  Map<String, dynamic> toJson(Object Function(T value) toJsonT) =>
      _$PaginatedResponseToJson(this, toJsonT);
  
  bool get hasNextPage => currentPage < lastPage;
  bool get hasPreviousPage => currentPage > 1;
}

// Usage example
void fetchUsers() async {
  final response = await dio.get('/users?page=1');
  
  final paginatedUsers = PaginatedResponse<User>.fromJson(
    response.data,
    (json) => User.fromJson(json as Map<String, dynamic>),
  );
  
  print('Page ${paginatedUsers.currentPage} of ${paginatedUsers.lastPage}');
  print('Users: ${paginatedUsers.data.length}');
}
```

