// src/lib/queries.ts
import { prisma } from './db';

// Find all tasks for a user with optional status filter
export async function findTasksByUser(
  userId: string,
  options?: { status?: string; categoryId?: string }
) {
  // Your code here
}

// Find a single task by ID (only if owned by user)
export async function findTaskById(taskId: string, userId: string) {
  // Your code here
}

// Count tasks grouped by status for a user
export async function countTasksByStatus(userId: string) {
  // Your code here
}