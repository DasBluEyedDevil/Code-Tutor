---
type: "THEORY"
title: "Query Methods"
---

**Prepared Statements (always use for user input!):**
```javascript
const stmt = db.prepare('SELECT * FROM users WHERE id = ?');
stmt.get(1);  // Single row or undefined
stmt.all();   // All rows as array
stmt.run();   // For INSERT/UPDATE/DELETE, returns changes info
```

**Direct Queries (for trusted SQL only):**
```javascript
db.query('SELECT COUNT(*) as count FROM users').get();  // { count: 42 }
db.run('DELETE FROM logs WHERE age > 30');  // Execute without results
```

**Transactions:**
```javascript
const transaction = db.transaction((users) => {
  for (const user of users) {
    insert.run(user);
  }
});

// Runs all inserts atomically
transaction([{ $name: 'Carol', $email: 'carol@example.com' }]);
```