---
type: "WARNING"
title: "Common Auth Security Mistakes"
---


**1. Revealing User Existence**

Problem: Different error messages for existing vs non-existing emails.

```dart
// BAD - Reveals if email exists
if (user == null) {
  throw Exception('Email not found');
}
if (!passwordMatch) {
  throw Exception('Wrong password');
}

// GOOD - Generic message
if (user == null || !passwordMatch) {
  throw AuthException(
    message: 'Invalid email or password',
  );
}
```

**2. Trusting Client-Provided Tokens**

Problem: Using OAuth tokens without server verification.

```dart
// BAD - Trusting client data
Future<void> signIn(String email, String googleId) async {
  // Anyone can call this with any googleId!
  await createUser(email, googleId);
}

// GOOD - Verify with provider
Future<void> signIn(String idToken) async {
  final verified = await verifyGoogleToken(idToken);
  if (verified == null) throw Exception('Invalid token');
  await createUser(verified.email, verified.sub);
}
```

**3. Not Rate Limiting**

Problem: Allowing unlimited login attempts.

```dart
// GOOD - Rate limit implementation
Future<void> login(Session session, String email, String password) async {
  final attempts = await getRecentAttempts(session, email);
  
  if (attempts >= 5) {
    throw AuthException(
      message: 'Too many attempts. Try again in 15 minutes.',
    );
  }
  
  // ... continue with login
}
```

**4. Storing Tokens Insecurely**

Problem: Storing tokens in localStorage or plain SharedPreferences.

```dart
// BAD - Insecure storage
SharedPreferences.setString('token', authToken);

// GOOD - Use secure storage
FlutterSecureStorage().write(key: 'token', value: authToken);
```

**5. Not Invalidating Sessions**

Problem: Old sessions remain valid after password change.

```dart
// GOOD - Invalidate all sessions on password change
Future<void> changePassword(...) async {
  await updatePassword(newPassword);
  await invalidateAllSessions(userId);  // Important!
  await createNewSession(userId);  // Only current session
}
```

