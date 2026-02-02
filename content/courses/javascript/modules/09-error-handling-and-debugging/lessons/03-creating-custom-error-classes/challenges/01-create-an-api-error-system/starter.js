// Base API error class
class APIError extends Error {
  // YOUR CODE HERE
}

// Bad Request (400)
class BadRequestError extends APIError {
  // YOUR CODE HERE
}

// Unauthorized (401)
class UnauthorizedError extends APIError {
  // YOUR CODE HERE
}

// Not Found (404)
class NotFoundError extends APIError {
  // YOUR CODE HERE
}

// Test your error classes
let badReq = new BadRequestError('Invalid input', 'INVALID_INPUT');
console.log(badReq.toResponse());

let unauth = new UnauthorizedError('Token expired', 'TOKEN_EXPIRED');
console.log(unauth.toResponse());

let notFound = new NotFoundError('User not found', 'USER_NOT_FOUND');
console.log(notFound.toResponse());

// Test instanceof
console.log(badReq instanceof APIError); // should be true
console.log(notFound instanceof Error);  // should be true