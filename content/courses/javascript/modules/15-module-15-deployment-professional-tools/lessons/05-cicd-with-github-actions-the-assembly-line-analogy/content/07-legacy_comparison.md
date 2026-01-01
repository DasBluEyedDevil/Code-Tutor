---
type: "LEGACY_COMPARISON"
title: "npm/Node.js Equivalent"
---

Here's the same CI/CD workflow using traditional npm and Node.js. Notice the slower install times and different setup.

```yaml
# .github/workflows/ci.yml (npm version)
name: CI/CD Pipeline (npm)

on:
  push:
    branches: [main, develop]
  pull_request:
    branches: [main]

jobs:
  test:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      
      # Node.js setup (instead of Bun)
      - name: Setup Node.js
        uses: actions/setup-node@v4
        with:
          node-version: '20'
          cache: 'npm'
      
      # npm install (slower than bun)
      - name: Install dependencies
        run: npm ci
      
      # Run tests with npm
      - run: npm run lint
      - run: npm test
      - run: npm run build
      
      - uses: actions/upload-artifact@v4
        with:
          name: build
          path: dist/

  deploy:
    needs: test
    runs-on: ubuntu-latest
    if: github.ref == 'refs/heads/main'
    steps:
      - uses: actions/checkout@v4
      - run: curl -X POST ${{ secrets.RENDER_DEPLOY_HOOK }}

# Key Differences:
# - setup-node instead of oven-sh/setup-bun
# - npm ci instead of bun install (slower)
# - npm test instead of bun test
# - Separate test runner (Jest/Vitest vs bun test)
# - Build times typically 2-5x slower
```
