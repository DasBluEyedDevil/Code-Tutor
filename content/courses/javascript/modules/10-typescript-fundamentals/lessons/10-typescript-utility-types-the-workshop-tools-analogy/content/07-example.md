---
type: "EXAMPLE"
title: "Awaited<T>"
---

Awaited<T> unwraps Promise types to get the resolved value type. It handles nested promises too - Awaited<Promise<Promise<string>>> gives you string. This utility is essential for async/await typing, especially when you need to work with the resolved value type of an async function.

```typescript
// AWAITED<T> - Unwrap Promise types

// Basic Promise unwrapping
type PromiseString = Promise<string>;
type UnwrappedString = Awaited<PromiseString>;  // string

type PromiseNumber = Promise<number>;
type UnwrappedNumber = Awaited<PromiseNumber>;  // number

// Nested Promise handling - Awaited unwraps ALL layers
type NestedPromise = Promise<Promise<Promise<boolean>>>;
type DeeplyUnwrapped = Awaited<NestedPromise>;  // boolean

// Non-Promise types pass through unchanged
type NotAPromise = Awaited<string>;  // string
type AlsoNotPromise = Awaited<number | null>;  // number | null

// Real use case: Working with async function return types
async function fetchUser(id: number) {
  // Simulated API call
  return {
    id,
    name: 'Alice',
    email: 'alice@example.com',
    role: 'admin' as const
  };
}

// ReturnType gives Promise<{...}>
type FetchUserReturn = ReturnType<typeof fetchUser>;
// Promise<{ id: number; name: string; email: string; role: 'admin' }>

// Awaited unwraps it
type User = Awaited<ReturnType<typeof fetchUser>>;
// { id: number; name: string; email: string; role: 'admin' }

// Now you can use the unwrapped type
function processUser(user: User): void {
  console.log(`Processing ${user.name} (${user.role})`);
}

// Awaited with Promise.all
async function loadDashboard() {
  const [user, posts, settings] = await Promise.all([
    fetchUser(1),
    fetchPosts(1),
    fetchSettings(1)
  ]);
  return { user, posts, settings };
}

async function fetchPosts(userId: number) {
  return [{ id: 1, title: 'First Post' }, { id: 2, title: 'Second Post' }];
}

async function fetchSettings(userId: number) {
  return { theme: 'dark', notifications: true };
}

type DashboardData = Awaited<ReturnType<typeof loadDashboard>>;
// { user: {...}; posts: {...}[]; settings: {...} }

// Awaited in generic functions
type UnwrapPromise<T> = T extends Promise<infer U> ? U : T;
// This is essentially what Awaited does (simplified)

async function fetchAndTransform<T>(
  fetcher: () => Promise<T>,
  transformer: (data: Awaited<Promise<T>>) => string
): Promise<string> {
  const data = await fetcher();
  return transformer(data);
}

const result = await fetchAndTransform(
  () => fetchUser(1),
  (user) => `User: ${user.name}`  // TypeScript knows 'user' type!
);

console.log(result);  // 'User: Alice'

// Handling union types with Promise
type MaybePromise<T> = T | Promise<T>;

async function handleMaybeAsync<T>(value: MaybePromise<T>): Promise<T> {
  return await value;  // Works for both Promise<T> and T
}

type ResultType = Awaited<MaybePromise<string>>;  // string

// Practical example: Caching async results
type AsyncCache<T extends (...args: any[]) => Promise<any>> = {
  get: (key: string) => Awaited<ReturnType<T>> | undefined;
  set: (key: string, value: Awaited<ReturnType<T>>) => void;
};

function createAsyncCache<T extends (...args: any[]) => Promise<any>>(): AsyncCache<T> {
  const cache = new Map<string, Awaited<ReturnType<T>>>();
  return {
    get: (key: string) => cache.get(key),
    set: (key: string, value: Awaited<ReturnType<T>>) => cache.set(key, value)
  };
}

const userCache = createAsyncCache<typeof fetchUser>();
userCache.set('user-1', { id: 1, name: 'Alice', email: 'alice@test.com', role: 'admin' });
console.log(userCache.get('user-1'));
// { id: 1, name: 'Alice', email: 'alice@test.com', role: 'admin' }
```
