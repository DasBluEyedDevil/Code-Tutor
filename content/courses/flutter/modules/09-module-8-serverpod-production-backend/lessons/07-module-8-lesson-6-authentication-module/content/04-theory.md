---
type: "THEORY"
title: "Setting Up the Auth Module"
---

Before using Serverpod's authentication features, you need to add the auth module to your project. This is a separate package that integrates seamlessly with your Serverpod server.

**Step 1: Add Dependencies**

In your server's pubspec.yaml (my_project_server/pubspec.yaml):

```yaml
dependencies:
  serverpod: ^2.0.0
  serverpod_auth_server: ^2.0.0  # Add this line
```

In your client's pubspec.yaml (my_project_client/pubspec.yaml):

```yaml
dependencies:
  serverpod_client: ^2.0.0
  serverpod_auth_client: ^2.0.0  # Add this line
```

In your Flutter app's pubspec.yaml (my_project_flutter/pubspec.yaml):

```yaml
dependencies:
  flutter:
    sdk: flutter
  my_project_client:
    path: ../my_project_client
  serverpod_auth_shared_flutter: ^2.0.0  # Add this line
  serverpod_auth_email_flutter: ^2.0.0   # For email auth UI
  serverpod_auth_google_flutter: ^2.0.0  # For Google Sign-In
  serverpod_auth_apple_flutter: ^2.0.0   # For Apple Sign-In
```

Run `flutter pub get` in each package directory.

**Step 2: Add Auth Tables to Database**

The auth module requires specific database tables. Create a migration or add these tables. Serverpod provides a migration that creates the necessary tables for users, sessions, and authentication data.

Run:
```bash
cd my_project_server
serverpod generate
dart run bin/main.dart --apply-migrations
```

