---
type: "THEORY"
title: "Key Workflow Concepts"
---

## Workflow Triggers

Triggers define when your workflow executes. GitHub Actions supports numerous trigger types for different automation scenarios:

| Trigger | Description | Common Use Case |
|---------|-------------|----------------|
| `push` | Code pushed to repository | Run CI on every commit |
| `pull_request` | PR opened, updated, or merged | Validate changes before merge |
| `schedule` | Cron-based timing | Nightly builds, dependency updates |
| `workflow_dispatch` | Manual trigger from UI | On-demand deployments |
| `release` | Release created or published | Production deployments |
| `workflow_call` | Called by another workflow | Reusable workflow libraries |

Triggers can be filtered by branches, tags, paths, and event types. For example, `push.branches: [main]` only triggers on pushes to main, while `push.paths-ignore: ['**.md']` skips workflow runs when only markdown files change. This filtering reduces unnecessary builds and conserves your Actions minutes.

## Runners: The Execution Environment

Runners are the machines that execute your workflows. GitHub provides hosted runners with pre-installed tools:

| Runner | OS | Specs | Use Case |
|--------|---------|-------|----------|
| `ubuntu-latest` | Ubuntu 24.04 | 4 cores, 16GB RAM | Default for most workflows |
| `windows-latest` | Windows Server 2022 | 4 cores, 16GB RAM | .NET Framework, Windows-specific |
| `macos-latest` | macOS Sonoma | 3 cores, 14GB RAM | iOS/macOS builds, Xcode |

Hosted runners are ephemeral: each job gets a fresh machine, ensuring no state carries over between runs. For specialized needs, self-hosted runners allow you to use your own infrastructure with custom software, hardware, or network access.

## Job Dependencies with `needs`

By default, jobs run in parallel. The `needs` keyword creates dependencies, forcing sequential execution:

```yaml
jobs:
  build:
    runs-on: ubuntu-latest
    steps: [...]
  
  test:
    needs: build        # Waits for build to complete
    runs-on: ubuntu-latest
    steps: [...]
  
  deploy:
    needs: [build, test] # Waits for BOTH to complete
    runs-on: ubuntu-latest
    steps: [...]
```

Use `needs` when jobs depend on artifacts or state from previous jobs. Keep jobs independent when possible to maximize parallelization and reduce total pipeline duration.

## Job Outputs and Artifacts

Jobs can share data through outputs and artifacts. Outputs are small values like version strings or status flags. Artifacts are files like build outputs, test results, or logs.

**Outputs** pass small data between jobs:
```yaml
jobs:
  build:
    outputs:
      version: ${{ steps.ver.outputs.version }}
    steps:
      - id: ver
        run: echo "version=1.2.3" >> $GITHUB_OUTPUT
  
  deploy:
    needs: build
    steps:
      - run: echo "Deploying version ${{ needs.build.outputs.version }}"
```

**Artifacts** pass files between jobs or preserve them for download:
```yaml
- uses: actions/upload-artifact@v4
  with:
    name: build-output
    path: bin/Release/

# In another job:
- uses: actions/download-artifact@v4
  with:
    name: build-output
```

## Conditional Execution

The `if` keyword controls whether jobs or steps run based on context:

```yaml
jobs:
  deploy:
    if: github.ref == 'refs/heads/main'    # Only deploy from main
    
  notify:
    if: always()                             # Run even if previous jobs failed
    
steps:
  - name: Upload coverage
    if: success()                            # Only if previous steps succeeded
    
  - name: Send failure alert
    if: failure()                            # Only if previous steps failed
```

Common conditions include `success()`, `failure()`, `always()`, `cancelled()`, and expressions comparing context variables like `github.event_name`, `github.actor`, or `github.ref`.