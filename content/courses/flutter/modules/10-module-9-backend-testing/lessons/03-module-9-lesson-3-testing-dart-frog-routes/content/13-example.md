---
type: "EXAMPLE"
title: "Testing JSON Body Parsing"
---

Here is how to test POST requests with JSON body validation:

```dart
// test/routes/users/create_user_test.dart
import 'dart:convert';
import 'package:dart_frog/dart_frog.dart';
import 'package:mocktail/mocktail.dart';
import 'package:test/test.dart';

void main() {
  group('POST /users', () {
    late MockRequestContext context;
    late MockRequest request;
    late MockUserRepository userRepository;

    setUp(() {
      context = MockRequestContext();
      request = MockRequest();
      userRepository = MockUserRepository();

      when(() => context.request).thenReturn(request);
      when(() => request.method).thenReturn(HttpMethod.post);
      when(() => context.read<UserRepository>()).thenReturn(userRepository);
    });

    test('creates user with valid data', () async {
      when(() => request.json()).thenAnswer((_) async => {
        'name': 'John Doe',
        'email': 'john@example.com',
      });

      final createdUser = User(id: '123', name: 'John Doe', email: 'john@example.com');
      when(() => userRepository.create(
        name: 'John Doe',
        email: 'john@example.com',
      )).thenAnswer((_) async => createdUser);

      final response = await route.onRequest(context);

      expect(response.statusCode, equals(201));
      final body = jsonDecode(await response.body()) as Map<String, dynamic>;
      expect(body['id'], equals('123'));
    });

    test('returns 400 when name is missing', () async {
      when(() => request.json()).thenAnswer((_) async => {
        'email': 'john@example.com',
      });

      final response = await route.onRequest(context);

      expect(response.statusCode, equals(400));
    });

    test('returns 400 for invalid JSON', () async {
      when(() => request.json()).thenThrow(FormatException('Invalid JSON'));

      final response = await route.onRequest(context);

      expect(response.statusCode, equals(400));
    });
  });
}
```
