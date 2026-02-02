---
type: "THEORY"
title: "Drift Package Setup"
---


**Step 1: Add Dependencies**

```yaml
dependencies:
  drift: ^2.14.0
  sqlite3_flutter_libs: ^0.5.0
  path_provider: ^2.1.0
  path: ^1.8.0

dev_dependencies:
  drift_dev: ^2.14.0
  build_runner: ^2.4.0
```

**Step 2: Create Database File**

Create `lib/database/database.dart`:



```dart
import 'dart:io';
import 'package:drift/drift.dart';
import 'package:drift/native.dart';
import 'package:path_provider/path_provider.dart';
import 'package:path/path.dart' as p;

part 'database.g.dart';

// Define your tables
class Notes extends Table {
  IntColumn get id => integer().autoIncrement()();
  TextColumn get title => text().withLength(min: 1, max: 200)();
  TextColumn get content => text()();
  DateTimeColumn get createdAt => dateTime().withDefault(currentDateAndTime)();
  DateTimeColumn get updatedAt => dateTime().nullable()();
  BoolColumn get isArchived => boolean().withDefault(const Constant(false))();
}

class Categories extends Table {
  IntColumn get id => integer().autoIncrement()();
  TextColumn get name => text().unique()();
  TextColumn get color => text().withDefault(const Constant('#000000'))();
}

class NoteCategories extends Table {
  IntColumn get noteId => integer().references(Notes, #id, 
      onDelete: KeyAction.cascade)();
  IntColumn get categoryId => integer().references(Categories, #id)();
  
  @override
  Set<Column> get primaryKey => {noteId, categoryId};
}

@DriftDatabase(tables: [Notes, Categories, NoteCategories])
class AppDatabase extends _$AppDatabase {
  AppDatabase() : super(_openConnection());
  
  @override
  int get schemaVersion => 1;
}

LazyDatabase _openConnection() {
  return LazyDatabase(() async {
    final dbFolder = await getApplicationDocumentsDirectory();
    final file = File(p.join(dbFolder.path, 'app.db'));
    return NativeDatabase.createInBackground(file);
  });
}
```
