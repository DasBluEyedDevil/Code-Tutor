function safeParseConfig(jsonString) {
  const defaultConfig = { name: 'Unknown', version: '0.0.0' };
  
  // YOUR CODE HERE
  // Use try-catch-finally to:
  // 1. Parse the JSON
  // 2. Validate it has 'name' and 'version' properties
  // 3. Return the parsed config or defaultConfig
  // 4. Always log 'Parse attempt complete' in finally
}

// Test cases
console.log(safeParseConfig('{"name": "MyApp", "version": "1.0.0"}'));
console.log(safeParseConfig('not valid json'));
console.log(safeParseConfig('{"name": "MyApp"}'));