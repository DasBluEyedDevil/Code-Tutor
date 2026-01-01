// Simulating bun:test for this exercise
const describe = (name, fn) => { console.log(`\ndescribe: ${name}`); fn(); };
const it = async (name, fn) => {
  try { await fn(); console.log(`  \u2713 ${name}`); }
  catch (e) { console.log(`  \u2717 ${name}: ${e.message}`); }
};
const expect = (val) => ({
  toBe: (exp) => { if (val !== exp) throw new Error(`Expected ${exp}, got ${val}`); },
  toBeDefined: () => { if (val === undefined) throw new Error('Expected defined'); },
  toBeGreaterThan: (n) => { if (!(val > n)) throw new Error(`Expected > ${n}`); }
});
const beforeEach = (fn) => fn();

function createOrderApp() {
  const products = new Map([
    [1, { id: 1, name: 'Widget', price: 10, stock: 5 }],
    [2, { id: 2, name: 'Gadget', price: 25, stock: 0 }]
  ]);

  return {
    request: async (path, options = {}) => {
      const method = options.method || 'GET';
      const body = options.body ? JSON.parse(options.body) : null;

      if (path === '/api/orders' && method === 'POST') {
        if (!body?.productId || !body?.quantity) {
          return { 
            status: 400, 
            json: async () => ({ 
              error: 'Validation failed',
              details: [{ field: 'productId/quantity', message: 'Required fields missing' }]
            })
          };
        }

        if (body.quantity < 1) {
          return {
            status: 400,
            json: async () => ({
              error: 'Validation failed',
              details: [{ field: 'quantity', message: 'Must be at least 1' }]
            })
          };
        }

        const product = products.get(body.productId);
        if (!product) {
          return {
            status: 404,
            json: async () => ({ error: 'Product not found', productId: body.productId })
          };
        }

        if (product.stock < body.quantity) {
          return {
            status: 409,
            json: async () => ({
              error: 'Insufficient stock',
              available: product.stock,
              requested: body.quantity
            })
          };
        }

        product.stock -= body.quantity;
        return {
          status: 201,
          json: async () => ({
            orderId: Date.now(),
            product: product.name,
            quantity: body.quantity,
            total: product.price * body.quantity
          })
        };
      }

      return { status: 404 };
    }
  };
}

describe('Order API Error Scenarios', () => {
  let app;

  beforeEach(() => {
    app = createOrderApp();
  });

  describe('400 Validation Errors', () => {
    it('rejects order without productId', async () => {
      const res = await app.request('/api/orders', {
        method: 'POST',
        body: JSON.stringify({ quantity: 2 })
      });

      expect(res.status).toBe(400);
      const error = await res.json();
      expect(error.error).toBe('Validation failed');
      expect(error.details).toBeDefined();
    });

    it('rejects order with quantity less than 1', async () => {
      const res = await app.request('/api/orders', {
        method: 'POST',
        body: JSON.stringify({ productId: 1, quantity: 0 })
      });
      expect(res.status).toBe(400);
    });

    it('rejects order with negative quantity', async () => {
      const res = await app.request('/api/orders', {
        method: 'POST',
        body: JSON.stringify({ productId: 1, quantity: -5 })
      });
      expect(res.status).toBe(400);
    });
  });

  describe('404 Not Found', () => {
    it('returns 404 for non-existent product', async () => {
      const res = await app.request('/api/orders', {
        method: 'POST',
        body: JSON.stringify({ productId: 999, quantity: 1 })
      });
      expect(res.status).toBe(404);
    });

    it('includes productId in error response', async () => {
      const res = await app.request('/api/orders', {
        method: 'POST',
        body: JSON.stringify({ productId: 999, quantity: 1 })
      });
      const error = await res.json();
      expect(error.productId).toBe(999);
    });
  });

  describe('409 Conflict (Business Logic)', () => {
    it('rejects order exceeding stock', async () => {
      const res = await app.request('/api/orders', {
        method: 'POST',
        body: JSON.stringify({ productId: 1, quantity: 100 })
      });
      expect(res.status).toBe(409);
    });

    it('rejects order for out-of-stock item', async () => {
      const res = await app.request('/api/orders', {
        method: 'POST',
        body: JSON.stringify({ productId: 2, quantity: 1 })
      });
      expect(res.status).toBe(409);
    });

    it('includes stock info in error response', async () => {
      const res = await app.request('/api/orders', {
        method: 'POST',
        body: JSON.stringify({ productId: 1, quantity: 100 })
      });

      expect(res.status).toBe(409);
      const error = await res.json();
      expect(error.available).toBeDefined();
      expect(error.requested).toBe(100);
    });
  });

  describe('Success Case (for comparison)', () => {
    it('creates order with valid data', async () => {
      const res = await app.request('/api/orders', {
        method: 'POST',
        body: JSON.stringify({ productId: 1, quantity: 2 })
      });

      expect(res.status).toBe(201);
      const order = await res.json();
      expect(order.orderId).toBeDefined();
      expect(order.total).toBe(20);
    });
  });
});

console.log('\n--- Error Tests Complete ---');