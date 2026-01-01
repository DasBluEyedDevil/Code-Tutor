---
type: "EXAMPLE"
title: "Error Properties (message, name, stack, cause)"
---

Every Error object has properties that help you understand what went wrong, where, and why.

```javascript
// Creating an error to examine its properties
function demonstrateErrorProperties() {
  try {
    throw new Error('Something went wrong!');
  } catch (error) {
    // 1. name - The type of error
    console.log('Name:', error.name);
    // Output: Name: Error
    
    // 2. message - Human-readable description
    console.log('Message:', error.message);
    // Output: Message: Something went wrong!
    
    // 3. stack - Full stack trace (where the error occurred)
    console.log('Stack trace:');
    console.log(error.stack);
    // Output: Error: Something went wrong!
    //     at demonstrateErrorProperties (file.js:3:11)
    //     at main (file.js:20:5)
    //     at file.js:25:1
  }
}

demonstrateErrorProperties();

// Real example: parsing stack trace info
function getUserData(userId) {
  if (userId < 0) {
    throw new Error('User ID must be positive');
  }
  return { id: userId, name: 'User' };
}

function displayUser(userId) {
  let user = getUserData(userId);
  console.log('User:', user.name);
}

try {
  displayUser(-1);
} catch (error) {
  console.log('Error name:', error.name);
  console.log('Error message:', error.message);
  console.log('\nFull stack trace shows the call path:');
  console.log(error.stack);
  // Stack shows: getUserData -> displayUser -> (your code)
}

// 4. cause - The original error that caused this one (ES2022)
function fetchData() {
  throw new Error('Network timeout');
}

function loadUserProfile(userId) {
  try {
    return fetchData();
  } catch (originalError) {
    // Create a new error with 'cause' linking to the original
    throw new Error(`Failed to load user ${userId}`, {
      cause: originalError
    });
  }
}

try {
  loadUserProfile(123);
} catch (error) {
  console.log('Error:', error.message);
  // Output: Error: Failed to load user 123
  
  console.log('Caused by:', error.cause?.message);
  // Output: Caused by: Network timeout
  
  // You can chain causes for deep error tracking!
}
```
