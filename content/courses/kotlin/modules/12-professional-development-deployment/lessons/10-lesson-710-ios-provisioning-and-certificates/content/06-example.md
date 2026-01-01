---
type: "EXAMPLE"
title: "Exporting Certificates for CI/CD"
---

Export certificates and profiles for use in GitHub Actions:

```bash
# Export certificate as .p12 file from Keychain Access:
# 1. Open Keychain Access
# 2. Find your distribution certificate
# 3. Right-click → Export
# 4. Save as .p12 with a strong password

# Base64 encode for GitHub Secrets:
base64 -i Certificates.p12 | pbcopy
# Paste into GitHub Secret: IOS_CERTIFICATE_BASE64

# Export provisioning profile:
base64 -i ~/Library/MobileDevice/Provisioning\ Profiles/YOUR_PROFILE.mobileprovision | pbcopy
# Paste into GitHub Secret: IOS_PROVISIONING_PROFILE_BASE64

# In GitHub Actions, decode and install:
- name: Install Certificate
  env:
    CERTIFICATE_BASE64: ${{ secrets.IOS_CERTIFICATE_BASE64 }}
    CERTIFICATE_PASSWORD: ${{ secrets.IOS_CERTIFICATE_PASSWORD }}
  run: |
    # Create keychain
    security create-keychain -p "" build.keychain
    security default-keychain -s build.keychain
    security unlock-keychain -p "" build.keychain
    
    # Import certificate
    echo $CERTIFICATE_BASE64 | base64 --decode > certificate.p12
    security import certificate.p12 -k build.keychain -P "$CERTIFICATE_PASSWORD" -T /usr/bin/codesign
    security set-key-partition-list -S apple-tool:,apple:,codesign: -s -k "" build.keychain

- name: Install Provisioning Profile
  env:
    PROFILE_BASE64: ${{ secrets.IOS_PROVISIONING_PROFILE_BASE64 }}
  run: |
    mkdir -p ~/Library/MobileDevice/Provisioning\ Profiles
    echo $PROFILE_BASE64 | base64 --decode > ~/Library/MobileDevice/Provisioning\ Profiles/profile.mobileprovision
```
