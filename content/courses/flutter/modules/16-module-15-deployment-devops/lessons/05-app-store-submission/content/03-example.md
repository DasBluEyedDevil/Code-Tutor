---
type: "EXAMPLE"
title: "Build Upload Process"
---


Uploading your Flutter app to App Store Connect:



```bash
## Step 1: Create Archive in Xcode

# First, build the iOS release
flutter build ios --release

# Open Xcode workspace
open ios/Runner.xcworkspace

## Step 2: Archive the App

1. Select "Any iOS Device" as build target (not simulator)
2. Product -> Archive
3. Wait for archive to complete
4. Organizer window opens automatically

## Step 3: Validate and Upload

In Xcode Organizer:
1. Select your archive
2. Click "Validate App"
   - Checks signing, entitlements, icons
   - Fixes issues before upload
3. Click "Distribute App"
4. Select "App Store Connect"
5. Choose "Upload"
6. Select distribution options:
   - Include bitcode (optional, deprecated in Xcode 14+)
   - Include symbols for crash reports (recommended)
   - Manage signing automatically or manually
7. Click "Upload"

## Alternative: Upload via Transporter

1. Export .ipa from Xcode Organizer
2. Download Transporter app from Mac App Store
3. Drag .ipa file into Transporter
4. Click "Deliver"

## Build Processing

After upload:
- Build appears as "Processing" in App Store Connect
- Processing takes 10-30 minutes typically
- You'll receive email when processing completes
- Build then available for TestFlight or submission

## Common Upload Errors

ERROR: Missing compliance information
-> Add export compliance in Info.plist or answer in ASC

ERROR: Invalid bundle identifier
-> Bundle ID must match registered identifier

ERROR: Missing required icon
-> Add 1024x1024 App Store icon in Assets.xcassets
```
