---
type: "THEORY"
title: "Introduction: What is OAuth and Why Social Login?"
---

In the previous lessons, you built email-based registration and login systems. While email authentication works well, many users prefer the convenience of signing in with their existing Google or Apple accounts. This lesson teaches you how to implement OAuth-based social login that integrates seamlessly with your Serverpod backend.

**What is OAuth?**

OAuth (Open Authorization) is an industry-standard protocol that allows users to grant your app limited access to their accounts on other services without sharing their passwords. Think of it like a hotel key card system:

```
Traditional Login (Password):
User gives you their house key → You can access everything → High risk if key is lost

OAuth (Social Login):
User asks hotel to create a room key → Key only opens specific doors → Key expires automatically → Hotel can revoke access anytime
```

When a user signs in with Google, they never give your app their Google password. Instead:

1. Your app redirects to Google's login page
2. User authenticates directly with Google
3. Google asks user to approve specific permissions (email, profile picture)
4. Google sends your app a token representing that approval
5. Your app uses the token to get user info and create a session

**Why Offer Social Login?**

Social login provides significant benefits for both users and developers:

1. **Reduced Friction**: Users sign up in seconds without creating yet another password
2. **Higher Conversion**: Studies show social login can increase sign-up rates by 20-50%
3. **Better Security**: Major providers like Google and Apple have sophisticated security (2FA, anomaly detection)
4. **Verified Emails**: Social providers verify email addresses, reducing fake accounts
5. **Less Password Fatigue**: Users have fewer passwords to remember and manage
6. **Profile Data**: You can optionally request profile information (name, photo) to personalize the experience

**What You Will Build**

By the end of this lesson, you will have:

1. **Google Sign-In** configured for both Android and iOS with Firebase
2. **Apple Sign-In** configured with proper entitlements and capabilities
3. **Serverpod integration** that validates OAuth tokens and creates user sessions
4. **Account linking** that connects social accounts to existing email accounts
5. **Platform-adaptive UI** with properly styled social login buttons
6. **Error handling** for all OAuth failure scenarios

**OAuth Providers in This Lesson**

We focus on Google and Apple because:

- **Google**: Most popular OAuth provider, works on all platforms
- **Apple**: Required by App Store if you offer any social login options

**Prerequisites**

This lesson builds on Lessons 10.4 (Registration) and 10.5 (Login). You should have:
- Working email authentication with Serverpod
- SecureStorageService for token management
- AuthService and auth state management with Riverpod
- A Firebase project (we will configure it for Google Sign-In)
- An Apple Developer account (required for Apple Sign-In)

Let us start by setting up Google Sign-In.

