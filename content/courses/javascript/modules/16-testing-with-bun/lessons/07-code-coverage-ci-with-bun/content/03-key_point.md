---
type: "KEY_POINT"
title: "Coverage Best Practices"
---

**Do:**
- Set coverage thresholds to prevent regression
- Focus on critical paths (auth, payments, core logic)
- Use coverage to find untested code, not prove quality
- Review coverage reports in CI to catch gaps

**Don't:**
- Chase 100% coverage
- Write tests just to increase numbers
- Ignore uncovered code if it's dead code
- Test trivial getters/setters

Configure coverage in bunfig.toml:
```toml
[test]
coverage = true
coverageReporter = ["text", "lcov"]
coverageThreshold = { line = 80, function = 80, branch = 75 }
```

Or use CLI flags:
```bash
bun test --coverage --coverage-threshold 80
```

CI will fail if coverage drops below these thresholds.