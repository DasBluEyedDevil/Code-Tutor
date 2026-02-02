// REFACTORED: Proper separation of concerns

// ============================================================
// DATA LAYER (simulated database)
// ============================================================
const usersDb = [
  { id: '1', name: 'Alice', email: 'alice@example.com', createdAt: '2024-01-01' },
  { id: '2', name: 'Bob', email: 'bob@example.com', createdAt: '2024-01-02' }
];

const postsDb = [
  { id: '1', userId: '1', title: 'Hello World', content: 'My first post', createdAt: '2024-01-01' },
  { id: '2', userId: '1', title: 'Learning Bun', content: 'Bun is fast!', createdAt: '2024-01-02' },
  { id: '3', userId: '2', title: 'TypeScript Tips', content: 'Use strict mode!', createdAt: '2024-01-03' }
];

// ============================================================
// UTILS LAYER
// ============================================================
const utils = {
  generateId: (collection) => String(collection.length + 1),
  getCurrentDate: () => new Date().toISOString().split('T')[0],
  normalizeEmail: (email) => email.toLowerCase().trim()
};

// ============================================================
// SERVICE LAYER (Business Logic)
// ============================================================
class UserService {
  getAllUsers() {
    return usersDb;
  }
  
  getUserById(id) {
    return usersDb.find(u => u.id === id) || null;
  }
  
  createUser(data) {
    const newUser = {
      id: utils.generateId(usersDb),
      name: data.name,
      email: utils.normalizeEmail(data.email),
      createdAt: utils.getCurrentDate()
    };
    usersDb.push(newUser);
    return newUser;
  }
}

class PostService {
  getAllPosts() {
    return postsDb;
  }
  
  getPostsByUserId(userId) {
    return postsDb.filter(p => p.userId === userId);
  }
  
  createPost(data) {
    const newPost = {
      id: utils.generateId(postsDb),
      userId: data.userId,
      title: data.title,
      content: data.content,
      createdAt: utils.getCurrentDate()
    };
    postsDb.push(newPost);
    return newPost;
  }
}

// ============================================================
// HANDLER LAYER (HTTP Request/Response)
// ============================================================
const userService = new UserService();
const postService = new PostService();

const usersHandler = {
  list(c) {
    const users = userService.getAllUsers();
    return c.json({ success: true, data: users });
  },
  
  getById(c) {
    const id = c.req.param('id');
    const user = userService.getUserById(id);
    
    if (!user) {
      return c.json({ success: false, error: 'User not found' }, 404);
    }
    
    return c.json({ success: true, data: user });
  },
  
  async create(c) {
    const body = await c.req.json();
    
    // Validation (could move to middleware)
    if (!body.name || !body.email) {
      return c.json({ success: false, error: 'Name and email required' }, 400);
    }
    
    const user = userService.createUser(body);
    return c.json({ success: true, data: user }, 201);
  }
};

const postsHandler = {
  list(c) {
    const posts = postService.getAllPosts();
    return c.json({ success: true, data: posts });
  },
  
  getByUserId(c) {
    const userId = c.req.param('userId');
    const posts = postService.getPostsByUserId(userId);
    return c.json({ success: true, data: posts });
  },
  
  async create(c) {
    const body = await c.req.json();
    
    if (!body.userId || !body.title || !body.content) {
      return c.json({ success: false, error: 'userId, title, and content required' }, 400);
    }
    
    const post = postService.createPost(body);
    return c.json({ success: true, data: post }, 201);
  }
};

// ============================================================
// ROUTES LAYER (Thin - just connects URLs to handlers)
// ============================================================
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

// User routes - thin, just connecting paths to handlers
app.get('/users', usersHandler.list);
app.get('/users/:id', usersHandler.getById);
app.post('/users', usersHandler.create);

// Post routes
app.get('/posts', postsHandler.list);
app.get('/users/:userId/posts', postsHandler.getByUserId);
app.post('/posts', postsHandler.create);

// ============================================================
// TEST
// ============================================================
console.log('=== Testing Refactored API ===');
console.log('GET /users:', JSON.stringify(app.handle('GET', '/users').body));
console.log('GET /users/1:', JSON.stringify(app.handle('GET', '/users/1').body));
console.log('GET /users/999:', JSON.stringify(app.handle('GET', '/users/999').body));
console.log('GET /posts:', JSON.stringify(app.handle('GET', '/posts').body));
console.log('GET /users/1/posts:', JSON.stringify(app.handle('GET', '/users/1/posts').body));

console.log('\n=== Structure Summary ===');
console.log('Routes: Thin layer connecting URLs to handlers');
console.log('Handlers: HTTP request/response processing');
console.log('Services: Business logic (UserService, PostService)');
console.log('Utils: Shared helper functions');