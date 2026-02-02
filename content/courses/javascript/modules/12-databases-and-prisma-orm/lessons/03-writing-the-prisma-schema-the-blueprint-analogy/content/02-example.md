---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// Understanding Prisma Schema (schema.prisma file)
// This is NOT JavaScript - it's Prisma's special language!

// CONCEPTUAL DEMO - showing schema structure in JavaScript comments

/*
Prisma Schema Structure:

// 1. DATABASE CONNECTION
datasource db {
  provider = "postgresql"  // or "mysql", "sqlite", "mongodb"
  url      = env("DATABASE_URL")  // Connection string from .env
}

// 2. PRISMA CLIENT GENERATOR
generator client {
  provider = "prisma-client-js"
}

// 3. DATA MODELS (Tables)

model User {
  id        Int      @id @default(autoincrement())
  email     String   @unique
  name      String
  password  String
  role      String   @default("user")
  createdAt DateTime @default(now())
  updatedAt DateTime @updatedAt
  
  posts     Post[]   // Relationship: User has many Posts
  profile   Profile? // Relationship: User has one optional Profile
}

model Post {
  id        Int      @id @default(autoincrement())
  title     String
  content   String?
  published Boolean  @default(false)
  viewCount Int      @default(0)
  createdAt DateTime @default(now())
  updatedAt DateTime @updatedAt
  
  authorId  Int
  author    User     @relation(fields: [authorId], references: [id])
  
  categories Category[]
}

model Profile {
  id       Int    @id @default(autoincrement())
  bio      String?
  avatar   String?
  
  userId   Int    @unique
  user     User   @relation(fields: [userId], references: [id])
}

model Category {
  id    Int    @id @default(autoincrement())
  name  String @unique
  
  posts Post[]
}
*/

// SCHEMA CONCEPTS EXPLAINED

let schemaExplanation = {
  'Field Types': {
    'String': 'Text data ("hello", "user@example.com")',
    'Int': 'Whole numbers (1, 42, 1000)',
    'Float': 'Decimal numbers (3.14, 99.99)',
    'Boolean': 'true or false',
    'DateTime': 'Dates and times',
    'Json': 'JSON objects (flexible data)'
  },
  
  'Attributes': {
    '@id': 'Primary key (unique identifier)',
    '@unique': 'Value must be unique across all records',
    '@default(value)': 'Default value if not provided',
    '@updatedAt': 'Auto-updates to current time on changes',
    '@relation': 'Defines relationships between models'
  },
  
  'Type Modifiers': {
    'field String': 'Required field (cannot be null)',
    'field String?': 'Optional field (can be null)',
    'field String[]': 'Array of strings'
  },
  
  'Default Functions': {
    'autoincrement()': 'Auto-increment integers (1, 2, 3...)',
    'now()': 'Current timestamp',
    'uuid()': 'Generate random UUID',
    'cuid()': 'Generate random CUID (shorter than UUID)'
  }
};

console.log('=== Prisma Schema Concepts ===\n');

for (let [category, details] of Object.entries(schemaExplanation)) {
  console.log(category + ':');
  for (let [key, desc] of Object.entries(details)) {
    console.log(`  ${key}: ${desc}`);
  }
  console.log('');
}

// EXAMPLE: What this schema creates

let exampleData = {
  users: [
    {
      id: 1,
      email: 'alice@example.com',
      name: 'Alice',
      password: 'hashed_password',
      role: 'user',
      createdAt: new Date('2025-01-01'),
      updatedAt: new Date('2025-01-15')
    }
  ],
  posts: [
    {
      id: 1,
      title: 'My First Post',
      content: 'Hello world!',
      published: true,
      viewCount: 42,
      authorId: 1,  // References user with id 1
      createdAt: new Date('2025-01-10')
    }
  ],
  profiles: [
    {
      id: 1,
      bio: 'Software developer',
      avatar: 'https://example.com/avatar.jpg',
      userId: 1  // References user with id 1
    }
  ]
};

console.log('Example database structure:');
console.log(JSON.stringify(exampleData, null, 2));

// WORKFLOW
console.log('\n=== Prisma Schema Workflow ===');

let workflow = [
  '1. Write schema.prisma file (define your models)',
  '2. Run: npx prisma migrate dev --name init',
  '   → Creates database tables',
  '   → Creates migration files',
  '3. Run: npx prisma generate',
  '   → Generates TypeScript types',
  '   → Updates Prisma Client',
  '4. Use in your code with full type safety!'
];

workflow.forEach(step => console.log(step));
```
