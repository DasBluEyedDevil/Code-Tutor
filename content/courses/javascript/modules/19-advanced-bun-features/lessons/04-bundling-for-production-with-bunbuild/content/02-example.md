---
type: "EXAMPLE"
title: "Basic Bundling"
---

Bundle your app with one function call:

```javascript
const result = await Bun.build({
  entrypoints: ['./src/index.ts'],
  outdir: './dist',
  minify: true,
  sourcemap: 'external',
  target: 'browser',  // or 'bun', 'node'
});

if (!result.success) {
  console.error('Build failed:', result.logs);
  process.exit(1);
}

console.log('Build complete!');
console.log('Output files:', result.outputs.map(o => o.path));

// For multiple entry points (e.g., multi-page app)
await Bun.build({
  entrypoints: [
    './src/home.ts',
    './src/about.ts',
    './src/contact.ts',
  ],
  outdir: './dist',
  splitting: true,  // Enable code splitting
});
```
