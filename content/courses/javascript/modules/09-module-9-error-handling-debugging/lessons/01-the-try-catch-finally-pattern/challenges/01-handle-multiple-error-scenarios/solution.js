function safeParseConfig(jsonString) {
  const defaultConfig = { name: 'Unknown', version: '0.0.0' };
  
  try {
    const config = JSON.parse(jsonString);
    
    if (!config.name || !config.version) {
      console.log('Missing required fields');
      return defaultConfig;
    }
    
    return config;
  } catch (error) {
    console.log('Invalid JSON format');
    return defaultConfig;
  } finally {
    console.log('Parse attempt complete');
  }
}

// Test cases
console.log(safeParseConfig('{"name": "MyApp", "version": "1.0.0"}'));
// Output: Parse attempt complete, { name: 'MyApp', version: '1.0.0' }

console.log(safeParseConfig('not valid json'));
// Output: Invalid JSON format, Parse attempt complete, { name: 'Unknown', version: '0.0.0' }

console.log(safeParseConfig('{"name": "MyApp"}'));
// Output: Missing required fields, Parse attempt complete, { name: 'Unknown', version: '0.0.0' }