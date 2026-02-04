---
type: KEY_POINT
---

- Increment `schemaVersion` in your database class every time you change the table structure (add columns, rename tables, etc.)
- Write explicit migration steps in `migration.onUpgrade` using `MigrationStrategy` -- never rely on automatic schema recreation in production
- Use `m.addColumn(table, column)` to add columns, `m.createTable(table)` for new tables, and `customStatement` for data transforms
- Test migrations by creating a database at the old schema version, running migration, and verifying data integrity survives the upgrade
- Drift's `schema_verifier` package catches mismatches between your Dart table definitions and actual SQLite schema at test time
