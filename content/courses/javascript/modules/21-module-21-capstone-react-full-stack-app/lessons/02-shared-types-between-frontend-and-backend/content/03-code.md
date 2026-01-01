---
type: "CODE"
title: "User Types"
---

Define user-related types shared between frontend and backend:

```typescript
// packages/shared/src/types/user.ts
import { z } from 'zod';

export interface User {
  id: string;
  email: string;
  name: string | null;
  createdAt: string; // ISO date string
  updatedAt: string;
}

export interface UserProfile extends User {
  taskCount: number;
  categoryCount: number;
}

export interface AuthResponse {
  user: User;
  token: string;
}

export interface AuthError {
  error: string;
  code: 'INVALID_CREDENTIALS' | 'USER_NOT_FOUND' | 'EMAIL_EXISTS';
}

// Schemas for validation (both sides)
export const registerInputSchema = z.object({
  email: z.string().email(),
  password: z.string().min(8),
  name: z.string().optional(),
});

export const loginInputSchema = z.object({
  email: z.string().email(),
  password: z.string().min(1),
});

export type RegisterInput = z.infer<typeof registerInputSchema>;
export type LoginInput = z.infer<typeof loginInputSchema>;
```
