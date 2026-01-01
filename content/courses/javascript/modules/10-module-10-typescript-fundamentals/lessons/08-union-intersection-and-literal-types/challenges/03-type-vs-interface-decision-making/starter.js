// 1. Base interface for database config
interface DatabaseConfig {
  // your code: host, port, database
}

// 2. Extended interfaces for specific databases
interface PostgresConfig extends DatabaseConfig {
  // your code: add ssl: boolean
}

interface MySQLConfig extends DatabaseConfig {
  // your code: add charset: string
}

// 3. Type alias for connection string
type ConnectionString = // your code

// 4. Literal union for config source
type ConfigSource = // your code

// 5. Combined config with source metadata
type Config<T extends DatabaseConfig> = // your code
// Should have: config: T, source: ConfigSource, loadedAt: Date

// Test your types
const postgresConfig: Config<PostgresConfig> = {
  config: {
    host: 'localhost',
    port: 5432,
    database: 'myapp',
    ssl: true
  },
  source: 'env',
  loadedAt: new Date()
};

console.log(`Connecting to ${postgresConfig.config.host}:${postgresConfig.config.port}`);
console.log(`SSL enabled: ${postgresConfig.config.ssl}`);
console.log(`Loaded from: ${postgresConfig.source}`);