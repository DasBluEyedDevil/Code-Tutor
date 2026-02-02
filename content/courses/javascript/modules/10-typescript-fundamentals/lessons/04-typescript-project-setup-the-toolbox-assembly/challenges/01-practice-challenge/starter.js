// Define ProjectConfig interface
interface ProjectConfig {
  // Add properties here
}

// Create validation function
function validateConfig(config) {
  return config.strict === true;
}

// Create a config object
let myConfig = {
  target: 'ES2024',
  strict: true,
  outDir: './dist'
};

console.log('Strict mode enabled?', validateConfig(myConfig));