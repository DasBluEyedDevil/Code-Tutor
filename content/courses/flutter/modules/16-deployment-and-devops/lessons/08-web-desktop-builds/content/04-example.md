---
type: "EXAMPLE"
title: "macOS Builds"
---


Build, notarize, and distribute macOS applications:



```bash
# Prerequisites:
# - Xcode with command line tools
# - Apple Developer account (for distribution)

# Build macOS app
flutter build macos --release

# Output: build/macos/Build/Products/Release/YourApp.app

---

# Configure entitlements for app capabilities
# macos/Runner/Release.entitlements
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "...">
<plist version="1.0">
<dict>
    <key>com.apple.security.app-sandbox</key>
    <true/>
    <key>com.apple.security.network.client</key>
    <true/>
    <key>com.apple.security.files.user-selected.read-write</key>
    <true/>
</dict>
</plist>

---

# Code Signing and Notarization
# Step 1: Create Developer ID Application certificate in Apple Developer Portal
# Step 2: Download and install in Keychain

# Sign the app
codesign --deep --force --verify --verbose \
  --sign "Developer ID Application: Your Name (TEAM_ID)" \
  --options runtime \
  build/macos/Build/Products/Release/YourApp.app

# Verify signature
codesign --verify --deep --strict --verbose=2 \
  build/macos/Build/Products/Release/YourApp.app

---

# Create DMG for distribution
# Install create-dmg: brew install create-dmg

create-dmg \
  --volname "My App" \
  --volicon "assets/icons/app_icon.icns" \
  --window-pos 200 120 \
  --window-size 600 400 \
  --icon-size 100 \
  --icon "YourApp.app" 150 190 \
  --hide-extension "YourApp.app" \
  --app-drop-link 450 185 \
  --background "assets/dmg_background.png" \
  "MyApp-1.0.0.dmg" \
  "build/macos/Build/Products/Release/YourApp.app"

---

# Notarization (required for distribution outside App Store)
# Store credentials in keychain
xcrun notarytool store-credentials "AC_PASSWORD" \
  --apple-id "your@email.com" \
  --team-id "TEAM_ID" \
  --password "app-specific-password"

# Submit for notarization
xcrun notarytool submit MyApp-1.0.0.dmg \
  --keychain-profile "AC_PASSWORD" \
  --wait

# Staple the notarization ticket
xcrun stapler staple MyApp-1.0.0.dmg

# Verify notarization
spctl --assess --type open --context context:primary-signature -v MyApp-1.0.0.dmg

---

# Mac App Store Submission
# Use Xcode Archive or Transporter app

# Configure for App Store in Xcode:
# 1. Set bundle identifier matching App Store Connect
# 2. Configure App Sandbox entitlements
# 3. Set correct provisioning profile
# 4. Archive: Product > Archive
# 5. Distribute: Window > Organizer > Distribute App

# Alternatively, use xcrun for command-line submission:
xcrun altool --upload-app \
  --type macos \
  --file YourApp.pkg \
  --apiKey YOUR_API_KEY \
  --apiIssuer YOUR_ISSUER_ID

---

# GitHub Actions for macOS Build
jobs:
  build-macos:
    runs-on: macos-latest
    steps:
      - uses: actions/checkout@v4
      
      - uses: subosito/flutter-action@v2
        with:
          flutter-version: '3.24.0'
          cache: true

      - run: flutter config --enable-macos-desktop
      - run: flutter pub get
      - run: flutter build macos --release

      - name: Import certificates
        env:
          MACOS_CERTIFICATE: ${{ secrets.MACOS_CERTIFICATE }}
          MACOS_CERTIFICATE_PWD: ${{ secrets.MACOS_CERTIFICATE_PWD }}
        run: |
          echo $MACOS_CERTIFICATE | base64 --decode > certificate.p12
          security create-keychain -p "" build.keychain
          security import certificate.p12 -k build.keychain -P $MACOS_CERTIFICATE_PWD -T /usr/bin/codesign
          security set-key-partition-list -S apple-tool:,apple:,codesign: -s -k "" build.keychain

      - name: Sign app
        run: |
          codesign --deep --force --verify --verbose \
            --sign "Developer ID Application: Your Name (TEAM_ID)" \
            --options runtime \
            build/macos/Build/Products/Release/YourApp.app

      - uses: actions/upload-artifact@v4
        with:
          name: macos-app
          path: build/macos/Build/Products/Release/YourApp.app
```
