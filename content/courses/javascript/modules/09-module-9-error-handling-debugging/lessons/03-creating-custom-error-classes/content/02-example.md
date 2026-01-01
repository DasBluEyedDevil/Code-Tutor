---
type: "EXAMPLE"
title: "Defining Custom Errors"
---

```javascript
// 1. Creating a Basic Custom Error
// We use ES6 classes to extend the built-in Error
class ValidationError extends Error {
    constructor(message) {
        super(message); // Call the parent Error constructor
        this.name = "ValidationError"; // Set the error type name
    }
}

// 2. Custom Error with Extra Data
class NetworkError extends Error {
    constructor(message, statusCode) {
        super(message);
        this.name = "NetworkError";
        this.statusCode = statusCode; // Add custom property
    }
}

// 3. Using Custom Errors
function login(username, password) {
    if (!username || !password) {
        throw new ValidationError("Username and password are required.");
    }
    
    // Simulate server error
    throw new NetworkError("Server unreachable", 503);
}

try {
    login("Alice", "");
} catch (e) {
    if (e instanceof ValidationError) {
        console.warn(`User Mistake: ${e.message}`);
    } else if (e instanceof NetworkError) {
        console.error(`System Error: ${e.message} (Status: ${e.statusCode})`);
    }
}
```