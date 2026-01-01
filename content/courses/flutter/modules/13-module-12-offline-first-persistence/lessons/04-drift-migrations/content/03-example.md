---
type: "EXAMPLE"
title: "Complex Migrations"
---


**Handling Data Transformations:**



```dart
@override
MigrationStrategy get migration {
  return MigrationStrategy(
    onUpgrade: (Migrator m, int from, int to) async {
      // Version 3 -> 4: Rename column and migrate data
      if (from < 4) {
        // Step 1: Add new column
        await m.addColumn(notes, notes.updatedAt);
        
        // Step 2: Copy data from old column (if needed)
        await customStatement(
          'UPDATE notes SET updated_at = created_at WHERE updated_at IS NULL'
        );
      }
      
      // Version 4 -> 5: Split name into firstName/lastName
      if (from < 5) {
        // Add new columns
        await m.addColumn(users, users.firstName);
        await m.addColumn(users, users.lastName);
        
        // Migrate data
        await customStatement('''
          UPDATE users SET
            first_name = SUBSTR(name, 1, INSTR(name, ' ') - 1),
            last_name = SUBSTR(name, INSTR(name, ' ') + 1)
          WHERE name LIKE '% %'
        ''');
        
        // Handle single names
        await customStatement('''
          UPDATE users SET first_name = name
          WHERE first_name IS NULL
        ''');
        
        // Note: Removing old column requires table recreation in SQLite
      }
      
      // Version 5 -> 6: Change column type (requires table recreation)
      if (from < 6) {
        // SQLite doesn't support ALTER COLUMN, need to recreate table
        await customStatement('''
          CREATE TABLE notes_new (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            title TEXT NOT NULL,
            content TEXT NOT NULL,
            priority INTEGER NOT NULL DEFAULT 0,
            created_at INTEGER NOT NULL,
            updated_at INTEGER
          )
        ''');
        
        await customStatement('''
          INSERT INTO notes_new SELECT * FROM notes
        ''');
        
        await customStatement('DROP TABLE notes');
        await customStatement('ALTER TABLE notes_new RENAME TO notes');
      }
    },
  );
}
```
