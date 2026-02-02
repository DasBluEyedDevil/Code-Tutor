---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Understanding the GitHub Actions workflow with Bun:

1. **Workflow File Location**:
   ```
   .github/workflows/ci.yml
   
   GitHub automatically detects YAML files here and runs them
   ```

2. **Triggers (on)**:
   ```yaml
   on:
     push:
       branches: [main, develop]  # Run on push to these branches
     pull_request:
       branches: [main]           # Run on PRs to main
   ```

3. **Setting up Bun** (key difference from npm!):
   ```yaml
   - name: Setup Bun
     uses: oven-sh/setup-bun@v2
     with:
       bun-version: latest
   ```

4. **Bun Commands** (faster than npm!):
   ```yaml
   # Install dependencies (10x faster)
   - run: bun install
   
   # Run tests (native test runner)
   - run: bun test
   
   # Build project
   - run: bun run build
   
   # Run scripts from package.json
   - run: bun run lint
   ```

5. **Job Dependencies** (deploy only if tests pass):
   ```yaml
   deploy:
     needs: test  # Wait for test job to succeed
     if: github.ref == 'refs/heads/main'  # Only on main
   ```

6. **Secrets** (never hardcode!):
   ```yaml
   - run: curl -X POST ${{ secrets.RENDER_DEPLOY_HOOK }}
   
   # Add secrets in: GitHub → Repo → Settings → Secrets
   ```

7. **Caching** (speed up future runs):
   ```yaml
   - name: Cache Bun dependencies
     uses: actions/cache@v4
     with:
       path: ~/.bun/install/cache
       key: ${{ runner.os }}-bun-${{ hashFiles('bun.lockb') }}
   ```

8. **Matrix Testing** (test multiple versions):
   ```yaml
   strategy:
     matrix:
       bun-version: ['1.0', '1.1', 'latest']
   steps:
     - uses: oven-sh/setup-bun@v2
       with:
         bun-version: ${{ matrix.bun-version }}
   ```

9. **Package.json Scripts for CI**:
   ```json
   {
     "scripts": {
       "test": "bun test",
       "lint": "bunx biome check .",
       "build": "bun run build:app",
       "typecheck": "bunx tsc --noEmit"
     }
   }
   ```

10. **Complete CI Pipeline Order**:
    ```
    1. Checkout code (actions/checkout)
    2. Setup Bun (oven-sh/setup-bun)
    3. Install dependencies (bun install)
    4. Lint code (bun run lint)
    5. Type check (bunx tsc --noEmit)
    6. Run tests (bun test)
    7. Build (bun run build)
    8. Deploy (if main branch and tests pass)
    ```