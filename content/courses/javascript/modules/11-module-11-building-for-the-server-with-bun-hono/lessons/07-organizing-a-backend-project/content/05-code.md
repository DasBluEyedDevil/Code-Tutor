---
type: "CODE"
title: "Environment Configuration"
---

Environment configuration is how you manage settings that change between development, staging, and production environments. Database connection strings, API keys, and feature flags all vary by environment. A type-safe configuration module centralizes these settings, validates them at startup, and provides autocomplete in your IDE. Never commit sensitive values to version control. Use environment variables and .env files for local development. In production, use your platform secrets manager or environment variable injection.

```typescript
// src/config/index.ts
// Type-safe environment configuration

import { z } from 'zod';

// Define the shape of your environment variables with validation
const envSchema = z.object({
  // Server configuration
  NODE_ENV: z.enum(['development', 'staging', 'production']).default('development'),
  PORT: z.coerce.number().int().min(1).max(65535).default(3000),
  HOST: z.string().default('0.0.0.0'),
  
  // Database configuration
  DATABASE_URL: z.string().url(),
  DATABASE_POOL_SIZE: z.coerce.number().int().min(1).max(100).default(10),
  
  // Authentication
  JWT_SECRET: z.string().min(32, 'JWT secret must be at least 32 characters'),
  JWT_EXPIRES_IN: z.string().default('7d'),
  
  // External services
  SMTP_HOST: z.string().optional(),
  SMTP_PORT: z.coerce.number().optional(),
  SMTP_USER: z.string().optional(),
  SMTP_PASS: z.string().optional(),
  
  // Feature flags
  ENABLE_SIGNUP: z.coerce.boolean().default(true),
  ENABLE_RATE_LIMITING: z.coerce.boolean().default(true),
  
  // API keys (optional in development)
  STRIPE_SECRET_KEY: z.string().optional(),
  SENDGRID_API_KEY: z.string().optional(),
  
  // Logging
  LOG_LEVEL: z.enum(['debug', 'info', 'warn', 'error']).default('info')
});

// Parse and validate environment variables at startup
function loadConfig() {
  const result = envSchema.safeParse(process.env);
  
  if (!result.success) {
    console.error('Invalid environment configuration:');
    console.error(result.error.flatten().fieldErrors);
    process.exit(1);  // Fail fast on invalid config
  }
  
  return result.data;
}

// Export validated and typed configuration
export const env = loadConfig();

// Derived configuration with computed values
export const config = {
  // Environment
  isDev: env.NODE_ENV === 'development',
  isProd: env.NODE_ENV === 'production',
  isStaging: env.NODE_ENV === 'staging',
  
  // Server
  port: env.PORT,
  host: env.HOST,
  
  // Database
  database: {
    url: env.DATABASE_URL,
    poolSize: env.DATABASE_POOL_SIZE
  },
  
  // JWT
  jwt: {
    secret: env.JWT_SECRET,
    expiresIn: env.JWT_EXPIRES_IN
  },
  
  // CORS (different for each environment)
  cors: {
    origin: env.NODE_ENV === 'production'
      ? ['https://myapp.com', 'https://www.myapp.com']
      : ['http://localhost:3000', 'http://localhost:5173'],
    credentials: true
  },
  
  // Email
  email: env.SMTP_HOST ? {
    host: env.SMTP_HOST,
    port: env.SMTP_PORT || 587,
    auth: {
      user: env.SMTP_USER,
      pass: env.SMTP_PASS
    }
  } : null,
  
  // Feature flags
  features: {
    signup: env.ENABLE_SIGNUP,
    rateLimiting: env.ENABLE_RATE_LIMITING
  },
  
  // Logging
  logLevel: env.LOG_LEVEL
};

// Type export for use in other files
export type Config = typeof config;

// ============================================================
// .env.example (commit this to version control)
// ============================================================
/*
# Server
NODE_ENV=development
PORT=3000
HOST=0.0.0.0

# Database
DATABASE_URL=postgresql://user:password@localhost:5432/mydb
DATABASE_POOL_SIZE=10

# Authentication (generate with: openssl rand -base64 32)
JWT_SECRET=your-super-secret-jwt-key-at-least-32-chars
JWT_EXPIRES_IN=7d

# Email (optional in development)
SMTP_HOST=smtp.mailtrap.io
SMTP_PORT=587
SMTP_USER=
SMTP_PASS=

# Feature Flags
ENABLE_SIGNUP=true
ENABLE_RATE_LIMITING=true

# External Services (optional in development)
STRIPE_SECRET_KEY=
SENDGRID_API_KEY=

# Logging
LOG_LEVEL=debug
*/

// ============================================================
// .env (DO NOT COMMIT - add to .gitignore)
// ============================================================
/*
NODE_ENV=development
PORT=3000
DATABASE_URL=postgresql://dev:devpass@localhost:5432/myapp_dev
JWT_SECRET=dev-secret-key-only-for-local-development-32chars
*/

// ============================================================
// .gitignore entries for secrets
// ============================================================
/*
# Environment files with secrets
.env
.env.local
.env.*.local

# Keep example file
!.env.example
*/

// ============================================================
// Usage in other files
// ============================================================

import { config } from './config';

// Type-safe access with autocomplete!
if (config.features.signup) {
  console.log('Signup is enabled');
}

if (config.isDev) {
  console.log('Running in development mode');
}

// Database connection
const db = createConnection(config.database.url, {
  poolSize: config.database.poolSize
});

// JWT signing
const token = signJwt(payload, config.jwt.secret, {
  expiresIn: config.jwt.expiresIn
});

// ============================================================
// Secrets Management Best Practices
// ============================================================
//
// 1. NEVER commit secrets to version control
//    - Use .env.example for documentation
//    - Add .env to .gitignore immediately
//
// 2. Use different secrets per environment
//    - Development: local secrets, can be simple
//    - Staging: real secrets, rotated regularly
//    - Production: strongest secrets, strict access
//
// 3. Production secrets management:
//    - AWS: Secrets Manager or Parameter Store
//    - GCP: Secret Manager
//    - Azure: Key Vault
//    - Kubernetes: Secrets or external-secrets
//    - Fly.io/Railway/Render: Built-in secrets
//
// 4. Validate at startup
//    - Fail fast if required config is missing
//    - Log which optional config is missing
//    - Never log actual secret values!
//
// 5. Rotate secrets regularly
//    - API keys: every 90 days
//    - JWT secrets: every 30 days (with grace period)
//    - Database passwords: per security policy
```
