---
type: "THEORY"
title: "The TypeScript Ecosystem"
---

Setting up a project involves understanding three main components: The Compiler, the Config, and the Dependencies.

### 1. The Compiler (`tsc`)
The `tsc` command is the heart of TypeScript. It performs two jobs:
1.  **Type Checking:** Analyzing your code for errors.
2.  **Transpilation:** Translating your TypeScript into JavaScript.

### 2. The Configuration (`tsconfig.json`)
This file is a giant JSON object that controls the compiler. 
*   **Target:** Do you want to support very old browsers (ES5)? Or only modern ones (ESNext)? 
*   **Strict:** In professional development, `strict: true` is mandatory. It enables all safety features, including `noImplicitAny` and `strictNullChecks`.
*   **Module Resolution:** Tells TypeScript how to find files when you use `import`.

### 3. Declaration Files (`.d.ts`)
Since most npm packages are written in plain JavaScript, TypeScript needs a "Translation Guide" to understand them. 
*   Many packages come with these guides built-in.
*   For those that don't, you often have to install them separately: `npm install --save-dev @types/lodash`.

### 4. Build Tools
While `tsc` is great for small projects, larger apps often use **Bundlers** like **Bun**, **Vite**, or **Webpack**. These tools use TypeScript internally but are optimized for speed and web deployment.
