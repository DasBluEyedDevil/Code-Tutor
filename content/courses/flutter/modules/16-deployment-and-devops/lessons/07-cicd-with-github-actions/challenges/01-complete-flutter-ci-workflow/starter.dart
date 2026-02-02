# .github/workflows/flutter-ci.yml
name: Flutter CI

on:
  pull_request:
    branches: [___]

jobs:
  test:
    name: Test & Analyze
    runs-on: ubuntu-latest
    
    steps:
      - name: Checkout code
        uses: actions/checkout@v4

      - name: Setup Flutter
        uses: subosito/flutter-action@v2
        with:
          flutter-version: '___'
          channel: stable
          cache: ___

      - name: Install dependencies
        run: flutter pub get

      # TODO: Add formatting check
      - name: Check formatting
        run: ___

      # TODO: Add static analysis
      - name: Analyze code
        run: ___

      # TODO: Run tests with coverage
      - name: Run tests
        run: ___

      # TODO: Upload coverage
      - name: Upload coverage
        uses: codecov/codecov-action@v4
        with:
          files: ___
          token: ${{ secrets.CODECOV_TOKEN }}