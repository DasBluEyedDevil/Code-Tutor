---
type: "EXAMPLE"
title: "Migration Strategies"
---


**Basic Migration Setup:**



```dart
@DriftDatabase(tables: [Notes, Categories, Tags])
class AppDatabase extends _$AppDatabase {
  AppDatabase() : super(_openConnection());
  
  @override
  int get schemaVersion => 3;
  
  @override
  MigrationStrategy get migration {
    return MigrationStrategy(
      // Called when database is created for the first time
      onCreate: (Migrator m) async {
        await m.createAll();
      },
      
      // Called when version increases
      onUpgrade: (Migrator m, int from, int to) async {
        // Version 1 -> 2: Add priority column to notes
        if (from < 2) {
          await m.addColumn(notes, notes.priority);
        }
        
        // Version 2 -> 3: Add tags table
        if (from < 3) {
          await m.createTable(tags);
        }
      },
      
      // Run before any migration
      beforeOpen: (details) async {
        // Enable foreign keys
        await customStatement('PRAGMA foreign_keys = ON');
        
        // Log migration info
        if (details.wasCreated) {
          print('Database created');
        }
        if (details.hadUpgrade) {
          print('Migrated from ${details.versionBefore} to ${details.versionNow}');
        }
      },
    );
  }
}
```
