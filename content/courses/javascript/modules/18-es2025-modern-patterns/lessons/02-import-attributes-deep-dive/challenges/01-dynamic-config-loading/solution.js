async function loadConfig(env) {
  const configPath = env === 'production'
    ? './config.prod.json'
    : './config.dev.json';
  
  const module = await import(configPath, {
    with: { type: 'json' }
  });
  
  return module.default;
}

const config = await loadConfig('development');
console.log(config.apiUrl);