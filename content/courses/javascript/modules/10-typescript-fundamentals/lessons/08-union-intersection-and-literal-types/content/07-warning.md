---
type: "WARNING"
title: "Common Mistakes to Avoid"
---

**1. Confusing Union and Intersection for Objects:**
```typescript
type A = { a: string };
type B = { b: number };

type Union = A | B;        // Has 'a' OR 'b' (or both)
type Intersection = A & B;  // Has BOTH 'a' AND 'b'

// Union: only shared properties accessible without narrowing
function process(x: Union) {
  x.a;  // Error! Not all union members have 'a'
  if ('a' in x) x.a;  // OK after narrowing
}
```

**2. Forgetting `as const` for Literal Inference:**
```typescript
const status = 'active';        // Type: string (widened)
const status2 = 'active' as const;  // Type: 'active' (literal)

const arr = ['a', 'b'];         // Type: string[]
const arr2 = ['a', 'b'] as const;   // Type: readonly ['a', 'b']
```

**3. Accidental Type Widening:**
```typescript
function getStatus() {
  return 'success';  // Returns string, not 'success'
}

function getStatus2(): 'success' | 'error' {
  return 'success';  // Returns literal type
}
```

**4. Impossible Intersections:**
```typescript
type Never = string & number;  // Type: never (impossible)
type Also = { a: string } & { a: number };  // a is never
```

**5. Over-using Type Assertions:**
```typescript
// Avoid: as Type hides errors
const data = JSON.parse(input) as User;  // Dangerous!

// Better: validate at runtime
function isUser(data: unknown): data is User {
  return typeof data === 'object' && data !== null
    && 'name' in data && typeof data.name === 'string';
}
```