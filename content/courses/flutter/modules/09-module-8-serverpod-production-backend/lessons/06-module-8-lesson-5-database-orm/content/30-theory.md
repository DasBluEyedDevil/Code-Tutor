---
type: "THEORY"
title: "Summary: Database Operations Checklist"
---

**Configuration:**
- Database settings in `config/development.yaml`
- Passwords in `config/passwords.yaml` (gitignored)
- Docker provides PostgreSQL and Redis locally

**Models:**
- Define in `lib/src/protocol/*.yaml`
- Add `table:` for database persistence
- The `id` field is automatic for database tables
- Run `serverpod generate` after changes

**CRUD Operations:**
- Create: `Model.db.insertRow()` or `insert()`
- Read: `Model.db.findById()`, `find()`, `findFirstRow()`, `count()`
- Update: `Model.db.updateRow()` or `update()`
- Delete: `Model.db.deleteRow()`, `delete()`, or `deleteWhere()`

**Queries:**
- Use `where:` parameter with lambda functions
- Combine conditions with `&` (AND) and `|` (OR)
- Add `orderBy:` for sorting
- Add `limit:` and `offset:` for pagination

**Relations:**
- One-to-One: Single foreign key reference
- One-to-Many: Foreign key on the "many" side
- Many-to-Many: Junction table with two foreign keys
- Use `include:` to load related objects

**Migrations:**
- Run `serverpod create-migration` after model changes
- Review generated SQL before applying
- Apply with `--apply-migrations` flag
- Never edit committed migrations

**Transactions:**
- Use `session.db.transaction()` for atomic operations
- Keep transactions short
- All operations succeed or all are rolled back

