---
type: "THEORY"
title: "GitHub Actions Fundamentals"
---


### What is GitHub Actions?

GitHub Actions is a CI/CD platform that runs workflows when events occur in your repository.

**Events**: Push, pull request, release, schedule, manual trigger
**Runners**: Ubuntu, Windows, macOS virtual machines
**Actions**: Reusable workflow steps

### Basic Workflow

**.github/workflows/build.yml**:

**What happens**:
1. Code is checked out
2. JDK 17 is installed
3. Gradle builds the project
4. Tests run
5. Test results are uploaded (even if tests fail)

---



```yaml
name: Build and Test

on:
  push:
    branches: [ main, develop ]
  pull_request:
    branches: [ main ]

jobs:
  build:
    runs-on: ubuntu-latest

    steps:
      - name: Checkout code
        uses: actions/checkout@v4

      - name: Set up JDK 17
        uses: actions/setup-java@v4
        with:
          java-version: '17'
          distribution: 'temurin'

      - name: Grant execute permission for gradlew
        run: chmod +x gradlew

      - name: Build with Gradle
        run: ./gradlew build

      - name: Run tests
        run: ./gradlew test

      - name: Upload test results
        if: always()
        uses: actions/upload-artifact@v4
        with:
          name: test-results
          path: build/test-results/
```
