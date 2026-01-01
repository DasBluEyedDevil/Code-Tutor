---
type: "WARNING"
title: "Common iOS Signing Issues"
---

### Issue 1: "No signing certificate found"

```bash
# Check if certificate is in keychain
security find-identity -v -p codesigning

# If empty, reinstall your certificate:
# 1. Download from Developer Portal
# 2. Double-click to install
```

### Issue 2: "Provisioning profile doesn't include signing certificate"

- Regenerate the provisioning profile with the correct certificate
- Make sure the certificate isn't expired

### Issue 3: "App ID doesn't match"

- Check Bundle ID in Xcode matches App ID in Developer Portal
- For KMP: Check `iosApp/iosApp/Info.plist` → `CFBundleIdentifier`

### Issue 4: Certificate Expiration

- Development certificates expire after 1 year
- Distribution certificates expire after 1 year
- Set calendar reminders to renew before expiration

### Pro Tip: Use Fastlane Match

For teams, use `fastlane match` to share certificates:
```bash
fastlane match init
fastlane match appstore  # Generate/fetch App Store certs
fastlane match development  # Generate/fetch dev certs
```