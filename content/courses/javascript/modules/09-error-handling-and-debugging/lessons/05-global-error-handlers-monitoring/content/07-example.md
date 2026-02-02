---
type: "EXAMPLE"
title: "Error Logging and Monitoring Patterns"
---

Professional error logging patterns for production applications.

```javascript
// Error logging utility for production
class ErrorLogger {
  constructor(config = {}) {
    this.serviceName = config.serviceName || 'app';
    this.environment = config.environment || 'development';
    this.externalService = config.externalService || null;
  }
  
  formatError(error, context = {}) {
    return {
      timestamp: new Date().toISOString(),
      service: this.serviceName,
      environment: this.environment,
      error: {
        name: error.name,
        message: error.message,
        stack: error.stack,
        cause: error.cause ? this.formatError(error.cause) : undefined
      },
      context: context,
      // Add useful debugging info
      process: {
        pid: process.pid,
        uptime: process.uptime(),
        memoryUsage: process.memoryUsage()
      }
    };
  }
  
  log(error, context = {}) {
    const formatted = this.formatError(error, context);
    
    // Always log to console in development
    if (this.environment === 'development') {
      console.error('\n=== ERROR ===');
      console.error(JSON.stringify(formatted, null, 2));
    } else {
      // In production, log as single JSON line for log aggregators
      console.error(JSON.stringify(formatted));
    }
    
    // Send to external service (Sentry, DataDog, etc.)
    if (this.externalService) {
      this.sendToExternalService(formatted);
    }
    
    return formatted;
  }
  
  async sendToExternalService(errorData) {
    // Example: Send to error tracking API
    try {
      await fetch('https://errors.example.com/api/log', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(errorData)
      });
    } catch (sendError) {
      // Don't let logging errors crash the app!
      console.error('Failed to send error to external service:', sendError);
    }
  }
}

// Usage
const logger = new ErrorLogger({
  serviceName: 'user-api',
  environment: process.env.NODE_ENV || 'development'
});

// In your error handlers
try {
  await processOrder(order);
} catch (error) {
  logger.log(error, {
    orderId: order.id,
    userId: order.userId,
    action: 'processOrder'
  });
  throw error; // Re-throw if needed
}
```
