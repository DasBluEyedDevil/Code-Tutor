---
type: "CONCEPT"
title: "Type-Safe API Client"
---

A fetch wrapper that combines shared types with HTTP requests gives you:

**Full Type Safety:**
- Request/response types known at compile time
- IDE autocomplete for API responses
- No need for manual type casting

**Consistent Error Handling:**
- Standardized error responses
- Automatic error parsing
- User-friendly error messages

**Easy Testing:**
- Mock the client with matching interface
- Test error scenarios
- Verify request payloads

**Key Components:**
1. **Base client** - HTTP layer with defaults
2. **Type wrapper** - Enforces response types
3. **Error handling** - Standardized errors
4. **Interceptors** - Add auth tokens, logging
5. **React Query** - Caching and state management

**Comparison:**
```typescript
// Without type safety (error-prone)
const res = await fetch('/api/tasks');
const data = await res.json(); // any type
data.items.map(t => t.title); // Could fail at runtime

// With type safety (safe)
const { items } = await apiClient.getTasks();
items.map(t => t.title); // TypeScript ensures this works
```