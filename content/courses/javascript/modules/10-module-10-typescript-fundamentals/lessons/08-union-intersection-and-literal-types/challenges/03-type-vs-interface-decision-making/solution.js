// 1. Base interface for database config
interface DatabaseConfig {
  host: string;
  port: number;
  database: string;
}

// 2. Extended interfaces for specific databases
interface PostgresConfig extends DatabaseConfig {
  ssl: boolean;
}

interface MySQLConfig extends DatabaseConfig {
  charset: string;
}

// 3. Type alias for connection string
type ConnectionString = string;

// 4. Literal union for config source
type ConfigSource = 'env' | 'file' | 'remote';

// 5. Combined config with source metadata
type Config<T extends DatabaseConfig> = {
  config: T;
  source: ConfigSource;
  loadedAt: Date;
};

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