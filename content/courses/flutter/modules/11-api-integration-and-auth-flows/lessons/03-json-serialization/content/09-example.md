---
type: "EXAMPLE"
title: "Testing Your Serialization Code"
---

Testing serialization is crucial to catch API contract changes early. Here are comprehensive testing patterns.

**Basic Model Tests**

```dart
import 'package:flutter_test/flutter_test.dart';
import 'package:my_app/models/user.dart';

void main() {
  group('User', () {
    test('fromJson creates User from valid JSON', () {
      // Arrange
      final json = {
        'id': 1,
        'email': 'test@example.com',
        'full_name': 'John Doe',
        'avatar_url': 'https://example.com/avatar.jpg',
        'role': 'admin',
        'created_at': '2024-01-15T10:30:00Z',
        'is_verified': true,
        'is_active': true,
        'permissions': ['read', 'write', 'delete'],
      };
      
      // Act
      final user = User.fromJson(json);
      
      // Assert
      expect(user.id, 1);
      expect(user.email, 'test@example.com');
      expect(user.fullName, 'John Doe');
      expect(user.avatarUrl, 'https://example.com/avatar.jpg');
      expect(user.role, UserRole.admin);
      expect(user.createdAt, DateTime.utc(2024, 1, 15, 10, 30));
      expect(user.isVerified, true);
      expect(user.permissions, ['read', 'write', 'delete']);
    });
    
    test('fromJson uses default values for missing optional fields', () {
      final json = {
        'id': 1,
        'email': 'test@example.com',
        'full_name': 'John Doe',
        'created_at': '2024-01-15T10:30:00Z',
      };
      
      final user = User.fromJson(json);
      
      expect(user.avatarUrl, isNull);
      expect(user.role, UserRole.user);  // default
      expect(user.isVerified, false);    // default
      expect(user.isActive, true);       // default
      expect(user.permissions, isEmpty); // default
    });
    
    test('toJson produces correct JSON', () {
      final user = User(
        id: 1,
        email: 'test@example.com',
        fullName: 'John Doe',
        createdAt: DateTime.utc(2024, 1, 15, 10, 30),
        role: UserRole.admin,
        isVerified: true,
      );
      
      final json = user.toJson();
      
      expect(json['id'], 1);
      expect(json['email'], 'test@example.com');
      expect(json['full_name'], 'John Doe');
      expect(json['created_at'], '2024-01-15T10:30:00.000Z');
      expect(json['role'], 'admin');
      expect(json['is_verified'], true);
    });
    
    test('roundtrip preserves data', () {
      final original = User(
        id: 1,
        email: 'test@example.com',
        fullName: 'John Doe',
        createdAt: DateTime.utc(2024, 1, 15, 10, 30),
        avatarUrl: 'https://example.com/avatar.jpg',
        role: UserRole.moderator,
        permissions: ['read', 'write'],
        isVerified: true,
        isActive: true,
      );
      
      final json = original.toJson();
      final restored = User.fromJson(json);
      
      expect(restored, original);  // freezed provides equality
    });
  });
}
```

**Testing Nested Objects**

```dart
group('User with Address', () {
  test('fromJson handles nested Address', () {
    final json = {
      'id': 1,
      'email': 'test@example.com',
      'full_name': 'John Doe',
      'created_at': '2024-01-15T10:30:00Z',
      'address': {
        'street': '123 Main St',
        'city': 'Springfield',
        'state': 'IL',
        'zip_code': '62701',
        'country': 'USA',
      },
    };
    
    final user = User.fromJson(json);
    
    expect(user.address, isNotNull);
    expect(user.address!.street, '123 Main St');
    expect(user.address!.city, 'Springfield');
    expect(user.address!.zipCode, '62701');
  });
  
  test('fromJson handles null Address', () {
    final json = {
      'id': 1,
      'email': 'test@example.com',
      'full_name': 'John Doe',
      'created_at': '2024-01-15T10:30:00Z',
      'address': null,
    };
    
    final user = User.fromJson(json);
    
    expect(user.address, isNull);
  });
});
```

**Testing Union Types**

```dart
import 'package:flutter_test/flutter_test.dart';
import 'package:my_app/models/network_state.dart';

void main() {
  group('NetworkState', () {
    test('idle state', () {
      const state = NetworkState<String>.idle();
      
      final result = state.when(
        idle: () => 'idle',
        loading: () => 'loading',
        data: (d) => 'data: $d',
        error: (m, _) => 'error: $m',
      );
      
      expect(result, 'idle');
    });
    
    test('data state contains value', () {
      const state = NetworkState.data('Hello');
      
      final result = state.maybeWhen(
        data: (value) => value,
        orElse: () => 'not data',
      );
      
      expect(result, 'Hello');
    });
    
    test('error state contains message', () {
      const state = NetworkState<String>.error('Network failed');
      
      String? errorMessage;
      state.whenOrNull(
        error: (message, _) => errorMessage = message,
      );
      
      expect(errorMessage, 'Network failed');
    });
  });
}
```

**Testing with Real API Response Fixtures**

Create test fixtures from real API responses:

```dart
// test/fixtures/user_fixture.dart
const userJsonFixture = '''
{
  "id": 1,
  "email": "test@example.com",
  "full_name": "John Doe",
  "avatar_url": "https://example.com/avatar.jpg",
  "role": "admin",
  "created_at": "2024-01-15T10:30:00Z",
  "last_login_at": "2024-03-20T15:45:00Z",
  "is_verified": true,
  "is_active": true,
  "permissions": ["read", "write", "delete"],
  "address": {
    "street": "123 Main St",
    "city": "Springfield",
    "state": "IL",
    "zip_code": "62701",
    "country": "USA"
  }
}
''';

// test/models/user_test.dart
import 'dart:convert';
import 'package:flutter_test/flutter_test.dart';
import '../fixtures/user_fixture.dart';

void main() {
  test('parses real API response', () {
    final json = jsonDecode(userJsonFixture) as Map<String, dynamic>;
    final user = User.fromJson(json);
    
    expect(user.id, 1);
    expect(user.fullName, 'John Doe');
    expect(user.address?.city, 'Springfield');
  });
}
```

