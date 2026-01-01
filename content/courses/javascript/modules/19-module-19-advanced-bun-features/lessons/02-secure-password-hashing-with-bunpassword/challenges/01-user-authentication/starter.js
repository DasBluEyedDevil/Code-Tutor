const users = new Map();

async function signup(username, password) {
  // Hash password and store in users Map
}

async function login(username, password) {
  // Verify password and return true/false
}

// Test
await signup('alice', 'secret123');
console.log(await login('alice', 'secret123'));  // true
console.log(await login('alice', 'wrong'));      // false