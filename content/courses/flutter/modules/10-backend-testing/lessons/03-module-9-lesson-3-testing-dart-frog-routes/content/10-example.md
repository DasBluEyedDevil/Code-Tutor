---
type: "EXAMPLE"
title: "Testing Authentication Middleware"
---

Here is how to test authentication middleware that validates Bearer tokens:

```dart
// test/middleware/auth_middleware_test.dart
import 'package:dart_frog/dart_frog.dart';
import 'package:mocktail/mocktail.dart';
import 'package:test/test.dart';

import '../../routes/_middleware.dart';
import '../helpers/mock_context.dart';

void main() {
  group('authMiddleware', () {
    late Handler mockNextHandler;

    setUp(() {
      mockNextHandler = (_) async => Response(body: 'success');
    });

    test('returns 401 when no authorization header', () async {
      final context = createMockContext(headers: {});

      final middleware = authMiddleware(mockNextHandler);
      final response = await middleware(context);

      expect(response.statusCode, equals(401));
      expect(await response.body(), equals('Unauthorized'));
    });

    test('returns 401 when authorization is not Bearer', () async {
      final context = createMockContext(
        headers: {'authorization': 'Basic abc123'},
      );

      final middleware = authMiddleware(mockNextHandler);
      final response = await middleware(context);

      expect(response.statusCode, equals(401));
    });

    test('returns 403 when token is invalid', () async {
      final context = createMockContext(
        headers: {'authorization': 'Bearer invalid_token'},
      );

      final middleware = authMiddleware(mockNextHandler);
      final response = await middleware(context);

      expect(response.statusCode, equals(403));
    });

    test('calls next handler when token is valid', () async {
      var handlerCalled = false;
      final testHandler = (RequestContext ctx) async {
        handlerCalled = true;
        return Response(body: 'protected content');
      };

      final context = createMockContext(
        headers: {'authorization': 'Bearer valid_test_token'},
      );

      final middleware = authMiddleware(testHandler);
      await middleware(context);

      expect(handlerCalled, isTrue);
    });
  });
}
```
