---
type: "WARNING"
title: "Common Mistakes"
---

**1. Don't rely on error messages for logic:**
```javascript
// BAD: Error messages can change!
try {
  someOperation();
} catch (error) {
  if (error.message === 'Network request failed') {
    // Fragile! Message wording might change
  }
}

// GOOD: Check error type or use custom errors
try {
  someOperation();
} catch (error) {
  if (error instanceof TypeError) {
    // Reliable check based on error type
  }
}
```

**2. Don't ignore the error parameter:**
```javascript
// BAD: Not using the error object
try {
  riskyOperation();
} catch {
  console.log('Something failed'); // What failed? No idea!
}

// GOOD: Always examine the error
try {
  riskyOperation();
} catch (error) {
  console.log(`${error.name}: ${error.message}`);
}
```

**3. Don't assume error structure from external sources:**
```javascript
// BAD: Assuming all errors have standard properties
try {
  await fetch(url);
} catch (error) {
  console.log(error.message); // Might not exist!
}

// GOOD: Safely access error properties
try {
  await fetch(url);
} catch (error) {
  const message = error?.message || 'Unknown error';
  console.log(message);
}
```

**4. Remember: not all thrown values are Error objects:**
```javascript
// JavaScript allows throwing anything!
throw 'just a string';
throw 42;
throw { code: 500 };

// GOOD: Handle non-Error throws safely
try {
  mightThrowAnything();
} catch (error) {
  if (error instanceof Error) {
    console.log(error.message);
  } else {
    console.log('Non-error thrown:', error);
  }
}
```