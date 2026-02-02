---
type: "EXAMPLE"
title: "Readonly<T> and ReadonlyArray<T>"
---

Readonly<T> makes all properties of T read-only, preventing reassignment after creation. ReadonlyArray<T> creates an array type that prevents modifications (push, pop, splice, etc.). These enforce immutability at the type level, catching accidental mutations at compile time rather than runtime.

```typescript
// READONLY<T> - Make all properties read-only

interface Config {
  apiUrl: string;
  timeout: number;
  retries: number;
  debug: boolean;
}

// Mutable config - can be changed anytime
let mutableConfig: Config = {
  apiUrl: 'https://api.example.com',
  timeout: 5000,
  retries: 3,
  debug: false
};

mutableConfig.timeout = 10000;  // OK - config can be mutated

// Readonly config - frozen after creation
const frozenConfig: Readonly<Config> = {
  apiUrl: 'https://api.example.com',
  timeout: 5000,
  retries: 3,
  debug: false
};

// frozenConfig.timeout = 10000;
// ERROR: Cannot assign to 'timeout' because it is a read-only property

console.log(frozenConfig.timeout);  // 5000 - reading is fine

// Real use case: Application settings that shouldn't change
function createAppConfig(env: 'dev' | 'prod'): Readonly<Config> {
  const baseConfig: Config = {
    apiUrl: env === 'prod' ? 'https://api.prod.com' : 'https://api.dev.com',
    timeout: env === 'prod' ? 10000 : 5000,
    retries: 3,
    debug: env === 'dev'
  };
  
  // Return as Readonly - callers can't modify
  return baseConfig;
}

const appConfig = createAppConfig('prod');
// appConfig.debug = true;  // ERROR: read-only

// READONLYARRAY<T> - Immutable arrays

const numbers: ReadonlyArray<number> = [1, 2, 3, 4, 5];

console.log(numbers[0]);  // 1 - reading OK
console.log(numbers.length);  // 5 - length OK

// numbers.push(6);      // ERROR: Property 'push' does not exist
// numbers[0] = 10;      // ERROR: Index signature in type 'readonly number[]' only permits reading
// numbers.pop();        // ERROR: Property 'pop' does not exist
// numbers.splice(0, 1); // ERROR: Property 'splice' does not exist

// Alternative syntax using readonly modifier
const moreNumbers: readonly number[] = [10, 20, 30];
// Same as ReadonlyArray<number>

// Functions that don't modify their input should use readonly
function sum(values: readonly number[]): number {
  // values.push(0);  // ERROR - can't modify
  return values.reduce((a, b) => a + b, 0);
}

console.log(sum([1, 2, 3, 4]));  // 10

// Readonly doesn't make nested objects immutable (shallow!)
interface Team {
  name: string;
  members: string[];
}

const team: Readonly<Team> = {
  name: 'Engineering',
  members: ['Alice', 'Bob']
};

// team.name = 'Product';  // ERROR: read-only
team.members.push('Charlie');  // ALLOWED! Only the reference is readonly
console.log(team.members);  // ['Alice', 'Bob', 'Charlie']

// For deep immutability, create a DeepReadonly type
type DeepReadonly<T> = {
  readonly [P in keyof T]: T[P] extends object ? DeepReadonly<T[P]> : T[P];
};

interface NestedConfig {
  server: {
    host: string;
    port: number;
  };
  features: string[];
}

const deepConfig: DeepReadonly<NestedConfig> = {
  server: { host: 'localhost', port: 3000 },
  features: ['auth', 'api']
};

// deepConfig.server.port = 8080;     // ERROR: read-only
// deepConfig.features.push('logs');  // ERROR: read-only array

// Readonly for function parameters - prevent accidental mutation
function processUsers(users: readonly { name: string; score: number }[]): void {
  // Safe operations
  users.forEach(u => console.log(u.name));
  const sorted = [...users].sort((a, b) => b.score - a.score);  // Copy first!
  console.log('Top scorer:', sorted[0].name);
}

processUsers([{ name: 'Alice', score: 95 }, { name: 'Bob', score: 88 }]);
// Alice
// Bob
// Top scorer: Alice
```
