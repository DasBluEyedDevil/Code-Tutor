name: PR Check

on:
  pull_request:
    branches: [main]

env:
  JAVA_VERSION: '17'

jobs:
  build-and-test:
    name: Build and Test
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
          cache-read-only: true
      
      - name: Build Android Debug
        run: ./gradlew :composeApp:assembleDebug
      
      - name: Run Shared Tests
        run: ./gradlew :shared:jvmTest
      
      - name: Publish Test Results
        uses: EnricoMi/publish-unit-test-result-action@v2
        if: always()
        with:
          files: '**/build/test-results/**/*.xml'
          comment_mode: always
          check_name: Test Results
      
      - name: Upload APK
        uses: actions/upload-artifact@v4
        with:
          name: pr-${{ github.event.number }}-apk
          path: composeApp/build/outputs/apk/debug/*.apk
          retention-days: 3