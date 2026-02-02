---
type: "THEORY"
title: "Why Bun's Test Runner?"
---

Bun has a built-in test runner that requires zero configuration:

- **Zero Setup** - No installation, no config files, just works
- **Blazing Fast** - Native speed, runs tests in parallel
- **Jest-Compatible** - Same API you already know
- **TypeScript Ready** - No setup needed for TS

No installation needed! Just create a test file:
```bash
# Create test file (*.test.ts, *.test.js, or *_test.ts)
touch math.test.ts

# Run tests
bun test
```

That's it. No package.json scripts, no config files, no dependencies.

`bun test` watches for changes by default. Use `bun test --run` to run once and exit.