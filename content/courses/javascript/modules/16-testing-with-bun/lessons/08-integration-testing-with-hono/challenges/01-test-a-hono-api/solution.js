// Simulating bun:test and Hono
const describe = (name, fn) => { console.log(`\ndescribe: ${name}`); fn(); };
const it = async (name, fn) => {
  try { await fn(); console.log(`  \u2713 ${name}`); }
  catch (e) { console.log(`  \u2717 ${name}: ${e.message}`); }
};
const expect = (val) => ({
  toBe: (exp) => { if (val !== exp) throw new Error(`Expected ${exp}, got ${val}`); },
  toEqual: (exp) => { if (JSON.stringify(val) !== JSON.stringify(exp)) throw new Error(`Expected ${JSON.stringify(exp)}`); },
  toContain: (exp) => { if (!val.includes(exp)) throw new Error(`Expected to contain ${exp}`); }
});
const beforeEach = (fn) => { fn(); };

function createApp() {
  const todos = [];
  let nextId = 1;
  
  return {
    request: async (path, options = {}) => {
      const method = options.method || 'GET';
      const body = options.body ? JSON.parse(options.body) : null;
      
      if (path === '/todos' && method === 'GET') {
        return { status: 200, json: async () => [...todos] };
      }
      
      if (path === '/todos' && method === 'POST') {
        if (!body?.text) {
          return { status: 400, json: async () => ({ error: 'Text required' }) };
        }
        const todo = { id: nextId++, text: body.text, done: false };
        todos.push(todo);
        return { status: 201, json: async () => todo };
      }
      
      if (path.startsWith('/todos/') && method === 'PATCH') {
        const id = parseInt(path.split('/')[2]);
        const todo = todos.find(t => t.id === id);
        if (!todo) return { status: 404, json: async () => ({ error: 'Not found' }) };
        todo.done = !todo.done;
        return { status: 200, json: async () => todo };
      }
      
      return { status: 404 };
    }
  };
}

describe('Todo API Integration Tests', () => {
  let app;
  
  beforeEach(() => {
    app = createApp();
  });

  describe('GET /todos', () => {
    it('returns empty array initially', async () => {
      const res = await app.request('/todos');
      expect(res.status).toBe(200);
      expect(await res.json()).toEqual([]);
    });
  });

  describe('POST /todos', () => {
    it('creates a todo with valid text', async () => {
      const res = await app.request('/todos', {
        method: 'POST',
        body: JSON.stringify({ text: 'Learn Bun testing' })
      });
      
      expect(res.status).toBe(201);
      const todo = await res.json();
      expect(todo.text).toBe('Learn Bun testing');
      expect(todo.done).toBe(false);
    });

    it('returns 400 for missing text', async () => {
      const res = await app.request('/todos', {
        method: 'POST',
        body: JSON.stringify({})
      });
      expect(res.status).toBe(400);
    });
  });

  describe('PATCH /todos/:id', () => {
    it('toggles todo done status', async () => {
      const createRes = await app.request('/todos', {
        method: 'POST',
        body: JSON.stringify({ text: 'Test todo' })
      });
      const { id } = await createRes.json();
      
      const patchRes = await app.request(`/todos/${id}`, { method: 'PATCH' });
      expect(patchRes.status).toBe(200);
      const updated = await patchRes.json();
      expect(updated.done).toBe(true);
    });

    it('returns 404 for non-existent todo', async () => {
      const res = await app.request('/todos/999', { method: 'PATCH' });
      expect(res.status).toBe(404);
    });
  });
});

console.log('\n--- Integration Tests Complete ---');