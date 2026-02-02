---
type: "THEORY"
title: "Understanding Code Coverage"
---

Code coverage measures how much of your code is executed by tests.

**Coverage types:**
- **Lines** - % of lines executed
- **Statements** - % of statements executed
- **Branches** - % of if/else paths taken
- **Functions** - % of functions called

Enable coverage in Bun with a single flag:
```bash
bun test --coverage
```

Bun outputs coverage to the console by default. No configuration needed!

For detailed reports, Bun generates lcov format:
```bash
bun test --coverage --coverage-reporter=lcov
```

**80% coverage** is a common target. 100% is often impractical and can lead to testing implementation details.