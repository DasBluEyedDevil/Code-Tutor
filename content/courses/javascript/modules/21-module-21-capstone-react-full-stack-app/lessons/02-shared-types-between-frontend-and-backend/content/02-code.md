---
type: "CODE"
title: "Create Shared Types Index"
---

Set up the main export file for shared types:

```typescript
// packages/shared/src/index.ts

// Export all types from this single point
export * from './types/user';
export * from './types/task';
export * from './types/api';
export * from './schemas/validation';
```
