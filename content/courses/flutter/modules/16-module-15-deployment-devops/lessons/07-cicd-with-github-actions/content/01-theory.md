---
type: "THEORY"
title: "Introduction"
---


**What is CI/CD?**

CI/CD (Continuous Integration/Continuous Deployment) automates the process of testing, building, and deploying your Flutter and Serverpod applications. Instead of manually running tests and building releases, your code is automatically verified every time you push changes.

**The CI/CD Pipeline:**

```
Code Push → Tests → Analysis → Build → Deploy
    ↓         ↓         ↓         ↓        ↓
  GitHub   Unit &    Lint &    APK/IPA   Stores/
           Widget    Format    Artifacts  Servers
```

**Benefits of CI/CD:**

| Benefit | Description |
|---------|-------------|
| Early bug detection | Tests run on every PR, catching issues before merge |
| Consistent builds | Same environment every time, no "works on my machine" |
| Faster releases | Automated deployment reduces manual steps |
| Quality gates | Code must pass tests before merging |
| Audit trail | Every build and deployment is logged |

**GitHub Actions Basics:**

GitHub Actions uses YAML workflow files stored in `.github/workflows/`. Key concepts:

- **Workflow**: Automated process defined in a YAML file
- **Job**: A set of steps that run on the same runner
- **Step**: Individual task (run command, use action)
- **Action**: Reusable unit of code (e.g., `actions/checkout@v4`)
- **Runner**: Virtual machine that executes jobs
- **Trigger**: Event that starts a workflow (push, PR, schedule)

**Common Triggers:**

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

