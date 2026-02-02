---
type: "EXAMPLE"
title: "Common Error Classes: Validation, NotFound, Authentication"
---

A set of practical custom error classes commonly used in web applications.

```javascript
// ValidationError - for input validation failures
class ValidationError extends Error {
  constructor(message, field = null, value = null) {
    super(message);
    this.name = 'ValidationError';
    this.field = field;     // Which field failed
    this.value = value;     // What value was provided
    this.statusCode = 400;  // Bad Request
  }
}

// NotFoundError - for missing resources
class NotFoundError extends Error {
  constructor(resource, id) {
    super(`${resource} with id '${id}' not found`);
    this.name = 'NotFoundError';
    this.resource = resource;
    this.resourceId = id;
    this.statusCode = 404;  // Not Found
  }
}

// AuthenticationError - for auth failures
class AuthenticationError extends Error {
  constructor(message = 'Authentication required') {
    super(message);
    this.name = 'AuthenticationError';
    this.statusCode = 401;  // Unauthorized
  }
}

// AuthorizationError - for permission failures
class AuthorizationError extends Error {
  constructor(action, resource) {
    super(`Not authorized to ${action} ${resource}`);
    this.name = 'AuthorizationError';
    this.action = action;
    this.resource = resource;
    this.statusCode = 403;  // Forbidden
  }
}

// Using these in a real scenario
function updateUserProfile(userId, profileData, currentUser) {
  // Check authentication
  if (!currentUser) {
    throw new AuthenticationError('Please log in to update profile');
  }
  
  // Check authorization
  if (currentUser.id !== userId && !currentUser.isAdmin) {
    throw new AuthorizationError('update', 'user profile');
  }
  
  // Validate input
  if (!profileData.email || !profileData.email.includes('@')) {
    throw new ValidationError(
      'Invalid email format',
      'email',
      profileData.email
    );
  }
  
  // Find user (might not exist)
  const user = findUserById(userId);
  if (!user) {
    throw new NotFoundError('User', userId);
  }
  
  // Update the profile...
  return { success: true };
}

// Handling different error types
try {
  updateUserProfile(123, { email: 'invalid' }, null);
} catch (error) {
  if (error instanceof AuthenticationError) {
    console.log('Please log in:', error.message);
  } else if (error instanceof AuthorizationError) {
    console.log('Permission denied:', error.message);
  } else if (error instanceof ValidationError) {
    console.log(`Invalid ${error.field}:`, error.message);
  } else if (error instanceof NotFoundError) {
    console.log('Resource not found:', error.message);
  } else {
    console.log('Unexpected error:', error.message);
  }
}
```
