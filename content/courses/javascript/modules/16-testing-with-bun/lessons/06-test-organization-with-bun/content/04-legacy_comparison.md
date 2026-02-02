---
type: "LEGACY_COMPARISON"
title: "Vitest Equivalent"
---

Vitest uses vitest.config.ts for setup files. Bun uses --preload flag or bunfig.toml.

```javascript
// Vitest config (vitest.config.ts)
import { defineConfig } from 'vitest/config';

export default defineConfig({
  test: {
    setupFiles: ['./tests/setup.ts'],  // Vitest setup
    globals: true,
    environment: 'node',
    include: ['**/*.test.ts']
  }
});

// Bun equivalent (bunfig.toml)
// [test]
// preload = ["./tests/setup.ts"]
```
