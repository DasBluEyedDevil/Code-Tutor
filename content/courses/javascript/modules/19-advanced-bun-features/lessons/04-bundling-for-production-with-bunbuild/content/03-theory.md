---
type: "THEORY"
title: "Build Options"
---

**Common options:**
```javascript
await Bun.build({
  entrypoints: ['./src/index.ts'],
  outdir: './dist',
  
  // Optimization
  minify: true,           // Minify output
  sourcemap: 'external',  // 'none' | 'inline' | 'external'
  
  // Target environment
  target: 'browser',      // 'bun' | 'node' | 'browser'
  
  // Code splitting (for multiple entry points)
  splitting: true,
  
  // Naming
  naming: '[dir]/[name]-[hash].[ext]',
  
  // External packages (don't bundle these)
  external: ['react', 'react-dom'],
  
  // Define globals
  define: {
    'process.env.NODE_ENV': '"production"',
  },
});
```