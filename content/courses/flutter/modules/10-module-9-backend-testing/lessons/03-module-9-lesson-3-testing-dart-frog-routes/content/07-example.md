---
type: "EXAMPLE"
title: "Complete RequestContext Mocking"
---

Here is a comprehensive example showing how to mock various aspects of RequestContext:

```dart
// test/helpers/mock_context.dart
import 'dart:convert';
import 'package:dart_frog/dart_frog.dart';
import 'package:mocktail/mocktail.dart';

class MockRequestContext extends Mock implements RequestContext {}
class MockRequest extends Mock implements Request {}
class MockUri extends Mock implements Uri {}

/// Creates a fully configured mock context for testing
MockRequestContext createMockContext({
  String method = 'GET',
  Map<String, String>? headers,
  Map<String, String>? pathParameters,
  Map<String, String>? queryParameters,
  Object? body,
  String path = '/',
}) {
  final context = MockRequestContext();
  final request = MockRequest();
  final uri = MockUri();

  // Configure URI
  when(() => uri.path).thenReturn(path);
  when(() => uri.queryParameters).thenReturn(queryParameters ?? {});

  // Configure Request
  when(() => request.method).thenReturn(HttpMethod(method));
  when(() => request.headers).thenReturn(headers ?? {});
  when(() => request.uri).thenReturn(uri);

  if (body != null) {
    final jsonBody = jsonEncode(body);
    when(() => request.body()).thenAnswer((_) async => jsonBody);
    when(() => request.json()).thenAnswer((_) async => body);
  } else {
    when(() => request.body()).thenAnswer((_) async => '');
  }

  // Configure Context
  when(() => context.request).thenReturn(request);

  return context;
}
```
