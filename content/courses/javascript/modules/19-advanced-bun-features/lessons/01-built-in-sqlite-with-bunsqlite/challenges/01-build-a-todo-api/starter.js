import { Database } from 'bun:sqlite';

const db = new Database(':memory:');

// Create todos table

// Function to add a todo
function addTodo(title) {
  // Your code here
}

// Function to get all todos
function getTodos() {
  // Your code here
}

// Test it
addTodo('Learn Bun');
addTodo('Build an app');
console.log(getTodos());