import { $ } from 'bun';

await $`mkdir -p dist`;
await $`cp *.js dist/ 2>/dev/null || true`;
const files = await $`ls dist`.text();
console.log('Files in dist:', files);
console.log('Build complete!');