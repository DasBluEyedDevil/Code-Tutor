---
type: "LEGACY_COMPARISON"
title: "Vitest Equivalent"
---

Vitest requires configuration files and external coverage providers. Bun's coverage is built-in.

```javascript
// Vitest requires a config file (vitest.config.ts)
import { defineConfig } from 'vitest/config';

export default defineConfig({
  test: {
    coverage: {
      provider: 'v8',  // Must specify provider
      reporter: ['text', 'html', 'lcov'],
      thresholds: {
        lines: 80,
        branches: 75,
        functions: 80,
        statements: 80
      }
    }
  }
});

// And in GitHub Actions:
// - uses: actions/setup-node@v4
// - run: npm ci
// - run: npx vitest run --coverage

// Bun equivalent - just one command!
// bun test --coverage
```
