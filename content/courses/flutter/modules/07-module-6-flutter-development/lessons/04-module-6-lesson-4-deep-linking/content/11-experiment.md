---
type: "EXAMPLE"
title: "Security Best Practices"
---


✅ **DO:**
- Use HTTPS for all deep links
- Verify domains with assetlinks.json / apple-app-site-association
- Validate incoming data from deep links
- Handle invalid/malicious links gracefully

❌ **DON'T:**
- Use HTTP (insecure!)
- Trust deep link data without validation
- Expose sensitive operations via deep links
- Use custom schemes for production (use App Links/Universal Links)

