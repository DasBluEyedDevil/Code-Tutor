---
type: "WARNING"
title: "Caching Best Practices"
---

**Good Cache Keys:**
- Include all parameters: `['tasks', { status, priority, page }]`
- Use stable objects: `['task', taskId]`
- Avoid random values: NO `Math.random()`

**Bad Cache Keys:**
- ❌ `['tasks']` - Ignores filters
- ❌ `['tasks', Math.random()]` - No caching
- ❌ Non-serializable objects

**Stale Time vs GC Time:**
- `staleTime` - When to refetch (data freshness)
- `gcTime` - When to remove from memory (resource cleanup)
- Set `staleTime < gcTime` always

**Invalidation Patterns:**
```typescript
// Specific query
queryClient.invalidateQueries({ queryKey: ['tasks', '123'] })

// All tasks
queryClient.invalidateQueries({ queryKey: ['tasks'] })

// By predicate
queryClient.invalidateQueries({
  predicate: query => query.queryKey[0] === 'tasks'
})
```