---
type: "KEY_POINT"
title: "Code Coverage Goals and Metrics"
---


**Code Coverage** measures what percentage of your code is executed during testing. It is a useful metric but not the only measure of test quality.

**Understanding Coverage Metrics:**

- **Line Coverage**: Percentage of code lines executed
- **Branch Coverage**: Percentage of conditional branches taken
- **Function Coverage**: Percentage of functions called

**Recommended Coverage Targets:**

| Code Type | Minimum | Target | Notes |
|-----------|---------|--------|-------|
| Business Logic | 80% | 95%+ | Critical, test thoroughly |
| Route Handlers | 70% | 85% | Cover success and error paths |
| Middleware | 80% | 90% | Security-critical |
| Data Models | 60% | 80% | Focus on validation |
| Utilities | 70% | 90% | Pure functions are easy to test |
| Overall | 70% | 80% | Balance effort with value |

**Generating Coverage Reports:**

```bash
# Run tests with coverage
dart test --coverage=coverage

# Generate HTML report (requires lcov)
genhtml coverage/lcov.info -o coverage/html

# Open report
open coverage/html/index.html
```

**Coverage Caveats:**

1. **100% coverage does not mean bug-free**: You can have full coverage with bad tests
2. **Quality over quantity**: One good test beats ten superficial ones
3. **Diminishing returns**: Going from 80% to 90% is harder than 60% to 80%
4. **Focus on critical paths**: Not all code is equally important

