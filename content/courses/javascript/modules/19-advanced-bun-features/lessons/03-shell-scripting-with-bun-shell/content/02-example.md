---
type: "EXAMPLE"
title: "The $ Tagged Template"
---

Run shell commands with the $ tag:

```javascript
import { $ } from 'bun';

// Simple commands
await $`echo 'Hello from Bun Shell!'`;

// Variable interpolation (safely escaped!)
const name = 'my-project';
await $`mkdir -p ${name}/src`;

// Capture output
const result = await $`ls -la`.text();
console.log(result);

// Get as JSON (if command outputs JSON)
const pkg = await $`cat package.json`.json();
console.log(pkg.name);

// Pipe commands
await $`cat file.txt | grep 'pattern' | wc -l`;

// Environment variables
await $`NODE_ENV=production bun run build`;
```
