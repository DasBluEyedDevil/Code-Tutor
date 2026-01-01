---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// Understanding Databases - Concept Demo

// PROBLEM: In-memory storage (data is temporary)
let users = [
  { id: 1, name: 'Alice', email: 'alice@example.com' },
  { id: 2, name: 'Bob', email: 'bob@example.com' }
];

console.log('Users in memory:', users.length); // 2

// When server restarts... POOF! Data is gone!
// users = [];  // Lost forever!

// DATABASE CONCEPTS

// 1. PERSISTENCE - Data survives server restarts
console.log('Database benefit: Data persists across restarts');

// 2. STRUCTURED DATA - Tables with columns
/*
  Users Table:
  ┌────┬────────┬──────────────────────┐
  │ id │ name   │ email                │
  ├────┼────────┼──────────────────────┤
  │ 1  │ Alice  │ alice@example.com    │
  │ 2  │ Bob    │ bob@example.com      │
  └────┴────────┴──────────────────────┘
*/

// 3. RELATIONSHIPS - Tables can connect to each other
/*
  Posts Table:
  ┌────┬────────────────┬───────────┐
  │ id │ title          │ userId    │
  ├────┼────────────────┼───────────┤
  │ 1  │ First Post     │ 1 (Alice) │
  │ 2  │ Hello World    │ 2 (Bob)   │
  └────┴────────────────┴───────────┘
*/

console.log('Database benefit: Relationships between data');

// 4. QUERIES - Ask questions about your data
let sqlExamples = [
  'SELECT * FROM users WHERE name = "Alice"',
  'SELECT * FROM posts WHERE userId = 1',
  'SELECT users.name, posts.title FROM users JOIN posts ON users.id = posts.userId'
];

console.log('SQL queries let you ask complex questions:');
sqlExamples.forEach(sql => console.log('  -', sql));

// 5. TRANSACTIONS - All-or-nothing operations
console.log('Database benefit: Transactions ensure data integrity');
/*
  Example: Transferring money
  - Subtract $100 from Account A
  - Add $100 to Account B
  
  Either BOTH happen, or NEITHER happens!
  No lost money!
*/

// TYPES OF DATABASES

let databaseTypes = {
  'Relational (SQL)': {
    examples: ['PostgreSQL', 'MySQL', 'SQLite'],
    structure: 'Tables with rows and columns',
    uses: 'Most applications, e-commerce, banking'
  },
  'NoSQL (Document)': {
    examples: ['MongoDB', 'Firestore'],
    structure: 'JSON-like documents',
    uses: 'Flexible schemas, real-time apps'
  },
  'Key-Value': {
    examples: ['Redis', 'DynamoDB'],
    structure: 'Simple key-value pairs',
    uses: 'Caching, sessions, real-time'
  }
};

console.log('\nTypes of databases:');
for (let [type, info] of Object.entries(databaseTypes)) {
  console.log(`${type}: ${info.examples.join(', ')}`);
}

// WHY USE A DATABASE?
let benefits = [
  'Persistence: Data survives restarts',
  'Scalability: Handle millions of records',
  'Concurrency: Multiple users at once',
  'Query power: Complex data searches',
  'Data integrity: Constraints and validation',
  'Security: Access control and encryption',
  'Backup: Restore data if something breaks'
];

console.log('\nDatabase benefits:');
benefits.forEach(b => console.log(`  ✓ ${b}`));
```
