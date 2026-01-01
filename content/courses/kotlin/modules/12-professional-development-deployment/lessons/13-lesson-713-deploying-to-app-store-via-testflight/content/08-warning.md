---
type: "WARNING"
title: "Common App Store Issues"
---

### Issue 1: "Missing compliance"

- Your app uses encryption
- Add `ITSAppUsesNonExemptEncryption` to Info.plist:
  ```xml
  <key>ITSAppUsesNonExemptEncryption</key>
  <false/>
  ```

### Issue 2: "Invalid provisioning profile"

- Profile doesn't match bundle ID or certificate
- Regenerate profile in Developer Portal

### Issue 3: "Build processing stuck"

- Wait longer (can take up to 1 hour)
- Check App Store Connect status page
- Re-upload if stuck > 2 hours

### Issue 4: "App rejected for guideline violation"

- Read rejection reason carefully
- Common issues:
  - Placeholder content
  - Broken links
  - Missing privacy policy
  - Login required without demo account
  - Crashes (test thoroughly!)

### Issue 5: "Invalid binary"

- Check minimum iOS version
- Ensure all architectures included
- Verify signing is correct