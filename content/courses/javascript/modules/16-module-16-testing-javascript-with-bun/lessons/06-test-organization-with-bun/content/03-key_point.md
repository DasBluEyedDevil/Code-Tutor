---
type: "KEY_POINT"
title: "Bun Test CLI Options"
---

Useful bun test flags:

```bash
# Run specific file or pattern
bun test user.test.ts
bun test --filter "auth"

# Watch mode (default)
bun test --watch

# Run once and exit
bun test --run

# Timeout per test (default 5000ms)
bun test --timeout 10000

# Run tests in parallel (default)
bun test --parallel

# Run tests sequentially
bun test --no-parallel

# Bail on first failure
bun test --bail

# Show verbose output
bun test --verbose
```

Configure defaults in bunfig.toml:
```toml
[test]
preload = ["./tests/setup.ts"]
timeout = 10000
root = "./tests"
```