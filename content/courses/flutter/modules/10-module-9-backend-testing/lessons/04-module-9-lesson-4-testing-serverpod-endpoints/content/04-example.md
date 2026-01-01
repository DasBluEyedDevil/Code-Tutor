---
type: "EXAMPLE"
title: "Basic Endpoint Testing with TestSession"
---

Serverpod provides TestSession for creating test contexts. Here is how to test a simple endpoint:

```dart
// test/integration/user_endpoint_test.dart
import 'package:test/test.dart';
import 'package:serverpod_test/serverpod_test.dart';
import 'package:my_project_server/src/generated/protocol.dart';
import 'package:my_project_server/src/generated/endpoints.dart';

void main() {
  late TestSession session;
  late UserEndpoint userEndpoint;

  setUpAll(() async {
    // Initialize test server with test database
    await TestServer.start();
  });

  tearDownAll(() async {
    await TestServer.stop();
  });

  setUp(() async {
    // Create fresh session for each test
    session = await TestSession.create();
    userEndpoint = UserEndpoint();
  });

  tearDown(() async {
    // Clean up session and test data
    await session.close();
    await TestDatabase.truncateAll();
  });

  group('UserEndpoint', () {
    test('getUser returns user by ID', () async {
      // Arrange - Create test user in database
      final testUser = User(
        id: 1,
        name: 'John Doe',
        email: 'john@example.com',
        createdAt: DateTime.now(),
      );
      await User.db.insertRow(session, testUser);

      // Act - Call the endpoint
      final result = await userEndpoint.getUser(session, 1);

      // Assert
      expect(result, isNotNull);
      expect(result!.name, equals('John Doe'));
      expect(result.email, equals('john@example.com'));
    });

    test('getUser returns null for non-existent ID', () async {
      final result = await userEndpoint.getUser(session, 999);

      expect(result, isNull);
    });

    test('createUser inserts user into database', () async {
      // Act
      final newUser = await userEndpoint.createUser(
        session,
        name: 'Jane Smith',
        email: 'jane@example.com',
      );

      // Assert
      expect(newUser.id, isNotNull);
      expect(newUser.name, equals('Jane Smith'));

      // Verify in database
      final dbUser = await User.db.findById(session, newUser.id!);
      expect(dbUser, isNotNull);
      expect(dbUser!.email, equals('jane@example.com'));
    });
  });
}
```
