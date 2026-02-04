---
type: "EXAMPLE"
title: "Advanced Workflows"
---


Matrix builds, caching strategies, and reusable workflows for efficient CI/CD:



```yaml
# .github/workflows/advanced-ci.yml
name: Advanced Flutter CI

on:
  push:
    branches: [main, develop]
  pull_request:
    branches: [main]

env:
  FLUTTER_VERSION: '3.38.0'

jobs:
  # Matrix build for multiple Flutter versions
  test-matrix:
    name: Test on Flutter ${{ matrix.flutter-version }}
    runs-on: ${{ matrix.os }}
    
    strategy:
      fail-fast: false
      matrix:
        os: [ubuntu-latest, macos-latest, windows-latest]
        flutter-version: ['3.36.0', '3.38.0']
        exclude:
          # Skip older Flutter on Windows for speed
          - os: windows-latest
            flutter-version: '3.36.0'
    
    steps:
      - uses: actions/checkout@v4

      - name: Setup Flutter
        uses: subosito/flutter-action@v2
        with:
          flutter-version: ${{ matrix.flutter-version }}
          cache: true
          cache-key: flutter-${{ matrix.os }}-${{ matrix.flutter-version }}

      - name: Install dependencies
        run: flutter pub get

      - name: Run tests
        run: flutter test

  # Optimized caching for faster builds
  build-with-cache:
    name: Build with Advanced Caching
    runs-on: ubuntu-latest
    
    steps:
      - uses: actions/checkout@v4

      - name: Setup Flutter
        uses: subosito/flutter-action@v2
        with:
          flutter-version: ${{ env.FLUTTER_VERSION }}
          cache: true

      # Cache Gradle for Android builds
      - name: Cache Gradle
        uses: actions/cache@v4
        with:
          path: |
            ~/.gradle/caches
            ~/.gradle/wrapper
          key: gradle-${{ runner.os }}-${{ hashFiles('**/*.gradle*', '**/gradle-wrapper.properties') }}
          restore-keys: gradle-${{ runner.os }}-

      # Cache pub packages
      - name: Cache Pub packages
        uses: actions/cache@v4
        with:
          path: |
            ~/.pub-cache
            .dart_tool
          key: pub-${{ runner.os }}-${{ hashFiles('**/pubspec.lock') }}
          restore-keys: pub-${{ runner.os }}-

      - run: flutter pub get
      - run: flutter build apk --release

---

# .github/workflows/reusable-flutter-test.yml
# Reusable workflow that can be called from other workflows
name: Reusable Flutter Test

on:
  workflow_call:
    inputs:
      flutter-version:
        required: false
        type: string
        default: '3.38.0'
      working-directory:
        required: false
        type: string
        default: '.'
    secrets:
      codecov-token:
        required: false

jobs:
  test:
    runs-on: ubuntu-latest
    defaults:
      run:
        working-directory: ${{ inputs.working-directory }}
    
    steps:
      - uses: actions/checkout@v4

      - uses: subosito/flutter-action@v2
        with:
          flutter-version: ${{ inputs.flutter-version }}
          cache: true

      - run: flutter pub get
      - run: dart format --set-exit-if-changed .
      - run: flutter analyze
      - run: flutter test --coverage

      - name: Upload coverage
        if: ${{ secrets.codecov-token != '' }}
        uses: codecov/codecov-action@v4
        with:
          token: ${{ secrets.codecov-token }}
          files: coverage/lcov.info

---

# .github/workflows/monorepo-ci.yml
# CI for Flutter + Serverpod monorepo
name: Monorepo CI

on:
  push:
    branches: [main]
  pull_request:

jobs:
  # Determine which packages changed
  changes:
    runs-on: ubuntu-latest
    outputs:
      flutter: ${{ steps.filter.outputs.flutter }}
      server: ${{ steps.filter.outputs.server }}
      client: ${{ steps.filter.outputs.client }}
    steps:
      - uses: actions/checkout@v4
      - uses: dorny/paths-filter@v3
        id: filter
        with:
          filters: |
            flutter:
              - 'my_app_flutter/**'
            server:
              - 'my_app_server/**'
            client:
              - 'my_app_client/**'

  # Test Flutter app only if it changed
  test-flutter:
    needs: changes
    if: ${{ needs.changes.outputs.flutter == 'true' }}
    uses: ./.github/workflows/reusable-flutter-test.yml
    with:
      working-directory: my_app_flutter
    secrets:
      codecov-token: ${{ secrets.CODECOV_TOKEN }}

  # Test server only if it changed
  test-server:
    needs: changes
    if: ${{ needs.changes.outputs.server == 'true' }}
    runs-on: ubuntu-latest
    defaults:
      run:
        working-directory: my_app_server
    
    services:
      postgres:
        image: postgres:16
        env:
          POSTGRES_PASSWORD: postgres
          POSTGRES_DB: test_db
        ports:
          - 5432:5432
        options: >-
          --health-cmd pg_isready
          --health-interval 10s
          --health-timeout 5s
          --health-retries 5
    
    steps:
      - uses: actions/checkout@v4
      
      - uses: dart-lang/setup-dart@v1
        with:
          sdk: stable

      - run: dart pub get
      - run: dart analyze
      - run: dart test

  # Require all relevant tests to pass
  all-tests:
    runs-on: ubuntu-latest
    needs: [changes, test-flutter, test-server]
    if: always()
    steps:
      - name: Check test results
        run: |
          if [[ "${{ needs.test-flutter.result }}" == "failure" ]] || \
             [[ "${{ needs.test-server.result }}" == "failure" ]]; then
            exit 1
          fi
```
