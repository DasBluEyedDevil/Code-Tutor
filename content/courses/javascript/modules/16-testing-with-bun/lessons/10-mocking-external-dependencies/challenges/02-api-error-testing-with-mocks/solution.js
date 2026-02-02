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

function createMockFetch(response) {
  return async () => response;
}

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
    const mockFetch = createMockFetch(
      mockResponse(404, { error: 'Not found' })
    );
    const client = new ApiClient(mockFetch);
    
    try {
      await client.getUser(999);
      throw new Error('Should have thrown');
    } catch (e) {
      expect(e.message).toBe('User not found');
    }
  });

  it('handles 400 validation error', async () => {
    const mockFetch = createMockFetch(
      mockResponse(400, { message: 'Invalid ID format' })
    );
    const client = new ApiClient(mockFetch);
    
    try {
      await client.getUser('abc');
      throw new Error('Should have thrown');
    } catch (e) {
      expect(e.message).toContain('Validation error');
    }
  });

  it('handles 500 server error', async () => {
    const mockFetch = createMockFetch(
      mockResponse(500, { error: 'Internal error' })
    );
    const client = new ApiClient(mockFetch);
    
    try {
      await client.getUser(1);
      throw new Error('Should have thrown');
    } catch (e) {
      expect(e.message).toBe('Server error');
    }
  });

  it('handles network failure', async () => {
    const mockFetch = async () => { throw new Error('Network error'); };
    const client = new ApiClient(mockFetch);
    
    try {
      await client.getUser(1);
      throw new Error('Should have thrown');
    } catch (e) {
      expect(e.message).toBe('Network error');
    }
  });

  it('handles create user validation failure', async () => {
    const mockFetch = createMockFetch(
      mockResponse(400, { message: 'Email is required' })
    );
    const client = new ApiClient(mockFetch);
    
    try {
      await client.createUser({ name: 'Bob' });
      throw new Error('Should have thrown');
    } catch (e) {
      expect(e.message).toBe('Email is required');
    }
  });
});

console.log('\n--- API Error Tests Complete ---');