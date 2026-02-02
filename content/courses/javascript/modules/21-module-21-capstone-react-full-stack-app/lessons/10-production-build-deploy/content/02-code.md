---
type: "EXAMPLE"
title: "Vite Build Config"
---

Optimize build for production:

```typescript
// vite.config.ts
export default defineConfig({
  build: {
    minify: 'terser',
    sourcemap: 'hidden',
    rollupOptions: {
      output: {
        manualChunks: {
          'vendor': ['react', 'react-dom'],
          'query': ['@tanstack/react-query']
        }
      }
    }
  }
});
```
