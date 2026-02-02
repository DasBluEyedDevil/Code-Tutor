const errorLog = [];

function setupErrorHandlers() {
  // YOUR CODE HERE
  // 1. Set up window.onerror for sync errors
  // 2. Set up window.onunhandledrejection for async errors
  // 3. Log errors to errorLog array with: timestamp, type, message, stack
}

// Helper to view logged errors
function getErrorSummary() {
  return errorLog.map(e => `[${e.type}] ${e.message}`);
}

// Set up the handlers
setupErrorHandlers();

// Test functions (uncomment to test)
// function testSyncError() {
//   throw new Error('Sync test error');
// }

// function testAsyncError() {
//   Promise.reject(new Error('Async test error'));
// }

// After triggering errors:
// console.log('Logged errors:', getErrorSummary());