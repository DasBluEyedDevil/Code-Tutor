---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// Full-Stack Architecture Overview (Modern Stack)

// FRONTEND (React) - What users see
let frontEnd = {
  technology: 'React 19',
  responsibilities: [
    'User interface (buttons, forms, displays)',
    'User interactions (clicks, typing)',
    'API calls to backend',
    'Display data from backend'
  ],
  example: 'Login form, dashboard, user profile page'
};

console.log('Frontend:', frontEnd);

// BACKEND (Node.js + Hono) - Business logic
let backEnd = {
  technology: 'Node.js 24 + Hono 4',
  responsibilities: [
    'API endpoints (routes)',
    'Authentication & authorization',
    'Business logic (validation, calculations)',
    'Database operations',
    'Security'
  ],
  example: 'POST /api/login, GET /api/users, PUT /api/profile'
};

console.log('\nBackend:', backEnd);

// DATABASE (PostgreSQL + Prisma) - Data storage
let database = {
  technology: 'PostgreSQL + Prisma ORM',
  responsibilities: [
    'Store data persistently',
    'Relationships between data',
    'Query optimization',
    'Data integrity'
  ],
  example: 'Users table, Posts table, Comments table'
};

console.log('\nDatabase:', database);

// HOW THEY COMMUNICATE (Example flow)
console.log('\n=== Full-Stack Data Flow ===\n');

function simulateFullStackFlow() {
  console.log('1. USER ACTION: User clicks "Login" button in React');
  console.log('   Frontend: <button onClick={handleLogin}>Login</button>');
  
  console.log('\n2. FRONTEND: React sends HTTP request to backend');
  console.log('   fetch("/api/login", { method: "POST", body: { email, password } })');
  
  console.log('\n3. BACKEND: Hono receives request at route');
  console.log('   app.post("/api/login", async (c) => { ... })');
  
  console.log('\n4. BACKEND: Queries database via Prisma');
  console.log('   const user = await prisma.user.findUnique({ where: { email } });');
  
  console.log('\n5. DATABASE: Returns user data to backend');
  console.log('   { id: 1, email: "user@example.com", name: "Alice" }');
  
  console.log('\n6. BACKEND: Sends JSON response to frontend');
  console.log('   return c.json({ success: true, user: { id: 1, name: "Alice" } });');
  
  console.log('\n7. FRONTEND: React receives data and updates UI');
  console.log('   setUser(data.user); // State update triggers re-render');
  
  console.log('\n8. USER SEES: Dashboard with their name displayed');
  console.log('   <h1>Welcome, {user.name}!</h1>');
}

simulateFullStackFlow();

// TECH STACK OPTIONS
let stacks = {
  'Modern Stack': 'PostgreSQL + Hono + React + Node.js (recommended)',
  PERN: 'PostgreSQL + Express + React + Node.js (legacy)',
  MERN: 'MongoDB + Express + React + Node.js (legacy)',
  'T3 Stack': 'TypeScript + tRPC + Tailwind + Prisma + Next.js'
};

console.log('\nPopular Full-Stack Combinations:');
for (let [name, stack] of Object.entries(stacks)) {
  console.log(`${name}: ${stack}`);
}

console.log('\nWe\'ll use: Hono + Prisma + React + TypeScript (modern, edge-ready)');
```
