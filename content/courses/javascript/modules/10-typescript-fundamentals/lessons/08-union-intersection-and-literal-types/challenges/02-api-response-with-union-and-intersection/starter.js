// 1. Define Success type: { ok: true, data: T }
interface Success<T> {
  // your code
}

// 2. Define Error type: { ok: false, error: string, code: number }
interface ApiError {
  // your code
}

// 3. Create union type for ApiResponse
type ApiResponse<T> = // your code

// 4. Add metadata using intersection
type Metadata = {
  timestamp: Date;
  requestId: string;
};

type TimestampedApiResponse<T> = // your code

// 5. Create a handler function
function handleResponse<T>(response: TimestampedApiResponse<T>): void {
  console.log(`Request ${response.requestId} at ${response.timestamp.toISOString()}`);
  
  // Use type narrowing to handle success/error
  // your code
}

// Test with success
const successResponse: TimestampedApiResponse<{ name: string }> = {
  ok: true,
  data: { name: 'Alice' },
  timestamp: new Date(),
  requestId: 'req-001'
};

// Test with error
const errorResponse: TimestampedApiResponse<never> = {
  ok: false,
  error: 'Not found',
  code: 404,
  timestamp: new Date(),
  requestId: 'req-002'
};

handleResponse(successResponse);
handleResponse(errorResponse);