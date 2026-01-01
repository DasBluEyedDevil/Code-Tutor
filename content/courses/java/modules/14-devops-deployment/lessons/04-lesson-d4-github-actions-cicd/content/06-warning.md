---
type: "WARNING"
title: "Common GitHub Actions Pitfalls"
---

PITFALL 1: Not caching dependencies

# BAD - downloads dependencies every run (slow!)
- uses: actions/setup-java@v4
  with:
    java-version: '21'
    distribution: 'temurin'

# GOOD - caches Maven dependencies
- uses: actions/setup-java@v4
  with:
    java-version: '21'
    distribution: 'temurin'
    cache: 'maven'

PITFALL 2: Secrets in logs

# BAD - prints secret to logs
- run: echo ${{ secrets.API_KEY }}

# GOOD - GitHub masks secrets, but be careful
- run: ./deploy.sh
  env:
    API_KEY: ${{ secrets.API_KEY }}

PITFALL 3: Not failing fast

# If one test fails, stop immediately
jobs:
  test:
    strategy:
      fail-fast: true
      matrix:
        java: [17, 21]

PITFALL 4: Ignoring exit codes

# BAD - continues even if command fails
- run: |
    ./risky-command || true
    ./next-command

# GOOD - let failures stop the workflow
- run: ./risky-command
- run: ./next-command