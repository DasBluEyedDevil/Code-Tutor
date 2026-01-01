---
type: "EXAMPLE"
title: "Setting up Global Listeners"
---

```javascript
// 1. In the Browser: window.onerror
// Catches standard synchronous runtime errors
window.onerror = function(message, source, lineno, colno, error) {
    console.log("--- BROWSER GLOBAL ERROR ---");
    console.error(`Message: ${message}`);
    // Here, you would send this to a service like Sentry or LogRocket
    return true; // Prevents the error from showing in the dev console
};

// 2. In the Browser: unhandledrejection
// Catches async errors (Promises) that don't have a .catch()
window.addEventListener('unhandledrejection', (event) => {
    console.warn("--- UNHANDLED PROMISE ---");
    console.warn(`Reason: ${event.reason}`);
});

// 3. In Node.js: uncaughtException
process.on('uncaughtException', (err) => {
    console.error("--- NODE CRITICAL ERROR ---");
    console.error(err.stack);
    // CRITICAL: Always restart the server after an uncaught exception!
    process.exit(1); 
});

// 4. In Node.js: unhandledRejection
process.on('unhandledRejection', (reason, promise) => {
    console.log('Unhandled Rejection at:', promise, 'reason:', reason);
    // Application-specific logging here
});
```