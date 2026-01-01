---
type: "EXAMPLE"
title: "Setting Up Fastlane"
---

Install and configure Fastlane for your KMP project:

```bash
# Install Fastlane
brew install fastlane
# Or: gem install fastlane

# Project structure for KMP
my-kmp-app/
├── composeApp/          # Android app
│   └── fastlane/
│       ├── Fastfile
│       ├── Appfile
│       └── Matchfile    # If using match
├── iosApp/              # iOS app
│   └── fastlane/
│       ├── Fastfile
│       ├── Appfile
│       └── Matchfile
├── shared/              # Shared KMP code
└── fastlane/            # Root Fastfile (optional)
    └── Fastfile

# Initialize Fastlane for Android
cd composeApp
fastlane init
# Select: Manual setup

# Initialize Fastlane for iOS
cd ../iosApp
fastlane init
# Select: Manual setup or App Store Connect API

# Create Appfile (app configuration)
# composeApp/fastlane/Appfile
package_name("com.yourcompany.yourapp")
json_key_file("play-store-key.json")

# iosApp/fastlane/Appfile
app_identifier("com.yourcompany.yourapp")
apple_id("your@email.com")
itc_team_id("123456789")  # App Store Connect Team ID
team_id("ABCD1234")        # Developer Portal Team ID
```
