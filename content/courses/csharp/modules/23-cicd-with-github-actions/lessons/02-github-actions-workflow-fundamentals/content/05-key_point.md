---
type: "KEY_POINT"
title: "GitHub Actions Workflow Structure"
---

## Key Takeaways

- **Triggers define when workflows run** -- `push` for every commit, `pull_request` for PR validation, `schedule` for nightly builds, `workflow_dispatch` for manual triggers. Filter by branches and file paths to avoid unnecessary runs.

- **Jobs run in parallel by default** -- use `needs: [build]` to create dependencies between jobs. Each job runs on a fresh virtual machine, so state must be shared via artifacts or caching.

- **Steps are sequential within a job** -- each step is either a `uses` (pre-built action) or `run` (shell command). Use `actions/setup-dotnet@v4` to install .NET, then `dotnet build` and `dotnet test`.
