---
type: "EXAMPLE"
title: "Testing Query Parameters and Pagination"
---

Here is how to test query parameters for pagination and filtering:

```dart
// test/routes/users/index_test.dart
import 'dart:convert';
import 'package:test/test.dart';
import '../../../routes/users/index.dart' as route;
import '../../helpers/mock_context.dart';

void main() {
  group('GET /users', () {
    late MockUserRepository userRepository;

    setUp(() {
      userRepository = MockUserRepository();
    });

    test('uses default pagination when no params', () async {
      final context = createMockContext(queryParameters: {});
      when(() => context.read<UserRepository>()).thenReturn(userRepository);
      when(() => userRepository.findAll(page: 1, limit: 10, status: null))
          .thenAnswer((_) async => []);

      final response = await route.onRequest(context);
      final body = jsonDecode(await response.body()) as Map<String, dynamic>;

      expect(body['page'], equals(1));
      expect(body['limit'], equals(10));
    });

    test('respects custom pagination', () async {
      final context = createMockContext(
        queryParameters: {'page': '3', 'limit': '25'},
      );
      when(() => context.read<UserRepository>()).thenReturn(userRepository);
      when(() => userRepository.findAll(page: 3, limit: 25, status: null))
          .thenAnswer((_) async => []);

      final response = await route.onRequest(context);
      final body = jsonDecode(await response.body()) as Map<String, dynamic>;

      expect(body['page'], equals(3));
      expect(body['limit'], equals(25));
    });

    test('returns 400 when limit exceeds 100', () async {
      final context = createMockContext(
        queryParameters: {'limit': '150'},
      );
      when(() => context.read<UserRepository>()).thenReturn(userRepository);

      final response = await route.onRequest(context);

      expect(response.statusCode, equals(400));
      expect(await response.body(), contains('cannot exceed'));
    });

    test('filters by status when provided', () async {
      final context = createMockContext(
        queryParameters: {'status': 'active'},
      );
      when(() => context.read<UserRepository>()).thenReturn(userRepository);
      when(() => userRepository.findAll(
        page: any(named: 'page'),
        limit: any(named: 'limit'),
        status: 'active',
      )).thenAnswer((_) async => []);

      await route.onRequest(context);

      verify(() => userRepository.findAll(
        page: any(named: 'page'),
        limit: any(named: 'limit'),
        status: 'active',
      )).called(1);
    });
  });
}
```
