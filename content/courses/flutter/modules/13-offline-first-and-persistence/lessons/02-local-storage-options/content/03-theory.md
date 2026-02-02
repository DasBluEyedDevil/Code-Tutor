---
type: "THEORY"
title: "Drift Introduction"
---


**Drift** (formerly Moor) is a reactive persistence library for Flutter with compile-time SQL verification.

**Best For:**
- Complex data relationships
- Type-safe SQL queries
- Reactive data streams
- Schema migrations
- Large datasets

**Features:**
- Compile-time query validation
- Auto-generated code
- Reactive streams
- Transaction support
- Migration system



```dart
// Add to pubspec.yaml:
// drift: ^2.14.0
// sqlite3_flutter_libs: ^0.5.0
// path_provider: ^2.1.0
// path: ^1.8.0
// dev_dependencies:
//   drift_dev: ^2.14.0
//   build_runner: ^2.4.0

import 'package:drift/drift.dart';

// Define tables
class Notes extends Table {
  IntColumn get id => integer().autoIncrement()();
  TextColumn get title => text().withLength(min: 1, max: 100)();
  TextColumn get content => text()();
  DateTimeColumn get createdAt => dateTime().withDefault(currentDateAndTime)();
  DateTimeColumn get updatedAt => dateTime().nullable()();
  BoolColumn get isSynced => boolean().withDefault(const Constant(false))();
}

class Tags extends Table {
  IntColumn get id => integer().autoIncrement()();
  TextColumn get name => text().unique()();
}

class NoteTags extends Table {
  IntColumn get noteId => integer().references(Notes, #id)();
  IntColumn get tagId => integer().references(Tags, #id)();
  
  @override
  Set<Column> get primaryKey => {noteId, tagId};
}
```
