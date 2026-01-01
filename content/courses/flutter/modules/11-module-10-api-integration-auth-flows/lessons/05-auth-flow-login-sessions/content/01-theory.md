---
type: "THEORY"
title: "Introduction: Login vs Registration and Session Management"
---

In the previous lesson, you built a complete user registration flow. Now it is time to create the complementary login system that allows users to authenticate and maintain their sessions across app restarts. While registration creates new accounts, login verifies existing credentials and establishes a secure session that persists until the user logs out or the session expires.

**What You Will Build**

By the end of this lesson, you will have a production-ready login system with:

1. **A polished login form** with email and password fields, remember me option, and forgot password link
2. **Serverpod authentication** that validates credentials and returns session tokens
3. **Session persistence** that keeps users logged in across app restarts
4. **Automatic token refresh** that silently refreshes tokens before they expire
5. **Session state management** using Riverpod to track authentication state globally
6. **Logout flow** that properly clears tokens and invalidates server sessions
7. **Session expiration handling** that gracefully redirects users when sessions expire

**Understanding Token-Based Authentication**

Modern mobile apps use token-based authentication instead of traditional session cookies. Here is how it works:

```
User logs in -> Server validates credentials -> Server returns access token + refresh token
                                                           |
                                                           v
                                              Tokens stored securely on device
                                                           |
                                                           v
                                              Access token sent with API requests
                                                           |
                                                           v
                                    Access token expires -> Refresh token gets new access token
                                                           |
                                                           v
                                    Refresh token expires -> User must log in again
```

**Token Types Explained**

1. **Access Token (Auth Key)**: Short-lived token (typically 15 minutes to 1 hour) sent with every API request to prove the user is authenticated. If stolen, damage is limited due to short lifespan.

2. **Refresh Token**: Long-lived token (days to weeks) used only to obtain new access tokens. Never sent with regular API requests. Stored more securely.

3. **Session Object**: Server-side representation of the user's authentication state, including user ID, permissions, and token expiration times.

**Security Principles**

The login flow must follow these security principles:

- **Never log passwords**: Not even in debug mode
- **Use HTTPS only**: All authentication requests must be encrypted
- **Secure token storage**: Use platform-specific secure storage (Keychain/EncryptedSharedPreferences)
- **Token rotation**: Refresh tokens should be rotated on each use to limit damage from theft
- **Graceful expiration**: Handle expired sessions without crashing or exposing errors

**Prerequisites**

This lesson builds directly on Lesson 10.4 (Registration). You should have:
- The SecureStorageService from Lesson 10.4
- The AuthService with registration functionality
- Understanding of Riverpod for state management
- Serverpod authentication endpoints configured on your backend

Let us begin by building the login form UI.

