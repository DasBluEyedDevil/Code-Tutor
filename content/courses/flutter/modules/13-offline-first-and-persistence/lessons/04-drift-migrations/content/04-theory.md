---
type: "THEORY"
title: "Testing Migrations"
---


**Why Test Migrations:**
- Catch data loss before production
- Verify data transformations work
- Ensure backwards compatibility
- Prevent app crashes on update

**Migration Testing Strategy:**



```dart
import 'package:drift_dev/api/migrations.dart';
import 'package:test/test.dart';

void main() {
  late SchemaVerifier verifier;
  
  setUpAll(() {
    // Load generated schema files
    verifier = SchemaVerifier(GeneratedHelper());
  });
  
  test('upgrade from v1 to v2', () async {
    // Create database at version 1
    final connection = await verifier.startAt(1);
    final db = AppDatabase.connect(connection);
    
    // Insert test data at v1 schema
    await db.customStatement(
      "INSERT INTO notes (title, content) VALUES ('Test', 'Content')"
    );
    
    // Verify migration to v2
    await verifier.migrateAndValidate(db, 2);
    
    // Verify data survived
    final notes = await db.select(db.notes).get();
    expect(notes.length, 1);
    expect(notes.first.title, 'Test');
    
    // Verify new column exists with default
    expect(notes.first.priority, 0);
    
    await db.close();
  });
  
  test('upgrade from v2 to v3', () async {
    final connection = await verifier.startAt(2);
    final db = AppDatabase.connect(connection);
    
    await verifier.migrateAndValidate(db, 3);
    
    // Verify new table exists
    await db.select(db.tags).get(); // Should not throw
    
    await db.close();
  });
  
  test('full upgrade from v1 to latest', () async {
    final connection = await verifier.startAt(1);
    final db = AppDatabase.connect(connection);
    
    // Insert data
    await db.customStatement(
      "INSERT INTO notes (title, content) VALUES ('Important', 'Keep this')"
    );
    
    // Run all migrations
    await verifier.migrateAndValidate(db, db.schemaVersion);
    
    // Verify data integrity
    final notes = await db.select(db.notes).get();
    expect(notes.first.title, 'Important');
    
    await db.close();
  });
}
```
