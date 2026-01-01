import config from './config.json' with { type: 'json' };

console.log('Starting...');

// Validate config
if (!config.port || !config.env) {
  throw new Error('Missing required config fields');
}

console.log(`Environment: ${config.env}`);
console.log('App ready');