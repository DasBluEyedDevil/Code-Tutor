---
type: "KEY_POINT"
title: "JWT Security Best Practices"
---


### 1. Keep Your Secret Key SECRET

**Never hardcode in production!**
```dart
// BAD - hardcoded secret
const secret = 'my-secret-key';

// GOOD - use environment variables
import 'dart:io';
final secret = Platform.environment['JWT_SECRET'] ?? 'dev-only-key';
```

### 2. Use HTTPS in Production

JWTs are encoded, NOT encrypted. Anyone who intercepts the token can read the payload. HTTPS encrypts all traffic.

### 3. Set Short Expiration Times

- Access tokens: 15 minutes to 24 hours
- Refresh tokens: 7-30 days
- Shorter = more secure, but requires more logins

### 4. Hash Passwords with bcrypt

```dart
// Add: bcrypt: ^1.1.3 to pubspec.yaml
import 'package:bcrypt/bcrypt.dart';

// When user registers:
final hashedPassword = BCrypt.hashpw(password, BCrypt.gensalt());
// Store hashedPassword in database

// When user logs in:
final isValid = BCrypt.checkpw(inputPassword, storedHash);
```

### 5. Never Store Sensitive Data in JWT Payload

```dart
// BAD - sensitive data in token
{'userId': '123', 'creditCard': '4242...'}

// GOOD - only identifiers
{'userId': '123', 'email': 'user@example.com'}
```

### 6. Handle Token Refresh

For better UX, implement refresh tokens:
- Access token: short-lived (15 min)
- Refresh token: long-lived (30 days)
- When access expires, use refresh token to get new access token
- Only force re-login when refresh token expires

