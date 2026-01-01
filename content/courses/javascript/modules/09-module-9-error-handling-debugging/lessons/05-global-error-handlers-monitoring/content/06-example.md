---
type: "EXAMPLE"
title: "Graceful Shutdown Patterns"
---

When a fatal error occurs, gracefully shutting down prevents data loss and corruption.

```javascript
// Graceful shutdown pattern for Node.js servers
let isShuttingDown = false;

async function gracefulShutdown(code = 0) {
  if (isShuttingDown) {
    console.log('Shutdown already in progress...');
    return;
  }
  
  isShuttingDown = true;
  console.log('Starting graceful shutdown...');
  
  try {
    // 1. Stop accepting new requests
    console.log('Closing HTTP server...');
    await new Promise((resolve) => {
      server.close(resolve);
    });
    
    // 2. Wait for existing requests to complete (with timeout)
    console.log('Waiting for requests to complete...');
    await waitForRequests(10000); // 10 second timeout
    
    // 3. Close database connections
    console.log('Closing database connections...');
    await database.close();
    
    // 4. Close other resources (queues, caches, etc.)
    console.log('Closing message queue...');
    await messageQueue.close();
    
    // 5. Flush any buffered logs
    console.log('Flushing logs...');
    await flushLogs();
    
    console.log('Graceful shutdown complete');
    process.exit(code);
    
  } catch (error) {
    console.error('Error during shutdown:', error);
    process.exit(1);
  }
}

// Set up signal handlers for controlled shutdown
process.on('SIGTERM', () => {
  console.log('Received SIGTERM');
  gracefulShutdown(0);
});

process.on('SIGINT', () => {
  console.log('Received SIGINT (Ctrl+C)');
  gracefulShutdown(0);
});

// Handle uncaught errors with graceful shutdown
process.on('uncaughtException', async (error) => {
  console.error('Uncaught exception:', error);
  await logError(error);
  await gracefulShutdown(1);
});

process.on('unhandledRejection', async (reason) => {
  console.error('Unhandled rejection:', reason);
  await logError(reason);
  await gracefulShutdown(1);
});

// Timeout safety - force exit if graceful shutdown takes too long
function forceExitTimeout(ms = 30000) {
  setTimeout(() => {
    console.error('Graceful shutdown timeout, forcing exit');
    process.exit(1);
  }, ms).unref(); // unref() prevents this timer from keeping the process alive
}
```
