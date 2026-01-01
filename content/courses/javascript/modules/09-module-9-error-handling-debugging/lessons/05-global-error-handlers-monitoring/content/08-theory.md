---
type: "THEORY"
title: "When to Use Global Handlers vs Local try-catch"
---

Understanding when to use each type of error handling:

**Use Local try-catch when:**
- You can meaningfully recover from the error
- You need to handle the error differently based on context
- You want to provide fallback behavior
- You're calling code that you know might throw

```javascript
try {
  const user = await fetchUser(id);
  return user;
} catch (error) {
  // Recover by returning cached user
  return getCachedUser(id);
}
```

**Use Global Handlers when:**
- As a safety net for errors that escape local handling
- For logging and monitoring all unhandled errors
- For graceful shutdown on fatal errors
- For sending errors to external monitoring services

```javascript
process.on('unhandledRejection', (reason) => {
  // Log it - this shouldn't happen in well-written code
  logger.error('Unhandled rejection', { reason });
  // Maybe alert on-call engineer
  alertOnCall('Unhandled rejection detected');
});
```

**The Error Handling Hierarchy:**

1. **First Line: Local try-catch**
   - Handle errors where they occur
   - Recover or transform errors
   - Most errors should be caught here

2. **Second Line: Function-level handlers**
   - Catch errors from called functions
   - Add context before re-throwing
   - Convert errors to API responses

3. **Third Line: Framework/middleware handlers**
   - Express error middleware
   - React error boundaries
   - Centralized API error formatting

4. **Last Line: Global handlers**
   - Safety net for escaped errors
   - Logging and monitoring
   - Graceful shutdown