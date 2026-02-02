---
type: "EXAMPLE"
title: "Session State Management with Riverpod"
---

To manage authentication state globally and reactively update the UI when auth state changes, we will use Riverpod. This allows any widget to observe and react to authentication changes.

**Create the Auth State Provider**

Update `lib/providers/auth_provider.dart`:

```dart
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:serverpod_flutter/serverpod_flutter.dart';
import 'package:your_app_client/your_app_client.dart';
import '../services/auth_service.dart';
import '../services/secure_storage_service.dart';
import '../services/session_manager.dart';
import '../utils/auth_interceptor.dart';
import '../utils/api_client.dart';

// ============ Core Providers ============

/// Serverpod client provider
final clientProvider = Provider<Client>((ref) {
  return Client(
    'https://your-server-url.com/',
    authenticationKeyManager: FlutterAuthenticationKeyManager(),
  )..connectivityMonitor = FlutterConnectivityMonitor();
});

/// Secure storage provider
final secureStorageProvider = Provider<SecureStorageService>((ref) {
  return SecureStorageService();
});

/// Auth service provider
final authServiceProvider = Provider<AuthService>((ref) {
  return AuthService(
    client: ref.watch(clientProvider),
    secureStorage: ref.watch(secureStorageProvider),
  );
});

/// Session manager provider
final sessionManagerProvider = Provider<SessionManager>((ref) {
  return SessionManager(
    authService: ref.watch(authServiceProvider),
    secureStorage: ref.watch(secureStorageProvider),
  );
});

// ============ Auth State ============

/// Represents the current authentication state.
class AuthState {
  final bool isAuthenticated;
  final bool isLoading;
  final UserInfo? user;
  final String? errorMessage;
  
  const AuthState({
    this.isAuthenticated = false,
    this.isLoading = false,
    this.user,
    this.errorMessage,
  });
  
  /// Initial state - checking for existing session
  factory AuthState.initial() {
    return const AuthState(isLoading: true);
  }
  
  /// Authenticated state with user info
  factory AuthState.authenticated(UserInfo user) {
    return AuthState(
      isAuthenticated: true,
      user: user,
    );
  }
  
  /// Unauthenticated state
  factory AuthState.unauthenticated() {
    return const AuthState();
  }
  
  /// Error state
  factory AuthState.error(String message) {
    return AuthState(errorMessage: message);
  }
  
  /// Creates a copy with updated values
  AuthState copyWith({
    bool? isAuthenticated,
    bool? isLoading,
    UserInfo? user,
    String? errorMessage,
  }) {
    return AuthState(
      isAuthenticated: isAuthenticated ?? this.isAuthenticated,
      isLoading: isLoading ?? this.isLoading,
      user: user ?? this.user,
      errorMessage: errorMessage ?? this.errorMessage,
    );
  }
}

/// Auth state notifier - manages authentication state changes
class AuthNotifier extends StateNotifier<AuthState> {
  final AuthService _authService;
  final SecureStorageService _secureStorage;
  final SessionManager _sessionManager;
  
  AuthNotifier({
    required AuthService authService,
    required SecureStorageService secureStorage,
    required SessionManager sessionManager,
  })  : _authService = authService,
        _secureStorage = secureStorage,
        _sessionManager = sessionManager,
        super(AuthState.initial()) {
    // Listen to session state changes
    _sessionManager.sessionStateStream.listen(_handleSessionStateChange);
  }
  
  void _handleSessionStateChange(SessionState sessionState) {
    switch (sessionState) {
      case SessionState.authenticated:
        // State will be updated when login succeeds
        break;
      case SessionState.unauthenticated:
        state = AuthState.unauthenticated();
        break;
      case SessionState.expired:
        state = AuthState.error('Your session has expired. Please log in again.');
        break;
      case SessionState.checking:
        state = AuthState.initial();
        break;
      case SessionState.unknown:
        break;
    }
  }
  
  /// Sets the authenticated state with user info.
  void setAuthenticated(UserInfo user) {
    state = AuthState.authenticated(user);
    _sessionManager.onLoginSuccess(user);
  }
  
  /// Signs out the current user.
  Future<void> signOut() async {
    state = state.copyWith(isLoading: true);
    
    try {
      // Invalidate session on server
      await _authService.signOut();
    } catch (e) {
      // Continue with local logout even if server call fails
    }
    
    // Clear local data
    await _sessionManager.logout();
    state = AuthState.unauthenticated();
  }
  
  /// Clears any error message.
  void clearError() {
    state = state.copyWith(errorMessage: null);
  }
}

/// Auth state provider
final authStateProvider = StateNotifierProvider<AuthNotifier, AuthState>((ref) {
  return AuthNotifier(
    authService: ref.watch(authServiceProvider),
    secureStorage: ref.watch(secureStorageProvider),
    sessionManager: ref.watch(sessionManagerProvider),
  );
});

// ============ Convenience Providers ============

/// Provider for checking if user is authenticated
final isAuthenticatedProvider = Provider<bool>((ref) {
  return ref.watch(authStateProvider).isAuthenticated;
});

/// Provider for current user info
final currentUserProvider = Provider<UserInfo?>((ref) {
  return ref.watch(authStateProvider).user;
});

/// Auth interceptor provider
final authInterceptorProvider = Provider<AuthInterceptor>((ref) {
  final authNotifier = ref.read(authStateProvider.notifier);
  
  return AuthInterceptor(
    secureStorage: ref.watch(secureStorageProvider),
    authService: ref.watch(authServiceProvider),
    onSessionExpired: () {
      authNotifier.signOut();
    },
  );
});

/// API client with automatic token refresh
final apiClientProvider = Provider<ApiClient>((ref) {
  return ApiClient(
    client: ref.watch(clientProvider),
    authInterceptor: ref.watch(authInterceptorProvider),
  );
});
```

**Using Auth State in Widgets**

You can now observe auth state anywhere in your app:

```dart
// In any widget:
class HomeScreen extends ConsumerWidget {
  const HomeScreen({super.key});
  
  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final authState = ref.watch(authStateProvider);
    final user = ref.watch(currentUserProvider);
    
    if (authState.isLoading) {
      return const Center(child: CircularProgressIndicator());
    }
    
    if (!authState.isAuthenticated) {
      // Redirect to login
      WidgetsBinding.instance.addPostFrameCallback((_) {
        Navigator.of(context).pushReplacementNamed('/login');
      });
      return const SizedBox.shrink();
    }
    
    return Scaffold(
      appBar: AppBar(
        title: Text('Welcome, ${user?.userName ?? "User"}'),
        actions: [
          IconButton(
            icon: const Icon(Icons.logout),
            onPressed: () => ref.read(authStateProvider.notifier).signOut(),
          ),
        ],
      ),
      body: const Center(
        child: Text('You are logged in!'),
      ),
    );
  }
}
```

This Riverpod-based state management ensures your UI always reflects the current authentication state.

