---
type: "THEORY"
title: "Understanding the Workflow"
---

WORKFLOW STRUCTURE:

name: CI/CD Pipeline           # Display name in GitHub UI

on:                             # Trigger events
  push:
    branches: [main, develop]   # Only these branches
  pull_request:
    branches: [main]            # PRs targeting main

jobs:                           # What to do

JOB STRUCTURE:

test:
  name: Build and Test          # Display name
  runs-on: ubuntu-latest        # Virtual machine type
  steps:                        # Sequential tasks

COMMON RUNNERS:
- ubuntu-latest: Linux (most common)
- windows-latest: Windows
- macos-latest: macOS (expensive minutes)

JOB DEPENDENCIES:

build:
  needs: test                   # Wait for 'test' to pass
  if: github.ref == 'refs/heads/main'  # Only on main branch

CONDITIONAL EXECUTION:

- if: always()                  # Run even if previous step failed
- if: success()                 # Only if previous steps succeeded (default)
- if: failure()                 # Only if previous steps failed
- if: github.event_name == 'push'
- if: github.ref == 'refs/heads/main'