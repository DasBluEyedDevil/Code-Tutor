---
type: "THEORY"
title: "GitHub Actions Basics"
---

**GitHub Actions = CI/CD built into GitHub**

**Key concepts:**

1. **Workflow** (`.github/workflows/*.yml`)
   - Automated process definition
   - Triggered by events
   - Contains one or more jobs

2. **Trigger** (`on:`)
   - When the workflow runs
   - push, pull_request, schedule, manual
   - Can filter by branch, path, etc.

3. **Job** (`jobs:`)
   - Group of steps that run together
   - Runs on a virtual machine (runner)
   - Jobs can run in parallel or sequence

4. **Step** (`steps:`)
   - Individual task within a job
   - Run commands or use actions
   - Share data via outputs

5. **Action** (`uses:`)
   - Reusable workflow component
   - Published on GitHub Marketplace
   - Example: `actions/checkout@v4`

**Workflow structure:**
```yaml
name: Workflow Name
on: [push, pull_request]

jobs:
  job-name:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      - run: echo "Hello"
```

**Common triggers:**
```yaml
on:
  push:
    branches: [main, develop]
  pull_request:
    branches: [main]
  schedule:
    - cron: '0 0 * * *'  # Daily at midnight
  workflow_dispatch:  # Manual trigger
```