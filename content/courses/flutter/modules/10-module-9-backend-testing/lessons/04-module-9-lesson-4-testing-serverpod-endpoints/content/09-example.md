---
type: "EXAMPLE"
title: "Database Integration Tests"
---

Here is a complete example of testing database operations:

```dart
// test/integration/database_operations_test.dart
import 'package:test/test.dart';
import 'package:serverpod_test/serverpod_test.dart';
import 'package:my_project_server/src/generated/protocol.dart';

void main() {
  late TestSession session;

  setUpAll(() async {
    await TestServer.start();
  });

  tearDownAll(() async {
    await TestServer.stop();
  });

  setUp(() async {
    session = await TestSession.create();
  });

  tearDown(() async {
    await session.close();
    // Clean all tables between tests
    await TestDatabase.truncateAll();
  });

  group('User Database Operations', () {
    test('insert and retrieve user', () async {
      // Create user
      final user = User(
        name: 'Database Test User',
        email: 'dbtest@example.com',
        createdAt: DateTime.now(),
      );

      // Insert
      final inserted = await User.db.insertRow(session, user);
      expect(inserted.id, isNotNull);

      // Retrieve
      final retrieved = await User.db.findById(session, inserted.id!);
      expect(retrieved, isNotNull);
      expect(retrieved!.name, equals('Database Test User'));
    });

    test('update user preserves other fields', () async {
      // Create user
      final user = User(
        name: 'Original Name',
        email: 'original@example.com',
        createdAt: DateTime.now(),
      );
      final inserted = await User.db.insertRow(session, user);

      // Update name only
      inserted.name = 'Updated Name';
      await User.db.updateRow(session, inserted);

      // Verify email unchanged
      final updated = await User.db.findById(session, inserted.id!);
      expect(updated!.name, equals('Updated Name'));
      expect(updated.email, equals('original@example.com'));
    });

    test('delete removes user from database', () async {
      // Create user
      final user = User(
        name: 'Delete Me',
        email: 'delete@example.com',
        createdAt: DateTime.now(),
      );
      final inserted = await User.db.insertRow(session, user);

      // Delete
      await User.db.deleteRow(session, inserted);

      // Verify gone
      final deleted = await User.db.findById(session, inserted.id!);
      expect(deleted, isNull);
    });

    test('find with where clause filters correctly', () async {
      // Create multiple users
      await User.db.insert(session, [
        User(name: 'Active User 1', email: 'a1@test.com', isActive: true, createdAt: DateTime.now()),
        User(name: 'Active User 2', email: 'a2@test.com', isActive: true, createdAt: DateTime.now()),
        User(name: 'Inactive User', email: 'inactive@test.com', isActive: false, createdAt: DateTime.now()),
      ]);

      // Find only active users
      final activeUsers = await User.db.find(
        session,
        where: (t) => t.isActive.equals(true),
      );

      expect(activeUsers, hasLength(2));
      expect(activeUsers.every((u) => u.isActive == true), isTrue);
    });

    test('transactions roll back on error', () async {
      // Create initial user
      final user = User(
        name: 'Transaction Test',
        email: 'transaction@test.com',
        createdAt: DateTime.now(),
      );
      await User.db.insertRow(session, user);

      // Attempt transaction that fails
      try {
        await session.db.transaction((transaction) async {
          // This succeeds
          await User.db.insertRow(
            session,
            User(name: 'New User', email: 'new@test.com', createdAt: DateTime.now()),
          );

          // This fails (duplicate email if unique constraint)
          throw Exception('Simulated failure');
        });
      } catch (_) {}

      // Verify transaction rolled back
      final users = await User.db.find(session);
      expect(users, hasLength(1)); // Only original user
    });
  });

  group('Relationship Tests', () {
    test('user posts relationship works correctly', () async {
      // Create user
      final user = User(
        name: 'Author',
        email: 'author@test.com',
        createdAt: DateTime.now(),
      );
      final insertedUser = await User.db.insertRow(session, user);

      // Create posts for user
      await Post.db.insert(session, [
        Post(title: 'Post 1', authorId: insertedUser.id!, createdAt: DateTime.now()),
        Post(title: 'Post 2', authorId: insertedUser.id!, createdAt: DateTime.now()),
      ]);

      // Find user with posts
      final userWithPosts = await User.db.findById(
        session,
        insertedUser.id!,
        include: User.include(posts: Post.includeList()),
      );

      expect(userWithPosts!.posts, hasLength(2));
    });
  });
}
```
