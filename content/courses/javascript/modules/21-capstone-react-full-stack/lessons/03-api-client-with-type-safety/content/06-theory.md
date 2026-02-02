---
type: "THEORY"
title: "Type Safety Throughout the Stack"
---

Complete type safety flow:

```
Backend Data → Shared Types → Frontend Usage
```

**Backend (Hono):**
```typescript
const task: Task = { /* ... */ };
return c.json(ApiResponse<Task> { data: task });
```

**Frontend (React):**
```typescript
const { data } = useTasks();
// data.items is Task[]
data.items.map(t => t.title) // ✓ TypeScript knows title exists
```

**Refactoring Safety:**
- Change Task interface → All usages break
- Add required field → Both frontend and backend compilation fails
- Rename field → IDE find-and-replace across packages

**Developer Experience:**
- IDE autocomplete for API responses
- Compile-time errors instead of runtime
- Self-documenting API contract
- No manual synchronization needed