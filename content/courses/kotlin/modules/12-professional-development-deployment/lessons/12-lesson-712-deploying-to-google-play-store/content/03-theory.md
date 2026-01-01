---
type: "THEORY"
title: "Play Console Setup"
---

### Step 1: Create Your App

1. Go to Play Console → All apps → Create app
2. Enter app name, default language, app type
3. Accept developer policies

### Step 2: Set Up Your App

Complete all sections in the Dashboard:

**Store Presence:**
- Main store listing (title, description, graphics)
- Store settings (app category, contact details)

**Release:**
- Production, testing tracks
- Countries and regions

**Policy:**
- App content (ads, target audience)
- Content ratings
- Privacy policy
- Data safety form

### Step 3: App Signing

1. Go to Setup → App signing
2. Enable Play App Signing (required for new apps)
3. Upload your upload key certificate:
   ```bash
   keytool -export -rfc \
     -keystore release.keystore \
     -alias your-alias \
     -file upload_certificate.pem
   ```