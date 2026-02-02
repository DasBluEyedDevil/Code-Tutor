// Database layer
let database = {
  users: [
    { id: 1, name: 'Alice', email: 'alice@example.com' },
    { id: 2, name: 'Bob', email: 'bob@example.com' }
  ],
  
  getUsers() {
    console.log('[Database] Querying users table');
    return this.users;
  }
};

// Backend layer
let backend = {
  handleGetUsers() {
    console.log('[Backend] Received GET /api/users');
    let users = database.getUsers();
    console.log('[Backend] Sending response:', users.length, 'users');
    return users;
  }
};

// Frontend layer  
let frontend = {
  fetchUsers() {
    console.log('[Frontend] Fetching users from API');
    let users = backend.handleGetUsers();
    console.log('[Frontend] Received users:', users);
    return users;
  }
};

// Test the full stack
console.log('=== Full-Stack Simulation ===\n');
frontend.fetchUsers();