---
type: "EXAMPLE"
title: "Complete KMP CI/CD Workflow"
---

Full workflow for building and testing KMP apps:

```yaml
# .github/workflows/build.yml
name: Build and Test KMP

on:
  push:
    branches: [main, develop]
  pull_request:
    branches: [main]

env:
  JAVA_VERSION: '17'
  GRADLE_OPTS: -Dorg.gradle.jvmargs="-Xmx4g -XX:+HeapDumpOnOutOfMemoryError"

jobs:
  # ===== ANDROID BUILD =====
  build-android:
    name: Build Android
    runs-on: ubuntu-latest
    
    steps:
      - name: Checkout
        uses: actions/checkout@v4
      
      - name: Set up JDK
        uses: actions/setup-java@v4
        with:
          java-version: ${{ env.JAVA_VERSION }}
          distribution: 'temurin'
      
      - name: Setup Gradle
        uses: gradle/gradle-build-action@v3
        with:
          cache-read-only: ${{ github.ref != 'refs/heads/main' }}
      
      - name: Build Android Debug
        run: ./gradlew :composeApp:assembleDebug
      
      - name: Run Android Unit Tests
        run: ./gradlew :composeApp:testDebugUnitTest
      
      - name: Upload APK
        uses: actions/upload-artifact@v4
        with:
          name: android-debug-apk
          path: composeApp/build/outputs/apk/debug/*.apk
          retention-days: 7
      
      - name: Upload Test Results
        if: always()
        uses: actions/upload-artifact@v4
        with:
          name: android-test-results
          path: '**/build/reports/tests/'

  # ===== IOS BUILD =====
  build-ios:
    name: Build iOS
    runs-on: macos-14
    
    steps:
      - name: Checkout
        uses: actions/checkout@v4
      
      - name: Set up JDK
        uses: actions/setup-java@v4
        with:
          java-version: ${{ env.JAVA_VERSION }}
          distribution: 'temurin'
      
      - name: Setup Gradle
        uses: gradle/gradle-build-action@v3
      
      - name: Build iOS Framework
        run: ./gradlew :shared:linkDebugFrameworkIosSimulatorArm64
      
      - name: Build iOS App (Simulator)
        run: |
          cd iosApp
          xcodebuild build \
            -scheme iosApp \
            -configuration Debug \
            -destination 'platform=iOS Simulator,name=iPhone 15 Pro' \
            -derivedDataPath build
      
      - name: Run iOS Tests
        run: ./gradlew :shared:iosSimulatorArm64Test

  # ===== SHARED TESTS (JVM) =====
  test-shared:
    name: Shared Tests
    runs-on: ubuntu-latest
    
    steps:
      - name: Checkout
        uses: actions/checkout@v4
      
      - name: Set up JDK
        uses: actions/setup-java@v4
        with:
          java-version: ${{ env.JAVA_VERSION }}
          distribution: 'temurin'
      
      - name: Setup Gradle
        uses: gradle/gradle-build-action@v3
      
      - name: Run Shared Tests on JVM
        run: ./gradlew :shared:jvmTest
      
      - name: Generate Coverage Report
        run: ./gradlew koverXmlReport
      
      - name: Upload Coverage to Codecov
        uses: codecov/codecov-action@v4
        with:
          token: ${{ secrets.CODECOV_TOKEN }}
          files: '**/build/reports/kover/report.xml'
      
      - name: Publish Test Results
        uses: EnricoMi/publish-unit-test-result-action@v2
        if: always()
        with:
          files: '**/build/test-results/**/*.xml'
```
