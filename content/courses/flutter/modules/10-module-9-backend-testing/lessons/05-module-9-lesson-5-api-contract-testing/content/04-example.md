---
type: "EXAMPLE"
title: "Serverpod Model and Endpoint Testing"
---


Here is how to test Serverpod models and endpoints for contract compliance:



```dart
// test/protocol/user_test.dart
import 'package:test/test.dart';
import 'package:my_server/src/generated/protocol.dart';

void main() {
  group('User model contract', () {
    test('serializes all required fields to JSON', () {
      final user = User(
        id: 1,
        name: 'John Doe',
        email: 'john@example.com',
        createdAt: DateTime(2024, 1, 15),
      );
      
      final json = user.toJson();
      
      expect(json['id'], 1);
      expect(json['name'], 'John Doe');
      expect(json['email'], 'john@example.com');
      expect(json['createdAt'], isNotNull);
    });
    
    test('deserializes from JSON correctly', () {
      final json = {
        'id': 42,
        'name': 'Jane Smith',
        'email': 'jane@example.com',
        'createdAt': '2024-01-15T10:30:00.000Z',
      };
      
      final user = User.fromJson(json);
      
      expect(user.id, 42);
      expect(user.name, 'Jane Smith');
      expect(user.email, 'jane@example.com');
      expect(user.createdAt.year, 2024);
    });
    
    test('roundtrip serialization preserves data', () {
      final original = User(
        id: 99,
        name: 'Roundtrip Test',
        email: 'round@trip.com',
        createdAt: DateTime.now(),
      );
      
      final json = original.toJson();
      final restored = User.fromJson(json);
      
      expect(restored.id, original.id);
      expect(restored.name, original.name);
      expect(restored.email, original.email);
    });
  });
}
```
