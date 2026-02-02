---
type: "WARNING"
title: "Security Reminders"
---

1. **Always use prepared statements for user input**
```javascript
// WRONG - SQL injection vulnerability!
db.run(`DELETE FROM users WHERE id = ${userId}`);

// RIGHT - Safe parameterized query
db.prepare('DELETE FROM users WHERE id = ?').run(userId);
```

2. **Close connections when done**
```javascript
db.close();
```

3. **Use WAL mode for concurrent access**
```javascript
db.run('PRAGMA journal_mode = WAL');
```