---
type: "EXAMPLE"
title: "Testing Error Cases"
---

Error testing ensures your API handles failures gracefully. Test validation errors (400), not found errors (404), unauthorized access (401), forbidden actions (403), and server errors (500). Every error response should have a consistent format.

```javascript
import { describe, it, expect, beforeEach } from 'bun:test';
import { Hono } from 'hono';

function createApp() {
  const app = new Hono();
  const products = new Map([
    [1, { id: 1, name: 'Widget', price: 29.99, stock: 10 }],
    [2, { id: 2, name: 'Gadget', price: 49.99, stock: 0 }]  // Out of stock
  ]);

  // Validation middleware
  const validateProduct = async (c, next) => {
    const body = await c.req.json();
    const errors = [];
    
    if (!body.name || typeof body.name !== 'string') {
      errors.push({ field: 'name', message: 'Name is required and must be a string' });
    }
    if (body.name && body.name.length < 2) {
      errors.push({ field: 'name', message: 'Name must be at least 2 characters' });
    }
    if (typeof body.price !== 'number' || body.price < 0) {
      errors.push({ field: 'price', message: 'Price must be a positive number' });
    }
    if (body.stock !== undefined && (!Number.isInteger(body.stock) || body.stock < 0)) {
      errors.push({ field: 'stock', message: 'Stock must be a non-negative integer' });
    }
    
    if (errors.length > 0) {
      return c.json({ error: 'Validation failed', details: errors }, 400);
    }
    
    c.set('validatedBody', body);
    await next();
  };

  // GET single product
  app.get('/api/products/:id', (c) => {
    const id = parseInt(c.req.param('id'));
    
    if (isNaN(id)) {
      return c.json({ error: 'Invalid product ID format' }, 400);
    }
    
    const product = products.get(id);
    if (!product) {
      return c.json({ error: 'Product not found', productId: id }, 404);
    }
    
    return c.json(product);
  });

  // POST create product
  app.post('/api/products', validateProduct, (c) => {
    const body = c.get('validatedBody');
    const id = products.size + 1;
    const product = { id, ...body, stock: body.stock ?? 0 };
    products.set(id, product);
    return c.json(product, 201);
  });

  // POST purchase product
  app.post('/api/products/:id/purchase', async (c) => {
    const id = parseInt(c.req.param('id'));
    const product = products.get(id);
    
    if (!product) {
      return c.json({ error: 'Product not found' }, 404);
    }
    
    const { quantity } = await c.req.json();
    
    if (!Number.isInteger(quantity) || quantity < 1) {
      return c.json({ 
        error: 'Validation failed',
        details: [{ field: 'quantity', message: 'Quantity must be a positive integer' }]
      }, 400);
    }
    
    if (product.stock < quantity) {
      return c.json({ 
        error: 'Insufficient stock',
        available: product.stock,
        requested: quantity
      }, 409);  // Conflict
    }
    
    product.stock -= quantity;
    return c.json({ 
      message: 'Purchase successful',
      product: product,
      purchased: quantity
    });
  });

  // Simulate server error
  app.get('/api/crash', () => {
    throw new Error('Simulated server error');
  });

  // Global error handler
  app.onError((err, c) => {
    console.error('Server error:', err.message);
    return c.json({ 
      error: 'Internal server error',
      message: process.env.NODE_ENV === 'development' ? err.message : undefined
    }, 500);
  });

  return app;
}

describe('Error Handling Tests', () => {
  let app;

  beforeEach(() => {
    app = createApp();
  });

  describe('400 Bad Request - Validation Errors', () => {
    it('rejects product with missing name', async () => {
      const res = await app.request('/api/products', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ price: 19.99 })
      });
      
      expect(res.status).toBe(400);
      const error = await res.json();
      expect(error.error).toBe('Validation failed');
      expect(error.details).toContainEqual({
        field: 'name',
        message: 'Name is required and must be a string'
      });
    });

    it('rejects product with negative price', async () => {
      const res = await app.request('/api/products', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ name: 'Test', price: -10 })
      });
      
      expect(res.status).toBe(400);
      const error = await res.json();
      expect(error.details.some(d => d.field === 'price')).toBe(true);
    });

    it('rejects invalid ID format', async () => {
      const res = await app.request('/api/products/abc');
      
      expect(res.status).toBe(400);
      const error = await res.json();
      expect(error.error).toBe('Invalid product ID format');
    });

    it('returns multiple validation errors at once', async () => {
      const res = await app.request('/api/products', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ name: '', price: -5, stock: -1 })
      });
      
      expect(res.status).toBe(400);
      const error = await res.json();
      expect(error.details.length).toBeGreaterThan(1);
    });
  });

  describe('404 Not Found', () => {
    it('returns 404 for non-existent product', async () => {
      const res = await app.request('/api/products/999');
      
      expect(res.status).toBe(404);
      const error = await res.json();
      expect(error.error).toBe('Product not found');
      expect(error.productId).toBe(999);
    });
  });

  describe('409 Conflict - Business Logic Errors', () => {
    it('rejects purchase with insufficient stock', async () => {
      const res = await app.request('/api/products/1/purchase', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ quantity: 100 })  // Only 10 in stock
      });
      
      expect(res.status).toBe(409);
      const error = await res.json();
      expect(error.error).toBe('Insufficient stock');
      expect(error.available).toBe(10);
      expect(error.requested).toBe(100);
    });

    it('rejects purchase of out-of-stock item', async () => {
      const res = await app.request('/api/products/2/purchase', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ quantity: 1 })
      });
      
      expect(res.status).toBe(409);
      const error = await res.json();
      expect(error.available).toBe(0);
    });
  });

  describe('500 Internal Server Error', () => {
    it('handles unexpected errors gracefully', async () => {
      const res = await app.request('/api/crash');
      
      expect(res.status).toBe(500);
      const error = await res.json();
      expect(error.error).toBe('Internal server error');
    });
  });

  describe('Error Response Format Consistency', () => {
    it('all errors have error field', async () => {
      const responses = await Promise.all([
        app.request('/api/products/abc'),           // 400
        app.request('/api/products/999'),           // 404
        app.request('/api/products', {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({})
        })                                           // 400
      ]);
      
      for (const res of responses) {
        expect(res.status).toBeGreaterThanOrEqual(400);
        const body = await res.json();
        expect(body.error).toBeDefined();
      }
    });
  });
});
```
