// 1. Environment type
type Environment = 'development' | 'staging' | 'production';

// 2. Environment config with optional properties
interface EnvironmentConfig {
  apiUrl?: string;
  debug?: boolean;
  logLevel?: 'error' | 'warn' | 'info' | 'debug';
  timeout?: number;
  maxRetries?: number;
}

// 3. Use Record to map Environment to EnvironmentConfig
type EnvironmentConfigs = /* Your code here */;

// 4. Required version - all properties must be set
type ValidatedConfig = /* Your code here */;

// 5. Readonly frozen config
type FrozenConfig = /* Your code here */;

// Base configs (some properties missing - will use defaults)
const configs: EnvironmentConfigs = {
  development: {
    apiUrl: 'http://localhost:3000',
    debug: true,
    logLevel: 'debug'
    // timeout and maxRetries will use defaults
  },
  staging: {
    apiUrl: 'https://staging.api.com',
    debug: true,
    logLevel: 'info'
  },
  production: {
    apiUrl: 'https://api.production.com',
    debug: false,
    logLevel: 'error'
  }
};

// Defaults for missing values
const defaults: Required<EnvironmentConfig> = {
  apiUrl: 'http://localhost:3000',
  debug: false,
  logLevel: 'info',
  timeout: 5000,
  maxRetries: 3
};

// 6. Get fully populated, frozen config
function getConfig(env: Environment): FrozenConfig {
  // Merge environment config with defaults
  // Return as frozen (readonly) config
}

// 7. Test all environments
const devConfig = getConfig('development');
console.log('Dev config:', devConfig);

const prodConfig = getConfig('production');
console.log('Prod config:', prodConfig);

// This should cause TypeScript error:
// prodConfig.debug = true;  // Uncomment to test - should error!