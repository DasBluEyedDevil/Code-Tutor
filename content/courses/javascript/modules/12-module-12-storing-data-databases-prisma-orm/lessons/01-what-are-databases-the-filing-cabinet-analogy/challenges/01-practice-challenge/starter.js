// Simulated database (array of objects)
let database = [];
let nextId = 1;

// Function to insert a user
function insertUser(name, email) {
  let user = {
    id: nextId++,
    name: name,
    email: email
  };
  database.push(user);
  return user;
}

// Function to find user by email
function findUserByEmail(email) {
  return database.find(user => user.email === email) || null;
}

// Function to update user email
function updateUserEmail(id, newEmail) {
  let user = database.find(u => u.id === id);
  if (user) {
    user.email = newEmail;
    return true;
  }
  return false;
}

// Test the functions
let user1 = insertUser('Alice', 'alice@example.com');
console.log('Inserted:', user1);

let found = findUserByEmail('alice@example.com');
console.log('Found:', found);

let updated = updateUserEmail(1, 'alice.new@example.com');
console.log('Updated:', updated);
console.log('Database:', database);