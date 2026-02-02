function withTimeout(promise, ms) {
  // Use Promise.withResolvers() here
  // Set up a timeout that rejects
  // Race between the timeout and the original promise
}

// Test it:
const slow = new Promise(r => setTimeout(() => r('done'), 5000));
withTimeout(slow, 1000).catch(console.log);  // Should log 'Timeout'