---
type: "THEORY"
title: "Common Issues and Solutions"
---


### Issue 1: Link Opens Browser Instead of App

**Cause:** Verification files not accessible or incorrect

**Solution:**

### Issue 2: Android App Not Verified

**Solution:**

### Issue 3: iOS Universal Links Not Working

**Solutions:**
- Make sure Associated Domains capability is added in Xcode
- Check Team ID is correct in apple-app-site-association
- Verify domain starts with `applinks:` in Xcode



```bash
# Check verification status
adb shell pm get-app-links com.mycompany.myapp

# Should show "verified" for your domain
```
