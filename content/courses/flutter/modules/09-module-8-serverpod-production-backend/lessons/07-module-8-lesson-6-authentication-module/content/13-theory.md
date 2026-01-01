---
type: "THEORY"
title: "Apple Sign-In Integration"
---

Apple Sign-In is required for iOS apps that offer third-party sign-in options (per Apple's App Store guidelines). It provides a privacy-focused authentication method where users can choose to hide their real email address.

**Prerequisites:**

1. **Apple Developer Account:**
   - Enable Sign in with Apple capability in your app's identifier
   - Create a Service ID for web authentication
   - Create a private key for server-side verification

2. **Xcode Configuration:**
   - Add Sign in with Apple capability to your target
   - Ensure proper entitlements are configured

**Key Features of Apple Sign-In:**

- **Hide My Email:** Users can generate a random email address that forwards to their real email
- **Name Control:** Users can provide any name they want
- **Minimal Data:** Apple provides only what is necessary
- **No Tracking:** Apple does not track users across apps

**Important Considerations:**

1. Apple only provides the user's email on the FIRST sign-in. Store it immediately.
2. Users may hide their email, giving you a relay address like xyz@privaterelay.appleid.com
3. The relay address still works for sending emails
4. On subsequent sign-ins, you only get the user identifier

