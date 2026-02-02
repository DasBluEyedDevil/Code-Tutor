---
type: "EXAMPLE"
title: "GitHub Actions with Bun"
---

A complete CI/CD pipeline that runs on every push using Bun for maximum speed.

```yaml
# .github/workflows/ci.yml
# CI/CD Pipeline with Bun - runs on every push

name: CI/CD Pipeline

on:
  push:
    branches: [main, develop]
  pull_request:
    branches: [main]

jobs:
  test:
    name: Test & Build
    runs-on: ubuntu-latest
    
    steps:
      # 1. Check out the code
      - name: Checkout repository
        uses: actions/checkout@v4
      
      # 2. Set up Bun (instead of Node.js!)
      - name: Setup Bun
        uses: oven-sh/setup-bun@v2
        with:
          bun-version: latest
      
      # 3. Install dependencies (10x faster than npm!)
      - name: Install dependencies
        run: bun install
      
      # 4. Run linting
      - name: Lint code
        run: bun run lint
      
      # 5. Run tests
      - name: Run tests
        run: bun test
      
      # 6. Build the project
      - name: Build
        run: bun run build
      
      # 7. Upload build artifacts
      - name: Upload build
        uses: actions/upload-artifact@v4
        with:
          name: build
          path: dist/

  deploy:
    name: Deploy to Production
    needs: test  # Only runs if tests pass!
    runs-on: ubuntu-latest
    if: github.ref == 'refs/heads/main'  # Only on main branch
    
    steps:
      - name: Checkout
        uses: actions/checkout@v4
      
      - name: Download build
        uses: actions/download-artifact@v4
        with:
          name: build
          path: dist/
      
      - name: Deploy to Render
        run: |
          curl -X POST ${{ secrets.RENDER_DEPLOY_HOOK }}
        # Or deploy to Cloudflare Workers:
        # - name: Deploy to Workers
        #   run: bunx wrangler deploy
```
