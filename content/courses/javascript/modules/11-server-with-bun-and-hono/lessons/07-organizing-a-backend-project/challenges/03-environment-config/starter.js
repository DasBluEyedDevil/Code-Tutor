// Simulated environment (in real code, use process.env)
let mockEnv = {
  NODE_ENV: 'development',
  PORT: '3000',
  DATABASE_URL: 'postgresql://localhost:5432/mydb',
  JWT_SECRET: 'my-super-secret-key-for-jwt-tokens',
  // SMTP_HOST is intentionally missing to test optional values
};

// TODO: Create configuration schema and loader

// Define expected environment variables
const configSchema = {
  // Define each expected variable with:
  // - type ('string', 'number', 'boolean')
  // - required (true/false)
  // - default (optional default value)
};

// Validate and load configuration
function loadConfig(env) {
  // 1. Validate all required variables exist
  // 2. Coerce types (string to number, etc.)
  // 3. Apply defaults
  // 4. Return typed config object
  // 5. Throw error with details if validation fails
}

// Create the configuration object
function createConfig(env) {
  const rawConfig = loadConfig(env);
  
  // Return config with derived values
  return {
    // ... base config values
    // ... derived values (isDev, isProd, etc.)
  };
}

// Test with valid config
console.log('=== Testing Valid Config ===');
try {
  const config = createConfig(mockEnv);
  console.log('Config loaded successfully!');
  console.log('Port:', config.port);
  console.log('Is Dev:', config.isDev);
} catch (e) {
  console.log('Error:', e.message);
}

// Test with missing required config
console.log('\n=== Testing Missing Config ===');
try {
  const badEnv = { NODE_ENV: 'production' };  // Missing DATABASE_URL
  const config = createConfig(badEnv);
} catch (e) {
  console.log('Caught error:', e.message);
}
