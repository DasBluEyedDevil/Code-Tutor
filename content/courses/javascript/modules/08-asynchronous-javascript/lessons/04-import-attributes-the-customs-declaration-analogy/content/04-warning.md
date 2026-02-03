---
type: "WARNING"
title: "Modern Import Pitfalls"
---

### 1. `with` vs. `assert`
In early versions of this feature (2021-2023), JavaScript used the `assert` keyword (e.g., `import data from "./file.json" assert { type: "json" }`). This was changed to `with` in the final ES2025 standard.
*   **Rule:** Always use `with`. The `assert` keyword is deprecated and will eventually stop working.

### 2. Browser & Runtime Support
Because this is a very new feature (standardized in 2025), it might not work in older browsers or older versions of Node.js.
*   **Fix:** If you are building for the web, ensure you are using a modern build tool (like Bun, Vite, or Esbuild) that can "transpile" this syntax for older browsers.

### 3. Missing Attributes
If you try to import a JSON file without the `type: "json"` attribute in a modern environment, the browser will likely block the import for security reasons. 
*   **Error:** "Failed to load module script: Expected a JavaScript module script but the server responded with a MIME type of 'application/json'."

### 4. Not for Logic
Import attributes are only for **Metadata** and **Validation**. You cannot use them to pass variables into the imported file or change how the code inside the file runs.
