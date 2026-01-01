// packages/shared/src/types/comment.ts
import { z } from 'zod';

export interface Comment {
  id: string;
  content: string;
  userId: string;
  taskId: string;
  createdAt: string;
  updatedAt: string;
}

export interface CommentWithAuthor extends Comment {
  author: {
    id: string;
    email: string;
    name: string | null;
  };
}

export const createCommentSchema = z.object({
  content: z
    .string()
    .min(1, 'Comment cannot be empty')
    .max(500, 'Comment too long'),
});

export type CreateCommentInput = z.infer<typeof createCommentSchema>;

// Update packages/shared/src/index.ts to add:
// export * from './types/comment';