---
type: "EXAMPLE"
title: "Literal Types - Specific Values Only"
---

Literal types restrict variables to specific exact values, not just any value of that type:

```typescript
// String literal types
type Direction = 'north' | 'south' | 'east' | 'west';

let heading: Direction = 'north';  // Valid
heading = 'up';  // Error: 'up' not in union

// Number literal types
type DiceRoll = 1 | 2 | 3 | 4 | 5 | 6;

function rollDice(): DiceRoll {
  return Math.ceil(Math.random() * 6) as DiceRoll;
}

// Boolean literal (useful for discriminated unions)
type Success = { ok: true; value: string };
type Failure = { ok: false; error: string };
type Result = Success | Failure;

function processResult(result: Result) {
  if (result.ok) {
    console.log(result.value);  // TypeScript knows it's Success
  } else {
    console.log(result.error);  // TypeScript knows it's Failure
  }
}

// Creating enums without the enum keyword
type HttpMethod = 'GET' | 'POST' | 'PUT' | 'DELETE' | 'PATCH';
type StatusCode = 200 | 201 | 400 | 401 | 403 | 404 | 500;

interface Request {
  method: HttpMethod;
  url: string;
  expectedStatus: StatusCode;
}

// const assertions for literal inference
const config = {
  apiUrl: 'https://api.example.com',
  timeout: 5000
} as const;
// config.apiUrl is type 'https://api.example.com', not string
// config.timeout is type 5000, not number

// Array as const for tuple literal types
const colors = ['red', 'green', 'blue'] as const;
type Color = typeof colors[number];  // 'red' | 'green' | 'blue'
```
