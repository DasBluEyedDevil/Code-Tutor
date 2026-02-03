---
type: "EXAMPLE"
title: "Test Database Setup - In-Memory SQLite"
---

Use an in-memory database for fast, isolated tests:

```typescript
// tests/setup.ts - Database setup for tests
import { PrismaClient } from '@prisma/client';

// Create a test database instance
const testDatabaseUrl = 'file::memory:';

export async function setupTestDatabase() {
  const prisma = new PrismaClient({
    datasources: {
      db: {
        url: testDatabaseUrl,
      },
    },
  });

  // Run migrations on the in-memory database
  await prisma.$executeRawUnsafe(
    `PRAGMA foreign_keys = ON`
  );

  return prisma;
}

export async function cleanupDatabase(prisma: PrismaClient) {
  // Delete all data in reverse order of dependencies
  await prisma.task.deleteMany();
  await prisma.category.deleteMany();
  await prisma.user.deleteMany();
}

// tests/database.integration.test.ts
import { describe, it, expect, beforeEach, afterEach } from 'bun:test';
import { setupTestDatabase, cleanupDatabase } from './setup';
import { PrismaClient } from '@prisma/client';

let prisma: PrismaClient;

describe('Database Operations', () => {
  beforeEach(async () => {
    prisma = await setupTestDatabase();
  });

  afterEach(async () => {
    await cleanupDatabase(prisma);
    await prisma.$disconnect();
  });

  describe('User Creation', () => {
    it('should create a user', async () => {
      const user = await prisma.user.create({
        data: {
          email: 'test@example.com',
          passwordHash: 'hashed_password',
          name: 'Test User',
        },
      });

      expect(user.email).toBe('test@example.com');
      expect(user.id).toBeDefined();
    });

    it('should enforce unique email constraint', async () => {
      await prisma.user.create({
        data: {
          email: 'unique@example.com',
          passwordHash: 'hash1',
        },
      });

      // This should fail
      expect(async () => {
        await prisma.user.create({
          data: {
            email: 'unique@example.com',
            passwordHash: 'hash2',
          },
        });
      }).toThrow();
    });
  });

  describe('Task Operations with Cascade Delete', () => {
    it('should delete tasks when user is deleted', async () => {
      const user = await prisma.user.create({
        data: {
          email: 'cascade@example.com',
          passwordHash: 'hash',
        },
      });

      const task = await prisma.task.create({
        data: {
          title: 'Test Task',
          userId: user.id,
        },
      });

      // Delete user
      await prisma.user.delete({
        where: { id: user.id },
      });

      // Task should be deleted too
      const taskExists = await prisma.task.findUnique({
        where: { id: task.id },
      });

      expect(taskExists).toBe(null);
    });
  });
});
```
