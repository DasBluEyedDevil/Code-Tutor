---
type: "THEORY"
title: "Phase 5: CI/CD Pipeline (1-2 hours)"
---


### GitHub Actions Workflow


---



```yaml
# .github/workflows/ci-cd.yml
name: ShopKotlin CI/CD

on:
  push:
    branches: [ main, develop ]
  pull_request:
    branches: [ main ]

env:
  JAVA_VERSION: '17'

jobs:
  backend-test:
    name: Backend Tests
    runs-on: ubuntu-latest

    services:
      postgres:
        image: postgres:15
        env:
          POSTGRES_PASSWORD: testpass
          POSTGRES_DB: shopkotlin_test
        options: >-
          --health-cmd pg_isready
          --health-interval 10s
          --health-timeout 5s
          --health-retries 5
        ports:
          - 5432:5432

    steps:
      - uses: actions/checkout@v4

      - name: Set up JDK
        uses: actions/setup-java@v4
        with:
          java-version: ${{ env.JAVA_VERSION }}
          distribution: 'temurin'

      - name: Run backend tests
        run: |
          cd shopkotlin-backend
          ./gradlew test

      - name: Upload coverage
        uses: codecov/codecov-action@v3
        with:
          files: shopkotlin-backend/build/reports/jacoco/test/jacocoTestReport.xml

  backend-build:
    name: Build Backend
    needs: backend-test
    runs-on: ubuntu-latest

    steps:
      - uses: actions/checkout@v4

      - name: Set up JDK
        uses: actions/setup-java@v4
        with:
          java-version: ${{ env.JAVA_VERSION }}
          distribution: 'temurin'

      - name: Build JAR
        run: |
          cd shopkotlin-backend
          ./gradlew shadowJar

      - name: Build Docker image
        run: |
          cd shopkotlin-backend
          docker build -t shopkotlin-backend:latest .

      - name: Push to registry (main only)
        if: github.ref == 'refs/heads/main'
        run: |
          echo "${{ secrets.DOCKER_PASSWORD }}" | docker login -u "${{ secrets.DOCKER_USERNAME }}" --password-stdin
          docker tag shopkotlin-backend:latest ${{ secrets.DOCKER_USERNAME }}/shopkotlin-backend:latest
          docker push ${{ secrets.DOCKER_USERNAME }}/shopkotlin-backend:latest

  android-test:
    name: Android Tests
    runs-on: ubuntu-latest

    steps:
      - uses: actions/checkout@v4

      - name: Set up JDK
        uses: actions/setup-java@v4
        with:
          java-version: ${{ env.JAVA_VERSION }}
          distribution: 'temurin'

      - name: Run unit tests
        run: |
          cd shopkotlin-android
          ./gradlew test

  android-build:
    name: Build Android APK
    needs: android-test
    runs-on: ubuntu-latest

    steps:
      - uses: actions/checkout@v4

      - name: Set up JDK
        uses: actions/setup-java@v4
        with:
          java-version: ${{ env.JAVA_VERSION }}
          distribution: 'temurin'

      - name: Build debug APK
        run: |
          cd shopkotlin-android
          ./gradlew assembleDebug

      - name: Upload APK
        uses: actions/upload-artifact@v4
        with:
          name: app-debug
          path: shopkotlin-android/app/build/outputs/apk/debug/app-debug.apk

  deploy:
    name: Deploy to Production
    needs: [backend-build, android-build]
    if: github.ref == 'refs/heads/main'
    runs-on: ubuntu-latest

    steps:
      - name: Deploy to Heroku
        uses: akhileshns/heroku-deploy@v3.12.14
        with:
          heroku_api_key: ${{ secrets.HEROKU_API_KEY }}
          heroku_app_name: "shopkotlin-api"
          heroku_email: ${{ secrets.HEROKU_EMAIL }}
          appdir: "shopkotlin-backend"
```
