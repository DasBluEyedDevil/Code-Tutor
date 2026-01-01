---
type: "EXAMPLE"
title: "Serverpod Client Provider"
---


**Client Configuration**

Create a provider for the Serverpod client that handles connection configuration:



```dart
// app/lib/shared/services/client_provider.dart
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:serverpod_flutter/serverpod_flutter.dart';
import 'package:social_chat_shared/social_chat_shared.dart';

import '../../core/config/app_config.dart';

/// Serverpod client provider
final clientProvider = Provider<Client>((ref) {
  return Client(
    AppConfig.serverUrl,
    authenticationKeyManager: FlutterAuthenticationKeyManager(),
  )..connectivityMonitor = FlutterConnectivityMonitor();
});

/// Session manager for authentication state
final sessionManagerProvider = Provider<SessionManager>((ref) {
  final client = ref.watch(clientProvider);
  return SessionManager(
    caller: client.modules.auth,
  );
});

/// Stream of authentication state changes
final authStateProvider = StreamProvider<AuthenticationState>((ref) {
  final sessionManager = ref.watch(sessionManagerProvider);
  return sessionManager.stream;
});

/// Current user info (null if not authenticated)
final currentUserProvider = Provider<UserInfo?>((ref) {
  final authState = ref.watch(authStateProvider);
  return authState.when(
    data: (state) => state.signedIn ? state.userInfo : null,
    loading: () => null,
    error: (_, __) => null,
  );
});
```
