---
type: "CODE"
title: "Mocking API Calls"
---

Mock Service Worker (MSW) intercepts network requests at the service worker level, making it perfect for testing fetch calls without changing your application code. It works in both Node/Bun and browser environments.

```javascript
import { describe, it, expect, beforeAll, afterAll, afterEach } from 'bun:test';
import { http, HttpResponse } from 'msw';
import { setupServer } from 'msw/node';

// Define request handlers
const handlers = [
  // Mock GET /api/users
  http.get('https://api.example.com/users', () => {
    return HttpResponse.json([
      { id: 1, name: 'Alice' },
      { id: 2, name: 'Bob' }
    ]);
  }),

  // Mock GET /api/users/:id with dynamic params
  http.get('https://api.example.com/users/:id', ({ params }) => {
    const { id } = params;
    if (id === '999') {
      return HttpResponse.json(
        { error: 'User not found' },
        { status: 404 }
      );
    }
    return HttpResponse.json({ id: Number(id), name: `User ${id}` });
  }),

  // Mock POST with request body
  http.post('https://api.example.com/users', async ({ request }) => {
    const body = await request.json();
    return HttpResponse.json(
      { id: 3, ...body },
      { status: 201 }
    );
  })
];

// Create server with handlers
const server = setupServer(...handlers);

// Start/stop server for tests
beforeAll(() => server.listen({ onUnhandledRequest: 'error' }));
afterEach(() => server.resetHandlers());  // Reset between tests
afterAll(() => server.close());

// Your application code
async function fetchUsers() {
  const response = await fetch('https://api.example.com/users');
  if (!response.ok) throw new Error('Failed to fetch users');
  return response.json();
}

async function fetchUser(id) {
  const response = await fetch(`https://api.example.com/users/${id}`);
  if (!response.ok) {
    const error = await response.json();
    throw new Error(error.error);
  }
  return response.json();
}

async function createUser(userData) {
  const response = await fetch('https://api.example.com/users', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(userData)
  });
  return response.json();
}

describe('API Client with MSW', () => {
  it('fetches users list', async () => {
    const users = await fetchUsers();
    
    expect(users).toHaveLength(2);
    expect(users[0].name).toBe('Alice');
  });

  it('fetches single user by ID', async () => {
    const user = await fetchUser(1);
    
    expect(user.id).toBe(1);
    expect(user.name).toBe('User 1');
  });

  it('handles 404 error', async () => {
    await expect(fetchUser(999)).rejects.toThrow('User not found');
  });

  it('creates new user', async () => {
    const newUser = await createUser({ name: 'Charlie', email: 'charlie@test.com' });
    
    expect(newUser.id).toBe(3);
    expect(newUser.name).toBe('Charlie');
  });

  // Override handlers for specific test scenarios
  it('handles server error', async () => {
    // Add temporary handler for this test only
    server.use(
      http.get('https://api.example.com/users', () => {
        return HttpResponse.json(
          { error: 'Internal server error' },
          { status: 500 }
        );
      })
    );

    await expect(fetchUsers()).rejects.toThrow('Failed to fetch users');
  });

  it('handles network error', async () => {
    server.use(
      http.get('https://api.example.com/users', () => {
        return HttpResponse.error();  // Simulates network failure
      })
    );

    await expect(fetchUsers()).rejects.toThrow();
  });

  it('handles slow responses for loading states', async () => {
    server.use(
      http.get('https://api.example.com/users', async () => {
        // Simulate slow API
        await new Promise(resolve => setTimeout(resolve, 100));
        return HttpResponse.json([]);
      })
    );

    const startTime = Date.now();
    await fetchUsers();
    const elapsed = Date.now() - startTime;
    
    expect(elapsed).toBeGreaterThanOrEqual(100);
  });
});
```
