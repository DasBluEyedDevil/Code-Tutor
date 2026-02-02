---
type: "EXAMPLE"
title: "Verifying JWT Tokens"
---


When a request comes in with a token, we need to verify it's valid:



```dart
// lib/utils/jwt_helper.dart (continued)
import 'package:dart_jsonwebtoken/dart_jsonwebtoken.dart';

/// Verifies a JWT token and returns the payload if valid
/// Returns null if token is invalid or expired
Map<String, dynamic>? verifyToken(String token) {
  try {
    // Verify the token signature and decode it
    final jwt = JWT.verify(token, SecretKey(jwtSecretKey));
    
    // Return the payload (userId, email, etc.)
    return jwt.payload as Map<String, dynamic>;
    
  } on JWTExpiredException {
    // Token has expired (past the 'exp' time)
    print('Token expired');
    return null;
    
  } on JWTInvalidException {
    // Token signature is invalid (tampered or wrong secret)
    print('Invalid token');
    return null;
    
  } catch (e) {
    // Any other error (malformed token, etc.)
    print('Token verification failed: $e');
    return null;
  }
}

// Example usage:
// final payload = verifyToken(token);
// if (payload != null) {
//   final userId = payload['userId'];
//   final email = payload['email'];
//   // User is authenticated!
// } else {
//   // Invalid token - reject request
// }
```
