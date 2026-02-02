async function loadConfig(env) {
  // Dynamically import config based on env
  // Use import attributes!
}

const config = await loadConfig('development');
console.log(config.apiUrl);