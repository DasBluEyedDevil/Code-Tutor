---
type: "EXAMPLE"
title: "Securing your Imports"
---

```javascript
// 1. Static Import with Attributes
// We use the 'with' keyword to declare the expected type
import config from "./config.json" with { type: "json" };

console.log(`Current Version: ${config.version}`);
console.log(`Server URL: ${config.apiUrl}`);

// 2. Dynamic Import with Attributes
// Useful for loading data on-demand
async function loadStyles() {
    // In some environments, you might import CSS this way:
    const theme = await import("./theme.json", {
        with: { type: "json" }
    });
    console.log("Theme loaded:", theme.default);
}

// 3. Why this is better than 'fetch'
// When you use import, the file is cached and managed 
// by the JavaScript module system automatically.
import userData from "./user-seed.json" with { type: "json" };
console.log(`Loaded ${userData.length} users.`);
```