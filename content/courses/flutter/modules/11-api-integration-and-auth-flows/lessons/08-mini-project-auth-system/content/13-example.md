---
type: "EXAMPLE"
title: "Section 6: Testing the Auth System"
---

Now let us test the complete authentication system.

**Manual Testing Checklist**

| Test Case | Steps | Expected Result |
|-----------|-------|----------------|
| Initial Load | Launch app | Splash screen shows, then redirects to login |
| Login Success | Enter valid credentials, tap Login | Redirects to home, shows welcome message |
| Login Failure | Enter invalid password | Shows error snackbar |
| Google Login | Tap Continue with Google | Opens Google sign-in, then redirects to home |
| Register | Fill form, tap Create Account | Creates account, redirects to home |
| Register Validation | Submit with mismatched passwords | Shows validation error |
| Protected Route | Try to access /profile when logged out | Redirects to login with redirect param |
| Deep Link | Login after redirect | Goes to originally requested page |
| Logout | Tap Logout on profile | Clears session, redirects to login |
| Session Persistence | Login, close app, reopen | Still logged in |

**Running the App**

```bash
# Run on emulator or device
flutter run

# Run with verbose logging
flutter run --verbose
```

**Common Issues and Fixes**

| Issue | Cause | Solution |
|-------|-------|----------|
| Google Sign-In fails on Android | Missing SHA-1 | Add SHA-1 to Firebase Console |
| Google Sign-In fails on iOS | Missing URL scheme | Add CFBundleURLSchemes to Info.plist |
| Token not persisting | Secure storage issue | Check flutter_secure_storage setup |
| Redirect loop | Auth state not updating | Check that notifyListeners is called |
| Router not refreshing | Missing refreshListenable | Ensure RouterRefreshNotifier is wired correctly |

**Debug Tips**

1. Enable `debugLogDiagnostics: true` in GoRouter to see navigation logs
2. Use Riverpod's ProviderObserver to log state changes
3. Check secure storage with platform-specific debugging tools