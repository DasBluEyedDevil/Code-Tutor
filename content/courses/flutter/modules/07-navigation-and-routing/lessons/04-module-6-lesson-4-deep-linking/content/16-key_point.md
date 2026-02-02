---
type: "KEY_POINT"
title: "Answer Key"
---

1. **B** - App Links and Universal Links are verified with your website, providing security and automatic fallback to web if app isn't installed
2. **B** - The assetlinks.json file must be hosted at `https://example.com/.well-known/assetlinks.json` for Android verification
3. **A** - You need `getInitialLink()` to handle the link that opened the app (cold start) and `uriLinkStream.listen()` to handle links while app is running

