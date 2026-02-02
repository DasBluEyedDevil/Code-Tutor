---
type: "EXAMPLE"
title: "process.on('uncaughtException') (Node.js)"
---

Node.js global handler for synchronous errors that escape all try-catch blocks.

```javascript
// process.on('uncaughtException') - Node.js sync error handler
process.on('uncaughtException', (error, origin) => {
  console.error('=== Uncaught Exception ===');
  console.error('Error:', error.message);
  console.error('Stack:', error.stack);
  console.error('Origin:', origin);
  
  // CRITICAL: Log the error
  logToFile('uncaught-exception', {
    message: error.message,
    stack: error.stack,
    origin: origin,
    timestamp: new Date().toISOString()
  });
  
  // Send to external monitoring
  sendToMonitoring(error);
  
  // IMPORTANT: After an uncaught exception, the process is in an
  // undefined state. You should exit after cleanup!
  
  // Give time for async logging to complete
  setTimeout(() => {
    process.exit(1); // Exit with error code
  }, 1000);
});

// Example of what triggers this:
function causeSyncError() {
  JSON.parse('invalid json'); // Throws SyntaxError
}

// If this runs without try-catch around it:
// causeSyncError(); // Would trigger uncaughtException handler

// WARNING: The Node.js docs say:
// "Note that 'uncaughtException' is a crude mechanism for exception handling
// intended to be used only as a last resort."
//
// After an uncaught exception, your app may be in an inconsistent state.
// The recommended approach is:
// 1. Log the error
// 2. Attempt graceful shutdown
// 3. Exit the process
// 4. Let a process manager (PM2, Docker, etc.) restart the app
```
