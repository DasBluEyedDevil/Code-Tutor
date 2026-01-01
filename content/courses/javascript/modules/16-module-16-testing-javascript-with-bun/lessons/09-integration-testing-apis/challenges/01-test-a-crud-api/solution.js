// Simulating bun:test and Hono for this exercise
const describe = (name, fn) => { console.log(`\ndescribe: ${name}`); fn(); };
const it = async (name, fn) => {
  try { await fn(); console.log(`  \u2713 ${name}`); }
  catch (e) { console.log(`  \u2717 ${name}: ${e.message}`); }
};
const expect = (val) => ({
  toBe: (exp) => { if (val !== exp) throw new Error(`Expected ${exp}, got ${val}`); },
  toEqual: (exp) => { if (JSON.stringify(val) !== JSON.stringify(exp)) throw new Error(`Expected ${JSON.stringify(exp)}`); },
  toHaveLength: (n) => { if (val.length !== n) throw new Error(`Expected length ${n}, got ${val.length}`); },
  toBeDefined: () => { if (val === undefined) throw new Error('Expected value to be defined'); }
});
const beforeEach = (fn) => fn();

function createBookApp() {
  const books = new Map();
  let nextId = 1;

  return {
    request: async (path, options = {}) => {
      const method = options.method || 'GET';
      const body = options.body ? JSON.parse(options.body) : null;

      if (path === '/api/books' && method === 'GET') {
        return { status: 200, json: async () => Array.from(books.values()) };
      }

      if (path.match(/\/api\/books\/\d+$/) && method === 'GET') {
        const id = parseInt(path.split('/').pop());
        const book = books.get(id);
        if (!book) return { status: 404, json: async () => ({ error: 'Book not found' }) };
        return { status: 200, json: async () => book };
      }

      if (path === '/api/books' && method === 'POST') {
        if (!body?.title || !body?.author) {
          return { status: 400, json: async () => ({ error: 'Title and author required' }) };
        }
        const book = { id: nextId++, title: body.title, author: body.author, year: body.year || null };
        books.set(book.id, book);
        return { status: 201, json: async () => book };
      }

      if (path.match(/\/api\/books\/\d+$/) && method === 'PUT') {
        const id = parseInt(path.split('/').pop());
        const book = books.get(id);
        if (!book) return { status: 404, json: async () => ({ error: 'Book not found' }) };
        const updated = { ...book, ...body };
        books.set(id, updated);
        return { status: 200, json: async () => updated };
      }

      if (path.match(/\/api\/books\/\d+$/) && method === 'DELETE') {
        const id = parseInt(path.split('/').pop());
        if (!books.has(id)) return { status: 404, json: async () => ({ error: 'Book not found' }) };
        books.delete(id);
        return { status: 200, json: async () => ({ deleted: true }) };
      }

      return { status: 404, json: async () => ({ error: 'Not found' }) };
    }
  };
}

describe('Book API CRUD Tests', () => {
  let app;

  beforeEach(() => {
    app = createBookApp();
  });

  describe('CREATE - POST /api/books', () => {
    it('creates a book with valid data', async () => {
      const res = await app.request('/api/books', {
        method: 'POST',
        body: JSON.stringify({ title: '1984', author: 'George Orwell', year: 1949 })
      });

      expect(res.status).toBe(201);
      const book = await res.json();
      expect(book.title).toBe('1984');
      expect(book.id).toBeDefined();
    });

    it('rejects book without title', async () => {
      const res = await app.request('/api/books', {
        method: 'POST',
        body: JSON.stringify({ author: 'Unknown' })
      });
      expect(res.status).toBe(400);
    });
  });

  describe('READ - GET /api/books', () => {
    it('returns empty array initially', async () => {
      const res = await app.request('/api/books');
      expect(res.status).toBe(200);
      expect(await res.json()).toEqual([]);
    });

    it('returns created books', async () => {
      await app.request('/api/books', {
        method: 'POST',
        body: JSON.stringify({ title: 'Test Book', author: 'Test Author' })
      });

      const res = await app.request('/api/books');
      const books = await res.json();
      expect(books).toHaveLength(1);
      expect(books[0].title).toBe('Test Book');
    });
  });

  describe('READ - GET /api/books/:id', () => {
    it('returns 404 for non-existent book', async () => {
      const res = await app.request('/api/books/999');
      expect(res.status).toBe(404);
    });
  });

  describe('UPDATE - PUT /api/books/:id', () => {
    it('updates existing book', async () => {
      const createRes = await app.request('/api/books', {
        method: 'POST',
        body: JSON.stringify({ title: 'Draft', author: 'Unknown' })
      });
      const { id } = await createRes.json();

      const updateRes = await app.request(`/api/books/${id}`, {
        method: 'PUT',
        body: JSON.stringify({ title: 'Final Title' })
      });
      expect(updateRes.status).toBe(200);
      const updated = await updateRes.json();
      expect(updated.title).toBe('Final Title');
    });
  });

  describe('DELETE - DELETE /api/books/:id', () => {
    it('deletes existing book', async () => {
      const createRes = await app.request('/api/books', {
        method: 'POST',
        body: JSON.stringify({ title: 'To Delete', author: 'Someone' })
      });
      const { id } = await createRes.json();

      const deleteRes = await app.request(`/api/books/${id}`, { method: 'DELETE' });
      expect(deleteRes.status).toBe(200);

      const getRes = await app.request(`/api/books/${id}`);
      expect(getRes.status).toBe(404);
    });
  });
});

console.log('\n--- CRUD Tests Complete ---');