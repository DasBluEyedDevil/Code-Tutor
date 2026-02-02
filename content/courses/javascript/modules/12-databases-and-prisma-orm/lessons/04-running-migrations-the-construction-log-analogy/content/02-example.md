---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// Understanding Prisma Migrations
// Migrations are SQL files that modify your database structure

// SCENARIO: Building a blog app

// Step 1: Initial schema (schema.prisma)
/*
model User {
  id    Int    @id @default(autoincrement())
  email String @unique
  name  String
}
*/

// Run migration command:
// npx prisma migrate dev --name init

// Prisma generates:
// migrations/20250114_init/migration.sql
/*
CREATE TABLE "User" (
  "id" INTEGER PRIMARY KEY AUTOINCREMENT,
  "email" TEXT UNIQUE NOT NULL,
  "name" TEXT NOT NULL
);
*/

console.log('Migration 1: Created User table');

// Step 2: Add posts table
// Update schema.prisma:
/*
model User {
  id    Int    @id @default(autoincrement())
  email String @unique
  name  String
  posts Post[]  // NEW: Added relationship
}

model Post {  // NEW: Added entire model
  id        Int      @id @default(autoincrement())
  title     String
  content   String?
  published Boolean  @default(false)
  authorId  Int
  author    User     @relation(fields: [authorId], references: [id])
}
*/

// Run migration:
// npx prisma migrate dev --name add_posts

// Prisma generates:
// migrations/20250114_add_posts/migration.sql
/*
CREATE TABLE "Post" (
  "id" INTEGER PRIMARY KEY AUTOINCREMENT,
  "title" TEXT NOT NULL,
  "content" TEXT,
  "published" BOOLEAN NOT NULL DEFAULT 0,
  "authorId" INTEGER NOT NULL,
  FOREIGN KEY ("authorId") REFERENCES "User"("id")
);
*/

console.log('Migration 2: Created Post table with relationship to User');

// Step 3: Add createdAt timestamps
// Update schema.prisma:
/*
model User {
  id        Int      @id @default(autoincrement())
  email     String   @unique
  name      String
  createdAt DateTime @default(now())  // NEW
  posts     Post[]
}

model Post {
  id        Int      @id @default(autoincrement())
  title     String
  content   String?
  published Boolean  @default(false)
  createdAt DateTime @default(now())  // NEW
  authorId  Int
  author    User     @relation(fields: [authorId], references: [id])
}
*/

// Run migration:
// npx prisma migrate dev --name add_timestamps

// Prisma generates:
// migrations/20250114_add_timestamps/migration.sql
/*
ALTER TABLE "User" ADD COLUMN "createdAt" DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP;
ALTER TABLE "Post" ADD COLUMN "createdAt" DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP;
*/

console.log('Migration 3: Added createdAt to User and Post tables');

// MIGRATION COMMANDS EXPLAINED

let migrationCommands = {
  'Development': {
    'npx prisma migrate dev': 'Create and apply migrations in development',
    'npx prisma migrate dev --name <name>': 'Create migration with descriptive name',
    'npx prisma migrate reset': 'Reset database and replay all migrations',
    'npx prisma db push': 'Quick prototype without creating migration file'
  },
  
  'Production': {
    'npx prisma migrate deploy': 'Apply pending migrations to production',
    'npx prisma migrate resolve': 'Mark migration as applied/rolled back',
    'npx prisma migrate status': 'Check which migrations are pending'
  },
  
  'Inspection': {
    'npx prisma migrate diff': 'Compare database to schema',
    'npx prisma db pull': 'Introspect existing database to generate schema',
    'npx prisma studio': 'Visual database browser'
  }
};

console.log('\n=== Prisma Migration Commands ===\n');

for (let [category, commands] of Object.entries(migrationCommands)) {
  console.log(category + ' Commands:');
  for (let [cmd, desc] of Object.entries(commands)) {
    console.log(`  ${cmd}`);
    console.log(`    → ${desc}`);
  }
  console.log('');
}

// MIGRATION WORKFLOW SIMULATION

let migrationHistory = [
  {
    name: '20250114_init',
    description: 'Initial database setup',
    changes: ['Created User table']
  },
  {
    name: '20250114_add_posts',
    description: 'Add blog posts',
    changes: ['Created Post table', 'Added User.posts relation']
  },
  {
    name: '20250114_add_timestamps',
    description: 'Add audit timestamps',
    changes: ['Added User.createdAt', 'Added Post.createdAt']
  }
];

console.log('Migration History:');
migrationHistory.forEach((migration, index) => {
  console.log(`\n${index + 1}. ${migration.name}`);
  console.log(`   Description: ${migration.description}`);
  console.log('   Changes:');
  migration.changes.forEach(change => {
    console.log(`     - ${change}`);
  });
});

// BENEFITS OF MIGRATIONS

let benefits = [
  'Version control: Track database changes like code',
  'Reproducible: Same migrations = identical databases',
  'Team sync: Everyone applies same changes',
  'Rollback: Can undo changes if needed',
  'Production safety: Test migrations before deploying',
  'Documentation: Clear history of schema evolution',
  'CI/CD: Automated database updates in pipelines'
];

console.log('\nBenefits of Migrations:');
benefits.forEach(benefit => console.log(`  ✓ ${benefit}`));
```
