---
type: "THEORY"
title: "Certificates"
---


**What is a Signing Certificate?**

A signing certificate is a digital identity that proves you are an authorized Apple developer. It contains:
- Your public key (shared with Apple)
- Your private key (kept secret on your Mac)
- Apple's signature verifying your identity

**Certificate Types:**

1. **Apple Development Certificate:**
   - For building and running apps on test devices
   - Can be used by any team member
   - Multiple developers can have their own

2. **Apple Distribution Certificate:**
   - For App Store and TestFlight submissions
   - Limited to 3 per team (organization accounts)
   - Typically shared among team members who do releases

**Creating Certificates in Xcode:**

1. Open Xcode -> Settings (Cmd + ,)
2. Go to Accounts tab
3. Select your Apple ID and team
4. Click "Manage Certificates"
5. Click "+" and choose certificate type
6. Xcode creates and installs the certificate automatically

**Certificate Lifespan:**
- Development certificates: Valid for 1 year
- Distribution certificates: Valid for 1 year (was 3 years before 2022)
- Must be renewed before expiration to continue distributing

