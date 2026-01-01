// Complete database simulation
let database = [];
let nextId = 1;

// Insert user
function insertUser(name, email) {
  let user = {
    id: nextId++,
    name: name,
    email: email,
    createdAt: new Date().toISOString()
  };
  database.push(user);
  console.log(`✓ Inserted user ID ${user.id}`);
  return user;
}

// Find user by email
function findUserByEmail(email) {
  let user = database.find(u => u.email === email);
  return user || null;
}

// Update user email
function updateUserEmail(id, newEmail) {
  let user = database.find(u => u.id === id);
  if (user) {
    let oldEmail = user.email;
    user.email = newEmail;
    console.log(`✓ Updated user ${id}: ${oldEmail} → ${newEmail}`);
    return true;
  }
  console.log(`✗ User ${id} not found`);
  return false;
}

// Delete user
function deleteUser(id) {
  let index = database.findIndex(u => u.id === id);
  if (index !== -1) {
    let deleted = database.splice(index, 1)[0];
    console.log(`✓ Deleted user ${id}`);
    return deleted;
  }
  return null;
}

// List all users
function getAllUsers() {
  return database;
}

// Test the database
console.log('=== Database Simulation ===\n');

let alice = insertUser('Alice', 'alice@example.com');
let bob = insertUser('Bob', 'bob@example.com');
let charlie = insertUser('Charlie', 'charlie@example.com');

console.log('\nAll users:', getAllUsers().length);

let found = findUserByEmail('bob@example.com');
console.log('\nFound Bob:', found ? found.name : 'Not found');

updateUserEmail(1, 'alice.new@example.com');

deleteUser(2);

console.log('\nFinal database:', database);