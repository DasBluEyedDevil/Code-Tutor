---
type: "WARNING"
title: "Setup Pitfalls"
---

### 1. Global vs. Local Installation
You might be tempted to run `npm install -g typescript`. 
*   **Problem:** If you share your project with a friend who doesn't have it installed globally, they won't be able to run your code. 
*   **Rule:** Always install TypeScript locally in your project (`--save-dev`) and use `npx tsc`.

### 2. The "Could not find a declaration file" Error
If you import a library like `lodash` and see a red squiggly line, it's usually because you forgot the types.
*   **Fix:** Look for a package starting with `@types/` (e.g., `npm install @types/express`).

### 3. Case Sensitivity in Config
The `tsconfig.json` file is case-sensitive on many operating systems (like Linux). If you type `Rootdir` instead of `rootDir`, your configuration will be ignored.

### 4. Forgetting `node_modules` in `.gitignore`
When you install TypeScript, it creates a massive `node_modules` folder. NEVER upload this folder to GitHub. 
*   **Fix:** Ensure your `.gitignore` file contains the line `node_modules/`.
