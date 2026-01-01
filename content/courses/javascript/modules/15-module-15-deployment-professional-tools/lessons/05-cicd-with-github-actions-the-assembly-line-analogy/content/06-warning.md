---
type: "WARNING"
title: "Common Pitfalls"
---

Common CI/CD mistakes:

1. **Using npm instead of bun**:
   ```yaml
   # WRONG! Slow installs
   - run: npm install
   - run: npm test
   
   # CORRECT! 10x faster
   - uses: oven-sh/setup-bun@v2
   - run: bun install
   - run: bun test
   ```

2. **Deploying on failed tests**:
   ```yaml
   # WRONG! Deploy runs even if tests fail
   jobs:
     test:
       ...
     deploy:
       ...  # No dependency!
   
   # CORRECT! Deploy waits for test
   jobs:
     test:
       ...
     deploy:
       needs: test  # Only if test succeeds!
   ```

3. **Secrets in code**:
   ```yaml
   # WRONG! Never do this
   - run: curl -X POST https://api.render.com/hooks/abc123
   
   # CORRECT! Use GitHub secrets
   - run: curl -X POST ${{ secrets.RENDER_DEPLOY_HOOK }}
   
   # Add in: Settings → Secrets → Actions
   ```

4. **Missing workflow file**:
   ```
   Correct location:
   .github/workflows/ci.yml
   
   NOT:
   - github/workflows/ci.yml (missing dot)
   - .github/workflow/ci.yml (singular)
   - workflows/ci.yml (wrong folder)
   ```

5. **Deploying every branch**:
   ```yaml
   # WRONG! Deploys on every push
   deploy:
     ...
   
   # CORRECT! Only deploy main
   deploy:
     if: github.ref == 'refs/heads/main'
   ```

6. **Not caching dependencies**:
   ```yaml
   # Add caching for faster builds
   - name: Cache Bun
     uses: actions/cache@v4
     with:
       path: ~/.bun/install/cache
       key: ${{ runner.os }}-bun-${{ hashFiles('bun.lockb') }}
   ```

7. **No test coverage requirements**:
   ```yaml
   # Add coverage check
   - name: Run tests with coverage
     run: bun test --coverage
   
   # Fail if coverage too low
   - run: bun run check-coverage
   ```

8. **Ignoring PR checks**:
   ```yaml
   # Always run on PRs!
   on:
     pull_request:
       branches: [main]
   
   # Catch bugs BEFORE merging
   ```