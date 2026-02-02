---
type: "EXAMPLE"
title: "GitHub Actions Workflow with Bun"
---

This GitHub Actions workflow runs your tests automatically on every push and pull request using Bun's built-in test runner.

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

      - name: Setup Bun
        uses: oven-sh/setup-bun@v2
        with:
          bun-version: latest

      - name: Install dependencies
        run: bun install

      - name: Run tests with coverage
        run: bun test --coverage

      - name: Run tests (fail if any fail)
        run: bun test --bail
```
