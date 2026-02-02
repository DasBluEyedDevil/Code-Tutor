// Simulating bun:test for this exercise
const describe = (name, fn) => { console.log(`\ndescribe: ${name}`); fn(); };
const it = async (name, fn) => {
  try { await fn(); console.log(`  \u2713 ${name}`); }
  catch (e) { console.log(`  \u2717 ${name}: ${e.message}`); }
};
const expect = (val) => ({
  toBe: (exp) => { if (val !== exp) throw new Error(`Expected ${exp}, got ${val}`); },
  toContain: (exp) => { if (!val.includes(exp)) throw new Error(`Expected to contain ${exp}`); }
});

// Mock fetch helper
function createMockFetch(response) {
  return async () => response;
}

// API Client to test
class ApiClient {
  constructor(fetchFn = fetch) {
    this.fetch = fetchFn;
  }

  async getUser(id) {
    const response = await this.fetch(`/api/users/${id}`);
    
    if (!response.ok) {
      const error = await response.json();
      if (response.status === 404) {
        throw new Error('User not found');
      }
      if (response.status === 400) {
        throw new Error(`Validation error: ${error.message}`);
      }
      if (response.status >= 500) {
        throw new Error('Server error');
      }
      throw new Error('Unknown error');
    }
    
    return await response.json();
  }

  async createUser(userData) {
    const response = await this.fetch('/api/users', {
      method: 'POST',
      body: JSON.stringify(userData)
    });
    
    if (!response.ok) {
      const error = await response.json();
      throw new Error(error.message || 'Failed to create user');
    }
    
    return await response.json();
  }
}

// Helper to create mock responses
function mockResponse(status, data) {
  return {
    ok: status >= 200 && status < 300,
    status,
    json: async () => data
  };
}

describe('ApiClient Error Handling', () => {
  it('handles successful response', async () => {
    const mockFetch = createMockFetch(
      mockResponse(200, { id: 1, name: 'Alice' })
    );
    const client = new ApiClient(mockFetch);
    
    const user = await client.getUser(1);
    expect(user.name).toBe('Alice');
  });

  it('handles 404 not found', async () => {
    // YOUR CODE: Create mockFetch that returns 404 response
    // Verify that getUser throws 'User not found'
  });

  it('handles 400 validation error', async () => {
    // YOUR CODE: Create mockFetch that returns 400 with { message: 'Invalid ID format' }
    // Verify that error message contains 'Validation error'
  });

  it('handles 500 server error', async () => {
    // YOUR CODE: Create mockFetch that returns 500 response
    // Verify that getUser throws 'Server error'
  });

  it('handles network failure', async () => {
    // YOUR CODE: Create mockFetch that throws a network error
    // Hint: async () => { throw new Error('Network error'); }
  });

  it('handles create user validation failure', async () => {
    // YOUR CODE: Create mockFetch that returns 400 for createUser
    // Verify the error message is passed through
  });
});

console.log('\n--- API Error Tests Complete ---');