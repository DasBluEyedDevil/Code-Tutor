---
type: "EXAMPLE"
title: "Setting Up Auth Dependencies"
---


**Adding Serverpod Auth**

First, add the auth packages to your project:



```yaml
# server/pubspec.yaml
name: social_chat_server
environment:
  sdk: '>=3.0.0 <4.0.0'

dependencies:
  serverpod: ^2.0.0
  serverpod_auth_server: ^2.0.0  # Add this
  
dev_dependencies:
  serverpod_test: ^2.0.0

---

# client/pubspec.yaml
name: social_chat_client
environment:
  sdk: '>=3.0.0 <4.0.0'

dependencies:
  serverpod_client: ^2.0.0
  serverpod_auth_client: ^2.0.0  # Add this

---

# flutter/pubspec.yaml (your Flutter app)
name: social_chat_flutter
environment:
  sdk: '>=3.0.0 <4.0.0'

dependencies:
  flutter:
    sdk: flutter
  social_chat_client:
    path: ../client
  serverpod_auth_shared_flutter: ^2.0.0  # Add this
  serverpod_auth_email_flutter: ^2.0.0   # For email auth UI
  serverpod_auth_google_flutter: ^2.0.0  # For Google sign-in
  serverpod_auth_apple_flutter: ^2.0.0   # For Apple sign-in
```
