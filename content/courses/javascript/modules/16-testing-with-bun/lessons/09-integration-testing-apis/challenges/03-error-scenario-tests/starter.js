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

// Order API with various error scenarios
function createOrderApp() {
  const products = new Map([
    [1, { id: 1, name: 'Widget', price: 10, stock: 5 }],
    [2, { id: 2, name: 'Gadget', price: 25, stock: 0 }]  // Out of stock
  ]);

  return {
    request: async (path, options = {}) => {
      const method = options.method || 'GET';
      const body = options.body ? JSON.parse(options.body) : null;

      // POST /api/orders
      if (path === '/api/orders' && method === 'POST') {
        // Validation: required fields
        if (!body?.productId || !body?.quantity) {
          return { 
            status: 400, 
            json: async () => ({ 
              error: 'Validation failed',
              details: [{ field: 'productId/quantity', message: 'Required fields missing' }]
            })
          };
        }

        // Validation: quantity must be positive
        if (body.quantity < 1) {
          return {
            status: 400,
            json: async () => ({
              error: 'Validation failed',
              details: [{ field: 'quantity', message: 'Must be at least 1' }]
            })
          };
        }

        // 404: Product not found
        const product = products.get(body.productId);
        if (!product) {
          return {
            status: 404,
            json: async () => ({ error: 'Product not found', productId: body.productId })
          };
        }

        // 409: Insufficient stock
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

        // Success
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
      // YOUR CODE: Test order with quantity: 0
    });

    it('rejects order with negative quantity', async () => {
      // YOUR CODE: Test order with quantity: -5
    });
  });

  describe('404 Not Found', () => {
    it('returns 404 for non-existent product', async () => {
      // YOUR CODE: Order product ID 999
    });

    it('includes productId in error response', async () => {
      // YOUR CODE: Verify error response contains the productId
    });
  });

  describe('409 Conflict (Business Logic)', () => {
    it('rejects order exceeding stock', async () => {
      // YOUR CODE: Order 100 of product 1 (only 5 in stock)
    });

    it('rejects order for out-of-stock item', async () => {
      // YOUR CODE: Order product 2 (0 stock)
    });

    it('includes stock info in error response', async () => {
      const res = await app.request('/api/orders', {
        method: 'POST',
        body: JSON.stringify({ productId: 1, quantity: 100 })
      });

      expect(res.status).toBe(409);
      const error = await res.json();
      // YOUR CODE: Verify error contains available and requested
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
      expect(order.total).toBe(20);  // 10 * 2
    });
  });
});

console.log('\n--- Error Tests Complete ---');