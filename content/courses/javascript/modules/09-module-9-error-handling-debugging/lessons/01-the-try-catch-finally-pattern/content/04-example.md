---
type: "EXAMPLE"
title: "Nested try-catch Patterns"
---

Sometimes you need different error handling strategies for different parts of your code. Nested try-catch blocks let you handle errors at multiple levels.

```javascript
// Nested try-catch for granular error handling
function processUserData(userId) {
  try {
    console.log('Processing user:', userId);
    
    // Outer try handles general errors
    let user = null;
    
    try {
      // Inner try handles specifically the fetch
      user = fetchUserFromDatabase(userId);
    } catch (fetchError) {
      console.log('Database unavailable, trying cache...');
      user = fetchUserFromCache(userId);
    }
    
    try {
      // Another inner try handles email validation
      validateEmail(user.email);
    } catch (validationError) {
      console.log('Invalid email, using default...');
      user.email = 'default@example.com';
    }
    
    return user;
    
  } catch (error) {
    // Outer catch handles anything that wasn't caught inside
    console.log('Complete failure:', error.message);
    return null;
  }
}

// Practical example: Multi-step process
function createOrder(orderData) {
  let orderId = null;
  
  try {
    // Step 1: Validate order
    try {
      validateOrderData(orderData);
      console.log('Order validated');
    } catch (validationError) {
      throw new Error('Invalid order: ' + validationError.message);
    }
    
    // Step 2: Process payment
    try {
      processPayment(orderData.payment);
      console.log('Payment processed');
    } catch (paymentError) {
      throw new Error('Payment failed: ' + paymentError.message);
    }
    
    // Step 3: Create order record
    try {
      orderId = saveOrder(orderData);
      console.log('Order saved:', orderId);
    } catch (dbError) {
      // Payment was processed, so we need to refund!
      refundPayment(orderData.payment);
      throw new Error('Order save failed, payment refunded');
    }
    
    return orderId;
    
  } catch (error) {
    console.log('Order creation failed:', error.message);
    return null;
  }
}
```
