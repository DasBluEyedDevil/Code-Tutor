---
type: "EXAMPLE"
title: "Combining json_serializable and freezed: Production Patterns"
---

In production apps, you will often use both packages together. Here are complete examples showing best practices.

**Complete User Model with All Features**

```dart
import 'package:freezed_annotation/freezed_annotation.dart';

part 'user.freezed.dart';
part 'user.g.dart';

@freezed
class User with _$User {
  const User._();  // Private constructor for custom methods
  
  @JsonSerializable(fieldRename: FieldRename.snake)
  const factory User({
    required int id,
    required String email,
    @JsonKey(name: 'full_name') required String fullName,
    String? avatarUrl,
    @JsonKey(name: 'phone_number') String? phoneNumber,
    @Default(UserRole.user) UserRole role,
    Address? address,
    @Default([]) List<String> permissions,
    required DateTime createdAt,
    DateTime? lastLoginAt,
    @Default(false) bool isVerified,
    @Default(true) bool isActive,
  }) = _User;
  
  factory User.fromJson(Map<String, dynamic> json) => _$UserFromJson(json);
  
  // Custom getters
  String get initials {
    final parts = fullName.split(' ');
    if (parts.length >= 2) {
      return '${parts.first[0]}${parts.last[0]}'.toUpperCase();
    }
    return fullName.isNotEmpty ? fullName[0].toUpperCase() : '?';
  }
  
  bool get isAdmin => role == UserRole.admin;
  bool get isModerator => role == UserRole.moderator || role == UserRole.admin;
  
  bool hasPermission(String permission) => permissions.contains(permission);
}

@JsonEnum(fieldRename: FieldRename.snake)
enum UserRole {
  admin,
  moderator,
  user,
  guest,
}

@freezed
class Address with _$Address {
  @JsonSerializable(fieldRename: FieldRename.snake)
  const factory Address({
    required String street,
    required String city,
    required String state,
    required String zipCode,
    required String country,
    String? apartment,
  }) = _Address;
  
  factory Address.fromJson(Map<String, dynamic> json) => _$AddressFromJson(json);
}
```

**API Response Wrapper**

```dart
import 'package:freezed_annotation/freezed_annotation.dart';

part 'api_response.freezed.dart';
part 'api_response.g.dart';

// Generic API response wrapper
@Freezed(genericArgumentFactories: true)
class ApiResponse<T> with _$ApiResponse<T> {
  const factory ApiResponse({
    required bool success,
    T? data,
    String? message,
    @JsonKey(name: 'error_code') String? errorCode,
    Map<String, List<String>>? errors,
  }) = _ApiResponse<T>;
  
  factory ApiResponse.fromJson(
    Map<String, dynamic> json,
    T Function(Object? json) fromJsonT,
  ) => _$ApiResponseFromJson(json, fromJsonT);
}

// Paginated response
@Freezed(genericArgumentFactories: true)
class PaginatedData<T> with _$PaginatedData<T> {
  @JsonSerializable(fieldRename: FieldRename.snake)
  const factory PaginatedData({
    required List<T> items,
    required int currentPage,
    required int lastPage,
    required int perPage,
    required int total,
    String? nextPageUrl,
    String? prevPageUrl,
  }) = _PaginatedData<T>;
  
  factory PaginatedData.fromJson(
    Map<String, dynamic> json,
    T Function(Object? json) fromJsonT,
  ) => _$PaginatedDataFromJson(json, fromJsonT);
}

// Usage
Future<ApiResponse<PaginatedData<User>>> fetchUsers(int page) async {
  final response = await dio.get('/users', queryParameters: {'page': page});
  
  return ApiResponse.fromJson(
    response.data,
    (json) => PaginatedData.fromJson(
      json as Map<String, dynamic>,
      (itemJson) => User.fromJson(itemJson as Map<String, dynamic>),
    ),
  );
}
```

**Request DTOs (Data Transfer Objects)**

```dart
import 'package:freezed_annotation/freezed_annotation.dart';

part 'requests.freezed.dart';
part 'requests.g.dart';

// Login request
@freezed
class LoginRequest with _$LoginRequest {
  @JsonSerializable(fieldRename: FieldRename.snake)
  const factory LoginRequest({
    required String email,
    required String password,
    @Default(false) bool rememberMe,
  }) = _LoginRequest;
  
  factory LoginRequest.fromJson(Map<String, dynamic> json) => 
      _$LoginRequestFromJson(json);
}

// Registration request
@freezed
class RegisterRequest with _$RegisterRequest {
  @JsonSerializable(fieldRename: FieldRename.snake)
  const factory RegisterRequest({
    required String email,
    required String password,
    required String passwordConfirmation,
    required String fullName,
    String? phoneNumber,
    @Default(true) bool acceptTerms,
  }) = _RegisterRequest;
  
  factory RegisterRequest.fromJson(Map<String, dynamic> json) => 
      _$RegisterRequestFromJson(json);
}

// Update profile request
@freezed
class UpdateProfileRequest with _$UpdateProfileRequest {
  @JsonSerializable(fieldRename: FieldRename.snake, includeIfNull: false)
  const factory UpdateProfileRequest({
    String? fullName,
    String? phoneNumber,
    String? avatarUrl,
    Address? address,
  }) = _UpdateProfileRequest;
  
  factory UpdateProfileRequest.fromJson(Map<String, dynamic> json) => 
      _$UpdateProfileRequestFromJson(json);
}

// Usage
Future<User> updateProfile(UpdateProfileRequest request) async {
  final response = await dio.patch(
    '/profile',
    data: request.toJson(),
  );
  return User.fromJson(response.data);
}
```

**build.yaml Configuration for Custom Settings**

Create a `build.yaml` file in your project root for global settings:

```yaml
targets:
  $default:
    builders:
      json_serializable:
        options:
          # Apply snake_case conversion globally
          field_rename: snake
          # Don't include null values in output
          include_if_null: false
          # Required for nested objects
          explicit_to_json: true
      freezed:
        options:
          # Generate toJson by default
          to_json: true
          # Generate fromJson by default  
          from_json: true
```

With this configuration, you can simplify your model classes:

```dart
// No need to specify @JsonSerializable(fieldRename: FieldRename.snake)
// The build.yaml settings apply globally
@freezed
class User with _$User {
  const factory User({
    required int id,
    required String fullName,  // Auto-converts to full_name
    required DateTime createdAt,  // Auto-converts to created_at
  }) = _User;
  
  factory User.fromJson(Map<String, dynamic> json) => _$UserFromJson(json);
}
```

