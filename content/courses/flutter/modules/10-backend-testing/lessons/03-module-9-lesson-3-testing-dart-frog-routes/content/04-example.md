---
type: "EXAMPLE"
title: "Testing a Simple Route Handler"
---

Let us start with the most basic test - verifying a simple route returns the expected response.

Consider this route handler in routes/health.dart:

```dart
import 'package:dart_frog/dart_frog.dart';

Response onRequest(RequestContext context) {
  return Response.json({
    'status': 'healthy',
    'timestamp': DateTime.now().toIso8601String(),
  });
}
```

Here is how to test it:

```dart
// test/routes/health_test.dart
import 'dart:convert';
import 'package:dart_frog/dart_frog.dart';
import 'package:mocktail/mocktail.dart';
import 'package:test/test.dart';

import '../../routes/health.dart' as route;

class MockRequestContext extends Mock implements RequestContext {}

void main() {
  group('GET /health', () {
    late MockRequestContext context;

    setUp(() {
      context = MockRequestContext();
    });

    test('returns 200 with healthy status', () async {
      final response = route.onRequest(context);

      expect(response.statusCode, equals(200));

      final body = await response.body();
      final json = jsonDecode(body) as Map<String, dynamic>;

      expect(json['status'], equals('healthy'));
      expect(json['timestamp'], isNotNull);
    });

    test('returns JSON content type', () async {
      final response = route.onRequest(context);

      expect(
        response.headers['content-type'],
        contains('application/json'),
      );
    });
  });
}
```
