---
type: "THEORY"
title: "Testing in CI/CD Pipelines"
---


**Continuous Integration (CI)** runs your tests automatically on every code change. This catches bugs before they reach production.

**Benefits of CI Testing:**

1. **Early Detection**: Catch bugs before they are merged
2. **Consistency**: Tests run the same way every time
3. **Team Confidence**: Everyone knows the code works
4. **Documentation**: CI history shows what was tested when

**GitHub Actions Example for Dart Backend:**

```yaml
# .github/workflows/test.yml
name: Tests

on:
  push:
    branches: [main]
  pull_request:
    branches: [main]

jobs:
  test:
    runs-on: ubuntu-latest
    
    steps:
      - uses: actions/checkout@v4
      
      - uses: dart-lang/setup-dart@v1
        with:
          sdk: stable
      
      - name: Install dependencies
        run: dart pub get
      
      - name: Analyze code
        run: dart analyze
      
      - name: Run tests
        run: dart test --coverage=coverage
      
      - name: Check coverage threshold
        run: |
          # Fail if coverage drops below 80%
          dart pub global activate coverage
          dart pub global run coverage:format_coverage \
            --lcov --in=coverage --out=coverage/lcov.info
```

**Best Practices for CI:**

1. **Run tests on every PR**: No exceptions
2. **Block merges on test failure**: Enforce quality gates
3. **Keep tests fast**: Slow CI discourages frequent commits
4. **Parallelize when possible**: Run independent tests concurrently
5. **Cache dependencies**: Speed up repeated runs

