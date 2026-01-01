---
type: "EXAMPLE"
title: "window.onerror (Browser)"
---

The classic browser global error handler for synchronous errors.

```javascript
// window.onerror - catches uncaught synchronous errors
window.onerror = function(message, source, lineno, colno, error) {
  console.log('=== Global Error Handler ===');
  console.log('Message:', message);
  console.log('Source:', source);    // Script URL
  console.log('Line:', lineno);       // Line number
  console.log('Column:', colno);      // Column number
  console.log('Error object:', error); // Full error object
  
  // Send to your error tracking service
  sendToErrorService({
    type: 'uncaught_error',
    message: message,
    source: source,
    line: lineno,
    column: colno,
    stack: error?.stack
  });
  
  // Return true to prevent default browser error logging
  // Return false (or nothing) to still log to console
  return false;
};

// Using addEventListener (modern approach)
window.addEventListener('error', function(event) {
  console.log('Error event:', event.error);
  console.log('Error message:', event.message);
  console.log('Filename:', event.filename);
  console.log('Line:', event.lineno);
  
  // Can prevent default error logging
  // event.preventDefault();
});

// Testing it
function causeError() {
  // This error will be caught by window.onerror
  throw new Error('Uncaught error in function');
}

// causeError(); // Uncomment to test

// Note: window.onerror does NOT catch:
// - Promise rejections (use onunhandledrejection)
// - Errors in async code without await
// - Syntax errors (script won't run at all)
```
