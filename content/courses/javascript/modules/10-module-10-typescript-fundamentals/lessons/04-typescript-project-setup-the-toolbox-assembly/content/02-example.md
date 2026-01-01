---
type: "EXAMPLE"
title: "Project Initialization"
---

```bash
# 1. Initialize a new NPM project
npm init -y

# 2. Install TypeScript as a development tool
npm install --save-dev typescript

# 3. Create the default configuration file
npx tsc --init
```

### The `tsconfig.json` File
This file lives in your project root and tells TypeScript how to behave. Here are the most common settings:

```json
{
  "compilerOptions": {
    "target": "ES2022",          // Which version of JS to produce
    "module": "NodeNext",        // How to handle imports
    "rootDir": "./src",          // Where your TS files live
    "outDir": "./dist",          // Where the compiled JS goes
    "strict": true,              // Turn on all strict type-checking
    "esModuleInterop": true,     // Better support for CJS modules
    "skipLibCheck": true         // Speed up compilation
  },
  "include": ["src/**/*"]        // Only compile files in /src
}
```

### Running the Compiler
```bash
# Run once
npx tsc

# "Watch" mode: automatically re-compiles when you save a file
npx tsc --watch
```