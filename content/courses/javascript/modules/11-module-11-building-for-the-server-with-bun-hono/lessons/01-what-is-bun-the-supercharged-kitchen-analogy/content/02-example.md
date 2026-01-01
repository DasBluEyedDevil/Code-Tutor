---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// Bun - The All-in-One JavaScript Runtime (2025)

// KEY ADVANTAGES OF BUN
// 1. SPEED - Up to 4x faster than Node.js
// 2. TypeScript works out of the box (no compilation!)
// 3. Built-in package manager (bun install)
// 4. Built-in bundler (bun build)
// 5. Built-in test runner (bun test)

// BUILT-IN APIs - Bun comes with powerful tools
// (Note: These won't work in browser JavaScript!)

// 1. FILE SYSTEM - Read and write files (simulated for demo)
let simulatedFileContent = 'Hello from Bun!';
console.log('File content:', simulatedFileContent);

// In real Bun, file operations are super simple:
// const file = Bun.file('data.txt');
// const content = await file.text();
// console.log(content);

// 2. PATH - Handle file paths
function joinPath(...parts) {
  return parts.join('/');
}

let filePath = joinPath('users', 'documents', 'notes.txt');
console.log('File path:', filePath);

// 3. HTTP SERVER - Built-in with Bun.serve()
// In real Bun:
// Bun.serve({
//   port: 3000,
//   fetch(request) {
//     return new Response('Hello from Bun!');
//   }
// });

console.log('Bun can create web servers with Bun.serve()!');

// 4. RUNTIME INFO
let bunVersion = '1.2.0'; // Bun version
let platform = 'linux'; // Could be 'win32', 'darwin' (macOS), etc.

console.log('Bun version:', bunVersion);
console.log('Platform:', platform);

// 5. NATIVE TYPESCRIPT SUPPORT
// In Bun, you can run .ts files directly!
// bun run app.ts   <- No compilation needed!
// interface User { name: string; age: number; }
// const user: User = { name: 'Alice', age: 30 };

console.log('Bun runs TypeScript natively!');

// 6. ASYNC OPERATIONS - Same as Node.js
function simulateFileRead(filename) {
  return new Promise((resolve) => {
    setTimeout(() => {
      resolve(`Contents of ${filename}`);
    }, 100);
  });
}

simulateFileRead('data.txt').then(data => {
  console.log('File data:', data);
});

// 7. NPM COMPATIBILITY - Use any npm package!
// Bun is compatible with npm packages
// bun install express  <- Works!
// bun install hono     <- Even better!

console.log('Bun works with npm packages!');

// 8. ES MODULES - Modern imports by default
// import { Hono } from 'hono';
// import express from 'express';

// 9. BUN-SPECIFIC FEATURES
let bunFeatures = [
  'Native TypeScript/JSX support',
  'Fastest JavaScript runtime',
  'Built-in SQLite database',
  'Built-in password hashing',
  'Built-in HTML rewriter',
  'Web-standard APIs (fetch, Response, Request)'
];

console.log('\nBun-specific features:');
bunFeatures.forEach(f => console.log('  -', f));

// 10. RUNNING BUN CODE
// Save file as app.ts or app.js
// Run with: bun run app.ts
// Or simply: bun app.ts

console.log('\nRun Bun code with: bun run app.ts');
```
