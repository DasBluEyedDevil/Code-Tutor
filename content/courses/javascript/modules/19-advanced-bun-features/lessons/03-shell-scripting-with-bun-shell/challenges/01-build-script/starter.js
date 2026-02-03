// NOTE: Requires Bun runtime -- Bun's shell API ($`...`) has no Node.js equivalent.
// In Node.js, you would use child_process.execSync() for similar functionality.
// Run this challenge with: bun run starter.js

import { $ } from 'bun';

// 1. Create dist directory
// 2. Copy all .js files to dist
// 3. List the dist contents
// 4. Print 'Build complete!'