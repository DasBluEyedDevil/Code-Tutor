---
type: "THEORY"
title: "Auth Architecture Overview"
---


**MVVM Pattern for Authentication**

Authentication is one of the most critical features in any application. A well-architected auth system needs to handle state persistence, secure token storage, navigation guards, and seamless user experience across app restarts. We'll use the MVVM (Model-View-ViewModel) pattern with Riverpod for state management.

**Authentication Architecture Layers**

| Layer | Component | Responsibility |
|-------|-----------|----------------|
| **Model** | AuthState, User | Data structures for auth state |
| **Repository** | AuthRepository | API calls to Serverpod auth client |
| **State** | AuthNotifier | Business logic, state transitions |
| **View** | LoginScreen, RegisterScreen | UI presentation |

**Riverpod Auth State Management**

Using Riverpod's `AsyncNotifier` pattern provides:

- **Automatic loading states**: Show spinners during async operations
- **Error handling**: Catch and display auth errors gracefully
- **State persistence**: Restore auth state on app restart
- **Dependency injection**: Easy testing with provider overrides

```
┌─────────────────────────────────────────────────────────────┐
│                    Auth State Flow                          │
├─────────────────────────────────────────────────────────────┤
│                                                             │
│   ┌──────────┐    ┌──────────────┐    ┌───────────────┐    │
│   │ Unauthd  │───>│ Authenticating│───>│ Authenticated │    │
│   └──────────┘    └──────────────┘    └───────────────┘    │
│        ^                                      │             │
│        │                                      │             │
│        └──────────────────────────────────────┘             │
│                      (logout/token expired)                 │
│                                                             │
└─────────────────────────────────────────────────────────────┘
```

**Token Storage with flutter_secure_storage**

Secure credential storage is essential:

- **iOS**: Keychain Services (hardware-backed encryption)
- **Android**: EncryptedSharedPreferences or KeyStore
- **Web**: Encrypted localStorage (with limitations)

**Key Security Principles**

1. **Never store passwords**: Only store tokens
2. **Use secure storage**: No SharedPreferences for tokens
3. **Implement token refresh**: Handle expiration gracefully
4. **Clear on logout**: Remove all auth data
5. **Biometric protection**: Optional additional layer

**Auth State Changes and Navigation**

Auth state changes should trigger navigation:

| State Change | Navigation Action |
|--------------|-------------------|
| Login success | Navigate to home, clear stack |
| Logout | Navigate to login, clear stack |
| Token expired | Show re-auth dialog or redirect |
| Registration success | Navigate to email verification |
| Email verified | Navigate to home |

**Error Handling Strategy**

| Error Type | User Experience |
|------------|----------------|
| Invalid credentials | Show inline error, keep form data |
| Network error | Show retry option, offline indicator |
| Rate limited | Show countdown timer |
| Account locked | Show unlock instructions |
| Server error | Show generic message, log details |

