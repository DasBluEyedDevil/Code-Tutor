---
type: KEY_POINT
---

- Structure authentication with MVVM: AuthViewModel manages state, AuthRepository handles API calls, screens observe state via Riverpod
- Store tokens securely with `flutter_secure_storage` and restore auth state on app launch for seamless session persistence
- Form validation should run in real-time (on change) for email format and password strength, plus on submit for server-side errors
- Handle loading, success, and error states explicitly in the UI with `AsyncValue` to prevent the screen from freezing during API calls
- Navigation guards (GoRouter redirect) ensure unauthenticated users cannot access protected screens even via deep links
