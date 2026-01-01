// MONOLITH: Everything in one file - needs refactoring!

const users = [
  { id: '1', name: 'Alice', email: 'alice@example.com', createdAt: '2024-01-01' },
  { id: '2', name: 'Bob', email: 'bob@example.com', createdAt: '2024-01-02' }
];

const posts = [
  { id: '1', userId: '1', title: 'Hello World', content: 'My first post', createdAt: '2024-01-01' },
  { id: '2', userId: '1', title: 'Learning Bun', content: 'Bun is fast!', createdAt: '2024-01-02' },
  { id: '3', userId: '2', title: 'TypeScript Tips', content: 'Use strict mode!', createdAt: '2024-01-03' }
];

// Simulated Hono-like router
class Router {
  constructor() { this.routes = []; }
  get(path, handler) { this.routes.push({ method: 'GET', path, handler }); }
  post(path, handler) { this.routes.push({ method: 'POST', path, handler }); }
  handle(method, path, body = null) {
    const route = this.routes.find(r => r.method === method && this.matchPath(r.path, path));
    if (!route) return { status: 404, body: { error: 'Not found' } };
    const params = this.extractParams(route.path, path);
    const c = {
      req: { param: (k) => params[k], json: async () => body, query: () => ({}) },
      json: (data, status = 200) => ({ status, body: data })
    };
    return route.handler(c);
  }
  matchPath(pattern, path) {
    const patternParts = pattern.split('/');
    const pathParts = path.split('/');
    if (patternParts.length !== pathParts.length) return false;
    return patternParts.every((p, i) => p.startsWith(':') || p === pathParts[i]);
  }
  extractParams(pattern, path) {
    const params = {};
    const patternParts = pattern.split('/');
    const pathParts = path.split('/');
    patternParts.forEach((p, i) => { if (p.startsWith(':')) params[p.slice(1)] = pathParts[i]; });
    return params;
  }
}

const app = new Router();

// MESSY: All routes and logic mixed together!

app.get('/users', (c) => {
  return c.json({ success: true, data: users });
});

app.get('/users/:id', (c) => {
  const id = c.req.param('id');
  const user = users.find(u => u.id === id);
  if (!user) return c.json({ success: false, error: 'User not found' }, 404);
  return c.json({ success: true, data: user });
});

app.post('/users', async (c) => {
  const body = await c.req.json();
  if (!body.name || !body.email) {
    return c.json({ success: false, error: 'Name and email required' }, 400);
  }
  // Logic mixed with HTTP handling!
  const newUser = {
    id: String(users.length + 1),
    name: body.name,
    email: body.email.toLowerCase(),
    createdAt: new Date().toISOString().split('T')[0]
  };
  users.push(newUser);
  return c.json({ success: true, data: newUser }, 201);
});

app.get('/posts', (c) => {
  return c.json({ success: true, data: posts });
});

app.get('/users/:userId/posts', (c) => {
  const userId = c.req.param('userId');
  const userPosts = posts.filter(p => p.userId === userId);
  return c.json({ success: true, data: userPosts });
});

app.post('/posts', async (c) => {
  const body = await c.req.json();
  if (!body.userId || !body.title || !body.content) {
    return c.json({ success: false, error: 'userId, title, and content required' }, 400);
  }
  const newPost = {
    id: String(posts.length + 1),
    userId: body.userId,
    title: body.title,
    content: body.content,
    createdAt: new Date().toISOString().split('T')[0]
  };
  posts.push(newPost);
  return c.json({ success: true, data: newPost }, 201);
});

// TODO: Refactor into proper structure below
// Create: UserService, PostService, usersHandler, postsHandler

// Test the refactored code
console.log('=== Testing API ===');
console.log('GET /users:', app.handle('GET', '/users'));
console.log('GET /users/1:', app.handle('GET', '/users/1'));
console.log('GET /posts:', app.handle('GET', '/posts'));
console.log('GET /users/1/posts:', app.handle('GET', '/users/1/posts'));
