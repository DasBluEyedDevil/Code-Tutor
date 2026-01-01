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

// Format Zod errors into clean field -> message mapping
function formatZodErrors(zodError) {
  const formatted = {};
  
  for (const error of zodError.errors) {
    // Convert path array to dot notation string
    const fieldPath = error.path.join('.');
    
    // Only keep first error per field
    if (!formatted[fieldPath]) {
      formatted[fieldPath] = error.message;
    }
  }
  
  return formatted;
}

// API Error Response class for consistent error structure
class ApiErrorResponse {
  constructor(type, message, details = []) {
    this.success = false;
    this.error = {
      type: type,
      message: message,
      details: details
    };
  }
  
  static fromZodError(zodError) {
    // Group errors by field, keeping only first error per field
    const seenPaths = new Set();
    const details = [];
    
    for (const err of zodError.errors) {
      const fieldPath = err.path.join('.');
      
      if (!seenPaths.has(fieldPath)) {
        seenPaths.add(fieldPath);
        details.push({
          field: fieldPath,
          message: err.message,
          code: err.code
        });
      }
    }
    
    // Create descriptive message
    const fieldCount = details.length;
    const message = fieldCount === 1 
      ? `Validation failed for field: ${details[0].field}`
      : `Validation failed for ${fieldCount} fields`;
    
    return new ApiErrorResponse('VALIDATION_ERROR', message, details);
  }
  
  toJSON() {
    return {
      success: this.success,
      error: this.error
    };
  }
}

// Test formatZodErrors
console.log('=== formatZodErrors ===');
const formatted = formatZodErrors(mockZodError);
console.log(formatted);
// Expected: { name: 'Name is required', email: 'Invalid email format', ... }

console.log('\n=== ApiErrorResponse ===');
const apiError = ApiErrorResponse.fromZodError(mockZodError);
console.log(JSON.stringify(apiError.toJSON(), null, 2));

// Verify it works correctly
console.log('\n=== Verification ===');
console.log('Number of unique fields:', Object.keys(formatted).length);
console.log('Has nested path (address.city):', 'address.city' in formatted);
console.log('First name error only:', formatted.name === 'Name is required');