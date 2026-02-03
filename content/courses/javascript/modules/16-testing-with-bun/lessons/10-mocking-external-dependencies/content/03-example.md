---
type: "EXAMPLE"
title: "Mocking Modules"
---

When you need to mock an entire module (like a database driver or SDK), use mock.module() in Bun or vi.mock() in Vitest. This replaces all exports before your code imports them.

```javascript
import { describe, it, expect, mock, beforeEach } from 'bun:test';

// Mock the entire database module BEFORE importing code that uses it
mock.module('./database', () => ({
  query: mock().mockResolvedValue([]),
  insert: mock().mockResolvedValue({ id: 1 }),
  update: mock().mockResolvedValue(true),
  delete: mock().mockResolvedValue(true)
}));

// Now import your code - it will get the mocked database
import { UserRepository } from './user-repository';
import * as database from './database';

describe('Module Mocking', () => {
  beforeEach(() => {
    // Reset all mocks between tests
    mock.restore();  // Clears call history
  });

  it('mocks database queries', async () => {
    // Configure mock for this specific test
    database.query.mockResolvedValue([
      { id: 1, name: 'Alice' },
      { id: 2, name: 'Bob' }
    ]);

    const repo = new UserRepository();
    const users = await repo.findAll();

    expect(users).toHaveLength(2);
    expect(database.query).toHaveBeenCalledWith('SELECT * FROM users');
  });

  it('mocks insert returning new ID', async () => {
    database.insert.mockResolvedValue({ id: 42 });

    const repo = new UserRepository();
    const newUser = await repo.create({ name: 'Charlie' });

    expect(newUser.id).toBe(42);
  });
});

// Partial Mocking - Keep some real implementations
mock.module('./utils', () => {
  // Import the real module
  const actual = require('./utils');
  
  return {
    // Keep real implementations
    ...actual,
    // Override only what you need to mock
    sendEmail: mock().mockResolvedValue(true),
    logToService: mock()  // Silence logging in tests
  };
});

// Mocking Default vs Named Exports
mock.module('./api-client', () => ({
  // Mock default export
  default: mock().mockResolvedValue({ data: [] }),
  
  // Mock named exports
  get: mock().mockResolvedValue({ status: 200 }),
  post: mock().mockResolvedValue({ status: 201 })
}));

// Vitest equivalent:
// vi.mock('./database', () => ({
//   query: vi.fn().mockResolvedValue([]),
//   insert: vi.fn().mockResolvedValue({ id: 1 })
// }));

// IMPORTANT: Mock Cleanup Patterns
describe('Proper Mock Cleanup', () => {
  const mockFn = mock();

  beforeEach(() => {
    // Option 1: Clear call history only
    mockFn.mockClear();
    
    // Option 2: Clear calls AND reset return values
    mockFn.mockReset();
    
    // Option 3: Restore original implementation (for spies)
    // mockFn.mockRestore();
  });

  it('test 1 - calls mock twice', () => {
    mockFn();
    mockFn();
    expect(mockFn).toHaveBeenCalledTimes(2);
  });

  it('test 2 - starts fresh', () => {
    mockFn();
    expect(mockFn).toHaveBeenCalledTimes(1);  // Not 3!
  });
});
```
