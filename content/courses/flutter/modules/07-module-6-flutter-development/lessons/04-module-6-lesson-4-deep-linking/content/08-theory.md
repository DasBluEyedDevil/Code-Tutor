---
type: "THEORY"
title: "Verification Files Checklist"
---


✅ **Android - assetlinks.json**
- Location: `https://mycompany.com/.well-known/assetlinks.json`
- Must be HTTPS (not HTTP)
- No redirects allowed
- Must return `Content-Type: application/json`

✅ **iOS - apple-app-site-association**
- Location: `https://mycompany.com/.well-known/apple-app-site-association`
- Must be HTTPS
- No `.json` extension!
- Must return `Content-Type: application/json`

**Test your files:**

Should return `200 OK` with `Content-Type: application/json`



```bash
# Test Android file
curl -I https://mycompany.com/.well-known/assetlinks.json

# Test iOS file
curl -I https://mycompany.com/.well-known/apple-app-site-association
```
