---
type: "EXAMPLE"
title: "Session Expiration Handling and Protected Routes"
---

Graceful session expiration handling prevents users from seeing cryptic errors or empty screens. Instead, they see a helpful message and are redirected to login.

**Create an Auth Guard Widget**

Create `lib/widgets/auth_guard.dart`:

```dart
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../providers/auth_provider.dart';

/// Wraps protected content and handles authentication state.
/// Redirects to login when session expires or user is not authenticated.
class AuthGuard extends ConsumerWidget {
  /// The protected content to show when authenticated.
  final Widget child;
  
  /// Optional custom loading widget.
  final Widget? loadingWidget;
  
  /// Route to redirect to when not authenticated.
  final String loginRoute;
  
  const AuthGuard({
    super.key,
    required this.child,
    this.loadingWidget,
    this.loginRoute = '/login',
  });
  
  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final authState = ref.watch(authStateProvider);
    
    // Show loading indicator while checking auth
    if (authState.isLoading) {
      return loadingWidget ??
          const Scaffold(
            body: Center(
              child: CircularProgressIndicator(),
            ),
          );
    }
    
    // Handle session expired with message
    if (authState.errorMessage != null && 
        authState.errorMessage!.contains('expired')) {
      WidgetsBinding.instance.addPostFrameCallback((_) {
        _showSessionExpiredDialog(context, ref);
      });
      return const Scaffold(
        body: Center(
          child: CircularProgressIndicator(),
        ),
      );
    }
    
    // Redirect to login if not authenticated
    if (!authState.isAuthenticated) {
      WidgetsBinding.instance.addPostFrameCallback((_) {
        Navigator.of(context).pushNamedAndRemoveUntil(
          loginRoute,
          (route) => false,
        );
      });
      return const SizedBox.shrink();
    }
    
    // User is authenticated - show protected content
    return child;
  }
  
  void _showSessionExpiredDialog(BuildContext context, WidgetRef ref) {
    showDialog(
      context: context,
      barrierDismissible: false,
      builder: (context) => AlertDialog(
        icon: const Icon(
          Icons.timer_off,
          color: Colors.orange,
          size: 48,
        ),
        title: const Text('Session Expired'),
        content: const Text(
          'Your session has expired for security reasons. Please sign in again to continue.',
        ),
        actions: [
          FilledButton(
            onPressed: () {
              // Clear the error and navigate to login
              ref.read(authStateProvider.notifier).clearError();
              Navigator.of(context).pushNamedAndRemoveUntil(
                loginRoute,
                (route) => false,
              );
            },
            child: const Text('Sign In'),
          ),
        ],
      ),
    );
  }
}
```

**Create a Session Expiration Listener**

Create `lib/widgets/session_expiration_listener.dart` for handling expiration anywhere in the app:

```dart
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../providers/auth_provider.dart';
import '../services/session_manager.dart';

/// Listens for session expiration and shows appropriate UI.
/// Place this near the root of your app to catch expiration globally.
class SessionExpirationListener extends ConsumerStatefulWidget {
  final Widget child;
  
  const SessionExpirationListener({
    super.key,
    required this.child,
  });
  
  @override
  ConsumerState<SessionExpirationListener> createState() =>
      _SessionExpirationListenerState();
}

class _SessionExpirationListenerState
    extends ConsumerState<SessionExpirationListener> {
  bool _hasShownExpirationDialog = false;
  
  @override
  void initState() {
    super.initState();
    // Listen to session state changes
    final sessionManager = ref.read(sessionManagerProvider);
    sessionManager.sessionStateStream.listen(_handleSessionChange);
  }
  
  void _handleSessionChange(SessionState state) {
    if (state == SessionState.expired && !_hasShownExpirationDialog) {
      _hasShownExpirationDialog = true;
      _showExpirationDialog();
    }
  }
  
  void _showExpirationDialog() {
    if (!mounted) return;
    
    showDialog(
      context: context,
      barrierDismissible: false,
      builder: (context) => AlertDialog(
        icon: const Icon(
          Icons.lock_clock,
          color: Colors.orange,
          size: 48,
        ),
        title: const Text('Session Expired'),
        content: const Text(
          'For your security, you have been signed out due to inactivity. '
          'Please sign in again to continue.',
        ),
        actions: [
          FilledButton(
            onPressed: () {
              Navigator.of(context).pop();
              Navigator.of(context).pushNamedAndRemoveUntil(
                '/login',
                (route) => false,
              );
              _hasShownExpirationDialog = false;
            },
            child: const Text('Sign In'),
          ),
        ],
      ),
    );
  }
  
  @override
  Widget build(BuildContext context) {
    // Also listen via Riverpod for redundancy
    ref.listen<AuthState>(authStateProvider, (previous, next) {
      if (next.errorMessage?.contains('expired') == true &&
          !_hasShownExpirationDialog) {
        _hasShownExpirationDialog = true;
        _showExpirationDialog();
      }
    });
    
    return widget.child;
  }
}
```

**Use Auth Guard in Your Router**

```dart
// In your app's route configuration:
final routes = {
  '/': (context) => const SplashScreen(),
  '/login': (context) => const LoginScreen(),
  '/register': (context) => const RegistrationScreen(),
  '/forgot-password': (context) => const ForgotPasswordScreen(),
  '/home': (context) => const AuthGuard(child: HomeScreen()),
  '/profile': (context) => const AuthGuard(child: ProfileScreen()),
  '/settings': (context) => const AuthGuard(child: SettingsScreen()),
};

// Wrap your MaterialApp with SessionExpirationListener:
class MyApp extends StatelessWidget {
  const MyApp({super.key});
  
  @override
  Widget build(BuildContext context) {
    return ProviderScope(
      child: Consumer(
        builder: (context, ref, child) {
          return SessionExpirationListener(
            child: MaterialApp(
              title: 'Your App',
              theme: ThemeData.light(useMaterial3: true),
              routes: routes,
              initialRoute: '/',
            ),
          );
        },
      ),
    );
  }
}
```

This comprehensive session expiration handling ensures users always understand what happened and can easily recover by logging in again.

