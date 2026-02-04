---
type: KEY_POINT
---

- Add `drift`, `sqlite3_flutter_libs`, and `drift_dev` (dev) to pubspec.yaml, then run `dart run build_runner build` to generate code
- Define tables as Dart classes extending `Table` with typed columns (`TextColumn`, `IntColumn`, `BoolColumn`, `DateTimeColumn`)
- Generated DAO methods provide type-safe CRUD: `into(table).insert(companion)`, `select(table)..where(...)`, `update(table)`, `delete(table)`
- Use `watch()` instead of `get()` on queries to return a reactive `Stream` that automatically emits when data changes
- Foreign keys and unique constraints are defined in the table class to enforce data integrity at the database level
