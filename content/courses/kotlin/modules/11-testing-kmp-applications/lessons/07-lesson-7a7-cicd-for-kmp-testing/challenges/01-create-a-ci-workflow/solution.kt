name: KMP Tests

on:
  push:
    branches: [main, develop]
  pull_request:
    branches: [main]

env:
  JAVA_VERSION: '17'

jobs:
  jvm-tests:
    name: JVM Tests
    runs-on: ubuntu-latest
    
    steps:
      - name: Checkout
        uses: actions/checkout@v4
      
      - name: Set up JDK ${{ env.JAVA_VERSION }}
        uses: actions/setup-java@v4
        with:
          java-version: ${{ env.JAVA_VERSION }}
          distribution: 'temurin'
      
      - name: Setup Gradle
        uses: gradle/gradle-build-action@v3
        with:
          cache-read-only: ${{ github.ref != 'refs/heads/main' }}
      
      - name: Run shared tests
        run: ./gradlew :shared:jvmTest --continue
      
      - name: Run Android unit tests
        run: ./gradlew :androidApp:testDebugUnitTest --continue
      
      - name: Test Report
        uses: dorny/test-reporter@v1
        if: always()
        with:
          name: JVM Test Results
          path: '**/build/test-results/**/*.xml'
          reporter: java-junit
      
      - name: Upload test results
        uses: actions/upload-artifact@v4
        if: always()
        with:
          name: jvm-test-results
          path: |
            **/build/reports/tests/
            **/build/test-results/
          retention-days: 7

  ios-tests:
    name: iOS Tests
    runs-on: macos-14
    if: github.event_name == 'pull_request'
    
    steps:
      - name: Checkout
        uses: actions/checkout@v4
      
      - name: Set up JDK ${{ env.JAVA_VERSION }}
        uses: actions/setup-java@v4
        with:
          java-version: ${{ env.JAVA_VERSION }}
          distribution: 'temurin'
      
      - name: Setup Gradle
        uses: gradle/gradle-build-action@v3
      
      - name: Run iOS tests
        run: ./gradlew :shared:iosSimulatorArm64Test
      
      - name: Upload test results
        uses: actions/upload-artifact@v4
        if: always()
        with:
          name: ios-test-results
          path: '**/build/reports/tests/'
          retention-days: 7

  all-tests-passed:
    name: All Tests Passed
    needs: [jvm-tests]
    if: always()
    runs-on: ubuntu-latest
    
    steps:
      - name: Check test results
        run: |
          if [ "${{ needs.jvm-tests.result }}" != "success" ]; then
            echo "JVM tests failed"
            exit 1
          fi
          echo "All required tests passed!"