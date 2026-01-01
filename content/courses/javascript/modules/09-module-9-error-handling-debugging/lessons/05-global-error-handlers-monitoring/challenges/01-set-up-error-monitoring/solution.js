const errorLog = [];

function setupErrorHandlers() {
  // Handle synchronous errors
  window.onerror = function(message, source, lineno, colno, error) {
    errorLog.push({
      timestamp: new Date().toISOString(),
      type: 'uncaught_exception',
      message: message,
      stack: error?.stack || `at ${source}:${lineno}:${colno}`,
      source: source,
      line: lineno,
      column: colno
    });
    
    console.error('[Error Monitor] Sync error caught:', message);
    return false; // Still show in console
  };
  
  // Handle asynchronous (promise) errors
  window.onunhandledrejection = function(event) {
    const reason = event.reason;
    
    errorLog.push({
      timestamp: new Date().toISOString(),
      type: 'unhandled_rejection',
      message: reason?.message || String(reason),
      stack: reason?.stack || 'No stack available'
    });
    
    console.error('[Error Monitor] Async error caught:', reason?.message || reason);
  };
  
  console.log('[Error Monitor] Global error handlers installed');
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
// Output: ['[uncaught_exception] Sync test error', '[unhandled_rejection] Async test error']