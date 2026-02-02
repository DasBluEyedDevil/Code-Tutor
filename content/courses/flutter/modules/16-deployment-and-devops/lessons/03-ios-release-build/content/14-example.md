---
type: "EXAMPLE"
title: "Troubleshooting Signing Errors"
---


Common error messages and solutions:



```bash
# Error: "No signing certificate 'iOS Distribution' found"
# Solution:
# 1. Open Xcode -> Settings -> Accounts
# 2. Select your team -> Manage Certificates
# 3. Click + and create "Apple Distribution" certificate
# Or import existing .p12 file into Keychain Access

# Error: "Provisioning profile doesn't include signing certificate"
# Solution:
# 1. Go to Apple Developer Portal -> Profiles
# 2. Edit the profile and select your current certificate
# 3. Download and double-click to install
# Or in Xcode: Delete derived data and let automatic signing regenerate

rm -rf ~/Library/Developer/Xcode/DerivedData

# Error: "The executable was signed with invalid entitlements"
# Solution:
# 1. Check entitlements in Xcode project match provisioning profile
# 2. Ensure App ID has required capabilities enabled
# 3. Regenerate provisioning profile with correct entitlements

# Error: "App installation failed: This app cannot be installed"
# Solution:
# - For development builds: Device UDID not in provisioning profile
# - For TestFlight: User not invited to beta test
# - For App Store: App not released or different region

# Verify provisioning profile contents:
security cms -D -i "path/to/profile.mobileprovision"

# List installed provisioning profiles:
ls ~/Library/MobileDevice/Provisioning\ Profiles/
```
