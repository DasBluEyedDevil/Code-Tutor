---
type: "WARNING"
title: "Common Mistakes"
---

**1. Forgetting to call super():**
```javascript
// BAD: Missing super() call
class MyError extends Error {
  constructor(message) {
    this.name = 'MyError'; // ERROR: Must call super first!
    this.message = message;
  }
}

// GOOD: Always call super() first
class MyError extends Error {
  constructor(message) {
    super(message);         // MUST be first!
    this.name = 'MyError';
  }
}
```

**2. Not setting the name property:**
```javascript
// BAD: Name defaults to 'Error'
class MyError extends Error {
  constructor(message) {
    super(message);
    // Forgot to set this.name!
  }
}
new MyError('test').name; // 'Error' - not helpful!

// GOOD: Always set the name
class MyError extends Error {
  constructor(message) {
    super(message);
    this.name = 'MyError';  // Now identifiable!
  }
}
```

**3. Checking instanceof in wrong order:**
```javascript
// BAD: Base class catches everything
if (error instanceof AppError) {
  // This catches ALL app errors!
} else if (error instanceof ValidationError) {
  // Never reached! ValidationError is an AppError
}

// GOOD: Check specific types first
if (error instanceof ValidationError) {
  // Most specific first
} else if (error instanceof AppError) {
  // General fallback
}
```

**4. Throwing strings instead of Error objects:**
```javascript
// BAD: Throwing a string
throw 'Something went wrong';

// GOOD: Always throw Error instances
throw new Error('Something went wrong');
throw new ValidationError('Invalid email', 'email');
```

**5. Not preserving the original error:**
```javascript
// BAD: Losing original error information
try {
  someOperation();
} catch (originalError) {
  throw new AppError('Operation failed');
  // Original error details are lost!
}

// GOOD: Preserve with cause
try {
  someOperation();
} catch (originalError) {
  throw new AppError('Operation failed', {
    cause: originalError
  });
}
```