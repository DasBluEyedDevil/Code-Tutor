---
type: "EXAMPLE"
title: "Flutter CI Workflow"
---


A complete CI workflow that runs tests and static analysis on every pull request:



```yaml
# .github/workflows/flutter-ci.yml
name: Flutter CI

on:
  push:
    branches: [main, develop]
    paths:
      - 'lib/**'
      - 'test/**'
      - 'pubspec.yaml'
      - '.github/workflows/flutter-ci.yml'
  pull_request:
    branches: [main]

env:
  FLUTTER_VERSION: '3.24.0'

jobs:
  analyze:
    name: Analyze & Test
    runs-on: ubuntu-latest
    
    steps:
      - name: Checkout code
        uses: actions/checkout@v4

      - name: Setup Flutter
        uses: subosito/flutter-action@v2
        with:
          flutter-version: ${{ env.FLUTTER_VERSION }}
          channel: stable
          cache: true

      - name: Install dependencies
        run: flutter pub get

      - name: Verify formatting
        run: dart format --set-exit-if-changed .

      - name: Analyze code
        run: flutter analyze --fatal-infos

      - name: Run tests with coverage
        run: flutter test --coverage

      - name: Upload coverage to Codecov
        uses: codecov/codecov-action@v4
        with:
          files: coverage/lcov.info
          fail_ci_if_error: true
          token: ${{ secrets.CODECOV_TOKEN }}

  # Separate job for widget tests (can run in parallel)
  widget-tests:
    name: Widget Tests
    runs-on: ubuntu-latest
    
    steps:
      - uses: actions/checkout@v4
      
      - uses: subosito/flutter-action@v2
        with:
          flutter-version: ${{ env.FLUTTER_VERSION }}
          cache: true

      - run: flutter pub get
      
      - name: Run widget tests
        run: flutter test test/widget_test.dart --reporter=github

  # Integration tests on multiple platforms
  integration-tests:
    name: Integration Tests
    runs-on: macos-latest
    
    steps:
      - uses: actions/checkout@v4
      
      - uses: subosito/flutter-action@v2
        with:
          flutter-version: ${{ env.FLUTTER_VERSION }}
          cache: true

      - run: flutter pub get

      - name: Start iOS Simulator
        run: |
          UDID=$(xcrun simctl list devices available -j | jq -r '.devices | to_entries | map(select(.key | contains("iOS"))) | .[0].value | .[0].udid')
          xcrun simctl boot $UDID

      - name: Run integration tests
        run: flutter test integration_test --device-id=iPhone
```
