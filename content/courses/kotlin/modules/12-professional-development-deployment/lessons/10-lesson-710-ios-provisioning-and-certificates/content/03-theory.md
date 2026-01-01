---
type: "THEORY"
title: "Setting Up Certificates"
---

### Step 1: Create a Certificate Signing Request (CSR)

1. Open **Keychain Access** on your Mac
2. Menu: Keychain Access → Certificate Assistant → Request a Certificate from a Certificate Authority
3. Enter your email and name
4. Select "Saved to disk"
5. Save the `.certSigningRequest` file

### Step 2: Create Certificate in Apple Developer Portal

1. Go to [developer.apple.com/account](https://developer.apple.com/account)
2. Certificates, Identifiers & Profiles → Certificates
3. Click "+" to create new certificate
4. Select certificate type (Development or Distribution)
5. Upload your CSR file
6. Download the `.cer` file
7. Double-click to install in Keychain

### Step 3: Verify in Keychain

```bash
# List signing identities
security find-identity -v -p codesigning

# You should see something like:
# 1) ABC123... "Apple Development: Your Name (TEAMID)"
# 2) DEF456... "Apple Distribution: Your Name (TEAMID)"
```