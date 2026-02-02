// Simulated Zod error object structure
const mockZodError = {
  errors: [
    { path: ['name'], message: 'Name is required', code: 'too_small' },
    { path: ['name'], message: 'Name must be string', code: 'invalid_type' },
    { path: ['email'], message: 'Invalid email format', code: 'invalid_string' },
    { path: ['address', 'city'], message: 'City is required', code: 'too_small' },
    { path: ['address', 'zipCode'], message: 'Invalid ZIP code', code: 'invalid_string' },
    { path: ['tags', 0], message: 'Tag must be string', code: 'invalid_type' }
  ]
};

// TODO: Create formatZodErrors function
function formatZodErrors(zodError) {
  // Return object like: { 'name': 'Name is required', 'address.city': 'City is required' }
}

// TODO: Create ApiErrorResponse class
class ApiErrorResponse {
  constructor(type, message, details) {
    // Initialize fields
  }
  
  static fromZodError(zodError) {
    // Create ApiErrorResponse from Zod error
  }
  
  toJSON() {
    // Return clean JSON structure
  }
}

// Test your implementation
console.log('=== formatZodErrors ===');
const formatted = formatZodErrors(mockZodError);
console.log(formatted);

console.log('\n=== ApiErrorResponse ===');
const apiError = ApiErrorResponse.fromZodError(mockZodError);
console.log(JSON.stringify(apiError.toJSON(), null, 2));
