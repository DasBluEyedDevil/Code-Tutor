---
type: "EXAMPLE"
title: "Release Workflow"
---

Workflow triggered on version tags for production releases:

```yaml
# .github/workflows/release.yml
name: Release

on:
  push:
    tags:
      - 'v*'

env:
  JAVA_VERSION: '17'

jobs:
  # ===== ANDROID RELEASE =====
  release-android:
    name: Release Android
    runs-on: ubuntu-latest
    
    steps:
      - name: Checkout
        uses: actions/checkout@v4
      
      - name: Set up JDK
        uses: actions/setup-java@v4
        with:
          java-version: ${{ env.JAVA_VERSION }}
          distribution: 'temurin'
      
      - name: Decode Keystore
        env:
          KEYSTORE_BASE64: ${{ secrets.ANDROID_KEYSTORE_BASE64 }}
        run: echo "$KEYSTORE_BASE64" | base64 --decode > release.keystore
      
      - name: Build Release Bundle
        env:
          KEYSTORE_PATH: release.keystore
          KEYSTORE_PASSWORD: ${{ secrets.ANDROID_KEYSTORE_PASSWORD }}
          KEY_ALIAS: ${{ secrets.ANDROID_KEY_ALIAS }}
          KEY_PASSWORD: ${{ secrets.ANDROID_KEY_PASSWORD }}
        run: ./gradlew :composeApp:bundleRelease
      
      - name: Upload to Play Store
        uses: r0adkll/upload-google-play@v1
        with:
          serviceAccountJsonPlainText: ${{ secrets.PLAY_STORE_SERVICE_ACCOUNT }}
          packageName: com.yourcompany.yourapp
          releaseFiles: composeApp/build/outputs/bundle/release/*.aab
          track: production
          whatsNewDirectory: distribution/whatsnew

  # ===== IOS RELEASE =====
  release-ios:
    name: Release iOS
    runs-on: macos-14
    
    steps:
      - name: Checkout
        uses: actions/checkout@v4
      
      - name: Set up JDK
        uses: actions/setup-java@v4
        with:
          java-version: ${{ env.JAVA_VERSION }}
          distribution: 'temurin'
      
      - name: Install Certificate
        env:
          CERTIFICATE_BASE64: ${{ secrets.IOS_CERTIFICATE_BASE64 }}
          CERTIFICATE_PASSWORD: ${{ secrets.IOS_CERTIFICATE_PASSWORD }}
          KEYCHAIN_PASSWORD: ${{ secrets.KEYCHAIN_PASSWORD }}
        run: |
          security create-keychain -p "$KEYCHAIN_PASSWORD" build.keychain
          security default-keychain -s build.keychain
          security unlock-keychain -p "$KEYCHAIN_PASSWORD" build.keychain
          echo "$CERTIFICATE_BASE64" | base64 --decode > certificate.p12
          security import certificate.p12 -k build.keychain -P "$CERTIFICATE_PASSWORD" -T /usr/bin/codesign
          security set-key-partition-list -S apple-tool:,apple:,codesign: -s -k "$KEYCHAIN_PASSWORD" build.keychain
      
      - name: Install Provisioning Profile
        env:
          PROFILE_BASE64: ${{ secrets.IOS_PROVISIONING_PROFILE_BASE64 }}
        run: |
          mkdir -p ~/Library/MobileDevice/Provisioning\ Profiles
          echo "$PROFILE_BASE64" | base64 --decode > ~/Library/MobileDevice/Provisioning\ Profiles/profile.mobileprovision
      
      - name: Build iOS Framework
        run: ./gradlew :shared:linkReleaseFrameworkIosArm64
      
      - name: Archive iOS App
        run: |
          cd iosApp
          xcodebuild archive \
            -scheme iosApp \
            -configuration Release \
            -archivePath build/iosApp.xcarchive \
            -destination 'generic/platform=iOS'
      
      - name: Export IPA
        run: |
          cd iosApp
          xcodebuild -exportArchive \
            -archivePath build/iosApp.xcarchive \
            -exportPath build/export \
            -exportOptionsPlist ExportOptions.plist
      
      - name: Upload to TestFlight
        env:
          APP_STORE_CONNECT_API_KEY_ID: ${{ secrets.APP_STORE_API_KEY_ID }}
          APP_STORE_CONNECT_ISSUER_ID: ${{ secrets.APP_STORE_ISSUER_ID }}
          APP_STORE_CONNECT_API_KEY: ${{ secrets.APP_STORE_API_KEY }}
        run: |
          xcrun altool --upload-app \
            -f iosApp/build/export/iosApp.ipa \
            --type ios \
            --apiKey "$APP_STORE_CONNECT_API_KEY_ID" \
            --apiIssuer "$APP_STORE_CONNECT_ISSUER_ID"
```
