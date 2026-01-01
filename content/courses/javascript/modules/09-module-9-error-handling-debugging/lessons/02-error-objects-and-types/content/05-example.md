---
type: "EXAMPLE"
title: "Inspecting and Logging Error Stacks"
---

Stack traces are invaluable for debugging. Here's how to effectively use and log them.

```javascript
// Understanding and using stack traces

function level3() {
  throw new Error('Something broke in level 3');
}

function level2() {
  level3();
}

function level1() {
  level2();
}

try {
  level1();
} catch (error) {
  console.log('=== Full Stack Trace ===');
  console.log(error.stack);
  // Output shows the call path:
  // Error: Something broke in level 3
  //     at level3 (script.js:4:9)
  //     at level2 (script.js:8:3)
  //     at level1 (script.js:12:3)
  //     at script.js:16:3
}

// Formatting error for logging
function formatErrorForLogging(error) {
  return {
    timestamp: new Date().toISOString(),
    name: error.name,
    message: error.message,
    stack: error.stack?.split('\n').slice(0, 5), // First 5 lines
    cause: error.cause ? formatErrorForLogging(error.cause) : undefined
  };
}

try {
  level1();
} catch (error) {
  const logEntry = formatErrorForLogging(error);
  console.log(JSON.stringify(logEntry, null, 2));
}

// Production-ready error logging
function logError(error, context = {}) {
  const errorInfo = {
    timestamp: new Date().toISOString(),
    error: {
      name: error.name,
      message: error.message,
      stack: error.stack
    },
    context: context,
    // Add environment info
    environment: typeof process !== 'undefined' ? 'node' : 'browser'
  };
  
  // In production, you'd send this to a logging service
  console.error('[ERROR]', JSON.stringify(errorInfo));
  
  // Could also send to external service:
  // sendToLogService(errorInfo);
}

try {
  level1();
} catch (error) {
  logError(error, {
    userId: 123,
    action: 'processing data',
    inputSize: 1000
  });
}
```
