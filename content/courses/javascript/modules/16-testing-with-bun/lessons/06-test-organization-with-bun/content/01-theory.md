---
type: "THEORY"
title: "Bun Test File Patterns & Preload"
---

Bun automatically finds test files matching these patterns:
- `*.test.ts` / `*.test.js`
- `*_test.ts` / `*_test.js`
- `*.spec.ts` / `*.spec.js`
- Files in `__tests__` directories

**Using --preload for setup:**
```bash
# Run setup before all tests
bun test --preload ./tests/setup.ts
```

Create a setup file for global configuration:
```javascript
// tests/setup.ts
import { beforeAll, afterAll } from 'bun:test';

beforeAll(() => {
  // Global setup: connect to test DB, etc.
});

afterAll(() => {
  // Global cleanup
});
```

Add to bunfig.toml for persistence:
```toml
[test]
preload = ["./tests/setup.ts"]
```