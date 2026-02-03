---
type: "EXAMPLE"
title: "Test Database Setup"
---

Integration tests need a real database, but you want tests to be fast and isolated. In-memory SQLite is perfect for testing - it behaves like a real database but runs entirely in memory with no cleanup needed.

```javascript
import { describe, it, expect, beforeAll, afterAll, beforeEach } from 'bun:test';
import { Database } from 'bun:sqlite';
import { Hono } from 'hono';

// Test database setup
function createTestDatabase() {
  // In-memory SQLite - destroyed when connection closes
  const db = new Database(':memory:');
  
  // Create schema
  db.run(`
    CREATE TABLE users (
      id INTEGER PRIMARY KEY AUTOINCREMENT,
      name TEXT NOT NULL,
      email TEXT UNIQUE NOT NULL,
      created_at TEXT DEFAULT CURRENT_TIMESTAMP
    )
  `);
  
  db.run(`
    CREATE TABLE posts (
      id INTEGER PRIMARY KEY AUTOINCREMENT,
      user_id INTEGER NOT NULL,
      title TEXT NOT NULL,
      content TEXT,
      created_at TEXT DEFAULT CURRENT_TIMESTAMP,
      FOREIGN KEY (user_id) REFERENCES users(id)
    )
  `);
  
  return db;
}

// Seed test data
function seedTestData(db) {
  const insertUser = db.prepare(
    'INSERT INTO users (name, email) VALUES (?, ?)'
  );
  const insertPost = db.prepare(
    'INSERT INTO posts (user_id, title, content) VALUES (?, ?, ?)'
  );
  
  // Create test users
  insertUser.run('Alice', 'alice@test.com');
  insertUser.run('Bob', 'bob@test.com');
  
  // Create test posts
  insertPost.run(1, 'First Post', 'Content of first post');
  insertPost.run(1, 'Second Post', 'Content of second post');
  insertPost.run(2, 'Bob Post', 'Bob writes something');
}

// Clear all data between tests
function clearTestData(db) {
  db.run('DELETE FROM posts');
  db.run('DELETE FROM users');
  // Reset autoincrement counters
  db.run("DELETE FROM sqlite_sequence WHERE name='users'");
  db.run("DELETE FROM sqlite_sequence WHERE name='posts'");
}

// Create app with injected database
function createApp(db) {
  const app = new Hono();

  app.get('/api/users', (c) => {
    const users = db.query('SELECT * FROM users').all();
    return c.json(users);
  });

  app.get('/api/users/:id/posts', (c) => {
    const userId = parseInt(c.req.param('id'));
    const posts = db.query(
      'SELECT * FROM posts WHERE user_id = ?'
    ).all(userId);
    return c.json(posts);
  });

  app.post('/api/users', async (c) => {
    const { name, email } = await c.req.json();
    try {
      const result = db.run(
        'INSERT INTO users (name, email) VALUES (?, ?)',
        [name, email]
      );
      const user = db.query(
        'SELECT * FROM users WHERE id = ?'
      ).get(result.lastInsertRowid);
      return c.json(user, 201);
    } catch (error) {
      if (error.message.includes('UNIQUE constraint')) {
        return c.json({ error: 'Email already exists' }, 409);
      }
      throw error;
    }
  });

  return app;
}

describe('Database Integration Tests', () => {
  let db;
  let app;

  beforeAll(() => {
    // Create database once for all tests
    db = createTestDatabase();
  });

  afterAll(() => {
    // Close database connection
    db.close();
  });

  beforeEach(() => {
    // Reset data before each test for isolation
    clearTestData(db);
    seedTestData(db);
    app = createApp(db);
  });

  describe('GET /api/users', () => {
    it('returns seeded users from database', async () => {
      const res = await app.request('/api/users');
      const users = await res.json();
      
      expect(users).toHaveLength(2);
      expect(users[0].name).toBe('Alice');
      expect(users[1].name).toBe('Bob');
    });
  });

  describe('GET /api/users/:id/posts', () => {
    it('returns posts for specific user', async () => {
      const res = await app.request('/api/users/1/posts');
      const posts = await res.json();
      
      expect(posts).toHaveLength(2);
      expect(posts[0].title).toBe('First Post');
    });

    it('returns empty array for user with no posts', async () => {
      // Create new user with no posts
      await app.request('/api/users', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ name: 'Charlie', email: 'charlie@test.com' })
      });

      const res = await app.request('/api/users/3/posts');
      const posts = await res.json();
      
      expect(posts).toEqual([]);
    });
  });

  describe('POST /api/users', () => {
    it('persists user to database', async () => {
      const res = await app.request('/api/users', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ name: 'Charlie', email: 'charlie@test.com' })
      });
      
      expect(res.status).toBe(201);
      
      // Verify in database directly
      const user = db.query(
        'SELECT * FROM users WHERE email = ?'
      ).get('charlie@test.com');
      expect(user.name).toBe('Charlie');
    });

    it('rejects duplicate email', async () => {
      const res = await app.request('/api/users', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ name: 'Duplicate', email: 'alice@test.com' })
      });
      
      expect(res.status).toBe(409);
      const error = await res.json();
      expect(error.error).toBe('Email already exists');
    });
  });
});
```
