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
type EnvironmentConfigs = Record<Environment, EnvironmentConfig>;

// 4. Required version - all properties must be set
type ValidatedConfig = Required<EnvironmentConfig>;

// 5. Readonly frozen config
type FrozenConfig = Readonly<Required<EnvironmentConfig>>;

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
  const envConfig = configs[env];
  
  // Merge defaults with environment config
  const merged: ValidatedConfig = {
    apiUrl: envConfig.apiUrl ?? defaults.apiUrl,
    debug: envConfig.debug ?? defaults.debug,
    logLevel: envConfig.logLevel ?? defaults.logLevel,
    timeout: envConfig.timeout ?? defaults.timeout,
    maxRetries: envConfig.maxRetries ?? defaults.maxRetries
  };
  
  // Return as frozen - caller cannot modify
  return merged;
}

// 7. Test all environments
const devConfig = getConfig('development');
console.log('Dev config:', devConfig);
// Dev config: { apiUrl: 'http://localhost:3000', debug: true, logLevel: 'debug', timeout: 5000, maxRetries: 3 }

const prodConfig = getConfig('production');
console.log('Prod config:', prodConfig);
// Prod config: { apiUrl: 'https://api.production.com', debug: false, logLevel: 'error', timeout: 5000, maxRetries: 3 }

// This causes TypeScript error:
// prodConfig.debug = true;  // ERROR: Cannot assign to 'debug' because it is a read-only property