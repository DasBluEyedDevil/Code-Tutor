---
type: "THEORY"
title: "useState and useEffect Hooks"
---

**useState Hook**
The `useState` hook lets functional components have state. Unlike class components, you can call it multiple times:

```typescript
const [count, setCount] = useState<number>(0);
const [name, setName] = useState<string>('');
```

TypeScript infers the type from the initial value, or you can explicitly type it.

**useEffect Hook**
The `useEffect` hook runs side effects. It takes two arguments:
1. A function to run
2. A dependency array that controls when it runs:

```typescript
// Runs once on mount
useEffect(() => {
  console.log('Component mounted');
}, []);

// Runs when 'userId' changes
useEffect(() => {
  fetchUser(userId);
}, [userId]);

// Runs after every render (no dependency array)
useEffect(() => {
  console.log('Component rendered');
});
```

**Cleanup Function**
Return a cleanup function to prevent memory leaks:

```typescript
useEffect(() => {
  const subscription = api.subscribe(onMessage);
  return () => subscription.unsubscribe(); // Cleanup
}, []);
```