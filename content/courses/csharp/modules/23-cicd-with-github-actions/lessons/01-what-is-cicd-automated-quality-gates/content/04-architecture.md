---
type: "ARCHITECTURE"
title: "Designing Build Pipelines"
---

## Pipeline Stages

A well-designed CI/CD pipeline follows a funnel pattern: fast checks run first, slow checks run last, and failures exit early. This minimizes wasted compute time and provides rapid feedback. For ShopFlow, our stages are:

**Stage 1: Build (seconds)**
Compile the code and verify it produces valid output. This catches syntax errors, missing dependencies, and type mismatches immediately. If the code does not compile, there is no point running tests.

**Stage 2: Unit Tests (seconds to minutes)**
Run fast, isolated tests that verify individual components work correctly. Unit tests have no external dependencies and should complete in under a minute for most projects. Failed unit tests indicate broken logic that must be fixed before proceeding.

**Stage 3: Integration Tests (minutes)**
Test components working together with real external dependencies (databases, APIs). These tests are slower but catch issues that unit tests miss: database queries that do not work in production, serialization problems, and configuration errors.

**Stage 4: Code Quality (parallel with tests)**
Run linters, static analyzers, and security scanners. These tools catch potential bugs, style violations, and security vulnerabilities. Running in parallel with tests optimizes total pipeline time.

**Stage 5: Deployment (minutes)**
If all checks pass on the main branch, automatically deploy to staging or production. This stage only runs for specific branches or events, not for every PR.

## Parallel vs Sequential Execution

GitHub Actions runs jobs in parallel by default. Use this for independent tasks:

```yaml
jobs:
  test-unit:
    runs-on: ubuntu-latest
    steps: [unit test steps]
  
  test-integration:
    runs-on: ubuntu-latest
    steps: [integration test steps]
  
  lint:
    runs-on: ubuntu-latest
    steps: [linting steps]
```

All three jobs run simultaneously, reducing total pipeline time. For dependent jobs, use `needs`:

```yaml
jobs:
  build:
    runs-on: ubuntu-latest
    steps: [build steps]
  
  deploy:
    needs: build  # Wait for build to complete
    runs-on: ubuntu-latest
    steps: [deploy steps]
```

## Environment Management

Different environments (development, staging, production) require different configurations. GitHub Actions supports this through:

**Environment Secrets**: Sensitive values like API keys and connection strings are stored encrypted in GitHub and injected at runtime. Never commit secrets to your repository.

**Environment Variables**: Non-sensitive configuration passed via the `env` key at workflow, job, or step level. Variables at narrower scopes override broader ones.

**GitHub Environments**: Named environments (staging, production) with protection rules requiring manual approval, specific reviewers, or wait timers before deployment.

## Caching for Performance

Dependency restoration can be slow. GitHub Actions provides caching to reuse downloaded packages:

```yaml
- uses: actions/cache@v4
  with:
    path: ~/.nuget/packages
    key: ${{ runner.os }}-nuget-${{ hashFiles('**/*.csproj') }}
```

This caches NuGet packages based on project file hashes. When dependencies do not change, the cache is reused, dramatically reducing pipeline time.

## Branch Protection Rules

CI pipelines are most effective when enforced. Configure branch protection rules requiring:
- All status checks to pass before merging
- PR reviews from team members
- Up-to-date branches (rebased on latest main)
- No force pushes or deletions

This ensures every change to main has passed all quality gates, making main always deployable.

## Monitoring and Notifications

Pipeline failures need immediate attention. Configure notifications via:
- GitHub's built-in email notifications
- Slack/Teams integrations for team channels
- Custom webhooks for advanced routing

Track pipeline metrics over time: average duration, failure rate, flaky test frequency. Slow or unreliable pipelines reduce developer productivity and erode trust in the process.