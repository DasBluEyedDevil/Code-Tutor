---
type: "EXAMPLE"
title: "Testing Route Parameters"
---

Here is how to test dynamic route parameters like /users/[id]:

```dart
// test/routes/users/user_id_test.dart
import 'dart:convert';
import 'package:dart_frog/dart_frog.dart';
import 'package:mocktail/mocktail.dart';
import 'package:test/test.dart';

import '../../../routes/users/[id].dart' as route;

class MockUserRepository extends Mock implements UserRepository {}

void main() {
  group('GET /users/:id', () {
    late MockRequestContext context;
    late MockUserRepository userRepository;

    setUp(() {
      context = MockRequestContext();
      userRepository = MockUserRepository();
      when(() => context.read<UserRepository>()).thenReturn(userRepository);
    });

    test('returns 400 when id is empty', () async {
      final response = await route.onRequest(context, '');

      expect(response.statusCode, equals(400));
      expect(await response.body(), contains('required'));
    });

    test('returns 404 when user not found', () async {
      when(() => userRepository.findById('nonexistent'))
          .thenAnswer((_) async => null);

      final response = await route.onRequest(context, 'nonexistent');

      expect(response.statusCode, equals(404));
    });

    test('returns user when found', () async {
      final testUser = User(id: '123', name: 'John Doe', email: 'john@example.com');
      when(() => userRepository.findById('123'))
          .thenAnswer((_) async => testUser);

      final response = await route.onRequest(context, '123');

      expect(response.statusCode, equals(200));
      final body = jsonDecode(await response.body()) as Map<String, dynamic>;
      expect(body['id'], equals('123'));
      expect(body['name'], equals('John Doe'));
    });
  });
}
```
