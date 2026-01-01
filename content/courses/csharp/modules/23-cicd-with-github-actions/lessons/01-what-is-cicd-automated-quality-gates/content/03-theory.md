---
type: "THEORY"
title: "GitHub Actions Concepts"
---

## Workflows

A workflow is an automated process defined in a YAML file within the `.github/workflows` directory of your repository. Each workflow responds to specific events (triggers) and executes one or more jobs. You can have multiple workflows in a single repository - one for CI, another for deployment, a third for nightly builds. Workflows are version-controlled alongside your code, meaning pipeline changes go through the same review process as application changes.

## Triggers (Events)

The `on` section defines when your workflow runs. Common triggers include:
- `push`: Code pushed to specified branches
- `pull_request`: PR opened, updated, or synchronized
- `schedule`: Cron-based scheduling for periodic runs
- `workflow_dispatch`: Manual trigger from GitHub UI
- `release`: When a new release is created

You can combine multiple triggers and filter by branches, tags, or file paths. For example, only run tests when `.cs` files change, not when documentation is updated.

## Jobs

Jobs are collections of steps that execute on the same runner (virtual machine). By default, jobs run in parallel, but you can define dependencies to run them sequentially. Each job starts with a fresh environment - no files or state from other jobs unless explicitly shared. This isolation ensures reproducibility.

## Steps

Steps are the individual tasks within a job. Each step can either run a shell command (`run`) or use a pre-built action (`uses`). Steps execute sequentially, and if any step fails, subsequent steps are skipped by default. Steps can share data through environment variables, output parameters, or the filesystem.

## Actions

Actions are reusable units of workflow logic. The `uses` keyword references an action from the GitHub Marketplace or a repository. Common actions include `actions/checkout` (clone your repo), `actions/setup-dotnet` (install .NET SDK), and thousands of community-contributed actions for tasks like sending notifications, deploying to cloud providers, or analyzing code.

## Runners

Runners are the machines that execute your workflows. GitHub provides hosted runners (Ubuntu, Windows, macOS) that are free for public repositories and have usage limits for private repositories. You can also configure self-hosted runners for custom hardware, specific software requirements, or cost optimization.

## Services

Services are Docker containers that run alongside your job, providing external dependencies like databases, caches, or message queues. Services are networked with your runner, so your tests can connect to them using service names as hostnames. This enables realistic integration testing without modifying your test configuration.