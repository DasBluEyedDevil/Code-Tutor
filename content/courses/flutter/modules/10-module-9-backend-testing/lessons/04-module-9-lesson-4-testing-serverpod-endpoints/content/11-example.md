---
type: "EXAMPLE"
title: "Testing Authenticated Endpoints"
---

Here is how to test endpoints that require authentication:

```dart
// test/integration/authenticated_endpoint_test.dart
import 'package:test/test.dart';
import 'package:serverpod_test/serverpod_test.dart';
import 'package:serverpod_auth_server/serverpod_auth_server.dart';
import 'package:my_project_server/src/generated/protocol.dart';
import 'package:my_project_server/src/generated/endpoints.dart';

void main() {
  late TestSession session;
  late ProfileEndpoint profileEndpoint;

  setUpAll(() async {
    await TestServer.start();
  });

  tearDownAll(() async {
    await TestServer.stop();
  });

  setUp(() async {
    session = await TestSession.create();
    profileEndpoint = ProfileEndpoint();
  });

  tearDown(() async {
    await session.close();
    await TestDatabase.truncateAll();
  });

  group('Authenticated Endpoints', () {
    test('getMyProfile throws when not authenticated', () async {
      // Session has no authenticated user
      expect(
        () => profileEndpoint.getMyProfile(session),
        throwsA(isA<ServerpodException>()),
      );
    });

    test('getMyProfile returns profile when authenticated', () async {
      // Create a test user and authenticate the session
      final testUser = await createAuthenticatedUser(session, 
        email: 'test@example.com',
        name: 'Test User',
      );

      // Now session is authenticated
      final profile = await profileEndpoint.getMyProfile(session);

      expect(profile, isNotNull);
      expect(profile.userId, equals(testUser.id));
      expect(profile.email, equals('test@example.com'));
    });

    test('updateMyProfile updates authenticated user only', () async {
      // Create two users
      final user1 = await createAuthenticatedUser(session,
        email: 'user1@example.com',
        name: 'User One',
      );
      
      // Create second user (not authenticated)
      final user2 = User(
        name: 'User Two',
        email: 'user2@example.com',
        createdAt: DateTime.now(),
      );
      await User.db.insertRow(session, user2);

      // Update profile as user1
      await profileEndpoint.updateMyProfile(
        session,
        name: 'Updated User One',
      );

      // Verify only user1 was updated
      final updatedUser1 = await User.db.findById(session, user1.id!);
      final unchangedUser2 = await User.db.findById(session, user2.id!);

      expect(updatedUser1!.name, equals('Updated User One'));
      expect(unchangedUser2!.name, equals('User Two'));
    });

    test('admin endpoint requires admin role', () async {
      // Create regular user
      await createAuthenticatedUser(session,
        email: 'regular@example.com',
        name: 'Regular User',
        isAdmin: false,
      );

      // Try to access admin endpoint
      expect(
        () => profileEndpoint.adminDeleteUser(session, userId: 999),
        throwsA(isA<UnauthorizedException>()),
      );
    });

    test('admin endpoint succeeds for admin user', () async {
      // Create admin user
      await createAuthenticatedUser(session,
        email: 'admin@example.com',
        name: 'Admin User',
        isAdmin: true,
      );

      // Create user to delete
      final userToDelete = User(
        name: 'Delete Me',
        email: 'delete@example.com',
        createdAt: DateTime.now(),
      );
      final inserted = await User.db.insertRow(session, userToDelete);

      // Admin can delete
      await profileEndpoint.adminDeleteUser(session, userId: inserted.id!);

      // Verify deleted
      final deleted = await User.db.findById(session, inserted.id!);
      expect(deleted, isNull);
    });
  });
}

/// Helper to create an authenticated user for testing
Future<UserInfo> createAuthenticatedUser(
  TestSession session, {
  required String email,
  required String name,
  bool isAdmin = false,
}) async {
  // Create UserInfo for auth
  final userInfo = UserInfo(
    email: email,
    userName: name,
    userIdentifier: email,
    created: DateTime.now(),
    scopeNames: isAdmin ? ['admin'] : [],
  );
  final insertedUserInfo = await UserInfo.db.insertRow(session, userInfo);

  // Authenticate the session
  session.updateAuthentication(AuthenticationInfo(
    userInfo: insertedUserInfo,
    authId: insertedUserInfo.id!,
  ));

  return insertedUserInfo;
}
```
