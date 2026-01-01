---
type: "THEORY"
title: "Step 1: Android Configuration (App Links)"
---


### A. Update AndroidManifest.xml


**Key parts:**
- `android:autoVerify="true"` - Tells Android to verify ownership
- `android:scheme="https"` - Use HTTPS (secure!)
- `android:host="mycompany.com"` - Your website domain

### B. Create assetlinks.json

Host this file at: `https://mycompany.com/.well-known/assetlinks.json`


**To get SHA256 fingerprint:**


Copy the SHA256 fingerprint from the output.



```bash
# Debug certificate (for testing)
keytool -list -v -keystore ~/.android/debug.keystore -alias androiddebugkey -storepass android -keypass android

# Release certificate (for production)
keytool -list -v -keystore /path/to/your/release.keystore -alias your-key-alias
```
