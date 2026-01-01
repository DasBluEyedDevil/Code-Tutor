---
type: "EXAMPLE"
title: "Union Types (A | B) - One or the Other"
---

Union types let a value be one of several possible types. Use the pipe | operator to combine types:

```typescript
// Basic union with primitives
let id: string | number;
id = 'user-123';  // Valid: string
id = 42;          // Valid: number
id = true;        // Error: boolean not in union

// Union with object types
interface Dog {
  kind: 'dog';
  bark: () => void;
}

interface Cat {
  kind: 'cat';
  meow: () => void;
}

type Pet = Dog | Cat;

function interact(pet: Pet) {
  // TypeScript knows pet is Dog OR Cat
  // We can only access shared properties safely:
  console.log(pet.kind);  // OK: both have 'kind'
  
  // pet.bark();  // Error: Cat doesn't have bark
  // pet.meow();  // Error: Dog doesn't have meow
  
  // Narrowing: check which type we have
  if (pet.kind === 'dog') {
    pet.bark();  // Now TypeScript knows it's a Dog
  } else {
    pet.meow();  // Now TypeScript knows it's a Cat
  }
}

// Function returning union type
function parseInput(input: string): number | null {
  const parsed = parseInt(input);
  return isNaN(parsed) ? null : parsed;
}

const result = parseInput('42');
if (result !== null) {
  // TypeScript knows result is number here
  console.log(result * 2);  // 84
}
```
