---
type: "EXAMPLE"
title: "Testing Hono Routes"
---

See the code example above demonstrating Testing Hono Routes.

```javascript
import { describe, it, expect, beforeEach } from 'bun:test';
import { Hono } from 'hono';

// Create app with routes
const createApp = () => {
  const app = new Hono();
  const todos: { id: number; text: string; done: boolean }[] = [];
  let nextId = 1;

  app.get('/todos', (c) => c.json(todos));
  
  app.post('/todos', async (c) => {
    const { text } = await c.req.json();
    if (!text) return c.json({ error: 'Text required' }, 400);
    const todo = { id: nextId++, text, done: false };
    todos.push(todo);
    return c.json(todo, 201);
  });

  app.patch('/todos/:id', async (c) => {
    const id = parseInt(c.req.param('id'));
    const todo = todos.find(t => t.id === id);
    if (!todo) return c.json({ error: 'Not found' }, 404);
    todo.done = !todo.done;
    return c.json(todo);
  });

  return app;
};

describe('Todo API', () => {
  let app: ReturnType<typeof createApp>;

  beforeEach(() => {
    app = createApp();  // Fresh app for each test
  });

  it('GET /todos returns empty array initially', async () => {
    const res = await app.request('/todos');
    expect(res.status).toBe(200);
    expect(await res.json()).toEqual([]);
  });

  it('POST /todos creates a todo', async () => {
    const res = await app.request('/todos', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ text: 'Learn Bun' })
    });
    
    expect(res.status).toBe(201);
    const todo = await res.json();
    expect(todo.text).toBe('Learn Bun');
    expect(todo.done).toBe(false);
  });

  it('POST /todos returns 400 for missing text', async () => {
    const res = await app.request('/todos', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({})
    });
    
    expect(res.status).toBe(400);
  });
});
```
