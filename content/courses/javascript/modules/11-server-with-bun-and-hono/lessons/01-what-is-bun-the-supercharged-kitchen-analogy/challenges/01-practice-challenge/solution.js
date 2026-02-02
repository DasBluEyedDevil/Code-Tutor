// Complete Bun utility module

// System info function (simulating Bun environment)
function getSystemInfo() {
  return {
    runtime: 'bun',
    bunVersion: '1.2.0',
    uptime: 12345,
    features: ['typescript', 'bundler', 'test-runner']
  };
}

// Async file reading simulation
// In real Bun: const file = Bun.file(filename); return file.text();
function readConfig(filename) {
  return new Promise((resolve, reject) => {
    setTimeout(() => {
      if (!filename) {
        reject(new Error('Filename is required'));
      } else {
        resolve(`Config from ${filename}`);
      }
    }, 100);
  });
}

// Test the functions
let info = getSystemInfo();
console.log('System info:', info);
console.log('Runtime:', info.runtime);       // bun
console.log('Bun version:', info.bunVersion); // 1.2.0

// Test async config reading
readConfig('app.config').then(data => {
  console.log('Config loaded:', data); // Config from app.config
}).catch(error => {
  console.log('Error:', error.message);
});

// Using async/await (modern pattern)
async function loadSystemConfig() {
  try {
    let config = await readConfig('database.config');
    console.log('Database config:', config);
  } catch (error) {
    console.log('Failed to load config:', error.message);
  }
}

loadSystemConfig();