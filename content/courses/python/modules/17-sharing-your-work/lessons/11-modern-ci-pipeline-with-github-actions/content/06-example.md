---
type: "EXAMPLE"
title: "Secrets and Environment Variables"
---

**Keep sensitive data secure in GitHub Actions:**

```yaml
# ============================================
# USING GITHUB SECRETS
# ============================================
# First, add secrets in GitHub:
# Repository Settings > Secrets and variables > Actions

# In your workflow:
steps:
  - name: Deploy to Fly.io
    run: flyctl deploy --remote-only
    env:
      FLY_API_TOKEN: ${{ secrets.FLY_API_TOKEN }}

  - name: Configure AWS
    run: aws configure set aws_access_key_id $AWS_KEY
    env:
      AWS_KEY: ${{ secrets.AWS_ACCESS_KEY_ID }}
      AWS_SECRET: ${{ secrets.AWS_SECRET_ACCESS_KEY }}

# ============================================
# BUILT-IN VARIABLES
# ============================================
steps:
  - name: Show context
    run: |
      echo "Repository: ${{ github.repository }}"
      echo "Branch: ${{ github.ref_name }}"
      echo "SHA: ${{ github.sha }}"
      echo "Actor: ${{ github.actor }}"
      echo "Event: ${{ github.event_name }}"
      echo "Run ID: ${{ github.run_id }}"

# ============================================
# ENVIRONMENT FILES
# ============================================
# Create a .env file for tests
steps:
  - name: Create test environment
    run: |
      cat > .env.test << EOF
      DATABASE_URL=postgresql+asyncpg://test:test@localhost:5432/test
      SECRET_KEY=test-secret-key
      DEBUG=true
      EOF

# ============================================
# CONDITIONAL SECRETS (for PRs from forks)
# ============================================
# Secrets are NOT available to PRs from forks (security)
steps:
  - name: Deploy (only if secrets available)
    if: github.event_name != 'pull_request' || github.event.pull_request.head.repo.full_name == github.repository
    run: flyctl deploy
    env:
      FLY_API_TOKEN: ${{ secrets.FLY_API_TOKEN }}
```
