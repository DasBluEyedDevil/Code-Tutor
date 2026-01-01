---
type: "WARNING"
title: "Bun Boundaries"
---

### 1. 99% Node Compatibility
Bun aims to be a drop-in replacement for Node.js, but it's not perfect. Some extremely niche or old Node.js libraries that rely on deep internal Node C++ APIs might not work perfectly. Always check your complex dependencies when migrating.

### 2. Windows Support
Bun's Windows support is much better than it used to be (version 1.1+), but historically it was built for Linux/macOS. If you are on Windows, ensure you are running the latest version of Bun (`bun upgrade`) to avoid early bugs.

### 3. Edge Cases in JavaScriptCore
Because Bun uses **JavaScriptCore** (Safari) instead of **V8** (Chrome/Node), there are very tiny differences in how certain edge-case JavaScript code might behave or how memory is managed. For 99.9% of apps, you will never notice this.

### 4. It moves fast!
Bun is updated almost weekly. What was a bug yesterday might be fixed today. Keep your environment updated to benefit from the latest performance boosts and fixes.
