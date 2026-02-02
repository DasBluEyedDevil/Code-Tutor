---
type: "EXAMPLE"
title: "Intersection Types (A & B) - All Combined"
---

Intersection types combine multiple types into one that has ALL properties from each:

```typescript
// Combining object types
interface HasName {
  name: string;
}

interface HasAge {
  age: number;
}

interface HasEmail {
  email: string;
}

// Intersection: must have ALL properties
type Person = HasName & HasAge;

const alice: Person = {
  name: 'Alice',
  age: 30
  // Both required!
};

// Extending with intersection
type ContactablePerson = Person & HasEmail;

const bob: ContactablePerson = {
  name: 'Bob',
  age: 25,
  email: 'bob@example.com'
  // All three required!
};

// Adding properties to existing types
interface ApiResponse<T> {
  data: T;
  status: number;
}

// Add timestamp to any response
type TimestampedResponse<T> = ApiResponse<T> & {
  timestamp: Date;
  requestId: string;
};

const response: TimestampedResponse<{ users: string[] }> = {
  data: { users: ['Alice', 'Bob'] },
  status: 200,
  timestamp: new Date(),
  requestId: 'req-123'
};

// Practical example: Mixins
interface Serializable {
  serialize(): string;
}

interface Loggable {
  log(): void;
}

type LoggableAndSerializable = Serializable & Loggable;

const item: LoggableAndSerializable = {
  serialize() { return JSON.stringify(this); },
  log() { console.log(this.serialize()); }
};
```
