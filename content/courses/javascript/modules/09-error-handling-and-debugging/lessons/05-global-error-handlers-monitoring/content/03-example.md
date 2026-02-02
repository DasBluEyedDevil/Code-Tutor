---
type: "EXAMPLE"
title: "window.onunhandledrejection (Browser)"
---

Catches unhandled promise rejections in the browser - essential for async error monitoring.

```javascript
// window.onunhandledrejection - catches unhandled promise rejections
window.onunhandledrejection = function(event) {
  console.log('=== Unhandled Promise Rejection ===');
  console.log('Reason:', event.reason);  // The rejection value
  console.log('Promise:', event.promise); // The promise that rejected
  
  // If reason is an Error, we can get the stack
  if (event.reason instanceof Error) {
    console.log('Stack:', event.reason.stack);
  }
  
  // Send to error tracking
  sendToErrorService({
    type: 'unhandled_rejection',
    message: event.reason?.message || String(event.reason),
    stack: event.reason?.stack
  });
  
  // Prevent default browser warning
  event.preventDefault();
};

// Using addEventListener (preferred modern approach)
window.addEventListener('unhandledrejection', function(event) {
  console.error('Unhandled rejection:', event.reason);
  
  // Log it, send to monitoring, etc.
  logToMonitoringService(event.reason);
});

// Also useful: rejectionhandled event
// Fires if a previously unhandled rejection is later handled
window.addEventListener('rejectionhandled', function(event) {
  console.log('Rejection was handled later:', event.promise);
});

// Test cases that trigger unhandledrejection:

// 1. Promise rejection without catch
// Promise.reject(new Error('Rejected!')); // Triggers handler

// 2. Error in async function without try-catch
// async function test() { throw new Error('Async error'); }
// test(); // Triggers handler

// 3. Error in .then() without .catch()
// Promise.resolve().then(() => { throw new Error('In then'); }); // Triggers handler
```
