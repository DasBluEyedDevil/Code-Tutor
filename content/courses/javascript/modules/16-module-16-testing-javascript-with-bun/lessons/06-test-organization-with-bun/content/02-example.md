---
type: "EXAMPLE"
title: "Organizing Tests by Feature"
---

See the code example above demonstrating test organization.

```javascript
// Project structure:
// src/
//   users/
//     user.service.ts
//     user.service.test.ts    <- Co-located tests
//   orders/
//     order.service.ts
//     order.service.test.ts
// tests/
//   setup.ts                  <- Global setup
//   integration/
//     api.test.ts             <- Integration tests

// tests/setup.ts - runs before all tests
import { beforeAll, afterAll, mock } from 'bun:test';

// Mock environment for all tests
process.env.NODE_ENV = 'test';

// Global test database setup
beforeAll(async () => {
  console.log('Setting up test environment...');
});

afterAll(async () => {
  console.log('Cleaning up test environment...');
});

// Run with: bun test --preload ./tests/setup.ts
```
