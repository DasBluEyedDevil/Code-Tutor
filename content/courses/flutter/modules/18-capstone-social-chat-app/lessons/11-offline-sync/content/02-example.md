---
type: "EXAMPLE"
title: "Local Database Setup"
---


**Drift Database Configuration for Offline Storage**

Drift (formerly Moor) provides a type-safe, reactive database layer for Flutter. This configuration creates tables for posts, messages, and users with DAOs for CRUD operations and reactive queries.



```dart
// lib/core/database/app_database.dart
import 'dart:io';
import 'package:drift/drift.dart';
import 'package:drift/native.dart';
import 'package:path_provider/path_provider.dart';
import 'package:path/path.dart' as p;
import 'tables/posts_table.dart';
import 'tables/messages_table.dart';
import 'tables/users_table.dart';
import 'tables/sync_queue_table.dart';
import 'daos/posts_dao.dart';
import 'daos/messages_dao.dart';
import 'daos/users_dao.dart';
import 'daos/sync_queue_dao.dart';

part 'app_database.g.dart';

@DriftDatabase(
  tables: [
    LocalPosts,
    LocalMessages,
    LocalUsers,
    SyncQueue,
  ],
  daos: [
    PostsDao,
    MessagesDao,
    UsersDao,
    SyncQueueDao,
  ],
)
class AppDatabase extends _$AppDatabase {
  AppDatabase() : super(_openConnection());

  @override
  int get schemaVersion => 1;

  @override
  MigrationStrategy get migration {
    return MigrationStrategy(
      onCreate: (Migrator m) async {
        await m.createAll();
      },
      onUpgrade: (Migrator m, int from, int to) async {
        // Handle schema migrations here
      },
    );
  }

  // Clear all local data (for logout)
  Future<void> clearAllData() async {
    await delete(localPosts).go();
    await delete(localMessages).go();
    await delete(localUsers).go();
    await delete(syncQueue).go();
  }
}

LazyDatabase _openConnection() {
  return LazyDatabase(() async {
    final dbFolder = await getApplicationDocumentsDirectory();
    final file = File(p.join(dbFolder.path, 'app_database.sqlite'));
    return NativeDatabase.createInBackground(file);
  });
}

---

// lib/core/database/tables/posts_table.dart
import 'package:drift/drift.dart';

enum SyncStatus { synced, pending, syncing, failed, conflict }

class LocalPosts extends Table {
  // Primary key - use server ID when available, or local UUID
  TextColumn get id => text()();
  TextColumn get localId => text().nullable()(); // Local UUID before server assigns ID
  TextColumn get userId => text()();
  TextColumn get content => text()();
  TextColumn get imageUrls => text().nullable()(); // JSON array of URLs
  TextColumn get localImagePaths => text().nullable()(); // JSON array of local paths
  IntColumn get likesCount => integer().withDefault(const Constant(0))();
  IntColumn get commentsCount => integer().withDefault(const Constant(0))();
  BoolColumn get isLikedByMe => boolean().withDefault(const Constant(false))();
  DateTimeColumn get createdAt => dateTime()();
  DateTimeColumn get updatedAt => dateTime()();
  DateTimeColumn get localModifiedAt => dateTime().nullable()(); // Track local changes
  IntColumn get syncStatus => intEnum<SyncStatus>().withDefault(const Constant(0))();
  TextColumn get syncError => text().nullable()(); // Store error message if sync failed
  IntColumn get syncRetryCount => integer().withDefault(const Constant(0))();

  @override
  Set<Column> get primaryKey => {id};

  @override
  List<Set<Column>> get uniqueKeys => [
        {localId},
      ];
}

---

// lib/core/database/tables/messages_table.dart
import 'package:drift/drift.dart';
import 'posts_table.dart'; // For SyncStatus enum

class LocalMessages extends Table {
  TextColumn get id => text()();
  TextColumn get localId => text().nullable()();
  TextColumn get conversationId => text()();
  TextColumn get senderId => text()();
  TextColumn get receiverId => text()();
  TextColumn get content => text()();
  TextColumn get mediaUrl => text().nullable()();
  TextColumn get localMediaPath => text().nullable()();
  TextColumn get mediaType => text().nullable()(); // image, video, audio
  BoolColumn get isRead => boolean().withDefault(const Constant(false))();
  DateTimeColumn get createdAt => dateTime()();
  DateTimeColumn get localModifiedAt => dateTime().nullable()();
  IntColumn get syncStatus => intEnum<SyncStatus>().withDefault(const Constant(0))();
  TextColumn get syncError => text().nullable()();

  @override
  Set<Column> get primaryKey => {id};
}

---

// lib/core/database/tables/users_table.dart
import 'package:drift/drift.dart';

class LocalUsers extends Table {
  TextColumn get id => text()();
  TextColumn get username => text()();
  TextColumn get displayName => text()();
  TextColumn get avatarUrl => text().nullable()();
  TextColumn get bio => text().nullable()();
  IntColumn get followersCount => integer().withDefault(const Constant(0))();
  IntColumn get followingCount => integer().withDefault(const Constant(0))();
  IntColumn get postsCount => integer().withDefault(const Constant(0))();
  BoolColumn get isFollowing => boolean().withDefault(const Constant(false))();
  DateTimeColumn get lastFetched => dateTime()(); // Cache invalidation

  @override
  Set<Column> get primaryKey => {id};
}

---

// lib/core/database/tables/sync_queue_table.dart
import 'package:drift/drift.dart';

enum SyncAction { create, update, delete }
enum SyncEntityType { post, message, comment, like, follow }

class SyncQueue extends Table {
  IntColumn get id => integer().autoIncrement()();
  IntColumn get entityType => intEnum<SyncEntityType>()();
  TextColumn get entityId => text()();
  IntColumn get action => intEnum<SyncAction>()();
  TextColumn get payload => text()(); // JSON payload for the action
  DateTimeColumn get createdAt => dateTime()();
  IntColumn get retryCount => integer().withDefault(const Constant(0))();
  DateTimeColumn get lastAttempt => dateTime().nullable()();
  TextColumn get errorMessage => text().nullable()();
  IntColumn get priority => integer().withDefault(const Constant(0))(); // Higher = process first
}

---

// lib/core/database/daos/posts_dao.dart
import 'package:drift/drift.dart';
import '../app_database.dart';
import '../tables/posts_table.dart';

part 'posts_dao.g.dart';

@DriftAccessor(tables: [LocalPosts])
class PostsDao extends DatabaseAccessor<AppDatabase> with _$PostsDaoMixin {
  PostsDao(super.db);

  // Get all posts as a reactive stream
  Stream<List<LocalPost>> watchAllPosts() {
    return (select(localPosts)
          ..orderBy([(p) => OrderingTerm.desc(p.createdAt)]))
        .watch();
  }

  // Get posts by user
  Stream<List<LocalPost>> watchUserPosts(String userId) {
    return (select(localPosts)
          ..where((p) => p.userId.equals(userId))
          ..orderBy([(p) => OrderingTerm.desc(p.createdAt)]))
        .watch();
  }

  // Get a single post
  Future<LocalPost?> getPost(String id) {
    return (select(localPosts)..where((p) => p.id.equals(id)))
        .getSingleOrNull();
  }

  // Get posts pending sync
  Future<List<LocalPost>> getPendingPosts() {
    return (select(localPosts)
          ..where((p) => p.syncStatus.equals(SyncStatus.pending.index))
          ..orderBy([(p) => OrderingTerm.asc(p.createdAt)]))
        .get();
  }

  // Insert or update post
  Future<void> upsertPost(LocalPostsCompanion post) {
    return into(localPosts).insertOnConflictUpdate(post);
  }

  // Batch insert posts from server
  Future<void> upsertPosts(List<LocalPostsCompanion> posts) async {
    await batch((batch) {
      batch.insertAllOnConflictUpdate(localPosts, posts);
    });
  }

  // Update sync status
  Future<void> updateSyncStatus(
    String id,
    SyncStatus status, {
    String? errorMessage,
    int? retryCount,
  }) {
    return (update(localPosts)..where((p) => p.id.equals(id))).write(
      LocalPostsCompanion(
        syncStatus: Value(status),
        syncError: Value(errorMessage),
        syncRetryCount: retryCount != null ? Value(retryCount) : const Value.absent(),
      ),
    );
  }

  // Delete post
  Future<void> deletePost(String id) {
    return (delete(localPosts)..where((p) => p.id.equals(id))).go();
  }

  // Get failed sync posts
  Future<List<LocalPost>> getFailedPosts() {
    return (select(localPosts)
          ..where((p) => p.syncStatus.equals(SyncStatus.failed.index))
          ..where((p) => p.syncRetryCount.isSmallerThanValue(3)))
        .get();
  }

  // Update post with server response (after successful sync)
  Future<void> updateFromServer(String localId, LocalPostsCompanion serverData) {
    return (update(localPosts)..where((p) => p.localId.equals(localId)))
        .write(serverData);
  }
}

---

// lib/core/database/daos/sync_queue_dao.dart
import 'package:drift/drift.dart';
import '../app_database.dart';
import '../tables/sync_queue_table.dart';

part 'sync_queue_dao.g.dart';

@DriftAccessor(tables: [SyncQueue])
class SyncQueueDao extends DatabaseAccessor<AppDatabase> with _$SyncQueueDaoMixin {
  SyncQueueDao(super.db);

  // Watch pending items count
  Stream<int> watchPendingCount() {
    final countExp = syncQueue.id.count();
    return (selectOnly(syncQueue)..addColumns([countExp]))
        .map((row) => row.read(countExp) ?? 0)
        .watchSingle();
  }

  // Get next items to sync (ordered by priority and creation time)
  Future<List<SyncQueueData>> getNextBatch({int limit = 10}) {
    return (select(syncQueue)
          ..orderBy([
            (q) => OrderingTerm.desc(q.priority),
            (q) => OrderingTerm.asc(q.createdAt),
          ])
          ..limit(limit))
        .get();
  }

  // Add to queue
  Future<int> enqueue(SyncQueueCompanion item) {
    return into(syncQueue).insert(item);
  }

  // Remove from queue after successful sync
  Future<void> dequeue(int id) {
    return (delete(syncQueue)..where((q) => q.id.equals(id))).go();
  }

  // Update retry info after failed attempt
  Future<void> markRetry(int id, String errorMessage) {
    return (update(syncQueue)..where((q) => q.id.equals(id))).write(
      SyncQueueCompanion(
        retryCount: syncQueue.retryCount + const Constant(1),
        lastAttempt: Value(DateTime.now()),
        errorMessage: Value(errorMessage),
      ),
    );
  }

  // Remove items that exceeded max retries
  Future<void> removeFailedItems({int maxRetries = 5}) {
    return (delete(syncQueue)
          ..where((q) => q.retryCount.isBiggerOrEqualValue(maxRetries)))
        .go();
  }

  // Check if entity already has pending action
  Future<bool> hasPendingAction(SyncEntityType type, String entityId) async {
    final result = await (select(syncQueue)
          ..where((q) => q.entityType.equals(type.index))
          ..where((q) => q.entityId.equals(entityId)))
        .get();
    return result.isNotEmpty;
  }

  // Clear all pending actions for an entity (e.g., when deleted)
  Future<void> clearEntityActions(SyncEntityType type, String entityId) {
    return (delete(syncQueue)
          ..where((q) => q.entityType.equals(type.index))
          ..where((q) => q.entityId.equals(entityId)))
        .go();
  }
}
```
