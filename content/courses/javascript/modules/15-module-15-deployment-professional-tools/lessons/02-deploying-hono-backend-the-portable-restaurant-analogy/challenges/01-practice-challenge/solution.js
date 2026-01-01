// Complete Hono app with multi-platform deployment simulation

const honoApp = {
  platform: process.env.PLATFORM || 'bun',
  env: process.env.NODE_ENV || 'development',
  
  routes: {
    '/': (c) => c.text('Hello from Hono!'),
    '/health': (c) => c.json({ 
      status: 'ok', 
      timestamp: new Date().toISOString(),
      platform: honoApp.platform
    }),
    '/api/users': (c) => c.json([
      { id: 1, name: 'Alice' },
      { id: 2, name: 'Bob' }
    ]),
    '/api/users/:id': (c) => c.json({ id: c.params.id, name: 'User' })
  },
  
  middleware: [
    { name: 'cors', applied: true },
    { name: 'logger', applied: true }
  ],
  
  createContext(params = {}) {
    return {
      text: (content) => ({ type: 'text/plain', body: content, status: 200 }),
      json: (data, status = 200) => ({ type: 'application/json', body: JSON.stringify(data), status }),
      html: (content) => ({ type: 'text/html', body: content, status: 200 }),
      params: params
    };
  },
  
  handleRequest(path, params = {}) {
    const c = this.createContext(params);
    console.log(`\n[${this.platform.toUpperCase()}] GET ${path}`);
    
    if (this.routes[path]) {
      const response = this.routes[path](c);
      console.log(`Status: ${response.status}`);
      console.log(`Content-Type: ${response.type}`);
      console.log(`Body: ${response.body}`);
      return response;
    }
    
    console.log('Status: 404');
    return c.json({ error: 'Not found' }, 404);
  },
  
  deployTo(platform) {
    this.platform = platform;
    
    const configs = {
      bun: {
        name: 'Bun on Render',
        buildCmd: 'bun install',
        startCmd: 'bun run src/index.ts',
        export: 'export default app'
      },
      'cloudflare-workers': {
        name: 'Cloudflare Workers',
        buildCmd: 'wrangler deploy',
        startCmd: '(serverless)',
        export: 'export default app'
      },
      deno: {
        name: 'Deno Deploy',
        buildCmd: '(git deploy)',
        startCmd: 'Deno.serve(app.fetch)',
        export: 'Deno.serve(app.fetch)'
      }
    };
    
    const config = configs[platform] || configs.bun;
    
    console.log('\n' + '='.repeat(45));
    console.log(`Deploying to: ${config.name}`);
    console.log('='.repeat(45));
    console.log(`Build: ${config.buildCmd}`);
    console.log(`Start: ${config.startCmd}`);
    console.log(`Export: ${config.export}`);
    console.log('');
    
    return config;
  },
  
  showCode() {
    console.log('\n// This code works on ALL platforms!');
    console.log('import { Hono } from "hono"');
    console.log('import { cors } from "hono/cors"');
    console.log('');
    console.log('const app = new Hono()');
    console.log('');
    console.log('app.use("*", cors())');
    console.log('');
    console.log('app.get("/", (c) => c.text("Hello!"))');
    console.log('app.get("/health", (c) => c.json({ status: "ok" }))');
    console.log('app.get("/api/users", (c) => c.json([...]))');
    console.log('');
    console.log('export default app  // Works for Bun & Workers');
    console.log('// OR: Deno.serve(app.fetch)  // For Deno');
  }
};

// Demonstrate multi-platform deployment
console.log('=== Hono Multi-Platform Deployment Demo ===\n');

// Show the universal code
honoApp.showCode();

// Test on each platform
const platforms = ['bun', 'cloudflare-workers', 'deno'];

platforms.forEach(platform => {
  honoApp.deployTo(platform);
  honoApp.handleRequest('/');
  honoApp.handleRequest('/health');
});

// Show portability advantage
console.log('\n' + '='.repeat(45));
console.log('KEY ADVANTAGE: Same Code Everywhere!');
console.log('='.repeat(45));
console.log('- Write once');
console.log('- Deploy to Bun, Deno, Cloudflare, or Node');
console.log('- Switch platforms without code changes');
console.log('- Edge-ready by default');