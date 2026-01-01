---
type: "THEORY"
title: "OAuth Integration"
---


**Social Authentication with OAuth 2.0**

OAuth allows users to sign in with existing accounts from Google, Apple, and other providers.

**Why OAuth?**

| Benefit | Description |
|---------|------------|
| **No passwords** | User doesn't create new credentials |
| **Trusted providers** | Google/Apple handle security |
| **Profile data** | Get name, email, avatar automatically |
| **Less friction** | One-tap sign-in experience |

**OAuth 2.0 Flow**

```
┌─────────────┐     ┌─────────────┐     ┌─────────────┐
│   Flutter   │     │  Serverpod  │     │   Google    │
│     App     │     │   Server    │     │   OAuth     │
└──────┬──────┘     └──────┬──────┘     └──────┬──────┘
       │                   │                   │
       │  1. Tap Sign In   │                   │
       │──────────────────>│                   │
       │                   │                   │
       │  2. Redirect to   │                   │
       │     Google        │                   │
       │<──────────────────│                   │
       │                   │                   │
       │  3. User consents │                   │
       │───────────────────────────────────────>
       │                   │                   │
       │  4. Auth code     │                   │
       │<──────────────────────────────────────│
       │                   │                   │
       │  5. Send code     │                   │
       │──────────────────>│                   │
       │                   │  6. Exchange code │
       │                   │──────────────────>│
       │                   │  7. Access token  │
       │                   │<──────────────────│
       │                   │                   │
       │                   │  8. Get user info │
       │                   │──────────────────>│
       │                   │  9. Profile data  │
       │                   │<──────────────────│
       │                   │                   │
       │  10. Session      │                   │
       │<──────────────────│                   │
```

**Provider Configuration**

| Provider | Required Setup |
|----------|---------------|
| **Google** | Google Cloud Console project, OAuth credentials |
| **Apple** | Apple Developer account, Sign in with Apple capability |
| **Firebase** | Firebase project, auth providers enabled |

**Server-Side Token Verification**

Never trust client-provided tokens without verification:

1. **Google**: Use Google's tokeninfo endpoint or client library
2. **Apple**: Validate JWT signature with Apple's public keys
3. **Firebase**: Use Firebase Admin SDK to verify ID tokens

