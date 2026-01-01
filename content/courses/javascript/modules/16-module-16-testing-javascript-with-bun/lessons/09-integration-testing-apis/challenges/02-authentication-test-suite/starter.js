// Simulating bun:test for this exercise
const describe = (name, fn) => { console.log(`\ndescribe: ${name}`); fn(); };
const it = async (name, fn) => {
  try { await fn(); console.log(`  \u2713 ${name}`); }
  catch (e) { console.log(`  \u2717 ${name}: ${e.message}`); }
};
const expect = (val) => ({
  toBe: (exp) => { if (val !== exp) throw new Error(`Expected ${exp}, got ${val}`); },
  toBeDefined: () => { if (val === undefined) throw new Error('Expected to be defined'); },
  toContain: (exp) => { if (!val.includes(exp)) throw new Error(`Expected to contain ${exp}`); }
});
const beforeEach = (fn) => fn();

// Auth API simulation
function createAuthApp() {
  const users = new Map([
    ['user@test.com', { id: 1, email: 'user@test.com', password: 'pass123' }]
  ]);
  const tokens = new Map();

  return {
    request: async (path, options = {}) => {
      const method = options.method || 'GET';
      const body = options.body ? JSON.parse(options.body) : null;
      const authHeader = options.headers?.Authorization || '';

      // POST /api/login
      if (path === '/api/login' && method === 'POST') {
        const user = users.get(body?.email);
        if (!user || user.password !== body?.password) {
          return { status: 401, json: async () => ({ error: 'Invalid credentials' }) };
        }
        const token = `token_${Date.now()}`;
        tokens.set(token, user);
        return { status: 200, json: async () => ({ token, user: { id: user.id, email: user.email } }) };
      }

      // GET /api/me (protected)
      if (path === '/api/me' && method === 'GET') {
        if (!authHeader.startsWith('Bearer ')) {
          return { status: 401, json: async () => ({ error: 'No token provided' }) };
        }
        const token = authHeader.slice(7);
        const user = tokens.get(token);
        if (!user) {
          return { status: 401, json: async () => ({ error: 'Invalid token' }) };
        }
        return { status: 200, json: async () => ({ id: user.id, email: user.email }) };
      }

      // POST /api/logout (protected)
      if (path === '/api/logout' && method === 'POST') {
        if (!authHeader.startsWith('Bearer ')) {
          return { status: 401, json: async () => ({ error: 'No token provided' }) };
        }
        const token = authHeader.slice(7);
        tokens.delete(token);
        return { status: 200, json: async () => ({ message: 'Logged out' }) };
      }

      return { status: 404, json: async () => ({ error: 'Not found' }) };
    }
  };
}

// Helper to login and get token
async function login(app, email, password) {
  const res = await app.request('/api/login', {
    method: 'POST',
    body: JSON.stringify({ email, password })
  });
  if (res.status !== 200) return null;
  const { token } = await res.json();
  return token;
}

describe('Authentication Flow Tests', () => {
  let app;

  beforeEach(() => {
    app = createAuthApp();
  });

  describe('Login', () => {
    it('returns token for valid credentials', async () => {
      const res = await app.request('/api/login', {
        method: 'POST',
        body: JSON.stringify({ email: 'user@test.com', password: 'pass123' })
      });

      expect(res.status).toBe(200);
      const data = await res.json();
      expect(data.token).toBeDefined();
      expect(data.user.email).toBe('user@test.com');
    });

    it('rejects invalid password', async () => {
      // YOUR CODE: Test login with wrong password returns 401
    });

    it('rejects non-existent user', async () => {
      // YOUR CODE: Test login with unknown email returns 401
    });
  });

  describe('Protected Routes', () => {
    it('allows access with valid token', async () => {
      const token = await login(app, 'user@test.com', 'pass123');

      const res = await app.request('/api/me', {
        headers: { Authorization: `Bearer ${token}` }
      });

      expect(res.status).toBe(200);
      const user = await res.json();
      expect(user.email).toBe('user@test.com');
    });

    it('rejects request without token', async () => {
      // YOUR CODE: Test /api/me without Authorization header
    });

    it('rejects request with invalid token', async () => {
      // YOUR CODE: Test /api/me with fake token
    });
  });

  describe('Logout', () => {
    it('invalidates token after logout', async () => {
      // YOUR CODE: Login, logout, then verify token no longer works
    });
  });
});

console.log('\n--- Auth Tests Complete ---');