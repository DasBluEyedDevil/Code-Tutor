// ProjectConfig interface
interface ProjectConfig {
  target: string;
  strict: boolean;
  outDir: string;
}

// Typed validation function
function validateConfig(config: ProjectConfig): boolean {
  return config.strict === true;
}

// Create typed config object
let myConfig: ProjectConfig = {
  target: 'ES2024',
  strict: true,
  outDir: './dist'
};

console.log('Strict mode enabled?', validateConfig(myConfig)); // true

// Test with strict mode disabled
let loosConfig: ProjectConfig = {
  target: 'ES2024',
  strict: false,
  outDir: './dist'
};

console.log('Loose mode:', validateConfig(loosConfig)); // false