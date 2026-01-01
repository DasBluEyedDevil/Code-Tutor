---
type: "EXAMPLE"
title: "Creating JWT Tokens"
---


First, add the JWT package to your Dart Frog project:

```yaml
# pubspec.yaml
dependencies:
  dart_frog: ^1.0.0
  dart_jsonwebtoken: ^2.12.0
```

Now let's create tokens:



```dart
// lib/utils/jwt_helper.dart
import 'package:dart_jsonwebtoken/dart_jsonwebtoken.dart';

// Secret key - in production, use environment variable!
// This should be a long, random string
const String jwtSecretKey = 'your-super-secret-key-keep-this-safe';

/// Creates a JWT token for an authenticated user
String createToken(String userId, String email) {
  // Create the JWT payload
  final jwt = JWT(
    {
      'userId': userId,
      'email': email,
      // Token expires in 24 hours
      'exp': DateTime.now()
          .add(Duration(hours: 24))
          .millisecondsSinceEpoch ~/ 1000,
      // Issued at time
      'iat': DateTime.now().millisecondsSinceEpoch ~/ 1000,
    },
  );
  
  // Sign the token with our secret key
  // This creates the signature that proves the token is authentic
  return jwt.sign(SecretKey(jwtSecretKey));
}

// Example usage:
// final token = createToken('user_123', 'john@example.com');
// Returns: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyS...
```
