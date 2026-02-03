// NOTE: This challenge uses Bun's built-in SQLite (bun:sqlite).
// The simulation below lets you practice the API patterns in any runtime.
// When running with Bun, replace the simulation with: import { Database } from 'bun:sqlite';

// --- Simulation for non-Bun runtimes ---
class Database {
  constructor() {
    this._tables = {};
    this._autoId = {};
  }
  run(sql) {
    const match = sql.match(/CREATE TABLE (\w+)/i);
    if (match) {
      this._tables[match[1]] = [];
      this._autoId[match[1]] = 1;
    }
  }
  prepare(sql) {
    const self = this;
    const insertMatch = sql.match(/INSERT INTO (\w+) \((\w+)\) VALUES \(\?\)/i);
    if (insertMatch) {
      const [, table, col] = insertMatch;
      return { run(val) { self._tables[table].push({ id: self._autoId[table]++, [col]: val, completed: 0 }); } };
    }
    return { all() { return []; } };
  }
  query(sql) {
    const selectMatch = sql.match(/SELECT \* FROM (\w+)/i);
    if (selectMatch) {
      return { all: () => [...(this._tables[selectMatch[1]] || [])] };
    }
    return { all: () => [] };
  }
}
// --- End simulation ---

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