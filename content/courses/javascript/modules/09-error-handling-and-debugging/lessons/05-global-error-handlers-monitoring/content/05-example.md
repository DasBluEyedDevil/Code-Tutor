---
type: "EXAMPLE"
title: "process.on('unhandledRejection') (Node.js)"
---

Node.js global handler for promise rejections that aren't caught.

```javascript
// process.on('unhandledRejection') - Node.js async error handler
process.on('unhandledRejection', (reason, promise) => {
  console.error('=== Unhandled Rejection ===');
  console.error('Reason:', reason);
  console.error('Promise:', promise);
  
  // If reason is an Error, log the stack
  if (reason instanceof Error) {
    console.error('Stack:', reason.stack);
  }
  
  // Log to file and monitoring
  logToFile('unhandled-rejection', {
    message: reason?.message || String(reason),
    stack: reason?.stack,
    timestamp: new Date().toISOString()
  });
  
  // In Node.js 15+, unhandled rejections crash the process by default
  // In earlier versions, they just warn
  // You can throw to convert to uncaughtException:
  // throw reason;
});

// Also track when rejections are later handled
process.on('rejectionHandled', (promise) => {
  console.log('A rejection was handled late:', promise);
});

// Setting up both handlers together (common pattern)
function setupGlobalErrorHandlers() {
  process.on('uncaughtException', (error, origin) => {
    console.error('Uncaught exception:', error.message);
    logError('uncaughtException', error);
    gracefulShutdown(1);
  });
  
  process.on('unhandledRejection', (reason, promise) => {
    console.error('Unhandled rejection:', reason);
    logError('unhandledRejection', reason);
    // In Node 15+, this will crash. Handle or let it crash.
  });
  
  // Optional: Handle warning events
  process.on('warning', (warning) => {
    console.warn('Warning:', warning.name, warning.message);
  });
}

setupGlobalErrorHandlers();
```
