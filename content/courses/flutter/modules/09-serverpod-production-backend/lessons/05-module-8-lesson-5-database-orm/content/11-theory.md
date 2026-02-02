---
type: "THEORY"
title: "CRUD Operations Overview"
---

CRUD stands for Create, Read, Update, Delete - the four fundamental database operations. Serverpod provides type-safe methods for each operation through the generated `db` object on your model classes.

**The Pattern:**

Every model with a `table` definition gets a `.db` property with these methods:

```dart
// CREATE - Insert new records
User.db.insert(session, [user1, user2]);  // Multiple rows
User.db.insertRow(session, user);          // Single row

// READ - Query existing records
User.db.find(session, where: ...);         // Multiple rows
User.db.findById(session, id);             // Single by ID
User.db.findFirstRow(session, where: ...); // First match
User.db.count(session, where: ...);        // Count rows

// UPDATE - Modify existing records
User.db.update(session, [user1, user2]);   // Multiple rows
User.db.updateRow(session, user);          // Single row

// DELETE - Remove records
User.db.delete(session, [user1, user2]);   // Multiple rows
User.db.deleteRow(session, user);          // Single row
User.db.deleteWhere(session, where: ...);  // By condition
```

**The Session Parameter:**

Every database operation requires a `Session` object as the first parameter. The session provides:
- Database connection
- Transaction context
- Authentication state
- Logging capabilities

