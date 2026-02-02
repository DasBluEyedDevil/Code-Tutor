// src/lib/queries.ts
import { prisma } from './db';

export async function findTasksByUser(
  userId: string,
  options?: { status?: string; categoryId?: string }
) {
  return prisma.task.findMany({
    where: {
      userId,
      ...(options?.status && { status: options.status }),
      ...(options?.categoryId && { categoryId: options.categoryId }),
    },
    include: {
      category: {
        select: { id: true, name: true, color: true },
      },
    },
    orderBy: [
      { priority: 'desc' },
      { dueDate: 'asc' },
      { createdAt: 'desc' },
    ],
  });
}

export async function findTaskById(taskId: string, userId: string) {
  return prisma.task.findFirst({
    where: {
      id: taskId,
      userId, // Ensures user can only access their own tasks
    },
    include: {
      category: true,
    },
  });
}

export async function countTasksByStatus(userId: string) {
  const counts = await prisma.task.groupBy({
    by: ['status'],
    where: { userId },
    _count: { status: true },
  });

  return counts.reduce((acc, item) => {
    acc[item.status] = item._count.status;
    return acc;
  }, {} as Record<string, number>);
}