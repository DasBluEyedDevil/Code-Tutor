---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// Prisma ORM Demonstration (Conceptual)
// In real projects, Prisma generates TypeScript types automatically!

// TRADITIONAL WAY: Raw SQL (error-prone)
let rawSQL = `
  SELECT users.name, posts.title 
  FROM users 
  JOIN posts ON users.id = posts.userId 
  WHERE users.email = 'alice@example.com'
`;

console.log('Raw SQL (requires SQL knowledge):', rawSQL);
console.log('Problems: SQL injection, typos, no type safety\n');

// PRISMA WAY: Type-safe JavaScript/TypeScript
// (Simulated - real Prisma connects to actual database)

class PrismaClient {
  constructor() {
    this.user = {
      create: async (data) => {
        console.log('[Prisma] Creating user:', data.data);
        return { id: 1, ...data.data, createdAt: new Date() };
      },
      findUnique: async (query) => {
        console.log('[Prisma] Finding user where:', query.where);
        return {
          id: 1,
          name: 'Alice',
          email: 'alice@example.com',
          posts: [
            { id: 1, title: 'First Post', content: 'Hello!' }
          ]
        };
      },
      findMany: async (query) => {
        console.log('[Prisma] Finding many users');
        return [
          { id: 1, name: 'Alice', email: 'alice@example.com' },
          { id: 2, name: 'Bob', email: 'bob@example.com' }
        ];
      },
      update: async (query) => {
        console.log('[Prisma] Updating user:', query.where, 'with:', query.data);
        return { id: 1, ...query.data };
      },
      delete: async (query) => {
        console.log('[Prisma] Deleting user:', query.where);
        return { id: 1, name: 'Alice' };
      }
    };
    
    this.post = {
      create: async (data) => {
        console.log('[Prisma] Creating post:', data.data);
        return { id: 1, ...data.data };
      }
    };
  }
}

let prisma = new PrismaClient();

// PRISMA EXAMPLES (async/await pattern)

// 1. CREATE a user
async function createUser() {
  let user = await prisma.user.create({
    data: {
      name: 'Alice',
      email: 'alice@example.com'
    }
  });
  console.log('Created user:', user);
  return user;
}

// 2. FIND a user by email
async function findUser() {
  let user = await prisma.user.findUnique({
    where: {
      email: 'alice@example.com'
    },
    include: {
      posts: true  // Include related posts!
    }
  });
  console.log('Found user with posts:', user);
  return user;
}

// 3. UPDATE a user
async function updateUser() {
  let user = await prisma.user.update({
    where: { id: 1 },
    data: {
      name: 'Alice Smith'
    }
  });
  console.log('Updated user:', user);
  return user;
}

// 4. DELETE a user
async function deleteUser() {
  let user = await prisma.user.delete({
    where: { id: 1 }
  });
  console.log('Deleted user:', user);
  return user;
}

// 5. LIST all users
async function listUsers() {
  let users = await prisma.user.findMany();
  console.log('All users:', users);
  return users;
}

// Run examples
console.log('=== Prisma ORM Examples ===\n');

createUser();
setTimeout(() => findUser(), 100);
setTimeout(() => updateUser(), 200);
setTimeout(() => listUsers(), 300);

// PRISMA BENEFITS
console.log('\n=== Prisma Benefits ===');
let benefits = [
  '✓ Type-safe database queries (TypeScript)',
  '✓ Auto-generated types from schema',
  '✓ Database agnostic (PostgreSQL, MySQL, SQLite, etc.)',
  '✓ Intuitive API (JavaScript objects, not SQL strings)',
  '✓ Migrations built-in',
  '✓ Prisma Studio (database GUI)',
  '✓ Query builder prevents SQL injection',
  '✓ Excellent autocomplete in IDE'
];

benefits.forEach(b => console.log(b));
```
