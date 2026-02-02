---
type: "EXAMPLE"
title: "Build Artifacts"
---


Generate APK and IPA files and upload them as downloadable artifacts:



```yaml
# .github/workflows/build.yml
name: Build Release Artifacts

on:
  push:
    tags:
      - 'v*'  # Triggers on version tags like v1.0.0
  workflow_dispatch:
    inputs:
      build_number:
        description: 'Build number'
        required: true
        default: '1'

jobs:
  build-android:
    name: Build Android APK & AAB
    runs-on: ubuntu-latest
    
    steps:
      - uses: actions/checkout@v4

      - name: Setup Java
        uses: actions/setup-java@v4
        with:
          distribution: 'zulu'
          java-version: '17'

      - name: Setup Flutter
        uses: subosito/flutter-action@v2
        with:
          flutter-version: '3.24.0'
          cache: true

      - name: Install dependencies
        run: flutter pub get

      - name: Decode keystore
        run: |
          echo "${{ secrets.KEYSTORE_BASE64 }}" | base64 -d > android/app/keystore.jks

      - name: Build APK
        env:
          KEYSTORE_PASSWORD: ${{ secrets.KEYSTORE_PASSWORD }}
          KEY_ALIAS: ${{ secrets.KEY_ALIAS }}
          KEY_PASSWORD: ${{ secrets.KEY_PASSWORD }}
        run: |
          flutter build apk --release \
            --build-number=${{ github.event.inputs.build_number || github.run_number }}

      - name: Build App Bundle
        env:
          KEYSTORE_PASSWORD: ${{ secrets.KEYSTORE_PASSWORD }}
          KEY_ALIAS: ${{ secrets.KEY_ALIAS }}
          KEY_PASSWORD: ${{ secrets.KEY_PASSWORD }}
        run: |
          flutter build appbundle --release \
            --build-number=${{ github.event.inputs.build_number || github.run_number }}

      - name: Upload APK
        uses: actions/upload-artifact@v4
        with:
          name: android-apk
          path: build/app/outputs/flutter-apk/app-release.apk
          retention-days: 30

      - name: Upload AAB
        uses: actions/upload-artifact@v4
        with:
          name: android-aab
          path: build/app/outputs/bundle/release/app-release.aab
          retention-days: 30

  build-ios:
    name: Build iOS IPA
    runs-on: macos-latest
    
    steps:
      - uses: actions/checkout@v4

      - name: Setup Flutter
        uses: subosito/flutter-action@v2
        with:
          flutter-version: '3.24.0'
          cache: true

      - name: Install dependencies
        run: flutter pub get

      - name: Install CocoaPods
        run: cd ios && pod install

      - name: Install Apple certificate
        env:
          BUILD_CERTIFICATE_BASE64: ${{ secrets.BUILD_CERTIFICATE_BASE64 }}
          P12_PASSWORD: ${{ secrets.P12_PASSWORD }}
          KEYCHAIN_PASSWORD: ${{ secrets.KEYCHAIN_PASSWORD }}
        run: |
          # Create keychain
          security create-keychain -p "$KEYCHAIN_PASSWORD" build.keychain
          security default-keychain -s build.keychain
          security unlock-keychain -p "$KEYCHAIN_PASSWORD" build.keychain
          
          # Import certificate
          echo "$BUILD_CERTIFICATE_BASE64" | base64 -d > certificate.p12
          security import certificate.p12 -k build.keychain -P "$P12_PASSWORD" -T /usr/bin/codesign
          security set-key-partition-list -S apple-tool:,apple:,codesign: -s -k "$KEYCHAIN_PASSWORD" build.keychain

      - name: Install provisioning profile
        env:
          PROVISIONING_PROFILE_BASE64: ${{ secrets.PROVISIONING_PROFILE_BASE64 }}
        run: |
          mkdir -p ~/Library/MobileDevice/Provisioning\ Profiles
          echo "$PROVISIONING_PROFILE_BASE64" | base64 -d > ~/Library/MobileDevice/Provisioning\ Profiles/profile.mobileprovision

      - name: Build iOS
        run: |
          flutter build ipa --release \
            --export-options-plist=ios/ExportOptions.plist \
            --build-number=${{ github.run_number }}

      - name: Upload IPA
        uses: actions/upload-artifact@v4
        with:
          name: ios-ipa
          path: build/ios/ipa/*.ipa
          retention-days: 30

      - name: Cleanup keychain
        if: always()
        run: security delete-keychain build.keychain
```
