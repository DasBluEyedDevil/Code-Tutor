// Hono app ready for multi-platform deployment

const honoApp = {
  platform: process.env.PLATFORM || 'bun',
  
  routes: {
    '/': (c) => c.text('Hello from Hono!'),
    '/health': (c) => c.json({ status: 'ok', timestamp: new Date() }),
    '/api/users': (c) => c.json([{ id: 1, name: 'Alice' }])
  },
  
  // Simulate Hono's context object
  createContext() {
    return {
      text: (content) => ({ type: 'text', body: content }),
      json: (data) => ({ type: 'json', body: JSON.stringify(data) })
    };
  },
  
  handleRequest(path) {
    const c = this.createContext();
    if (this.routes[path]) {
      const response = this.routes[path](c);
      console.log(`[${this.platform}] GET ${path}`);
      console.log(`Response (${response.type}):`, response.body);
      return response;
    }
    return c.json({ error: 'Not found' });
  }
};

// Test
console.log('=== Hono Multi-Platform Demo ===\n');
honoApp.handleRequest('/');
honoApp.handleRequest('/health');
honoApp.handleRequest('/api/users');