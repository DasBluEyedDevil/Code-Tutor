---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// TypeScript 5.7 Project Setup (2024-2025)

// STEP 1: Initialize a Node.js project (run in terminal)
// npm init -y

// STEP 2: Install TypeScript (run in terminal)
// npm install -D typescript

// STEP 3: Create tsconfig.json configuration file
// Content of tsconfig.json:
/*
{
  "compilerOptions": {
    "target": "ES2024",           // Use ES2024 features
    "module": "ESNext",           // Modern module system
    "outDir": "./dist",           // Compiled JS goes here
    "rootDir": "./src",           // TypeScript source files here
    "strict": true,               // Enable all strict type checks
    "esModuleInterop": true,      // Better import compatibility
    "skipLibCheck": true,         // Skip type checking of declaration files
    "forceConsistentCasingInFileNames": true  // Case-sensitive imports
  },
  "include": ["src/**/*"],       // Include all files in src folder
  "exclude": ["node_modules"]    // Exclude dependencies
}
*/

// STEP 4: Create folder structure
// project/
//   ├── src/           (TypeScript source files)
//   │   └── index.ts
//   ├── dist/          (Compiled JavaScript - auto-generated)
//   ├── node_modules/  (Dependencies)
//   ├── package.json
//   └── tsconfig.json

// STEP 5: Write TypeScript code in src/index.ts
interface Greeting {
  message: string;
  name: string;
}

function greet(greeting: Greeting): string {
  return `${greeting.message}, ${greeting.name}!`;
}

const welcome: Greeting = {
  message: 'Hello',
  name: 'TypeScript Developer'
};

console.log(greet(welcome));

// STEP 6: Compile TypeScript to JavaScript (run in terminal)
// npx tsc
// This creates dist/index.js

// STEP 7: Run the compiled JavaScript (run in terminal)
// node dist/index.js

// STEP 8: (Optional) Watch mode - auto-compile on file changes
// npx tsc --watch

// MODERN WORKFLOW: Use ts-node for development
// npm install -D ts-node
// npx ts-node src/index.ts  (runs TypeScript directly!)

console.log('TypeScript project setup complete!');
```
