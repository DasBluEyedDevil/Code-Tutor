---
type: "THEORY"
title: "Common iOS Build Issues"
---


**Certificate Issues:**

1. **"No signing certificate found"**
   - You don't have a valid certificate in Keychain
   - Create one in Xcode: Settings -> Accounts -> Manage Certificates

2. **"Certificate has expired"**
   - Certificates are valid for 1 year
   - Create a new certificate and update provisioning profiles

3. **"Private key not found"**
   - Certificate exists but private key is missing from Keychain
   - Import the .p12 file containing both, or create new certificate

4. **"Certificate is not trusted"**
   - Apple's intermediate certificates not installed
   - Download from Apple Developer Portal or update Xcode

**Provisioning Profile Issues:**

1. **"No provisioning profile matching"**
   - Bundle ID doesn't match any profile
   - Profile doesn't include required entitlements
   - Profile has expired

2. **"Profile doesn't include signing certificate"**
   - The certificate used to create the profile is different
   - Regenerate profile or use matching certificate

3. **"Device is not registered"**
   - For development/ad-hoc: Add device UDID to profile
   - Regenerate or update the provisioning profile

