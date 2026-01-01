---
type: "EXAMPLE"
title: "Building for App Store"
---

Build and archive your KMP iOS app:

```bash
# 1. Build the release framework
./gradlew :shared:linkReleaseFrameworkIosArm64

# 2. Archive in Xcode (GUI method)
# - Open iosApp/iosApp.xcworkspace in Xcode
# - Select "Any iOS Device (arm64)" as destination
# - Product → Archive
# - Once complete, Organizer window opens

# 3. Archive via command line
cd iosApp
xcodebuild archive \
  -workspace iosApp.xcworkspace \
  -scheme iosApp \
  -configuration Release \
  -archivePath build/iosApp.xcarchive \
  -destination 'generic/platform=iOS' \
  CODE_SIGN_IDENTITY="Apple Distribution" \
  DEVELOPMENT_TEAM="YOUR_TEAM_ID" \
  PROVISIONING_PROFILE_SPECIFIER="Your App Store Profile"

# 4. Export IPA
# Create ExportOptions.plist:
cat > ExportOptions.plist << EOF
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<dict>
    <key>method</key>
    <string>app-store</string>
    <key>teamID</key>
    <string>YOUR_TEAM_ID</string>
    <key>uploadSymbols</key>
    <true/>
    <key>uploadBitcode</key>
    <false/>
</dict>
</plist>
EOF

xcodebuild -exportArchive \
  -archivePath build/iosApp.xcarchive \
  -exportPath build/export \
  -exportOptionsPlist ExportOptions.plist

# 5. Upload to App Store Connect
xcrun altool --upload-app \
  -f build/export/iosApp.ipa \
  --type ios \
  --apiKey "YOUR_API_KEY_ID" \
  --apiIssuer "YOUR_ISSUER_ID"
```
