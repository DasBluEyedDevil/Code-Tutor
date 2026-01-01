---
type: "WARNING"
title: "Common Mistakes"
---

**1. Using global handlers as primary error handling:**
```javascript
// BAD: Relying on global handler instead of local try-catch
process.on('uncaughtException', (error) => {
  if (error.message.includes('validation')) {
    // Trying to do business logic in global handler!
  }
});

// GOOD: Handle validation locally
try {
  validateInput(data);
} catch (validationError) {
  return { error: validationError.message };
}
```

**2. Not exiting after uncaughtException:**
```javascript
// BAD: Continuing after uncaught exception
process.on('uncaughtException', (error) => {
  console.log('Error:', error);
  // App continues in potentially corrupted state!
});

// GOOD: Log and exit
process.on('uncaughtException', (error) => {
  console.error('Fatal error:', error);
  logSync(error); // Use sync logging
  process.exit(1); // Exit!
});
```

**3. Swallowing errors in global handlers:**
```javascript
// BAD: Hiding all errors
window.onerror = function() {
  return true; // Suppresses all error output
};

// GOOD: Log then optionally suppress
window.onerror = function(msg, url, line, col, error) {
  sendToMonitoring({ msg, url, line, error });
  return false; // Still show in console
};
```

**4. Async operations in sync error handlers:**
```javascript
// BAD: Async logging might not complete
process.on('uncaughtException', async (error) => {
  await sendToServer(error); // Might not complete!
  process.exit(1);
});

// GOOD: Use sync logging or wait properly
process.on('uncaughtException', (error) => {
  // Sync logging for critical errors
  fs.writeFileSync('error.log', error.stack);
  
  // Or give async time to complete
  sendToServer(error).finally(() => {
    process.exit(1);
  });
});
```

**5. Forgetting to set up handlers early:**
```javascript
// BAD: Handler registered after app starts
startServer();
// Error happens here before handler is set up!
process.on('uncaughtException', handler);

// GOOD: Set up handlers first
process.on('uncaughtException', handler);
process.on('unhandledRejection', handler);
startServer(); // Now handlers catch startup errors too
```