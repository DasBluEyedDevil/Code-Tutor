---
type: "THEORY"
title: "Why Environments Matter"
---


**The Problem Without Environments**

Imagine you're building a food delivery app. During development, you want to:
- Use a test API server (so you don't create fake orders in production)
- Show debug banners and logging
- Connect to a staging Stripe account (so you don't charge real money)
- Use a different app icon (so testers know it's not the real app)

Without proper environment configuration, developers often do this:

```dart
// DON'T DO THIS
const apiUrl = 'https://staging-api.myapp.com'; // Oops, forgot to change before release!
```

This leads to disasters:
- Shipping staging API URLs to production
- Test payments going to real customers
- Debug logs appearing in released apps
- Manual find-and-replace before each build

**The Solution: Build Flavors**

Build flavors (also called build configurations or schemes) let you:
- Define multiple environments (dev, staging, production)
- Switch environments with a simple command
- Guarantee the right config ships to the right build
- Use different app IDs, icons, and names per environment

