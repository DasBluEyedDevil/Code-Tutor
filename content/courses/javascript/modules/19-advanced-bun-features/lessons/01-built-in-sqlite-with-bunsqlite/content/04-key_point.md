---
type: "KEY_POINT"
title: "In-Memory Databases"
---

For testing or ephemeral data, use an in-memory database:

```javascript
// ':memory:' creates a database that exists only in RAM
const testDb = new Database(':memory:');

// Perfect for tests - fast and isolated
testDb.run('CREATE TABLE temp (id INTEGER)');
testDb.run('INSERT INTO temp VALUES (1)');

// Database is automatically deleted when the process ends
```