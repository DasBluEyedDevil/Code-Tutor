---
type: "CONCEPT"
title: "Type Sharing in Full-Stack Apps"
---

One of the biggest advantages of full-stack TypeScript is sharing types between frontend and backend. This ensures:

**Benefits:**
- No manual API contract documentation needed
- Type errors caught at compile time on both sides
- Automatic documentation through types
- Refactoring changes propagate everywhere
- Single source of truth for data structures

**What to Share:**
- API request/response types
- Domain models (User, Task, Category)
- Validation schemas
- Error types
- Constants (status enums, etc.)

**What NOT to share:**
- Backend-only logic (password hashing, auth)
- Frontend-only UI types
- Database-specific code
- Environment variables

**Import Path:**
```typescript
// In backend: api/src/routes/tasks.ts
import { Task } from '@app/shared';

// In frontend: web/src/pages/Tasks.tsx
import { Task } from '@app/shared';
```