---
type: "THEORY"
title: "Control Flow with try-catch-finally"
---

Understanding exactly how control flows through try-catch-finally blocks is crucial for writing robust error handling code. Here's the complete breakdown:

**Execution Order:**

1. **try block starts** - Code executes line by line
2. **If NO error occurs:**
   - try block completes fully
   - catch block is SKIPPED entirely
   - finally block runs
   - Execution continues after the try-catch-finally

3. **If an error DOES occur:**
   - try block stops immediately at the error line
   - Remaining try block code is SKIPPED
   - catch block runs with the error object
   - finally block runs
   - Execution continues after the try-catch-finally

**Key Rules:**

```javascript
// Rule 1: finally runs even after return
function test() {
  try {
    return 'from try';
  } finally {
    console.log('finally still runs!');
  }
}

// Rule 2: finally runs even after throw
function test2() {
  try {
    throw new Error('oops');
  } finally {
    console.log('finally still runs!');
  }
}

// Rule 3: return in finally overrides try/catch return
function test3() {
  try {
    return 'try';
  } finally {
    return 'finally'; // This wins!
  }
}
console.log(test3()); // Output: 'finally'

// Rule 4: You can have try-finally without catch
try {
  riskyOperation();
} finally {
  cleanup(); // Always runs, errors still propagate
}
```

**Common Patterns:**
- `try-catch`: Handle errors and continue
- `try-finally`: Ensure cleanup, let errors propagate
- `try-catch-finally`: Handle errors AND ensure cleanup