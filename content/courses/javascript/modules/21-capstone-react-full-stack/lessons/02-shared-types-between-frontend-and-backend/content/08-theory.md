---
type: "THEORY"
title: "Type Safety Benefits"
---

When frontend and backend share types:

**Compile-time Errors:**
```typescript
// Backend changes Task.description to required
// Frontend code breaks immediately at build time
const task: Task = {
  // TypeScript error: missing property 'description'
};
```

**API Contract Clarity:**
```typescript
// No ambiguity about what API returns
const response = await fetchApi<ApiResponse<Task[]>>('/api/tasks');
// autocomplete shows response.data, response.error, response.success
```

**Refactoring Confidence:**
```typescript
// Rename field: Task.description -> Task.details
// Both frontend and backend files show compile errors
// Search and replace across entire codebase
```