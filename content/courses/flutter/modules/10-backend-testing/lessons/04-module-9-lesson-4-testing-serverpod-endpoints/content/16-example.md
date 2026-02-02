---
type: "EXAMPLE"
title: "Database Cleanup Patterns"
---

Here are different cleanup approaches for different scenarios:

```dart
// test/test_utils/database_helpers.dart
import 'package:serverpod_test/serverpod_test.dart';

/// Pattern 1: Truncate all tables (fastest for full cleanup)
Future<void> cleanupAllTables() async {
  await TestDatabase.truncateAll();
}

/// Pattern 2: Transaction-based cleanup (auto-rollback)
Future<T> withRollback<T>(
  TestSession session,
  Future<T> Function() testBody,
) async {
  T result;
  await session.db.transaction((transaction) async {
    result = await testBody();
    throw RollbackException(); // Force rollback
  }).catchError((_) {});
  return result!;
}

class RollbackException implements Exception {}

/// Pattern 3: Track and delete specific records
class TestDataTracker {
  final List<int> _userIds = [];
  final List<int> _postIds = [];

  Future<User> createUser(TestSession session, User user) async {
    final inserted = await User.db.insertRow(session, user);
    _userIds.add(inserted.id!);
    return inserted;
  }

  Future<Post> createPost(TestSession session, Post post) async {
    final inserted = await Post.db.insertRow(session, post);
    _postIds.add(inserted.id!);
    return inserted;
  }

  Future<void> cleanup(TestSession session) async {
    // Delete in reverse order to respect foreign keys
    for (final postId in _postIds.reversed) {
      await Post.db.deleteWhere(
        session,
        where: (t) => t.id.equals(postId),
      );
    }
    for (final userId in _userIds.reversed) {
      await User.db.deleteWhere(
        session,
        where: (t) => t.id.equals(userId),
      );
    }
    _postIds.clear();
    _userIds.clear();
  }
}

// Usage in tests:
void main() {
  late TestSession session;
  late TestDataTracker tracker;

  setUp(() async {
    session = await TestSession.create();
    tracker = TestDataTracker();
  });

  tearDown(() async {
    await tracker.cleanup(session);
    await session.close();
  });

  test('uses tracked cleanup', () async {
    final user = await tracker.createUser(
      session,
      User(name: 'Test', email: 'test@test.com', createdAt: DateTime.now()),
    );

    final post = await tracker.createPost(
      session,
      Post(title: 'Test Post', authorId: user.id!, createdAt: DateTime.now()),
    );

    // Test logic here...
    // Cleanup happens automatically in tearDown
  });
}
```
