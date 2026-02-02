---
type: "WARNING"
title: "Critical: Protect Your Certificates"
---


**Private Key Security**

Your signing certificate's private key is stored in your Mac's Keychain. If you lose it:
- You cannot sign apps with that certificate
- You must revoke and create a new certificate
- Existing provisioning profiles become invalid

**Best Practices:**

1. **Export and backup your certificates:**
   - Open Keychain Access
   - Find your certificate (search for "Apple Development" or "Apple Distribution")
   - Right-click -> Export
   - Save as .p12 file with a strong password
   - Store backup securely (encrypted drive, password manager)

2. **Share certificates properly with team:**
   - Export as .p12 file
   - Share securely (not via email)
   - Each team member imports into their Keychain

3. **For CI/CD systems:**
   - Export certificate as base64-encoded .p12
   - Store as encrypted secret in CI system
   - Import into CI Keychain during build

**Revoking Certificates:**
- If a certificate is compromised, revoke it immediately in Apple Developer Portal
- Revoking invalidates all provisioning profiles using that certificate
- Create new certificate and profiles after revoking

