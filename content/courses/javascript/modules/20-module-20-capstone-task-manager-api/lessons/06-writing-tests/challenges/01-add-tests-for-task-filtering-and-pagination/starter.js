// tests/tasks-filtering.test.ts
import { describe, it, expect, beforeEach } from 'bun:test';
import { setupTestDatabase, cleanupDatabase } from './setup';
import app from '../src/index';
import { PrismaClient } from '@prisma/client';

let prisma: PrismaClient;
let userId: string;
let authToken: string;

describe('Task Filtering and Pagination', () => {
  beforeEach(async () => {
    prisma = await setupTestDatabase();
    
    // Create test user
    const user = await prisma.user.create({
      data: {
        email: 'test@example.com',
        passwordHash: 'hashed',
      },
    });
    userId = user.id;
    
    // Create test categories
    const workCategory = await prisma.category.create({
      data: {
        name: 'Work',
        userId,
      },
    });
    
    const personalCategory = await prisma.category.create({
      data: {
        name: 'Personal',
        userId,
      },
    });
    
    // Create test tasks
    await prisma.task.create({
      data: {
        title: 'Complete report',
        status: 'pending',
        userId,
        categoryId: workCategory.id,
      },
    });
    
    await prisma.task.create({
      data: {
        title: 'Review code',
        status: 'in_progress',
        userId,
        categoryId: workCategory.id,
      },
    });
    
    // Your test implementation here
  });
  
  // Add your tests here
});
