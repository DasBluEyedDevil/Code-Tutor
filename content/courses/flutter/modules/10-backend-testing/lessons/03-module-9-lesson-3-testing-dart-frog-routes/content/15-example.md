---
type: "EXAMPLE"
title: "Integration Test Setup"
---

Here is how to set up integration tests for a complete API flow:

```dart
// test/integration/api_integration_test.dart
import 'dart:convert';
import 'dart:io';
import 'package:http/http.dart' as http;
import 'package:test/test.dart';

void main() {
  late HttpServer server;
  late Uri baseUri;

  setUpAll(() async {
    server = await HttpServer.bind(InternetAddress.loopbackIPv4, 0);
    baseUri = Uri.parse('http://localhost:${server.port}');
  });

  tearDownAll(() async {
    await server.close();
  });

  group('API Integration Tests', () {
    test('GET /health returns healthy status', () async {
      final response = await http.get(baseUri.resolve('/health'));

      expect(response.statusCode, equals(200));
      final body = jsonDecode(response.body) as Map<String, dynamic>;
      expect(body['status'], equals('healthy'));
    });

    test('full user CRUD flow', () async {
      // Create
      final createResponse = await http.post(
        baseUri.resolve('/users'),
        headers: {'content-type': 'application/json'},
        body: jsonEncode({'name': 'Test User', 'email': 'test@example.com'}),
      );
      expect(createResponse.statusCode, equals(201));
      final created = jsonDecode(createResponse.body) as Map<String, dynamic>;
      final userId = created['id'];

      // Read
      final getResponse = await http.get(baseUri.resolve('/users/$userId'));
      expect(getResponse.statusCode, equals(200));

      // Update
      final updateResponse = await http.put(
        baseUri.resolve('/users/$userId'),
        headers: {'content-type': 'application/json'},
        body: jsonEncode({'name': 'Updated Name'}),
      );
      expect(updateResponse.statusCode, equals(200));

      // Delete
      final deleteResponse = await http.delete(baseUri.resolve('/users/$userId'));
      expect(deleteResponse.statusCode, equals(204));

      // Verify deleted
      final verifyResponse = await http.get(baseUri.resolve('/users/$userId'));
      expect(verifyResponse.statusCode, equals(404));
    });
  });
}
```
