// Complete full-stack simulation

// Database layer
let database = {
  users: [
    { id: 1, name: 'Alice', email: 'alice@example.com' },
    { id: 2, name: 'Bob', email: 'bob@example.com' },
    { id: 3, name: 'Charlie', email: 'charlie@example.com' }
  ],
  
  getUsers() {
    console.log('[Database] Executing: SELECT * FROM users');
    return this.users;
  },
  
  getUserById(id) {
    console.log(`[Database] Executing: SELECT * FROM users WHERE id = ${id}`);
    return this.users.find(u => u.id === id);
  },
  
  createUser(name, email) {
    console.log(`[Database] Executing: INSERT INTO users (name, email) VALUES ('${name}', '${email}')`);
    let newUser = { id: this.users.length + 1, name, email };
    this.users.push(newUser);
    return newUser;
  }
};

// Backend layer
let backend = {
  handleGetUsers() {
    console.log('[Backend] GET /api/users');
    let users = database.getUsers();
    console.log(`[Backend] Response: 200 OK (${users.length} users)`);
    return { status: 200, data: users };
  },
  
  handleGetUser(id) {
    console.log(`[Backend] GET /api/users/${id}`);
    let user = database.getUserById(id);
    if (user) {
      console.log('[Backend] Response: 200 OK');
      return { status: 200, data: user };
    } else {
      console.log('[Backend] Response: 404 Not Found');
      return { status: 404, error: 'User not found' };
    }
  },
  
  handleCreateUser(name, email) {
    console.log(`[Backend] POST /api/users`);
    let newUser = database.createUser(name, email);
    console.log('[Backend] Response: 201 Created');
    return { status: 201, data: newUser };
  }
};

// Frontend layer
let frontend = {
  state: { users: [], currentUser: null },
  
  async fetchUsers() {
    console.log('[Frontend] User clicked "View Users" button');
    console.log('[Frontend] fetch("http://localhost:4000/api/users")');
    
    let response = backend.handleGetUsers();
    
    if (response.status === 200) {
      this.state.users = response.data;
      console.log('[Frontend] State updated, re-rendering UI');
      console.log('[Frontend] Displaying:', this.state.users.length, 'users');
    }
    
    return this.state.users;
  },
  
  async fetchUser(id) {
    console.log(`[Frontend] User clicked on user ${id}`);
    console.log(`[Frontend] fetch("http://localhost:4000/api/users/${id}")`);
    
    let response = backend.handleGetUser(id);
    
    if (response.status === 200) {
      this.state.currentUser = response.data;
      console.log('[Frontend] Displaying user profile:', response.data.name);
    } else {
      console.log('[Frontend] Showing error: User not found');
    }
  },
  
  async createUser(name, email) {
    console.log('[Frontend] User submitted "Create User" form');
    console.log(`[Frontend] fetch("http://localhost:4000/api/users", { method: "POST", body: { name, email } })`);
    
    let response = backend.handleCreateUser(name, email);
    
    if (response.status === 201) {
      console.log('[Frontend] Success! Refreshing user list...');
      this.fetchUsers();
    }
  }
};

// Simulate full-stack application
console.log('=== Full-Stack Application Flow ===\n');

console.log('--- Scenario 1: List all users ---');
frontend.fetchUsers();

console.log('\n--- Scenario 2: View specific user ---');
frontend.fetchUser(1);

console.log('\n--- Scenario 3: Create new user ---');
frontend.createUser('Diana', 'diana@example.com');

console.log('\n--- Final database state ---');
console.log('Total users:', database.users.length);
console.log('Users:', database.users.map(u => u.name).join(', '));