---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Let's break down TypeScript project configuration:

1. **tsconfig.json**: The command center
   - Tells TypeScript how to compile your code
   - Lives in the project root directory
   - JSON format (strict syntax)

2. **Key Configuration Options**:
   - `target`: Which JavaScript version to output (ES2024 is latest)
   - `module`: How to handle imports/exports (ESNext for modern)
   - `outDir`: Where compiled JavaScript goes (usually 'dist' or 'build')
   - `rootDir`: Where TypeScript source files are (usually 'src')
   - `strict`: Turn on all strict type checking (HIGHLY recommended)

3. **Compilation Process**:
   - Write `.ts` files (TypeScript)
   - Run `npx tsc` command
   - TypeScript compiler reads tsconfig.json
   - Generates `.js` files in outDir
   - Run the `.js` files with Node.js

4. **Development Tools**:
   - `tsc`: TypeScript compiler
   - `ts-node`: Run TypeScript directly without manual compilation
   - `--watch`: Auto-recompile when files change
   - `npm install -D`: Install as development dependency

5. **Folder Structure Best Practices**:
   - `/src`: All TypeScript source code
   - `/dist`: Compiled JavaScript (gitignored)
   - `/node_modules`: Dependencies (gitignored)
   - Root: Config files (tsconfig.json, package.json)

6. **Why We Need Compilation**:
   - Browsers and Node.js don't understand TypeScript
   - TypeScript must be converted (transpiled) to JavaScript
   - The type checking happens during compilation
   - Runtime uses the generated JavaScript