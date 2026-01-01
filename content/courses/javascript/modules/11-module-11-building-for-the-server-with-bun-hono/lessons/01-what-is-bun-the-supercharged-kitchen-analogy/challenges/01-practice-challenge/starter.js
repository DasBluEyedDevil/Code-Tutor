// System info function (simulating Bun environment)
function getSystemInfo() {
  return {
    runtime: 'bun',
    bunVersion: '1.2.0',
    uptime: 12345
  };
}

// Async file reading simulation
// In real Bun: const file = Bun.file(filename); return file.text();
function readConfig(filename) {
  return new Promise((resolve, reject) => {
    setTimeout(() => {
      resolve(`Config from ${filename}`);
    }, 100);
  });
}

// Test the functions
let info = getSystemInfo();
console.log('System:', info);

readConfig('app.config').then(data => {
  console.log('Config:', data);
});