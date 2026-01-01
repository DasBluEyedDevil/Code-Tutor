---
type: "WARNING"
title: "Common Mistakes"
---

Avoid these common error handling anti-patterns that can make debugging harder and hide real problems:

**1. Empty catch blocks (Error swallowing):**
```javascript
// BAD: Silently ignoring errors
try {
  doSomethingRisky();
} catch (error) {
  // Error is completely lost!
}

// GOOD: At least log it
try {
  doSomethingRisky();
} catch (error) {
  console.error('Operation failed:', error);
}
```

**2. Catching too broadly:**
```javascript
// BAD: Catching all errors the same way
try {
  validateInput();
  processData();
  saveToDatabase();
} catch (error) {
  console.log('Something failed'); // Which step? No idea!
}

// GOOD: Handle specific scenarios
try {
  validateInput();
} catch (error) {
  console.log('Invalid input:', error.message);
  return;
}
// Now we know validation passed...
```

**3. Using try-catch for control flow:**
```javascript
// BAD: Using exceptions for normal logic
function isNumber(value) {
  try {
    parseInt(value);
    return true;
  } catch {
    return false; // parseInt doesn't throw for invalid input!
  }
}

// GOOD: Check conditions normally
function isNumber(value) {
  return !isNaN(parseFloat(value));
}
```

**4. Forgetting async error handling:**
```javascript
// BAD: try-catch doesn't catch async errors this way!
try {
  setTimeout(() => {
    throw new Error('This escapes!');
  }, 100);
} catch (error) {
  console.log('Never runs');
}

// We'll cover async error handling in Lesson 9.4
```