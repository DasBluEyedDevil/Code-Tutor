---
type: "EXAMPLE"
title: "CORS Middleware"
---

Cross-Origin Resource Sharing (CORS) is essential when your API is accessed from web browsers on different domains. Hono provides a built-in cors() middleware that handles preflight requests, allowed origins, methods, and headers. Proper CORS configuration is critical for security while enabling legitimate cross-origin requests from your frontend applications.

```javascript
// Hono CORS Middleware (2025)
// Enable cross-origin requests safely

import { Hono } from 'hono';
import { cors } from 'hono/cors';

const app = new Hono();

// BASIC CORS - Allow all origins (development only!)
// app.use('*', cors());

// PRODUCTION CORS CONFIGURATION
app.use('*', cors({
  // Allowed origins (your frontend domains)
  origin: [
    'https://myapp.com',
    'https://www.myapp.com',
    'http://localhost:3000',  // Local development
    'http://localhost:5173'   // Vite dev server
  ],
  
  // Or use a function for dynamic origin checking
  // origin: (origin) => {
  //   return origin.endsWith('.myapp.com') 
  //     ? origin 
  //     : 'https://myapp.com';
  // },
  
  // Allowed HTTP methods
  allowMethods: ['GET', 'POST', 'PUT', 'PATCH', 'DELETE', 'OPTIONS'],
  
  // Allowed headers in requests
  allowHeaders: [
    'Content-Type',
    'Authorization',
    'X-Requested-With',
    'X-Custom-Header'
  ],
  
  // Headers exposed to the browser
  exposeHeaders: [
    'Content-Length',
    'X-Request-Id'
  ],
  
  // Max age for preflight cache (in seconds)
  maxAge: 86400,  // 24 hours
  
  // Allow credentials (cookies, authorization headers)
  credentials: true
}));

// DIFFERENT CORS FOR DIFFERENT ROUTES

// Public API - more permissive
app.use('/api/public/*', cors({
  origin: '*',  // Allow any origin
  allowMethods: ['GET'],  // Read-only
  maxAge: 3600
}));

// Private API - restrictive
app.use('/api/private/*', cors({
  origin: ['https://myapp.com'],
  allowMethods: ['GET', 'POST', 'PUT', 'DELETE'],
  credentials: true
}));

// Webhook endpoints - specific origins
app.use('/webhooks/*', cors({
  origin: [
    'https://stripe.com',
    'https://github.com'
  ],
  allowMethods: ['POST']
}));

// ROUTES

app.get('/api/public/data', (c) => {
  return c.json({
    message: 'Public data - accessible from any origin',
    data: ['item1', 'item2', 'item3']
  });
});

app.get('/api/private/profile', (c) => {
  return c.json({
    message: 'Private data - only from allowed origins',
    user: { id: 1, name: 'Alice' }
  });
});

app.post('/api/private/update', async (c) => {
  const body = await c.req.json();
  return c.json({
    message: 'Data updated',
    received: body
  });
});

// MANUAL CORS HANDLING (for custom logic)
const customCors = async (c, next) => {
  const origin = c.req.header('Origin');
  const allowedOrigins = ['https://myapp.com', 'http://localhost:3000'];
  
  // Check if origin is allowed
  if (origin && allowedOrigins.includes(origin)) {
    c.header('Access-Control-Allow-Origin', origin);
    c.header('Access-Control-Allow-Credentials', 'true');
  }
  
  // Handle preflight requests
  if (c.req.method === 'OPTIONS') {
    c.header('Access-Control-Allow-Methods', 'GET, POST, PUT, DELETE');
    c.header('Access-Control-Allow-Headers', 'Content-Type, Authorization');
    c.header('Access-Control-Max-Age', '86400');
    return c.text('', 204);  // No content
  }
  
  await next();
};

// Apply custom CORS to specific route
app.use('/api/custom/*', customCors);

app.get('/api/custom/data', (c) => {
  return c.json({ message: 'Custom CORS applied' });
});

export default app;

// CORS CONCEPTS:
// - Preflight: Browser sends OPTIONS request first for non-simple requests
// - Simple requests: GET/POST with standard headers (no preflight needed)
// - Credentials: Cookies/auth headers require credentials: true
// - Origin: The domain making the request (sent by browser)

// SECURITY TIPS:
// 1. Never use origin: '*' with credentials: true
// 2. Whitelist specific origins in production
// 3. Limit allowed methods to what you need
// 4. Use short maxAge during development
```
