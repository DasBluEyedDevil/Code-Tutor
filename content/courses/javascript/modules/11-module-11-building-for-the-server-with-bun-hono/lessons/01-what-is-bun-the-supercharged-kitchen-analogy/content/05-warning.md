---
type: "WARNING"
title: "Common Pitfalls"
---

Common Bun beginner mistakes:

1. **Trying to use browser APIs**:
   ```javascript
   // ERROR in Bun - no DOM!
   document.getElementById('app'); // ReferenceError: document is not defined
   window.location.href;           // ReferenceError: window is not defined
   ```
   Solution: Bun is for servers, not browsers

2. **Forgetting Bun uses Web APIs**:
   ```javascript
   // Bun uses standard Web APIs
   // Use fetch() instead of axios for HTTP requests
   const response = await fetch('https://api.example.com/data');
   const data = await response.json();
   ```

3. **Using Node.js-specific patterns unnecessarily**:
   ```javascript
   // Old Node.js way (still works but not needed)
   import { fileURLToPath } from 'url';
   import { dirname } from 'path';
   const __filename = fileURLToPath(import.meta.url);
   const __dirname = dirname(__filename);
   
   // Bun way - import.meta has more built-in!
   console.log(import.meta.dir);  // Current directory
   console.log(import.meta.file); // Current filename
   ```

4. **Not leveraging Bun.file()**:
   ```javascript
   // Instead of fs.readFile callbacks:
   const file = Bun.file('data.txt');
   const text = await file.text();  // Simple!
   ```

5. **Installing TypeScript separately**:
   - Bun runs .ts files directly!
   - No need for `npm install typescript`
   - No need for tsconfig.json (unless you want custom settings)

6. **Using npm when bun is faster**:
   - Use `bun install` instead of `npm install`
   - Use `bun add package` instead of `npm install package`
   - Up to 25x faster package installation!

7. **Assuming all Node.js APIs work**:
   - Most do, but some edge cases differ
   - Check Bun docs for compatibility
   - When in doubt, use Web Standard APIs