---
type: "EXAMPLE"
title: "GitHub Actions Workflow"
---

Complete CI workflow for KMP testing:

```yaml
# .github/workflows/test.yml
name: Test KMP

on:
  push:
    branches: [main, develop]
  pull_request:
    branches: [main]

jobs:
  # Fast JVM tests - run on every commit
  jvm-tests:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      
      - name: Set up JDK 17
        uses: actions/setup-java@v4
        with:
          java-version: '17'
          distribution: 'temurin'
      
      - name: Setup Gradle
        uses: gradle/gradle-build-action@v3
        with:
          cache-read-only: ${{ github.ref != 'refs/heads/main' }}
      
      - name: Run shared tests on JVM
        run: ./gradlew :shared:jvmTest
      
      - name: Run Android unit tests
        run: ./gradlew :androidApp:testDebugUnitTest
      
      - name: Upload test results
        if: always()
        uses: actions/upload-artifact@v4
        with:
          name: jvm-test-results
          path: '**/build/reports/tests/'

  # iOS tests - only on PRs and main (expensive)
  ios-tests:
    runs-on: macos-14
    if: github.event_name == 'pull_request' || github.ref == 'refs/heads/main'
    steps:
      - uses: actions/checkout@v4
      
      - name: Set up JDK 17
        uses: actions/setup-java@v4
        with:
          java-version: '17'
          distribution: 'temurin'
      
      - name: Run iOS tests
        run: ./gradlew :shared:iosSimulatorArm64Test
      
      - name: Upload test results
        if: always()
        uses: actions/upload-artifact@v4
        with:
          name: ios-test-results
          path: '**/build/reports/tests/'

  # Android instrumented tests - only on PRs
  android-instrumented:
    runs-on: ubuntu-latest
    if: github.event_name == 'pull_request'
    steps:
      - uses: actions/checkout@v4
      
      - name: Set up JDK 17
        uses: actions/setup-java@v4
        with:
          java-version: '17'
          distribution: 'temurin'
      
      - name: Enable KVM
        run: |
          echo 'KERNEL=="kvm", GROUP="kvm", MODE="0666", OPTIONS+="static_node=kvm"' | sudo tee /etc/udev/rules.d/99-kvm4all.rules
          sudo udevadm control --reload-rules
          sudo udevadm trigger --name-match=kvm
      
      - name: Run instrumented tests
        uses: reactivecircus/android-emulator-runner@v2
        with:
          api-level: 30
          arch: x86_64
          script: ./gradlew :androidApp:connectedDebugAndroidTest
      
      - name: Upload test results
        if: always()
        uses: actions/upload-artifact@v4
        with:
          name: android-instrumented-results
          path: '**/build/reports/androidTests/'
```
