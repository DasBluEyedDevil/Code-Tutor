---
type: "EXAMPLE"
title: "Enhanced Logging Middleware with Timing"
---

Production-ready logging middleware captures request details, timing information, and response status. This middleware demonstrates the power of running code both before and after next() to measure request duration. Proper logging is essential for debugging, performance monitoring, and security auditing of your API.

```javascript
// Hono Enhanced Logging Middleware (2025)
// Production-ready request logging

import { Hono } from 'hono';
import { logger } from 'hono/logger';

const app = new Hono();

// USING HONO'S BUILT-IN LOGGER
// Simple one-liner for development
// app.use('*', logger());

// CUSTOM PRODUCTION LOGGER
const productionLogger = async (c, next) => {
  const start = Date.now();
  const requestId = crypto.randomUUID();
  
  // Store request ID for tracing
  c.set('requestId', requestId);
  
  // Extract request info
  const method = c.req.method;
  const path = c.req.path;
  const query = c.req.query();
  const userAgent = c.req.header('User-Agent') || 'unknown';
  const ip = c.req.header('X-Forwarded-For')?.split(',')[0] || 
             c.req.header('X-Real-IP') || 
             'unknown';
  
  // Log request start
  console.log(JSON.stringify({
    type: 'request',
    requestId,
    timestamp: new Date().toISOString(),
    method,
    path,
    query: Object.keys(query).length > 0 ? query : undefined,
    ip,
    userAgent
  }));
  
  try {
    // Process request
    await next();
    
    // Log successful response
    const duration = Date.now() - start;
    const status = c.res.status;
    
    console.log(JSON.stringify({
      type: 'response',
      requestId,
      timestamp: new Date().toISOString(),
      method,
      path,
      status,
      duration: `${duration}ms`,
      success: status < 400
    }));
    
    // Add timing header for clients
    c.header('X-Response-Time', `${duration}ms`);
    c.header('X-Request-Id', requestId);
    
  } catch (error) {
    // Log error
    const duration = Date.now() - start;
    
    console.error(JSON.stringify({
      type: 'error',
      requestId,
      timestamp: new Date().toISOString(),
      method,
      path,
      duration: `${duration}ms`,
      error: error.message,
      stack: process.env.NODE_ENV !== 'production' ? error.stack : undefined
    }));
    
    throw error;  // Re-throw for error handler
  }
};

// COLORIZED CONSOLE LOGGER (development)
const colorLogger = async (c, next) => {
  const start = Date.now();
  const method = c.req.method;
  const path = c.req.path;
  
  // Color codes for terminal
  const colors = {
    reset: '\x1b[0m',
    green: '\x1b[32m',
    yellow: '\x1b[33m',
    red: '\x1b[31m',
    cyan: '\x1b[36m',
    gray: '\x1b[90m'
  };
  
  const methodColors = {
    GET: colors.green,
    POST: colors.yellow,
    PUT: colors.cyan,
    DELETE: colors.red,
    PATCH: colors.yellow
  };
  
  await next();
  
  const duration = Date.now() - start;
  const status = c.res.status;
  
  // Color based on status
  let statusColor = colors.green;
  if (status >= 400) statusColor = colors.yellow;
  if (status >= 500) statusColor = colors.red;
  
  const methodColor = methodColors[method] || colors.gray;
  
  console.log(
    `${methodColor}${method.padEnd(7)}${colors.reset}` +
    `${path} ` +
    `${statusColor}${status}${colors.reset} ` +
    `${colors.gray}${duration}ms${colors.reset}`
  );
};

// REQUEST BODY LOGGER (for debugging)
const bodyLogger = async (c, next) => {
  if (['POST', 'PUT', 'PATCH'].includes(c.req.method)) {
    try {
      // Clone the request to read body without consuming it
      const body = await c.req.json();
      
      // Redact sensitive fields
      const sanitized = { ...body };
      const sensitiveFields = ['password', 'token', 'secret', 'apiKey', 'creditCard'];
      
      for (const field of sensitiveFields) {
        if (sanitized[field]) {
          sanitized[field] = '[REDACTED]';
        }
      }
      
      console.log('Request Body:', JSON.stringify(sanitized, null, 2));
      
      // Store for route handler
      c.set('body', body);
    } catch (e) {
      // Not JSON body, skip
    }
  }
  
  await next();
};

// SLOW REQUEST DETECTOR
const slowRequestLogger = (threshold = 1000) => {
  return async (c, next) => {
    const start = Date.now();
    
    await next();
    
    const duration = Date.now() - start;
    
    if (duration > threshold) {
      console.warn(JSON.stringify({
        type: 'slow_request',
        method: c.req.method,
        path: c.req.path,
        duration: `${duration}ms`,
        threshold: `${threshold}ms`,
        timestamp: new Date().toISOString()
      }));
    }
  };
};

// APPLY LOGGERS

// Use colorful logger in development
if (process.env.NODE_ENV !== 'production') {
  app.use('*', colorLogger);
} else {
  app.use('*', productionLogger);
}

// Log slow requests (> 500ms)
app.use('*', slowRequestLogger(500));

// ROUTES

app.get('/api/fast', (c) => {
  return c.json({ message: 'Fast response' });
});

app.get('/api/slow', async (c) => {
  // Simulate slow operation
  await new Promise(r => setTimeout(r, 600));
  return c.json({ message: 'Slow response' });
});

app.post('/api/data', async (c) => {
  const body = c.get('body') || await c.req.json();
  return c.json({ received: body });
});

export default app;

// LOGGING BEST PRACTICES:
// 1. Use structured JSON logs in production (for log aggregators)
// 2. Include request IDs for distributed tracing
// 3. Redact sensitive data (passwords, tokens, etc.)
// 4. Log both request and response details
// 5. Track timing for performance monitoring
// 6. Use different log levels (info, warn, error)
```
