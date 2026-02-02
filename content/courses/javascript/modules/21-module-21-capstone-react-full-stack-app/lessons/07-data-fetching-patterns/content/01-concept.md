---
type: "THEORY"
title: "Server State Management with React Query"
---

React Query (TanStack Query) automatically handles caching, synchronization, and background updates for server data.

**Core Features:**
1. **Automatic Deduplication** - Same query made twice = fetch once
2. **Smart Caching** - Configurable staleness and garbage collection
3. **Background Refetching** - Keep data fresh transparently
4. **Automatic Retry** - Retry failed requests with backoff
5. **Optimistic Updates** - Update UI instantly, sync with server
6. **Query Invalidation** - Selective cache updates

**Key Concepts:**
- `useQuery` - Fetch data (GET)
- `useMutation` - Modify data (POST/PUT/DELETE)
- Query Keys - Unique cache identifiers
- Stale Time - How long before refetch needed
- GC Time - How long to keep unused data