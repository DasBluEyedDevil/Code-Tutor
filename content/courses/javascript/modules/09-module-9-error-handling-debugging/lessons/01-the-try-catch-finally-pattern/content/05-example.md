---
type: "EXAMPLE"
title: "Re-throwing Errors"
---

Sometimes you want to catch an error, do something with it (like logging), and then throw it again so code higher up can also handle it.

```javascript
// Re-throwing errors for logging then propagating
function processOrder(orderId) {
  try {
    let order = fetchOrder(orderId);
    calculateTotal(order);
    return order;
  } catch (error) {
    // Log the error for debugging
    console.error('Error processing order', orderId, ':', error.message);
    
    // Re-throw so the caller knows something went wrong
    throw error;
  }
}

// Selective re-throwing based on error type
function handleRequest(request) {
  try {
    return processRequest(request);
  } catch (error) {
    if (error.message.includes('validation')) {
      // Validation errors: handle locally, don't re-throw
      console.log('Validation issue:', error.message);
      return { success: false, error: 'Invalid input' };
    } else {
      // Other errors: log and re-throw for caller to handle
      console.error('Unexpected error:', error.message);
      throw error;
    }
  }
}

// Adding context when re-throwing
function loadUserProfile(userId) {
  try {
    let profile = fetchProfile(userId);
    return profile;
  } catch (error) {
    // Add context to the error before re-throwing
    error.message = `Failed to load profile for user ${userId}: ${error.message}`;
    throw error;
  }
}

// Modern approach: Error cause chaining (ES2022)
function loadUserWithCause(userId) {
  try {
    return fetchProfile(userId);
  } catch (error) {
    // Create a new error that links to the original
    throw new Error(`Could not load user ${userId}`, { cause: error });
  }
}
```
