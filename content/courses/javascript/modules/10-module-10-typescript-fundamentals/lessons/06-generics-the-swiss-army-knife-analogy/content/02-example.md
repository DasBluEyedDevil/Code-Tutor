---
type: "EXAMPLE"
title: "The `<T>` Power"
---

```typescript
// 1. Basic Generic Function
// <T> is a placeholder for "The Type"
function identity<T>(arg: T): T {
    return arg;
}

const result1 = identity<string>("Hello"); // T is string
const result2 = identity<number>(42);      // T is number

// 2. Generic with Inference
// TypeScript is smart enough to guess T!
const result3 = identity(true); // T is inferred as boolean

// 3. Generic Constraints
// "T must be something that has a .length property"
interface HasLength {
    length: number;
}

function logSize<T extends HasLength>(item: T) {
    console.log(`Size is: ${item.length}`);
}

logSize("Apple"); // Works (strings have length)
logSize([1, 2]);  // Works (arrays have length)
// logSize(10);   // ERROR! Numbers don't have length.

// 4. Generic Interfaces
interface APIResponse<T> {
    data: T;
    status: number;
}

const userResponse: APIResponse<{ name: string }> = {
    data: { name: "Alice" },
    status: 200
};
```