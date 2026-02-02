---
type: "EXAMPLE"
title: "Zero-Dependency Database"
---

Create a fully functional database with zero npm packages:

```javascript
import { Database } from 'bun:sqlite';

// Create or open a database file
const db = new Database('app.db');

// Create tables
db.run(`
  CREATE TABLE IF NOT EXISTS users (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    email TEXT UNIQUE,
    created_at TEXT DEFAULT CURRENT_TIMESTAMP
  )
`);

// Insert data (prepared statement for safety)
const insert = db.prepare(
  'INSERT INTO users (name, email) VALUES ($name, $email)'
);

insert.run({ $name: 'Alice', $email: 'alice@example.com' });
insert.run({ $name: 'Bob', $email: 'bob@example.com' });

// Query data
const users = db.query('SELECT * FROM users').all();
console.log(users);
// [
//   { id: 1, name: 'Alice', email: 'alice@example.com', created_at: '...' },
//   { id: 2, name: 'Bob', email: 'bob@example.com', created_at: '...' }
// ]
```
