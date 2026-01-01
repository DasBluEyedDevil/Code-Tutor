// Simulated environment (in real code, use process.env)
let mockEnv = {
  NODE_ENV: 'development',
  PORT: '3000',
  DATABASE_URL: 'postgresql://localhost:5432/mydb',
  JWT_SECRET: 'my-super-secret-key-for-jwt-tokens',
  ENABLE_SIGNUP: 'true',
  LOG_LEVEL: 'debug'
};

// Configuration Error
class ConfigError extends Error {
  constructor(message, missingVars = []) {
    super(message);
    this.name = 'ConfigError';
    this.missingVars = missingVars;
  }
}

// Define expected environment variables with types and defaults
const configSchema = {
  NODE_ENV: {
    type: 'string',
    required: false,
    default: 'development',
    enum: ['development', 'staging', 'production']
  },
  PORT: {
    type: 'number',
    required: false,
    default: 3000
  },
  DATABASE_URL: {
    type: 'string',
    required: true
  },
  JWT_SECRET: {
    type: 'string',
    required: true,
    minLength: 20
  },
  JWT_EXPIRES_IN: {
    type: 'string',
    required: false,
    default: '7d'
  },
  SMTP_HOST: {
    type: 'string',
    required: false
  },
  SMTP_PORT: {
    type: 'number',
    required: false,
    default: 587
  },
  ENABLE_SIGNUP: {
    type: 'boolean',
    required: false,
    default: true
  },
  LOG_LEVEL: {
    type: 'string',
    required: false,
    default: 'info',
    enum: ['debug', 'info', 'warn', 'error']
  }
};

// Coerce value to expected type
function coerceValue(value, type) {
  if (value === undefined) return undefined;
  
  switch (type) {
    case 'number':
      const num = Number(value);
      if (isNaN(num)) throw new Error(`Cannot convert "${value}" to number`);
      return num;
    case 'boolean':
      if (value === 'true' || value === '1') return true;
      if (value === 'false' || value === '0') return false;
      throw new Error(`Cannot convert "${value}" to boolean`);
    case 'string':
    default:
      return String(value);
  }
}

// Validate and load configuration
function loadConfig(env) {
  const errors = [];
  const result = {};
  
  for (const [key, schema] of Object.entries(configSchema)) {
    const rawValue = env[key];
    
    // Check if required and missing
    if (rawValue === undefined) {
      if (schema.required) {
        errors.push(`${key} is required but not set`);
        continue;
      }
      // Apply default if available
      if (schema.default !== undefined) {
        result[key] = schema.default;
      }
      continue;
    }
    
    // Coerce to correct type
    try {
      const value = coerceValue(rawValue, schema.type);
      
      // Validate enum
      if (schema.enum && !schema.enum.includes(value)) {
        errors.push(`${key} must be one of: ${schema.enum.join(', ')}`);
        continue;
      }
      
      // Validate minLength
      if (schema.minLength && typeof value === 'string' && value.length < schema.minLength) {
        errors.push(`${key} must be at least ${schema.minLength} characters`);
        continue;
      }
      
      result[key] = value;
    } catch (e) {
      errors.push(`${key}: ${e.message}`);
    }
  }
  
  // Fail fast with all errors
  if (errors.length > 0) {
    const message = `Configuration validation failed:\n  - ${errors.join('\n  - ')}`;
    throw new ConfigError(message, errors);
  }
  
  return result;
}

// Create the typed configuration object
function createConfig(env) {
  const raw = loadConfig(env);
  
  return {
    // Environment
    nodeEnv: raw.NODE_ENV,
    isDev: raw.NODE_ENV === 'development',
    isProd: raw.NODE_ENV === 'production',
    isStaging: raw.NODE_ENV === 'staging',
    
    // Server
    port: raw.PORT,
    
    // Database
    database: {
      url: raw.DATABASE_URL
    },
    
    // JWT
    jwt: {
      secret: raw.JWT_SECRET,
      expiresIn: raw.JWT_EXPIRES_IN
    },
    
    // Email (might be undefined)
    email: raw.SMTP_HOST ? {
      host: raw.SMTP_HOST,
      port: raw.SMTP_PORT
    } : null,
    
    // Feature flags
    features: {
      signup: raw.ENABLE_SIGNUP
    },
    
    // Logging
    logLevel: raw.LOG_LEVEL
  };
}

// ============================================================
// TEST
// ============================================================

console.log('=== Testing Valid Config ===');
try {
  const config = createConfig(mockEnv);
  console.log('Config loaded successfully!');
  console.log('Environment:', config.nodeEnv);
  console.log('Is Dev:', config.isDev);
  console.log('Port:', config.port);
  console.log('Database URL:', config.database.url);
  console.log('JWT Expires:', config.jwt.expiresIn);
  console.log('Email configured:', config.email !== null);
  console.log('Signup enabled:', config.features.signup);
  console.log('Log level:', config.logLevel);
} catch (e) {
  console.log('Error:', e.message);
}

console.log('\n=== Testing Missing Required Config ===');
try {
  const badEnv = { NODE_ENV: 'production' };  // Missing DATABASE_URL and JWT_SECRET
  const config = createConfig(badEnv);
} catch (e) {
  console.log('Caught ConfigError!');
  console.log(e.message);
}

console.log('\n=== Testing Invalid Values ===');
try {
  const invalidEnv = {
    ...mockEnv,
    PORT: 'not-a-number',
    NODE_ENV: 'invalid-env'
  };
  const config = createConfig(invalidEnv);
} catch (e) {
  console.log('Caught validation error!');
  console.log(e.message);
}

console.log('\n=== Testing Production Config ===');
const prodEnv = {
  NODE_ENV: 'production',
  PORT: '8080',
  DATABASE_URL: 'postgresql://prod-server:5432/mydb',
  JWT_SECRET: 'production-secret-key-minimum-20-chars',
  LOG_LEVEL: 'warn'
};

try {
  const config = createConfig(prodEnv);
  console.log('Production config loaded!');
  console.log('Is Prod:', config.isProd);
  console.log('Port:', config.port);
  console.log('Log Level:', config.logLevel);
} catch (e) {
  console.log('Error:', e.message);
}