---
type: "THEORY"
title: "Solution 3"
---


**.github/workflows/release.yml**:

---



```yaml
name: Automated Release

on:
  workflow_dispatch:
    inputs:
      version_bump:
        description: 'Version bump type'
        required: true
        type: choice
        options:
          - patch
          - minor
          - major

jobs:
  release:
    runs-on: ubuntu-latest

    steps:
      - uses: actions/checkout@v4
        with:
          fetch-depth: 0
          token: ${{ secrets.GITHUB_TOKEN }}

      - name: Set up JDK
        uses: actions/setup-java@v4
        with:
          java-version: '17'
          distribution: 'temurin'

      - name: Get current version
        id: current_version
        run: |
          VERSION=$(grep "versionName" app/build.gradle.kts | sed 's/.*"\(.*\)".*/\1/')
          echo "version=$VERSION" >> $GITHUB_OUTPUT

      - name: Bump version
        id: bump_version
        run: |
          CURRENT="${{ steps.current_version.outputs.version }}"
          IFS='.' read -ra PARTS <<< "$CURRENT"
          MAJOR=${PARTS[0]}
          MINOR=${PARTS[1]}
          PATCH=${PARTS[2]}

          case "${{ github.event.inputs.version_bump }}" in
            major)
              MAJOR=$((MAJOR + 1))
              MINOR=0
              PATCH=0
              ;;
            minor)
              MINOR=$((MINOR + 1))
              PATCH=0
              ;;
            patch)
              PATCH=$((PATCH + 1))
              ;;
          esac

          NEW_VERSION="$MAJOR.$MINOR.$PATCH"
          echo "new_version=$NEW_VERSION" >> $GITHUB_OUTPUT

      - name: Update version in build.gradle.kts
        run: |
          sed -i 's/versionName = ".*"/versionName = "${{ steps.bump_version.outputs.new_version }}"/' app/build.gradle.kts

      - name: Generate changelog
        id: changelog
        run: |
          PREVIOUS_TAG=$(git describe --tags --abbrev=0)
          CHANGELOG=$(git log ${PREVIOUS_TAG}..HEAD --pretty=format:"- %s")
          echo "changelog<<EOF" >> $GITHUB_OUTPUT
          echo "$CHANGELOG" >> $GITHUB_OUTPUT
          echo "EOF" >> $GITHUB_OUTPUT

      - name: Commit version bump
        run: |
          git config user.name "GitHub Actions"
          git config user.email "actions@github.com"
          git add app/build.gradle.kts
          git commit -m "chore: bump version to ${{ steps.bump_version.outputs.new_version }}"
          git push

      - name: Create GitHub Release
        uses: actions/create-release@v1
        env:
          GITHUB_TOKEN: ${{ secrets.GITHUB_TOKEN }}
        with:
          tag_name: v${{ steps.bump_version.outputs.new_version }}
          release_name: Release ${{ steps.bump_version.outputs.new_version }}
          body: |
            ## What's Changed
            ${{ steps.changelog.outputs.changelog }}

            **Full Changelog**: https://github.com/${{ github.repository }}/compare/v${{ steps.current_version.outputs.version }}...v${{ steps.bump_version.outputs.new_version }}
          draft: false
          prerelease: false

      - name: Build release APK
        run: ./gradlew assembleRelease
        env:
          KEYSTORE_PATH: keystore.jks
          KEYSTORE_PASSWORD: ${{ secrets.KEYSTORE_PASSWORD }}
          KEY_ALIAS: ${{ secrets.KEY_ALIAS }}
          KEY_PASSWORD: ${{ secrets.KEY_PASSWORD }}

      - name: Deploy to production
        run: |
          # Your deployment logic here
          echo "Deploying version ${{ steps.bump_version.outputs.new_version }}"
```
