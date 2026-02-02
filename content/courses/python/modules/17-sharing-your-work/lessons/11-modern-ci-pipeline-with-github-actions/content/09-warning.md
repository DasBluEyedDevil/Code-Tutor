---
type: "WARNING"
title: "Common CI/CD Pitfalls"
---

### Watch out for these issues:

**1. Secrets in PRs from forks**
```yaml
# WRONG - Secrets exposed to fork PRs
- name: Deploy
  run: flyctl deploy
  env:
    FLY_API_TOKEN: ${{ secrets.FLY_API_TOKEN }}

# CORRECT - Only deploy on push to main
- name: Deploy
  if: github.event_name == 'push' && github.ref == 'refs/heads/main'
  run: flyctl deploy
  env:
    FLY_API_TOKEN: ${{ secrets.FLY_API_TOKEN }}
```

**2. Missing service health checks**
```yaml
# WRONG - Tests might start before DB is ready
services:
  postgres:
    image: postgres:16

# CORRECT - Wait for PostgreSQL to be ready
services:
  postgres:
    image: postgres:16
    options: >-
      --health-cmd pg_isready
      --health-interval 10s
      --health-timeout 5s
      --health-retries 5
```

**3. Hardcoded Python version**
```yaml
# WRONG - Breaks when Python updates
run: uv python install 3.12

# BETTER - Use matrix for multiple versions
matrix:
  python-version: ["3.11", "3.12", "3.13"]
```

**4. Not caching dependencies**
```yaml
# SLOW - Downloads everything every time
- run: uv sync --all-extras --dev

# FAST - Enable caching
- uses: astral-sh/setup-uv@v4
  with:
    enable-cache: true
```

**5. Deploying on every push**
```yaml
# DANGEROUS - Deploys untested PR code
deploy:
  runs-on: ubuntu-latest
  steps:
    - run: flyctl deploy

# SAFE - Only deploy after tests pass on main
deploy:
  needs: test
  if: github.ref == 'refs/heads/main'
```