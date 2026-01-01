---
type: "EXAMPLE"
title: "Session Persistence and Auto-Login"
---

One of the most important features for mobile apps is keeping users logged in across app restarts. Let us implement session persistence and automatic login on app startup.

**Create the Session Manager**

Create `lib/services/session_manager.dart`:

```dart
import 'dart:async';
import 'package:flutter/foundation.dart';
import 'package:your_app_client/your_app_client.dart';
import 'auth_service.dart';
import 'secure_storage_service.dart';

/// Manages user sessions including persistence, restoration, and expiration.
class SessionManager {
  final AuthService _authService;
  final SecureStorageService _secureStorage;
  
  // Stream controller for session state changes
  final _sessionStateController = StreamController<SessionState>.broadcast();
  
  // Current session state
  SessionState _currentState = SessionState.unknown;
  
  // Timer for proactive token refresh
  Timer? _refreshTimer;
  
  SessionManager({
    required AuthService authService,
    required SecureStorageService secureStorage,
  })  : _authService = authService,
        _secureStorage = secureStorage;
  
  /// Stream of session state changes.
  Stream<SessionState> get sessionStateStream => _sessionStateController.stream;
  
  /// Current session state.
  SessionState get currentState => _currentState;
  
  /// Initializes the session manager and checks for existing session.
  /// Call this on app startup before showing any UI.
  Future<SessionState> initialize() async {
    try {
      _updateState(SessionState.checking);
      
      // Check if we have stored credentials
      final hasCredentials = await _secureStorage.hasAuthCredentials();
      if (!hasCredentials) {
        _updateState(SessionState.unauthenticated);
        return _currentState;
      }
      
      // Check if token is expired
      final isExpired = await _secureStorage.isTokenExpiredOrExpiring();
      if (isExpired) {
        // Try to refresh the token
        final refreshed = await refreshSession();
        if (!refreshed) {
          await _secureStorage.clearAllAuthData();
          _updateState(SessionState.unauthenticated);
          return _currentState;
        }
      }
      
      // Verify session with server
      final user = await _authService.checkStoredSession();
      if (user != null) {
        _updateState(SessionState.authenticated, user: user);
        _startRefreshTimer();
      } else {
        await _secureStorage.clearAllAuthData();
        _updateState(SessionState.unauthenticated);
      }
      
      return _currentState;
    } catch (e) {
      if (kDebugMode) {
        print('Session initialization error: $e');
      }
      // On error, clear potentially corrupt data and require fresh login
      await _secureStorage.clearAllAuthData();
      _updateState(SessionState.unauthenticated);
      return _currentState;
    }
  }
  
  /// Attempts to refresh the current session.
  /// Returns true if refresh was successful, false otherwise.
  Future<bool> refreshSession() async {
    try {
      final refreshToken = await _secureStorage.getRefreshToken();
      if (refreshToken == null) {
        return false;
      }
      
      // Call Serverpod to refresh the session
      // The exact method depends on your Serverpod auth configuration
      final response = await _authService.refreshAuthToken(refreshToken);
      
      if (response.success && response.newAuthToken != null) {
        // Store new tokens
        await _secureStorage.saveAuthToken(response.newAuthToken!);
        if (response.newRefreshToken != null) {
          await _secureStorage.saveRefreshToken(response.newRefreshToken!);
        }
        await _secureStorage.saveTokenExpiration(
          DateTime.now().add(const Duration(hours: 1)),
        );
        
        // Restart the refresh timer
        _startRefreshTimer();
        
        return true;
      }
      
      return false;
    } catch (e) {
      if (kDebugMode) {
        print('Token refresh error: $e');
      }
      return false;
    }
  }
  
  /// Starts a timer to proactively refresh the token before it expires.
  void _startRefreshTimer() {
    _refreshTimer?.cancel();
    
    // Refresh 5 minutes before expiration
    // Assuming 1 hour token lifetime, refresh at 55 minutes
    _refreshTimer = Timer(const Duration(minutes: 55), () async {
      final success = await refreshSession();
      if (!success) {
        // Token refresh failed - session expired
        _handleSessionExpired();
      }
    });
  }
  
  /// Handles session expiration gracefully.
  void _handleSessionExpired() {
    _refreshTimer?.cancel();
    _updateState(SessionState.expired);
  }
  
  /// Called when user successfully logs in.
  void onLoginSuccess(UserInfo user) {
    _updateState(SessionState.authenticated, user: user);
    _startRefreshTimer();
  }
  
  /// Called when user logs out.
  Future<void> logout() async {
    _refreshTimer?.cancel();
    await _secureStorage.clearAllAuthData();
    _updateState(SessionState.unauthenticated);
  }
  
  /// Updates the session state and notifies listeners.
  void _updateState(SessionState newState, {UserInfo? user}) {
    _currentState = newState;
    _sessionStateController.add(newState);
  }
  
  /// Disposes resources.
  void dispose() {
    _refreshTimer?.cancel();
    _sessionStateController.close();
  }
}

/// Represents the current session state.
enum SessionState {
  /// Session state is being determined.
  unknown,
  
  /// Checking for existing session.
  checking,
  
  /// User is authenticated with a valid session.
  authenticated,
  
  /// No active session - user needs to log in.
  unauthenticated,
  
  /// Session expired - user needs to log in again.
  expired,
}
```

**Implement Auto-Login on App Startup**

Create `lib/screens/splash_screen.dart`:

```dart
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../providers/auth_provider.dart';
import '../services/session_manager.dart';

/// Splash screen that checks for existing session on app startup.
class SplashScreen extends ConsumerStatefulWidget {
  const SplashScreen({super.key});

  @override
  ConsumerState<SplashScreen> createState() => _SplashScreenState();
}

class _SplashScreenState extends ConsumerState<SplashScreen> {
  @override
  void initState() {
    super.initState();
    _initializeSession();
  }
  
  Future<void> _initializeSession() async {
    // Add a minimum delay for branding/UX purposes
    await Future.delayed(const Duration(milliseconds: 1500));
    
    final sessionManager = ref.read(sessionManagerProvider);
    final sessionState = await sessionManager.initialize();
    
    if (!mounted) return;
    
    // Navigate based on session state
    switch (sessionState) {
      case SessionState.authenticated:
        Navigator.of(context).pushReplacementNamed('/home');
        break;
      case SessionState.unauthenticated:
      case SessionState.expired:
      case SessionState.unknown:
      case SessionState.checking:
        Navigator.of(context).pushReplacementNamed('/login');
        break;
    }
  }
  
  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    
    return Scaffold(
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            // App logo
            Icon(
              Icons.flutter_dash,
              size: 80,
              color: theme.colorScheme.primary,
            ),
            const SizedBox(height: 24),
            
            // App name
            Text(
              'Your App Name',
              style: theme.textTheme.headlineMedium?.copyWith(
                fontWeight: FontWeight.bold,
                color: theme.colorScheme.primary,
              ),
            ),
            const SizedBox(height: 48),
            
            // Loading indicator
            const CircularProgressIndicator(),
            const SizedBox(height: 16),
            
            Text(
              'Loading...',
              style: theme.textTheme.bodyMedium?.copyWith(
                color: theme.colorScheme.onSurfaceVariant,
              ),
            ),
          ],
        ),
      ),
    );
  }
}
```

This implementation automatically checks for existing sessions on app startup and handles token refresh seamlessly in the background.

