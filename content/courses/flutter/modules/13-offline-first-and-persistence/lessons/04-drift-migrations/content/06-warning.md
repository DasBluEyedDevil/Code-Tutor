---
type: WARNING
---

**Schema changes without explicit migrations destroy user data.** If you change a table definition and increment `schemaVersion` without writing a migration step, Drift will attempt to recreate the database from scratch on existing devices. This means all locally stored data is permanently lost.

```dart
// WRONG - no migration step, data is deleted
@override
int get schemaVersion => 2; // Was 1, no onUpgrade defined

// RIGHT - explicit migration preserves data
@override
MigrationStrategy get migration => MigrationStrategy(
  onUpgrade: (migrator, from, to) async {
    if (from < 2) {
      await migrator.addColumn(notes, notes.category);
    }
  },
);
```

Test every migration path before releasing: create a database at the old schema version, populate it with test data, run the migration, and verify all data survived. Users on version 1 might update directly to version 5, so migration steps must be cumulative and order-independent.
