import { Database } from 'bun:sqlite';

const db = new Database(':memory:');

db.run(`
  CREATE TABLE todos (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    title TEXT NOT NULL,
    completed INTEGER DEFAULT 0
  )
`);

const insertStmt = db.prepare('INSERT INTO todos (title) VALUES (?)');

function addTodo(title) {
  insertStmt.run(title);
}

function getTodos() {
  return db.query('SELECT * FROM todos').all();
}

addTodo('Learn Bun');
addTodo('Build an app');
console.log(getTodos());