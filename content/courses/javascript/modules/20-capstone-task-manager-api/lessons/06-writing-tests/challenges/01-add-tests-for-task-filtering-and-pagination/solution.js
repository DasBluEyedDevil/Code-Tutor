// tests/tasks-filtering.test.ts
import { describe, it, expect, beforeEach } from 'bun:test';
import { setupTestDatabase, cleanupDatabase } from './setup';
import app from '../src/index';
import { PrismaClient } from '@prisma/client';

let prisma: PrismaClient;
let userId: string;
let authToken: string;
let workCategoryId: string;
let personalCategoryId: string;

describe('Task Filtering and Pagination', () => {
  beforeEach(async () => {
    prisma = await setupTestDatabase();
    
    // Create test user
    const user = await prisma.user.create({
      data: {
        email: 'filter@example.com',
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
    workCategoryId = workCategory.id;
    
    const personalCategory = await prisma.category.create({
      data: {
        name: 'Personal',
        userId,
      },
    });
    personalCategoryId = personalCategory.id;
    
    // Create test tasks with various statuses and categories
    await prisma.task.create({
      data: {
        title: 'Complete report',
        status: 'pending',
        userId,
        categoryId: workCategoryId,
      },
    });
    
    await prisma.task.create({
      data: {
        title: 'Review code',
        status: 'in_progress',
        userId,
        categoryId: workCategoryId,
      },
    });
    
    await prisma.task.create({
      data: {
        title: 'Buy groceries',
        status: 'pending',
        userId,
        categoryId: personalCategoryId,
      },
    });
    
    await prisma.task.create({
      data: {
        title: 'Call mom',
        status: 'completed',
        userId,
        categoryId: personalCategoryId,
      },
    });
  });
  
  describe('Status Filtering', () => {
    it('should filter tasks by pending status', async () => {
      const response = await app.request(new Request(
        `http://localhost/api/tasks?status=pending`,
        {
          method: 'GET',
          headers: { 'Authorization': `Bearer ${authToken}` },
        }
      ));
      
      expect(response.status).toBe(200);
      const data = await response.json();
      expect(data.data.length).toBe(2);
      expect(data.data.every(t => t.status === 'pending')).toBe(true);
    });
    
    it('should filter tasks by completed status', async () => {
      const response = await app.request(new Request(
        `http://localhost/api/tasks?status=completed`,
        {
          method: 'GET',
          headers: { 'Authorization': `Bearer ${authToken}` },
        }
      ));
      
      const data = await response.json();
      expect(data.data.length).toBe(1);
      expect(data.data[0].status).toBe('completed');
    });
    
    it('should reject invalid status', async () => {
      const response = await app.request(new Request(
        `http://localhost/api/tasks?status=invalid`,
        {
          method: 'GET',
          headers: { 'Authorization': `Bearer ${authToken}` },
        }
      ));
      
      expect(response.status).toBe(400);
    });
  });
  
  describe('Category Filtering', () => {
    it('should filter tasks by category', async () => {
      const response = await app.request(new Request(
        `http://localhost/api/tasks?categoryId=${workCategoryId}`,
        {
          method: 'GET',
          headers: { 'Authorization': `Bearer ${authToken}` },
        }
      ));
      
      const data = await response.json();
      expect(data.data.length).toBe(2);
      expect(data.data.every(t => t.categoryId === workCategoryId)).toBe(true);
    });
  });
  
  describe('Pagination', () => {
    it('should paginate with limit', async () => {
      const response = await app.request(new Request(
        `http://localhost/api/tasks?limit=2`,
        {
          method: 'GET',
          headers: { 'Authorization': `Bearer ${authToken}` },
        }
      ));
      
      const data = await response.json();
      expect(data.data.length).toBe(2);
      expect(data.pagination.total).toBe(4);
    });
    
    it('should paginate with page number', async () => {
      const response = await app.request(new Request(
        `http://localhost/api/tasks?page=2&limit=2`,
        {
          method: 'GET',
          headers: { 'Authorization': `Bearer ${authToken}` },
        }
      ));
      
      const data = await response.json();
      expect(data.data.length).toBe(2);
      expect(data.pagination.page).toBe(2);
    });
    
    it('should reject invalid pagination parameters', async () => {
      const response = await app.request(new Request(
        `http://localhost/api/tasks?limit=abc`,
        {
          method: 'GET',
          headers: { 'Authorization': `Bearer ${authToken}` },
        }
      ));
      
      expect(response.status).toBe(400);
    });
  });
  
  describe('Combined Filters', () => {
    it('should filter by status and category together', async () => {
      const response = await app.request(new Request(
        `http://localhost/api/tasks?status=pending&categoryId=${workCategoryId}`,
        {
          method: 'GET',
          headers: { 'Authorization': `Bearer ${authToken}` },
        }
      ));
      
      const data = await response.json();
      expect(data.data.length).toBe(1);
      expect(data.data[0].title).toBe('Complete report');
    });
  });
});
