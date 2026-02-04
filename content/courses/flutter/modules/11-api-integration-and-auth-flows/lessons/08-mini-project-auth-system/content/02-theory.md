---
type: "THEORY"
title: "Section 1: Project Structure"
---

A feature-first folder structure keeps authentication code organized and maintainable.

**Project Structure**

```
lib/
├── main.dart
├── app.dart
│
├── features/
│   └── auth/
│       ├── data/
│       │   ├── auth_service.dart
│       │   └── secure_storage_service.dart
│       │
│       ├── domain/
│       │   ├── auth_state.dart
│       │   └── user_model.dart
│       │
│       ├── presentation/
│       │   ├── screens/
│       │   │   ├── login_screen.dart
│       │   │   ├── register_screen.dart
│       │   │   ├── splash_screen.dart
│       │   │   └── profile_screen.dart
│       │   │
│       │   └── widgets/
│       │       ├── auth_form_field.dart
│       │       ├── social_login_button.dart
│       │       └── loading_button.dart
│       │
│       └── providers/
│           └── auth_provider.dart
│
├── core/
│   ├── router/
│   │   └── app_router.dart
│   │
│   └── widgets/
│       └── app_shell.dart
│
└── home/
    └── home_screen.dart
```

**File Responsibilities**

| File | Purpose |
|------|---------|  
| `auth_service.dart` | API calls for login, register, OAuth |
| `secure_storage_service.dart` | Token storage with flutter_secure_storage |
| `auth_state.dart` | Immutable state class for auth status |
| `user_model.dart` | User data model |
| `auth_provider.dart` | Riverpod notifier managing auth state |
| `app_router.dart` | GoRouter with auth-based redirects |
| `login_screen.dart` | Email/password + OAuth login |
| `register_screen.dart` | New user registration |
| `splash_screen.dart` | Initial loading while checking auth |
| `profile_screen.dart` | User info and logout |

**Dependencies**

Add these to your `pubspec.yaml`:

```yaml
dependencies:
  flutter:
    sdk: flutter
  flutter_riverpod: ^2.5.1
  go_router: ^17.0.0
  flutter_secure_storage: ^9.2.2
  google_sign_in: ^6.2.1
  dio: ^5.7.0  # For HTTP requests

dev_dependencies:
  flutter_test:
    sdk: flutter
  mocktail: ^1.0.4
```