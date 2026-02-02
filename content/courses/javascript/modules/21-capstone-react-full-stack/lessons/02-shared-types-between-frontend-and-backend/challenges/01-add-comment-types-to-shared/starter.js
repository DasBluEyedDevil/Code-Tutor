// packages/shared/src/types/comment.ts
import { z } from 'zod';

// Define Comment interface with:
// - id, content, userId, taskId, createdAt, updatedAt

// Define CommentWithAuthor extending Comment:
// - add author: { id, email, name } 

// Create validation schema for creating comments:
// - content: string, min 1, max 500

// Export types and schema