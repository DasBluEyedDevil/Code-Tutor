---
type: "EXAMPLE"
title: "Vitest/Bun Mocking Basics"
---

Both Bun and Vitest provide mock.fn() for creating mock functions. Mocks track every call, letting you verify your code interacts correctly with dependencies.

```javascript
import { describe, it, expect, mock, spyOn } from 'bun:test';
// For Vitest: import { vi } from 'vitest'; then use vi.fn() instead of mock()

describe('Mock Function Basics', () => {
  it('creates a mock function that tracks calls', () => {
    // Create a mock function
    const mockFn = mock(() => 'default return');
    
    // Call it with different arguments
    mockFn('hello');
    mockFn('world', 123);
    mockFn();
    
    // Verify call count
    expect(mockFn).toHaveBeenCalledTimes(3);
    
    // Verify specific calls
    expect(mockFn).toHaveBeenCalledWith('hello');
    expect(mockFn).toHaveBeenCalledWith('world', 123);
    
    // Access call details directly
    expect(mockFn.mock.calls[0]).toEqual(['hello']);
    expect(mockFn.mock.calls[1]).toEqual(['world', 123]);
    expect(mockFn.mock.calls[2]).toEqual([]);
  });

  it('configures return values', () => {
    const mockFn = mock();
    
    // Return same value every time
    mockFn.mockReturnValue(42);
    expect(mockFn()).toBe(42);
    expect(mockFn()).toBe(42);
    
    // Return different values on each call
    mockFn.mockReturnValueOnce('first');
    mockFn.mockReturnValueOnce('second');
    expect(mockFn()).toBe('first');
    expect(mockFn()).toBe('second');
    expect(mockFn()).toBe(42);  // Falls back to mockReturnValue
  });

  it('mocks async functions', async () => {
    const mockAsync = mock();
    
    // Return resolved promise
    mockAsync.mockResolvedValue({ id: 1, name: 'Alice' });
    const result = await mockAsync();
    expect(result.name).toBe('Alice');
    
    // Return rejected promise
    mockAsync.mockRejectedValueOnce(new Error('Network error'));
    await expect(mockAsync()).rejects.toThrow('Network error');
  });

  it('spies on existing methods', () => {
    const calculator = {
      add: (a, b) => a + b,
      multiply: (a, b) => a * b
    };
    
    // Spy keeps original implementation but tracks calls
    const addSpy = spyOn(calculator, 'add');
    
    const result = calculator.add(2, 3);
    
    expect(result).toBe(5);  // Real implementation ran
    expect(addSpy).toHaveBeenCalledWith(2, 3);  // Call was tracked
    expect(addSpy).toHaveBeenCalledTimes(1);
  });

  it('replaces spy implementation', () => {
    const api = {
      fetchUser: async (id) => {
        // Real implementation would hit network
        throw new Error('Should not call real API in tests');
      }
    };
    
    // Replace implementation entirely
    spyOn(api, 'fetchUser').mockResolvedValue({ id: 1, name: 'Mock User' });
    
    const user = await api.fetchUser(1);
    expect(user.name).toBe('Mock User');
  });
});

// Practical Example: Testing a UserService
class UserService {
  constructor(database, emailService) {
    this.database = database;
    this.emailService = emailService;
  }

  async createUser(name, email) {
    const user = await this.database.insert('users', { name, email });
    await this.emailService.sendWelcome(email, name);
    return user;
  }
}

describe('UserService with Mocks', () => {
  it('creates user and sends welcome email', async () => {
    // Create mock dependencies
    const mockDatabase = {
      insert: mock().mockResolvedValue({ id: 1, name: 'Alice', email: 'alice@test.com' })
    };
    const mockEmailService = {
      sendWelcome: mock().mockResolvedValue(true)
    };

    const service = new UserService(mockDatabase, mockEmailService);
    const user = await service.createUser('Alice', 'alice@test.com');

    // Verify user was created
    expect(user.id).toBe(1);
    
    // Verify database was called correctly
    expect(mockDatabase.insert).toHaveBeenCalledWith('users', {
      name: 'Alice',
      email: 'alice@test.com'
    });
    
    // Verify welcome email was sent
    expect(mockEmailService.sendWelcome).toHaveBeenCalledWith(
      'alice@test.com',
      'Alice'
    );
  });
});
```
