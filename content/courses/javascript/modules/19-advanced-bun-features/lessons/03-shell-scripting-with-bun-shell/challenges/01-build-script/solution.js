// NOTE: Requires Bun runtime -- Bun's shell API ($`...`) has no Node.js equivalent.
// In Node.js, you would use child_process.execSync() for similar functionality.
// Run this challenge with: bun run solution.js

import { $ } from 'bun';

await $`mkdir -p dist`;
await $`cp *.js dist/ 2>/dev/null || true`;
const files = await $`ls dist`.text();
console.log('Files in dist:', files);
console.log('Build complete!');