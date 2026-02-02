// Base API error class
class APIError extends Error {
  constructor(message, statusCode = 500, errorCode = 'INTERNAL_ERROR') {
    super(message);
    this.name = 'APIError';
    this.statusCode = statusCode;
    this.errorCode = errorCode;
  }
  
  toResponse() {
    return {
      status: this.statusCode,
      body: {
        error: this.errorCode,
        message: this.message
      }
    };
  }
}

// Bad Request (400)
class BadRequestError extends APIError {
  constructor(message, errorCode = 'BAD_REQUEST') {
    super(message, 400, errorCode);
    this.name = 'BadRequestError';
  }
}

// Unauthorized (401)
class UnauthorizedError extends APIError {
  constructor(message = 'Unauthorized', errorCode = 'UNAUTHORIZED') {
    super(message, 401, errorCode);
    this.name = 'UnauthorizedError';
  }
}

// Not Found (404)
class NotFoundError extends APIError {
  constructor(message = 'Resource not found', errorCode = 'NOT_FOUND') {
    super(message, 404, errorCode);
    this.name = 'NotFoundError';
  }
}

// Test your error classes
let badReq = new BadRequestError('Invalid input', 'INVALID_INPUT');
console.log(badReq.toResponse());
// { status: 400, body: { error: 'INVALID_INPUT', message: 'Invalid input' } }

let unauth = new UnauthorizedError('Token expired', 'TOKEN_EXPIRED');
console.log(unauth.toResponse());
// { status: 401, body: { error: 'TOKEN_EXPIRED', message: 'Token expired' } }

let notFound = new NotFoundError('User not found', 'USER_NOT_FOUND');
console.log(notFound.toResponse());
// { status: 404, body: { error: 'USER_NOT_FOUND', message: 'User not found' } }

// Test instanceof
console.log(badReq instanceof APIError); // true
console.log(notFound instanceof Error);  // true