# .github/workflows/release.yml
name: Release Pipeline

on:
  push:
    tags:
      - 'v*.*.*'

env:
  FLUTTER_VERSION: '3.24.0'

jobs:
  validate:
    runs-on: ubuntu-latest
    outputs:
      version: ${{ steps.version.outputs.value }}
      changelog: ${{ steps.changelog.outputs.content }}
    steps:
      - uses: actions/checkout@v4
        with:
          fetch-depth: 0

      - name: Extract version from tag
        id: version
        run: |
          # TODO: Extract version from GITHUB_REF
          # TODO: Validate version format
          # TODO: Compare with pubspec.yaml version
          echo "value=" >> $GITHUB_OUTPUT

      - name: Generate changelog
        id: changelog
        run: |
          # TODO: Get commits since last tag
          # TODO: Group by type (feat, fix, etc.)
          # TODO: Format as markdown
          echo "content=" >> $GITHUB_OUTPUT

  test:
    needs: validate
    runs-on: ubuntu-latest
    steps:
      # TODO: Run unit tests
      # TODO: Run integration tests
      # TODO: Check code coverage threshold
      # TODO: Fail if tests don't pass

  build-android:
    needs: test
    runs-on: ubuntu-latest
    steps:
      # TODO: Build release app bundle
      # TODO: Sign with release keystore
      # TODO: Upload debug symbols
      # TODO: Save artifact

  build-ios:
    needs: test
    runs-on: macos-latest
    steps:
      # TODO: Install certificates
      # TODO: Build release IPA
      # TODO: Upload dSYMs
      # TODO: Save artifact

  deploy-beta:
    needs: [build-android, build-ios]
    runs-on: ubuntu-latest
    steps:
      # TODO: Deploy Android to Internal track
      # TODO: Deploy iOS to TestFlight

  create-release:
    needs: deploy-beta
    runs-on: ubuntu-latest
    steps:
      # TODO: Create GitHub release
      # TODO: Attach artifacts
      # TODO: Add changelog

  promote-production:
    needs: create-release
    runs-on: ubuntu-latest
    environment: production  # Requires manual approval
    steps:
      # TODO: Promote Android to Production (staged)
      # TODO: Submit iOS for review

  notify:
    needs: [deploy-beta, promote-production]
    if: always()
    runs-on: ubuntu-latest
    steps:
      # TODO: Send Slack notification with results