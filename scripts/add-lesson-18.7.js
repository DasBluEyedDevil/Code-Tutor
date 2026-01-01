const fs = require('fs');

const coursePath = 'C:/Users/dasbl/Downloads/Code-Tutor/content/courses/flutter/course.json';

// Read the file
const data = fs.readFileSync(coursePath, 'utf8');
const course = JSON.parse(data);

// Find module 18 (capstone module)
const module18 = course.modules.find(m => m.id === 'module-18');

if (!module18) {
  console.error('Module 18 not found!');
  process.exit(1);
}

// Check if lesson 18.7 already exists
const existingLesson = module18.lessons.find(l => l.id === '18.7');
if (existingLesson) {
  console.log('Lesson 18.7 already exists!');
  process.exit(0);
}

// New lesson 18.7
const lesson187 = {
  "id": "18.7",
  "title": "Frontend: Auth Screens",
  "moduleId": "module-18",
  "order": 7,
  "estimatedMinutes": 50,
  "difficulty": "advanced",
  "contentSections": [
    {
      "type": "THEORY",
      "title": "Auth Architecture Overview",
      "content": `
**MVVM Pattern for Authentication**

Authentication is one of the most critical features in any application. A well-architected auth system needs to handle state persistence, secure token storage, navigation guards, and seamless user experience across app restarts. We'll use the MVVM (Model-View-ViewModel) pattern with Riverpod for state management.

**Authentication Architecture Layers**

| Layer | Component | Responsibility |
|-------|-----------|----------------|
| **Model** | AuthState, User | Data structures for auth state |
| **Repository** | AuthRepository | API calls to Serverpod auth client |
| **State** | AuthNotifier | Business logic, state transitions |
| **View** | LoginScreen, RegisterScreen | UI presentation |

**Riverpod Auth State Management**

Using Riverpod's \`AsyncNotifier\` pattern provides:

- **Automatic loading states**: Show spinners during async operations
- **Error handling**: Catch and display auth errors gracefully
- **State persistence**: Restore auth state on app restart
- **Dependency injection**: Easy testing with provider overrides

\`\`\`
┌─────────────────────────────────────────────────────────────┐
│                    Auth State Flow                          │
├─────────────────────────────────────────────────────────────┤
│                                                             │
│   ┌──────────┐    ┌──────────────┐    ┌───────────────┐    │
│   │ Unauthd  │───>│ Authenticating│───>│ Authenticated │    │
│   └──────────┘    └──────────────┘    └───────────────┘    │
│        ^                                      │             │
│        │                                      │             │
│        └──────────────────────────────────────┘             │
│                      (logout/token expired)                 │
│                                                             │
└─────────────────────────────────────────────────────────────┘
\`\`\`

**Token Storage with flutter_secure_storage**

Secure credential storage is essential:

- **iOS**: Keychain Services (hardware-backed encryption)
- **Android**: EncryptedSharedPreferences or KeyStore
- **Web**: Encrypted localStorage (with limitations)

**Key Security Principles**

1. **Never store passwords**: Only store tokens
2. **Use secure storage**: No SharedPreferences for tokens
3. **Implement token refresh**: Handle expiration gracefully
4. **Clear on logout**: Remove all auth data
5. **Biometric protection**: Optional additional layer

**Auth State Changes and Navigation**

Auth state changes should trigger navigation:

| State Change | Navigation Action |
|--------------|-------------------|
| Login success | Navigate to home, clear stack |
| Logout | Navigate to login, clear stack |
| Token expired | Show re-auth dialog or redirect |
| Registration success | Navigate to email verification |
| Email verified | Navigate to home |

**Error Handling Strategy**

| Error Type | User Experience |
|------------|----------------|
| Invalid credentials | Show inline error, keep form data |
| Network error | Show retry option, offline indicator |
| Rate limited | Show countdown timer |
| Account locked | Show unlock instructions |
| Server error | Show generic message, log details |

`
    },
    {
      "type": "EXAMPLE",
      "title": "Auth State and Providers",
      "content": `
**Implementing Auth State Management with Riverpod**

We'll create a robust auth state system that handles authentication, token storage, and seamless state restoration.

`,
      "code": `// lib/features/auth/domain/auth_state.dart
import 'package:freezed_annotation/freezed_annotation.dart';
import 'package:serverpod_auth_shared_flutter/serverpod_auth_shared_flutter.dart';

part 'auth_state.freezed.dart';

/// Represents the current authentication state
@freezed
class AuthState with _\$AuthState {
  const AuthState._();

  /// User is not authenticated
  const factory AuthState.unauthenticated() = AuthStateUnauthenticated;

  /// Authentication is in progress
  const factory AuthState.authenticating() = AuthStateAuthenticating;

  /// User is authenticated
  const factory AuthState.authenticated({
    required UserInfo userInfo,
    required String authToken,
  }) = AuthStateAuthenticated;

  /// Authentication failed
  const factory AuthState.error({
    required String message,
    String? code,
  }) = AuthStateError;

  /// Check if user is authenticated
  bool get isAuthenticated => this is AuthStateAuthenticated;

  /// Get user info if authenticated
  UserInfo? get userInfo => mapOrNull(
    authenticated: (state) => state.userInfo,
  );

  /// Get auth token if authenticated
  String? get authToken => mapOrNull(
    authenticated: (state) => state.authToken,
  );
}

/// Credentials for login
class LoginCredentials {
  final String email;
  final String password;

  const LoginCredentials({
    required this.email,
    required this.password,
  });
}

/// Registration data
class RegistrationData {
  final String email;
  final String password;
  final String? displayName;
  final bool acceptedTerms;

  const RegistrationData({
    required this.email,
    required this.password,
    this.displayName,
    required this.acceptedTerms,
  });
}

---

// lib/features/auth/data/auth_repository.dart
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:serverpod_auth_client/serverpod_auth_client.dart';
import '../../../core/providers/serverpod_client_provider.dart';
import '../domain/auth_state.dart';

/// Keys for secure storage
abstract class AuthStorageKeys {
  static const String authToken = 'auth_token';
  static const String refreshToken = 'refresh_token';
  static const String userId = 'user_id';
  static const String userEmail = 'user_email';
  static const String tokenExpiry = 'token_expiry';
}

/// Repository for authentication operations
class AuthRepository {
  final Ref _ref;
  final FlutterSecureStorage _secureStorage;

  AuthRepository(this._ref, this._secureStorage);

  /// Get the Serverpod client
  Client get _client => _ref.read(serverpodClientProvider);

  /// Login with email and password
  Future<AuthResult> login(LoginCredentials credentials) async {
    try {
      // Call Serverpod auth endpoint
      final response = await _client.modules.auth.email.authenticate(
        credentials.email,
        credentials.password,
      );

      if (!response.success) {
        return AuthResult.failure(
          message: response.failReason?.toString() ?? 'Login failed',
          code: 'auth_failed',
        );
      }

      // Store tokens securely
      await _storeAuthData(
        userInfo: response.userInfo!,
        keyId: response.keyId!,
        key: response.key!,
      );

      // Configure session manager
      await _ref.read(sessionManagerProvider).registerSignedInUser(
        response.userInfo!,
        response.keyId!,
        response.key!,
      );

      return AuthResult.success(
        userInfo: response.userInfo!,
        authToken: response.key!,
      );
    } on ServerpodClientException catch (e) {
      return AuthResult.failure(
        message: _mapServerError(e),
        code: 'server_error',
      );
    } catch (e) {
      return AuthResult.failure(
        message: 'Connection error. Please check your internet.',
        code: 'network_error',
      );
    }
  }

  /// Register a new user
  Future<AuthResult> register(RegistrationData data) async {
    try {
      // Create account
      final response = await _client.modules.auth.email.createAccountRequest(
        data.displayName ?? data.email.split('@').first,
        data.email,
        data.password,
      );

      if (!response) {
        return AuthResult.failure(
          message: 'Registration failed. Email may already be in use.',
          code: 'registration_failed',
        );
      }

      // Return success - user needs to verify email
      return AuthResult.pendingVerification(
        email: data.email,
      );
    } on ServerpodClientException catch (e) {
      return AuthResult.failure(
        message: _mapServerError(e),
        code: 'server_error',
      );
    } catch (e) {
      return AuthResult.failure(
        message: 'Connection error. Please try again.',
        code: 'network_error',
      );
    }
  }

  /// Logout and clear all stored data
  Future<void> logout() async {
    try {
      // Sign out from Serverpod
      await _ref.read(sessionManagerProvider).signOutDevice();
    } catch (e) {
      // Continue with local logout even if server call fails
    }

    // Clear secure storage
    await _secureStorage.deleteAll();
  }

  /// Restore auth state from stored tokens
  Future<AuthState> restoreSession() async {
    try {
      final sessionManager = _ref.read(sessionManagerProvider);

      // Check if we have a stored session
      if (sessionManager.isSignedIn) {
        final userInfo = await sessionManager.signedInUser;
        if (userInfo != null) {
          final token = await _secureStorage.read(
            key: AuthStorageKeys.authToken,
          );
          return AuthState.authenticated(
            userInfo: userInfo,
            authToken: token ?? '',
          );
        }
      }

      return const AuthState.unauthenticated();
    } catch (e) {
      // Clear potentially corrupted data
      await _secureStorage.deleteAll();
      return const AuthState.unauthenticated();
    }
  }

  /// Request password reset
  Future<bool> requestPasswordReset(String email) async {
    try {
      return await _client.modules.auth.email.initiatePasswordReset(email);
    } catch (e) {
      return false;
    }
  }

  /// Store auth data securely
  Future<void> _storeAuthData({
    required UserInfo userInfo,
    required int keyId,
    required String key,
  }) async {
    await Future.wait([
      _secureStorage.write(
        key: AuthStorageKeys.authToken,
        value: key,
      ),
      _secureStorage.write(
        key: AuthStorageKeys.userId,
        value: userInfo.id.toString(),
      ),
      _secureStorage.write(
        key: AuthStorageKeys.userEmail,
        value: userInfo.email ?? '',
      ),
    ]);
  }

  /// Map server errors to user-friendly messages
  String _mapServerError(ServerpodClientException e) {
    // Map common error codes
    switch (e.statusCode) {
      case 401:
        return 'Invalid email or password';
      case 403:
        return 'Account is locked. Please contact support.';
      case 429:
        return 'Too many attempts. Please try again later.';
      case 500:
        return 'Server error. Please try again.';
      default:
        return 'An error occurred. Please try again.';
    }
  }
}

/// Result of an auth operation
class AuthResult {
  final bool success;
  final UserInfo? userInfo;
  final String? authToken;
  final String? message;
  final String? code;
  final bool pendingVerification;
  final String? email;

  const AuthResult._(
    this.success, {
    this.userInfo,
    this.authToken,
    this.message,
    this.code,
    this.pendingVerification = false,
    this.email,
  });

  factory AuthResult.success({
    required UserInfo userInfo,
    required String authToken,
  }) => AuthResult._(
    true,
    userInfo: userInfo,
    authToken: authToken,
  );

  factory AuthResult.failure({
    required String message,
    String? code,
  }) => AuthResult._(
    false,
    message: message,
    code: code,
  );

  factory AuthResult.pendingVerification({
    required String email,
  }) => AuthResult._(
    true,
    pendingVerification: true,
    email: email,
  );
}

---

// lib/features/auth/providers/auth_provider.dart
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import '../data/auth_repository.dart';
import '../domain/auth_state.dart';

/// Provider for secure storage
final secureStorageProvider = Provider<FlutterSecureStorage>((ref) {
  return const FlutterSecureStorage(
    aOptions: AndroidOptions(
      encryptedSharedPreferences: true,
    ),
    iOptions: IOSOptions(
      accessibility: KeychainAccessibility.first_unlock_this_device,
    ),
  );
});

/// Provider for auth repository
final authRepositoryProvider = Provider<AuthRepository>((ref) {
  return AuthRepository(ref, ref.watch(secureStorageProvider));
});

/// Main auth state notifier
class AuthNotifier extends AsyncNotifier<AuthState> {
  @override
  Future<AuthState> build() async {
    // Restore session on app start
    return ref.read(authRepositoryProvider).restoreSession();
  }

  /// Login with email and password
  Future<void> login(LoginCredentials credentials) async {
    state = const AsyncValue.loading();

    final result = await ref.read(authRepositoryProvider).login(credentials);

    if (result.success && !result.pendingVerification) {
      state = AsyncValue.data(AuthState.authenticated(
        userInfo: result.userInfo!,
        authToken: result.authToken!,
      ));
    } else {
      state = AsyncValue.data(AuthState.error(
        message: result.message ?? 'Login failed',
        code: result.code,
      ));
    }
  }

  /// Register a new user
  Future<RegistrationResult> register(RegistrationData data) async {
    state = const AsyncValue.loading();

    final result = await ref.read(authRepositoryProvider).register(data);

    if (result.pendingVerification) {
      state = const AsyncValue.data(AuthState.unauthenticated());
      return RegistrationResult(
        success: true,
        pendingVerification: true,
        email: result.email,
      );
    }

    if (!result.success) {
      state = AsyncValue.data(AuthState.error(
        message: result.message ?? 'Registration failed',
        code: result.code,
      ));
      return RegistrationResult(
        success: false,
        message: result.message,
      );
    }

    return RegistrationResult(success: true);
  }

  /// Logout
  Future<void> logout() async {
    await ref.read(authRepositoryProvider).logout();
    state = const AsyncValue.data(AuthState.unauthenticated());
  }

  /// Request password reset
  Future<bool> requestPasswordReset(String email) async {
    return ref.read(authRepositoryProvider).requestPasswordReset(email);
  }

  /// Clear any error state
  void clearError() {
    if (state.valueOrNull is AuthStateError) {
      state = const AsyncValue.data(AuthState.unauthenticated());
    }
  }
}

/// Provider for auth state
final authProvider = AsyncNotifierProvider<AuthNotifier, AuthState>(() {
  return AuthNotifier();
});

/// Convenience provider for checking if authenticated
final isAuthenticatedProvider = Provider<bool>((ref) {
  return ref.watch(authProvider).valueOrNull?.isAuthenticated ?? false;
});

/// Provider for current user info
final currentUserProvider = Provider<UserInfo?>((ref) {
  return ref.watch(authProvider).valueOrNull?.userInfo;
});

class RegistrationResult {
  final bool success;
  final bool pendingVerification;
  final String? email;
  final String? message;

  const RegistrationResult({
    required this.success,
    this.pendingVerification = false,
    this.email,
    this.message,
  });
}`,
      "language": "dart"
    },
    {
      "type": "EXAMPLE",
      "title": "Login Screen Implementation",
      "content": `
**Building a Production-Ready Login Screen**

The login screen must handle form validation, error display, loading states, and provide access to password recovery.

`,
      "code": `// lib/features/auth/presentation/screens/login_screen.dart
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:go_router/go_router.dart';
import '../../domain/auth_state.dart';
import '../../providers/auth_provider.dart';
import '../widgets/auth_text_field.dart';
import '../widgets/auth_button.dart';
import '../widgets/social_login_buttons.dart';

class LoginScreen extends ConsumerStatefulWidget {
  const LoginScreen({super.key});

  @override
  ConsumerState<LoginScreen> createState() => _LoginScreenState();
}

class _LoginScreenState extends ConsumerState<LoginScreen> {
  final _formKey = GlobalKey<FormState>();
  final _emailController = TextEditingController();
  final _passwordController = TextEditingController();
  final _emailFocusNode = FocusNode();
  final _passwordFocusNode = FocusNode();

  bool _obscurePassword = true;
  bool _rememberMe = false;

  @override
  void dispose() {
    _emailController.dispose();
    _passwordController.dispose();
    _emailFocusNode.dispose();
    _passwordFocusNode.dispose();
    super.dispose();
  }

  Future<void> _handleLogin() async {
    // Clear any previous errors
    ref.read(authProvider.notifier).clearError();

    if (!_formKey.currentState!.validate()) {
      return;
    }

    await ref.read(authProvider.notifier).login(
      LoginCredentials(
        email: _emailController.text.trim(),
        password: _passwordController.text,
      ),
    );
  }

  void _navigateToRegister() {
    context.push('/register');
  }

  void _navigateToForgotPassword() {
    context.push('/forgot-password');
  }

  @override
  Widget build(BuildContext context) {
    final authState = ref.watch(authProvider);
    final theme = Theme.of(context);

    // Listen for successful authentication
    ref.listen<AsyncValue<AuthState>>(authProvider, (previous, next) {
      next.whenData((state) {
        if (state.isAuthenticated) {
          // Navigate to home and clear back stack
          context.go('/home');
        }
      });
    });

    return Scaffold(
      body: SafeArea(
        child: Center(
          child: SingleChildScrollView(
            padding: const EdgeInsets.all(24),
            child: ConstrainedBox(
              constraints: const BoxConstraints(maxWidth: 400),
              child: Form(
                key: _formKey,
                child: Column(
                  mainAxisAlignment: MainAxisAlignment.center,
                  crossAxisAlignment: CrossAxisAlignment.stretch,
                  children: [
                    // Logo and welcome text
                    Icon(
                      Icons.lock_outline,
                      size: 64,
                      color: theme.colorScheme.primary,
                    ),
                    const SizedBox(height: 24),
                    Text(
                      'Welcome Back',
                      style: theme.textTheme.headlineMedium?.copyWith(
                        fontWeight: FontWeight.bold,
                      ),
                      textAlign: TextAlign.center,
                    ),
                    const SizedBox(height: 8),
                    Text(
                      'Sign in to continue',
                      style: theme.textTheme.bodyLarge?.copyWith(
                        color: theme.colorScheme.onSurfaceVariant,
                      ),
                      textAlign: TextAlign.center,
                    ),
                    const SizedBox(height: 32),

                    // Error message
                    if (authState.valueOrNull is AuthStateError)
                      _buildErrorBanner(
                        (authState.valueOrNull as AuthStateError).message,
                      ),

                    // Email field
                    AuthTextField(
                      controller: _emailController,
                      focusNode: _emailFocusNode,
                      label: 'Email',
                      hint: 'Enter your email',
                      keyboardType: TextInputType.emailAddress,
                      textInputAction: TextInputAction.next,
                      prefixIcon: Icons.email_outlined,
                      enabled: !authState.isLoading,
                      validator: _validateEmail,
                      onFieldSubmitted: (_) {
                        _passwordFocusNode.requestFocus();
                      },
                    ),
                    const SizedBox(height: 16),

                    // Password field
                    AuthTextField(
                      controller: _passwordController,
                      focusNode: _passwordFocusNode,
                      label: 'Password',
                      hint: 'Enter your password',
                      obscureText: _obscurePassword,
                      textInputAction: TextInputAction.done,
                      prefixIcon: Icons.lock_outline,
                      enabled: !authState.isLoading,
                      suffixIcon: IconButton(
                        icon: Icon(
                          _obscurePassword
                              ? Icons.visibility_outlined
                              : Icons.visibility_off_outlined,
                        ),
                        onPressed: () {
                          setState(() {
                            _obscurePassword = !_obscurePassword;
                          });
                        },
                      ),
                      validator: _validatePassword,
                      onFieldSubmitted: (_) => _handleLogin(),
                    ),
                    const SizedBox(height: 8),

                    // Remember me and forgot password
                    Row(
                      mainAxisAlignment: MainAxisAlignment.spaceBetween,
                      children: [
                        Row(
                          children: [
                            Checkbox(
                              value: _rememberMe,
                              onChanged: authState.isLoading
                                  ? null
                                  : (value) {
                                      setState(() {
                                        _rememberMe = value ?? false;
                                      });
                                    },
                            ),
                            Text(
                              'Remember me',
                              style: theme.textTheme.bodyMedium,
                            ),
                          ],
                        ),
                        TextButton(
                          onPressed: authState.isLoading
                              ? null
                              : _navigateToForgotPassword,
                          child: const Text('Forgot Password?'),
                        ),
                      ],
                    ),
                    const SizedBox(height: 24),

                    // Login button
                    AuthButton(
                      onPressed: _handleLogin,
                      isLoading: authState.isLoading,
                      label: 'Sign In',
                    ),
                    const SizedBox(height: 24),

                    // Divider
                    Row(
                      children: [
                        const Expanded(child: Divider()),
                        Padding(
                          padding: const EdgeInsets.symmetric(horizontal: 16),
                          child: Text(
                            'Or continue with',
                            style: theme.textTheme.bodySmall?.copyWith(
                              color: theme.colorScheme.onSurfaceVariant,
                            ),
                          ),
                        ),
                        const Expanded(child: Divider()),
                      ],
                    ),
                    const SizedBox(height: 24),

                    // Social login buttons
                    const SocialLoginButtons(),
                    const SizedBox(height: 32),

                    // Register link
                    Row(
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: [
                        Text(
                          "Don't have an account?",
                          style: theme.textTheme.bodyMedium,
                        ),
                        TextButton(
                          onPressed: authState.isLoading
                              ? null
                              : _navigateToRegister,
                          child: const Text('Sign Up'),
                        ),
                      ],
                    ),
                  ],
                ),
              ),
            ),
          ),
        ),
      ),
    );
  }

  Widget _buildErrorBanner(String message) {
    return Container(
      margin: const EdgeInsets.only(bottom: 16),
      padding: const EdgeInsets.all(12),
      decoration: BoxDecoration(
        color: Theme.of(context).colorScheme.errorContainer,
        borderRadius: BorderRadius.circular(8),
      ),
      child: Row(
        children: [
          Icon(
            Icons.error_outline,
            color: Theme.of(context).colorScheme.onErrorContainer,
          ),
          const SizedBox(width: 12),
          Expanded(
            child: Text(
              message,
              style: TextStyle(
                color: Theme.of(context).colorScheme.onErrorContainer,
              ),
            ),
          ),
          IconButton(
            icon: const Icon(Icons.close),
            onPressed: () {
              ref.read(authProvider.notifier).clearError();
            },
            color: Theme.of(context).colorScheme.onErrorContainer,
            iconSize: 20,
          ),
        ],
      ),
    );
  }

  String? _validateEmail(String? value) {
    if (value == null || value.isEmpty) {
      return 'Email is required';
    }
    final emailRegex = RegExp(
      r'^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}\$',
    );
    if (!emailRegex.hasMatch(value)) {
      return 'Please enter a valid email';
    }
    return null;
  }

  String? _validatePassword(String? value) {
    if (value == null || value.isEmpty) {
      return 'Password is required';
    }
    if (value.length < 6) {
      return 'Password must be at least 6 characters';
    }
    return null;
  }
}

---

// lib/features/auth/presentation/screens/forgot_password_screen.dart
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:go_router/go_router.dart';
import '../../providers/auth_provider.dart';
import '../widgets/auth_text_field.dart';
import '../widgets/auth_button.dart';

class ForgotPasswordScreen extends ConsumerStatefulWidget {
  const ForgotPasswordScreen({super.key});

  @override
  ConsumerState<ForgotPasswordScreen> createState() =>
      _ForgotPasswordScreenState();
}

class _ForgotPasswordScreenState extends ConsumerState<ForgotPasswordScreen> {
  final _formKey = GlobalKey<FormState>();
  final _emailController = TextEditingController();
  bool _isLoading = false;
  bool _emailSent = false;

  @override
  void dispose() {
    _emailController.dispose();
    super.dispose();
  }

  Future<void> _handleSubmit() async {
    if (!_formKey.currentState!.validate()) return;

    setState(() => _isLoading = true);

    final success = await ref
        .read(authProvider.notifier)
        .requestPasswordReset(_emailController.text.trim());

    setState(() {
      _isLoading = false;
      _emailSent = success;
    });

    if (!success && mounted) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text('Failed to send reset email. Please try again.'),
        ),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);

    return Scaffold(
      appBar: AppBar(
        leading: IconButton(
          icon: const Icon(Icons.arrow_back),
          onPressed: () => context.pop(),
        ),
        title: const Text('Reset Password'),
      ),
      body: SafeArea(
        child: Center(
          child: SingleChildScrollView(
            padding: const EdgeInsets.all(24),
            child: ConstrainedBox(
              constraints: const BoxConstraints(maxWidth: 400),
              child: _emailSent
                  ? _buildSuccessState(theme)
                  : _buildFormState(theme),
            ),
          ),
        ),
      ),
    );
  }

  Widget _buildFormState(ThemeData theme) {
    return Form(
      key: _formKey,
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: [
          Icon(
            Icons.lock_reset,
            size: 64,
            color: theme.colorScheme.primary,
          ),
          const SizedBox(height: 24),
          Text(
            'Forgot Password?',
            style: theme.textTheme.headlineSmall?.copyWith(
              fontWeight: FontWeight.bold,
            ),
            textAlign: TextAlign.center,
          ),
          const SizedBox(height: 8),
          Text(
            'Enter your email address and we will send you a link to reset your password.',
            style: theme.textTheme.bodyMedium?.copyWith(
              color: theme.colorScheme.onSurfaceVariant,
            ),
            textAlign: TextAlign.center,
          ),
          const SizedBox(height: 32),
          AuthTextField(
            controller: _emailController,
            label: 'Email',
            hint: 'Enter your email',
            keyboardType: TextInputType.emailAddress,
            prefixIcon: Icons.email_outlined,
            enabled: !_isLoading,
            validator: (value) {
              if (value == null || value.isEmpty) {
                return 'Email is required';
              }
              final emailRegex = RegExp(
                r'^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}\$',
              );
              if (!emailRegex.hasMatch(value)) {
                return 'Please enter a valid email';
              }
              return null;
            },
            onFieldSubmitted: (_) => _handleSubmit(),
          ),
          const SizedBox(height: 24),
          AuthButton(
            onPressed: _handleSubmit,
            isLoading: _isLoading,
            label: 'Send Reset Link',
          ),
          const SizedBox(height: 16),
          TextButton(
            onPressed: _isLoading ? null : () => context.pop(),
            child: const Text('Back to Login'),
          ),
        ],
      ),
    );
  }

  Widget _buildSuccessState(ThemeData theme) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.stretch,
      children: [
        Icon(
          Icons.mark_email_read,
          size: 64,
          color: theme.colorScheme.primary,
        ),
        const SizedBox(height: 24),
        Text(
          'Check Your Email',
          style: theme.textTheme.headlineSmall?.copyWith(
            fontWeight: FontWeight.bold,
          ),
          textAlign: TextAlign.center,
        ),
        const SizedBox(height: 8),
        Text(
          'We have sent a password reset link to \${_emailController.text}',
          style: theme.textTheme.bodyMedium?.copyWith(
            color: theme.colorScheme.onSurfaceVariant,
          ),
          textAlign: TextAlign.center,
        ),
        const SizedBox(height: 32),
        AuthButton(
          onPressed: () => context.go('/login'),
          label: 'Back to Login',
        ),
        const SizedBox(height: 16),
        TextButton(
          onPressed: () {
            setState(() => _emailSent = false);
          },
          child: const Text('Resend Email'),
        ),
      ],
    );
  }
}`,
      "language": "dart"
    },
    {
      "type": "EXAMPLE",
      "title": "Registration Screen",
      "content": `
**Building a Complete Registration Flow**

The registration screen includes multi-field validation, password strength indication, terms acceptance, and navigation to email verification.

`,
      "code": `// lib/features/auth/presentation/screens/register_screen.dart
import 'package:flutter/gestures.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:go_router/go_router.dart';
import '../../domain/auth_state.dart';
import '../../providers/auth_provider.dart';
import '../widgets/auth_text_field.dart';
import '../widgets/auth_button.dart';
import '../widgets/password_strength_indicator.dart';

class RegisterScreen extends ConsumerStatefulWidget {
  const RegisterScreen({super.key});

  @override
  ConsumerState<RegisterScreen> createState() => _RegisterScreenState();
}

class _RegisterScreenState extends ConsumerState<RegisterScreen> {
  final _formKey = GlobalKey<FormState>();
  final _nameController = TextEditingController();
  final _emailController = TextEditingController();
  final _passwordController = TextEditingController();
  final _confirmPasswordController = TextEditingController();

  final _nameFocusNode = FocusNode();
  final _emailFocusNode = FocusNode();
  final _passwordFocusNode = FocusNode();
  final _confirmPasswordFocusNode = FocusNode();

  bool _obscurePassword = true;
  bool _obscureConfirmPassword = true;
  bool _acceptedTerms = false;
  String? _errorMessage;

  @override
  void dispose() {
    _nameController.dispose();
    _emailController.dispose();
    _passwordController.dispose();
    _confirmPasswordController.dispose();
    _nameFocusNode.dispose();
    _emailFocusNode.dispose();
    _passwordFocusNode.dispose();
    _confirmPasswordFocusNode.dispose();
    super.dispose();
  }

  Future<void> _handleRegister() async {
    setState(() => _errorMessage = null);

    if (!_formKey.currentState!.validate()) {
      return;
    }

    if (!_acceptedTerms) {
      setState(() {
        _errorMessage = 'Please accept the Terms of Service and Privacy Policy';
      });
      return;
    }

    final result = await ref.read(authProvider.notifier).register(
      RegistrationData(
        email: _emailController.text.trim(),
        password: _passwordController.text,
        displayName: _nameController.text.trim(),
        acceptedTerms: _acceptedTerms,
      ),
    );

    if (result.pendingVerification && mounted) {
      // Navigate to email verification screen
      context.go('/verify-email', extra: {'email': result.email});
    } else if (!result.success && mounted) {
      setState(() {
        _errorMessage = result.message ?? 'Registration failed';
      });
    }
  }

  void _navigateToLogin() {
    context.go('/login');
  }

  void _showTermsOfService() {
    // Navigate to terms page
    context.push('/terms');
  }

  void _showPrivacyPolicy() {
    // Navigate to privacy page
    context.push('/privacy');
  }

  @override
  Widget build(BuildContext context) {
    final authState = ref.watch(authProvider);
    final theme = Theme.of(context);

    return Scaffold(
      appBar: AppBar(
        leading: IconButton(
          icon: const Icon(Icons.arrow_back),
          onPressed: () => context.pop(),
        ),
        title: const Text('Create Account'),
      ),
      body: SafeArea(
        child: Center(
          child: SingleChildScrollView(
            padding: const EdgeInsets.all(24),
            child: ConstrainedBox(
              constraints: const BoxConstraints(maxWidth: 400),
              child: Form(
                key: _formKey,
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.stretch,
                  children: [
                    // Header
                    Text(
                      'Join Us',
                      style: theme.textTheme.headlineMedium?.copyWith(
                        fontWeight: FontWeight.bold,
                      ),
                      textAlign: TextAlign.center,
                    ),
                    const SizedBox(height: 8),
                    Text(
                      'Create an account to get started',
                      style: theme.textTheme.bodyLarge?.copyWith(
                        color: theme.colorScheme.onSurfaceVariant,
                      ),
                      textAlign: TextAlign.center,
                    ),
                    const SizedBox(height: 32),

                    // Error message
                    if (_errorMessage != null)
                      _buildErrorBanner(_errorMessage!),

                    // Name field
                    AuthTextField(
                      controller: _nameController,
                      focusNode: _nameFocusNode,
                      label: 'Display Name',
                      hint: 'Enter your name',
                      textInputAction: TextInputAction.next,
                      prefixIcon: Icons.person_outline,
                      enabled: !authState.isLoading,
                      validator: (value) {
                        if (value == null || value.isEmpty) {
                          return 'Name is required';
                        }
                        if (value.length < 2) {
                          return 'Name must be at least 2 characters';
                        }
                        return null;
                      },
                      onFieldSubmitted: (_) {
                        _emailFocusNode.requestFocus();
                      },
                    ),
                    const SizedBox(height: 16),

                    // Email field
                    AuthTextField(
                      controller: _emailController,
                      focusNode: _emailFocusNode,
                      label: 'Email',
                      hint: 'Enter your email',
                      keyboardType: TextInputType.emailAddress,
                      textInputAction: TextInputAction.next,
                      prefixIcon: Icons.email_outlined,
                      enabled: !authState.isLoading,
                      validator: _validateEmail,
                      onFieldSubmitted: (_) {
                        _passwordFocusNode.requestFocus();
                      },
                    ),
                    const SizedBox(height: 16),

                    // Password field
                    AuthTextField(
                      controller: _passwordController,
                      focusNode: _passwordFocusNode,
                      label: 'Password',
                      hint: 'Create a password',
                      obscureText: _obscurePassword,
                      textInputAction: TextInputAction.next,
                      prefixIcon: Icons.lock_outline,
                      enabled: !authState.isLoading,
                      suffixIcon: IconButton(
                        icon: Icon(
                          _obscurePassword
                              ? Icons.visibility_outlined
                              : Icons.visibility_off_outlined,
                        ),
                        onPressed: () {
                          setState(() {
                            _obscurePassword = !_obscurePassword;
                          });
                        },
                      ),
                      validator: _validatePassword,
                      onChanged: (_) => setState(() {}),
                      onFieldSubmitted: (_) {
                        _confirmPasswordFocusNode.requestFocus();
                      },
                    ),
                    const SizedBox(height: 8),

                    // Password strength indicator
                    PasswordStrengthIndicator(
                      password: _passwordController.text,
                    ),
                    const SizedBox(height: 16),

                    // Confirm password field
                    AuthTextField(
                      controller: _confirmPasswordController,
                      focusNode: _confirmPasswordFocusNode,
                      label: 'Confirm Password',
                      hint: 'Re-enter your password',
                      obscureText: _obscureConfirmPassword,
                      textInputAction: TextInputAction.done,
                      prefixIcon: Icons.lock_outline,
                      enabled: !authState.isLoading,
                      suffixIcon: IconButton(
                        icon: Icon(
                          _obscureConfirmPassword
                              ? Icons.visibility_outlined
                              : Icons.visibility_off_outlined,
                        ),
                        onPressed: () {
                          setState(() {
                            _obscureConfirmPassword = !_obscureConfirmPassword;
                          });
                        },
                      ),
                      validator: (value) {
                        if (value != _passwordController.text) {
                          return 'Passwords do not match';
                        }
                        return null;
                      },
                      onFieldSubmitted: (_) => _handleRegister(),
                    ),
                    const SizedBox(height: 24),

                    // Terms acceptance
                    Row(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Checkbox(
                          value: _acceptedTerms,
                          onChanged: authState.isLoading
                              ? null
                              : (value) {
                                  setState(() {
                                    _acceptedTerms = value ?? false;
                                    if (_acceptedTerms) {
                                      _errorMessage = null;
                                    }
                                  });
                                },
                        ),
                        Expanded(
                          child: Padding(
                            padding: const EdgeInsets.only(top: 12),
                            child: RichText(
                              text: TextSpan(
                                style: theme.textTheme.bodySmall,
                                children: [
                                  const TextSpan(
                                    text: 'I agree to the ',
                                  ),
                                  TextSpan(
                                    text: 'Terms of Service',
                                    style: TextStyle(
                                      color: theme.colorScheme.primary,
                                      decoration: TextDecoration.underline,
                                    ),
                                    recognizer: TapGestureRecognizer()
                                      ..onTap = _showTermsOfService,
                                  ),
                                  const TextSpan(
                                    text: ' and ',
                                  ),
                                  TextSpan(
                                    text: 'Privacy Policy',
                                    style: TextStyle(
                                      color: theme.colorScheme.primary,
                                      decoration: TextDecoration.underline,
                                    ),
                                    recognizer: TapGestureRecognizer()
                                      ..onTap = _showPrivacyPolicy,
                                  ),
                                ],
                              ),
                            ),
                          ),
                        ),
                      ],
                    ),
                    const SizedBox(height: 24),

                    // Register button
                    AuthButton(
                      onPressed: _handleRegister,
                      isLoading: authState.isLoading,
                      label: 'Create Account',
                    ),
                    const SizedBox(height: 24),

                    // Login link
                    Row(
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: [
                        Text(
                          'Already have an account?',
                          style: theme.textTheme.bodyMedium,
                        ),
                        TextButton(
                          onPressed: authState.isLoading
                              ? null
                              : _navigateToLogin,
                          child: const Text('Sign In'),
                        ),
                      ],
                    ),
                  ],
                ),
              ),
            ),
          ),
        ),
      ),
    );
  }

  Widget _buildErrorBanner(String message) {
    return Container(
      margin: const EdgeInsets.only(bottom: 16),
      padding: const EdgeInsets.all(12),
      decoration: BoxDecoration(
        color: Theme.of(context).colorScheme.errorContainer,
        borderRadius: BorderRadius.circular(8),
      ),
      child: Row(
        children: [
          Icon(
            Icons.error_outline,
            color: Theme.of(context).colorScheme.onErrorContainer,
          ),
          const SizedBox(width: 12),
          Expanded(
            child: Text(
              message,
              style: TextStyle(
                color: Theme.of(context).colorScheme.onErrorContainer,
              ),
            ),
          ),
        ],
      ),
    );
  }

  String? _validateEmail(String? value) {
    if (value == null || value.isEmpty) {
      return 'Email is required';
    }
    final emailRegex = RegExp(
      r'^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}\$',
    );
    if (!emailRegex.hasMatch(value)) {
      return 'Please enter a valid email';
    }
    return null;
  }

  String? _validatePassword(String? value) {
    if (value == null || value.isEmpty) {
      return 'Password is required';
    }
    if (value.length < 8) {
      return 'Password must be at least 8 characters';
    }
    if (!RegExp(r'[A-Z]').hasMatch(value)) {
      return 'Password must contain an uppercase letter';
    }
    if (!RegExp(r'[a-z]').hasMatch(value)) {
      return 'Password must contain a lowercase letter';
    }
    if (!RegExp(r'[0-9]').hasMatch(value)) {
      return 'Password must contain a number';
    }
    return null;
  }
}

---

// lib/features/auth/presentation/widgets/password_strength_indicator.dart
import 'package:flutter/material.dart';

class PasswordStrengthIndicator extends StatelessWidget {
  final String password;

  const PasswordStrengthIndicator({
    super.key,
    required this.password,
  });

  @override
  Widget build(BuildContext context) {
    final strength = _calculateStrength(password);
    final theme = Theme.of(context);

    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        // Strength bar
        Row(
          children: [
            Expanded(
              child: ClipRRect(
                borderRadius: BorderRadius.circular(4),
                child: LinearProgressIndicator(
                  value: strength.value,
                  backgroundColor: theme.colorScheme.surfaceContainerHighest,
                  valueColor: AlwaysStoppedAnimation(
                    strength.color,
                  ),
                  minHeight: 6,
                ),
              ),
            ),
            const SizedBox(width: 12),
            Text(
              strength.label,
              style: theme.textTheme.bodySmall?.copyWith(
                color: strength.color,
                fontWeight: FontWeight.w500,
              ),
            ),
          ],
        ),
        const SizedBox(height: 8),

        // Requirements checklist
        Wrap(
          spacing: 16,
          runSpacing: 4,
          children: [
            _buildRequirement(
              context,
              'At least 8 characters',
              password.length >= 8,
            ),
            _buildRequirement(
              context,
              'Uppercase letter',
              RegExp(r'[A-Z]').hasMatch(password),
            ),
            _buildRequirement(
              context,
              'Lowercase letter',
              RegExp(r'[a-z]').hasMatch(password),
            ),
            _buildRequirement(
              context,
              'Number',
              RegExp(r'[0-9]').hasMatch(password),
            ),
            _buildRequirement(
              context,
              'Special character',
              RegExp(r'[!@#\$%^&*(),.?":{}|<>]').hasMatch(password),
            ),
          ],
        ),
      ],
    );
  }

  Widget _buildRequirement(
    BuildContext context,
    String label,
    bool met,
  ) {
    final theme = Theme.of(context);
    final color = met
        ? theme.colorScheme.primary
        : theme.colorScheme.onSurfaceVariant;

    return Row(
      mainAxisSize: MainAxisSize.min,
      children: [
        Icon(
          met ? Icons.check_circle : Icons.circle_outlined,
          size: 14,
          color: color,
        ),
        const SizedBox(width: 4),
        Text(
          label,
          style: theme.textTheme.bodySmall?.copyWith(
            color: color,
          ),
        ),
      ],
    );
  }

  PasswordStrength _calculateStrength(String password) {
    if (password.isEmpty) {
      return PasswordStrength.none;
    }

    int score = 0;

    // Length checks
    if (password.length >= 8) score++;
    if (password.length >= 12) score++;
    if (password.length >= 16) score++;

    // Character type checks
    if (RegExp(r'[A-Z]').hasMatch(password)) score++;
    if (RegExp(r'[a-z]').hasMatch(password)) score++;
    if (RegExp(r'[0-9]').hasMatch(password)) score++;
    if (RegExp(r'[!@#\$%^&*(),.?":{}|<>]').hasMatch(password)) score++;

    if (score <= 2) return PasswordStrength.weak;
    if (score <= 4) return PasswordStrength.fair;
    if (score <= 5) return PasswordStrength.good;
    return PasswordStrength.strong;
  }
}

enum PasswordStrength {
  none(0, 'Enter password', Colors.grey),
  weak(0.25, 'Weak', Colors.red),
  fair(0.5, 'Fair', Colors.orange),
  good(0.75, 'Good', Colors.lightGreen),
  strong(1.0, 'Strong', Colors.green);

  final double value;
  final String label;
  final Color color;

  const PasswordStrength(this.value, this.label, this.color);
}

---

// lib/features/auth/presentation/screens/email_verification_screen.dart
import 'dart:async';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:go_router/go_router.dart';

class EmailVerificationScreen extends ConsumerStatefulWidget {
  final String email;

  const EmailVerificationScreen({
    super.key,
    required this.email,
  });

  @override
  ConsumerState<EmailVerificationScreen> createState() =>
      _EmailVerificationScreenState();
}

class _EmailVerificationScreenState
    extends ConsumerState<EmailVerificationScreen> {
  Timer? _resendTimer;
  int _resendCountdown = 0;

  @override
  void dispose() {
    _resendTimer?.cancel();
    super.dispose();
  }

  void _startResendTimer() {
    setState(() => _resendCountdown = 60);
    _resendTimer = Timer.periodic(const Duration(seconds: 1), (timer) {
      if (_resendCountdown <= 0) {
        timer.cancel();
      } else {
        setState(() => _resendCountdown--);
      }
    });
  }

  Future<void> _resendEmail() async {
    // Call resend verification email API
    _startResendTimer();

    if (mounted) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text('Verification email sent!'),
        ),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);

    return Scaffold(
      appBar: AppBar(
        leading: IconButton(
          icon: const Icon(Icons.close),
          onPressed: () => context.go('/login'),
        ),
      ),
      body: SafeArea(
        child: Center(
          child: Padding(
            padding: const EdgeInsets.all(24),
            child: ConstrainedBox(
              constraints: const BoxConstraints(maxWidth: 400),
              child: Column(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  Icon(
                    Icons.mark_email_unread_outlined,
                    size: 80,
                    color: theme.colorScheme.primary,
                  ),
                  const SizedBox(height: 24),
                  Text(
                    'Verify Your Email',
                    style: theme.textTheme.headlineSmall?.copyWith(
                      fontWeight: FontWeight.bold,
                    ),
                    textAlign: TextAlign.center,
                  ),
                  const SizedBox(height: 16),
                  Text(
                    'We sent a verification link to',
                    style: theme.textTheme.bodyLarge,
                    textAlign: TextAlign.center,
                  ),
                  const SizedBox(height: 8),
                  Text(
                    widget.email,
                    style: theme.textTheme.bodyLarge?.copyWith(
                      fontWeight: FontWeight.bold,
                      color: theme.colorScheme.primary,
                    ),
                    textAlign: TextAlign.center,
                  ),
                  const SizedBox(height: 24),
                  Text(
                    "Click the link in the email to verify your account. If you don't see it, check your spam folder.",
                    style: theme.textTheme.bodyMedium?.copyWith(
                      color: theme.colorScheme.onSurfaceVariant,
                    ),
                    textAlign: TextAlign.center,
                  ),
                  const SizedBox(height: 32),
                  SizedBox(
                    width: double.infinity,
                    child: FilledButton(
                      onPressed: () => context.go('/login'),
                      child: const Text('Back to Login'),
                    ),
                  ),
                  const SizedBox(height: 16),
                  TextButton(
                    onPressed: _resendCountdown > 0 ? null : _resendEmail,
                    child: Text(
                      _resendCountdown > 0
                          ? 'Resend in \${_resendCountdown}s'
                          : 'Resend Email',
                    ),
                  ),
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }
}`,
      "language": "dart"
    },
    {
      "type": "EXAMPLE",
      "title": "Auth-Guarded Navigation",
      "content": `
**Implementing Protected Routes with GoRouter**

The navigation system must protect routes based on authentication state, handle deep links properly, and redirect users after logout.

`,
      "code": `// lib/core/routing/app_router.dart
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:go_router/go_router.dart';
import '../../features/auth/domain/auth_state.dart';
import '../../features/auth/providers/auth_provider.dart';
import '../../features/auth/presentation/screens/login_screen.dart';
import '../../features/auth/presentation/screens/register_screen.dart';
import '../../features/auth/presentation/screens/forgot_password_screen.dart';
import '../../features/auth/presentation/screens/email_verification_screen.dart';
import '../../features/home/presentation/screens/home_screen.dart';
import '../../features/profile/presentation/screens/profile_screen.dart';
import '../../features/settings/presentation/screens/settings_screen.dart';

/// Routes that don't require authentication
const _publicRoutes = [
  '/login',
  '/register',
  '/forgot-password',
  '/verify-email',
  '/terms',
  '/privacy',
];

/// Provider for the app router
final appRouterProvider = Provider<GoRouter>((ref) {
  // Listen to auth state changes
  final authState = ref.watch(authProvider);

  return GoRouter(
    initialLocation: '/login',
    debugLogDiagnostics: true,

    // Refresh router when auth state changes
    refreshListenable: GoRouterRefreshStream(
      ref.watch(authProvider.notifier).stream,
    ),

    // Redirect logic based on auth state
    redirect: (context, state) {
      final isAuthenticated = authState.valueOrNull?.isAuthenticated ?? false;
      final isAuthRoute = _publicRoutes.contains(state.matchedLocation);

      // If loading auth state, don't redirect yet
      if (authState.isLoading) {
        return null;
      }

      // If not authenticated and trying to access protected route
      if (!isAuthenticated && !isAuthRoute) {
        // Store the intended destination for after login
        final redirectTo = Uri.encodeComponent(state.uri.toString());
        return '/login?redirect=\$redirectTo';
      }

      // If authenticated and trying to access auth routes
      if (isAuthenticated && isAuthRoute) {
        // Check for redirect parameter
        final redirect = state.uri.queryParameters['redirect'];
        if (redirect != null) {
          return Uri.decodeComponent(redirect);
        }
        return '/home';
      }

      // No redirect needed
      return null;
    },

    // Error page
    errorBuilder: (context, state) => ErrorScreen(
      error: state.error?.toString() ?? 'Page not found',
    ),

    // Route definitions
    routes: [
      // Auth routes
      GoRoute(
        path: '/login',
        name: 'login',
        builder: (context, state) => const LoginScreen(),
      ),
      GoRoute(
        path: '/register',
        name: 'register',
        builder: (context, state) => const RegisterScreen(),
      ),
      GoRoute(
        path: '/forgot-password',
        name: 'forgot-password',
        builder: (context, state) => const ForgotPasswordScreen(),
      ),
      GoRoute(
        path: '/verify-email',
        name: 'verify-email',
        builder: (context, state) {
          final extra = state.extra as Map<String, dynamic>?;
          final email = extra?['email'] as String? ?? '';
          return EmailVerificationScreen(email: email);
        },
      ),

      // Protected routes - wrapped in ShellRoute for shared layout
      ShellRoute(
        builder: (context, state, child) {
          return MainScaffold(child: child);
        },
        routes: [
          GoRoute(
            path: '/home',
            name: 'home',
            pageBuilder: (context, state) => NoTransitionPage(
              key: state.pageKey,
              child: const HomeScreen(),
            ),
          ),
          GoRoute(
            path: '/profile',
            name: 'profile',
            pageBuilder: (context, state) => NoTransitionPage(
              key: state.pageKey,
              child: const ProfileScreen(),
            ),
            routes: [
              GoRoute(
                path: 'edit',
                name: 'edit-profile',
                builder: (context, state) => const EditProfileScreen(),
              ),
            ],
          ),
          GoRoute(
            path: '/settings',
            name: 'settings',
            pageBuilder: (context, state) => NoTransitionPage(
              key: state.pageKey,
              child: const SettingsScreen(),
            ),
          ),
        ],
      ),

      // Deep link routes
      GoRoute(
        path: '/post/:postId',
        name: 'post-detail',
        builder: (context, state) {
          final postId = state.pathParameters['postId']!;
          return PostDetailScreen(postId: postId);
        },
      ),
      GoRoute(
        path: '/user/:userId',
        name: 'user-profile',
        builder: (context, state) {
          final userId = state.pathParameters['userId']!;
          return UserProfileScreen(userId: userId);
        },
      ),
    ],
  );
});

/// Stream wrapper for GoRouter refresh
class GoRouterRefreshStream extends ChangeNotifier {
  GoRouterRefreshStream(Stream<dynamic> stream) {
    notifyListeners();
    _subscription = stream.asBroadcastStream().listen(
      (_) => notifyListeners(),
    );
  }

  late final StreamSubscription<dynamic> _subscription;

  @override
  void dispose() {
    _subscription.cancel();
    super.dispose();
  }
}

---

// lib/core/routing/main_scaffold.dart
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:go_router/go_router.dart';
import '../../features/auth/providers/auth_provider.dart';

class MainScaffold extends ConsumerWidget {
  final Widget child;

  const MainScaffold({
    super.key,
    required this.child,
  });

  int _calculateSelectedIndex(BuildContext context) {
    final location = GoRouterState.of(context).matchedLocation;
    if (location.startsWith('/home')) return 0;
    if (location.startsWith('/profile')) return 1;
    if (location.startsWith('/settings')) return 2;
    return 0;
  }

  void _onItemTapped(BuildContext context, int index) {
    switch (index) {
      case 0:
        context.go('/home');
        break;
      case 1:
        context.go('/profile');
        break;
      case 2:
        context.go('/settings');
        break;
    }
  }

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final selectedIndex = _calculateSelectedIndex(context);

    return Scaffold(
      body: child,
      bottomNavigationBar: NavigationBar(
        selectedIndex: selectedIndex,
        onDestinationSelected: (index) => _onItemTapped(context, index),
        destinations: const [
          NavigationDestination(
            icon: Icon(Icons.home_outlined),
            selectedIcon: Icon(Icons.home),
            label: 'Home',
          ),
          NavigationDestination(
            icon: Icon(Icons.person_outline),
            selectedIcon: Icon(Icons.person),
            label: 'Profile',
          ),
          NavigationDestination(
            icon: Icon(Icons.settings_outlined),
            selectedIcon: Icon(Icons.settings),
            label: 'Settings',
          ),
        ],
      ),
    );
  }
}

---

// lib/features/settings/presentation/screens/settings_screen.dart
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:go_router/go_router.dart';
import '../../../auth/providers/auth_provider.dart';

class SettingsScreen extends ConsumerWidget {
  const SettingsScreen({super.key});

  Future<void> _handleLogout(BuildContext context, WidgetRef ref) async {
    final confirmed = await showDialog<bool>(
      context: context,
      builder: (context) => AlertDialog(
        title: const Text('Logout'),
        content: const Text('Are you sure you want to logout?'),
        actions: [
          TextButton(
            onPressed: () => Navigator.of(context).pop(false),
            child: const Text('Cancel'),
          ),
          FilledButton(
            onPressed: () => Navigator.of(context).pop(true),
            child: const Text('Logout'),
          ),
        ],
      ),
    );

    if (confirmed == true) {
      await ref.read(authProvider.notifier).logout();
      // Router will automatically redirect to login
    }
  }

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final user = ref.watch(currentUserProvider);
    final theme = Theme.of(context);

    return Scaffold(
      appBar: AppBar(
        title: const Text('Settings'),
      ),
      body: ListView(
        children: [
          // User info section
          if (user != null)
            ListTile(
              leading: CircleAvatar(
                backgroundImage: user.imageUrl != null
                    ? NetworkImage(user.imageUrl!)
                    : null,
                child: user.imageUrl == null
                    ? Text(user.userName?[0].toUpperCase() ?? '?')
                    : null,
              ),
              title: Text(user.userName ?? 'User'),
              subtitle: Text(user.email ?? ''),
              trailing: const Icon(Icons.chevron_right),
              onTap: () => context.push('/profile/edit'),
            ),
          const Divider(),

          // Settings sections
          ListTile(
            leading: const Icon(Icons.notifications_outlined),
            title: const Text('Notifications'),
            trailing: const Icon(Icons.chevron_right),
            onTap: () => context.push('/settings/notifications'),
          ),
          ListTile(
            leading: const Icon(Icons.security_outlined),
            title: const Text('Privacy & Security'),
            trailing: const Icon(Icons.chevron_right),
            onTap: () => context.push('/settings/privacy'),
          ),
          ListTile(
            leading: const Icon(Icons.palette_outlined),
            title: const Text('Appearance'),
            trailing: const Icon(Icons.chevron_right),
            onTap: () => context.push('/settings/appearance'),
          ),
          const Divider(),

          // Logout button
          ListTile(
            leading: Icon(
              Icons.logout,
              color: theme.colorScheme.error,
            ),
            title: Text(
              'Logout',
              style: TextStyle(
                color: theme.colorScheme.error,
              ),
            ),
            onTap: () => _handleLogout(context, ref),
          ),
        ],
      ),
    );
  }
}

---

// lib/main.dart
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'core/routing/app_router.dart';
import 'core/theme/app_theme.dart';

void main() async {
  WidgetsFlutterBinding.ensureInitialized();

  // Initialize Serverpod client, secure storage, etc.
  await _initializeApp();

  runApp(
    const ProviderScope(
      child: MyApp(),
    ),
  );
}

Future<void> _initializeApp() async {
  // Initialize secure storage, Serverpod client, etc.
}

class MyApp extends ConsumerWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final router = ref.watch(appRouterProvider);

    return MaterialApp.router(
      title: 'Social App',
      theme: AppTheme.light,
      darkTheme: AppTheme.dark,
      themeMode: ThemeMode.system,
      routerConfig: router,
      debugShowCheckedModeBanner: false,
    );
  }
}`,
      "language": "dart"
    }
  ],
  "challenges": [
    {
      "type": "FREE_CODING",
      "id": "18.7-challenge-0",
      "title": "Social Login Integration",
      "description": "Add Google and Apple sign-in buttons to the login screen with OAuth flow handling and account linking.",
      "instructions": "Implement social login functionality:\n\n1. Add Google Sign-In button with proper branding\n2. Add Apple Sign-In button (iOS only, or web)\n3. Implement OAuth flow for each provider\n4. Handle account linking when email already exists\n5. Store social auth tokens securely\n6. Update auth state after successful social login\n7. Handle cancellation and errors gracefully",
      "starterCode": `// lib/features/auth/presentation/widgets/social_login_buttons.dart
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class SocialLoginButtons extends ConsumerWidget {
  const SocialLoginButtons({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    // TODO: Implement social login buttons
    // 1. Add Google Sign-In button
    // 2. Add Apple Sign-In button (show only on iOS/macOS/web)
    // 3. Handle loading states
    // 4. Show errors in snackbar
    throw UnimplementedError();
  }
}

// lib/features/auth/data/social_auth_repository.dart
import 'package:flutter_riverpod/flutter_riverpod.dart';

class SocialAuthRepository {
  final Ref _ref;

  SocialAuthRepository(this._ref);

  /// Sign in with Google
  Future<SocialAuthResult> signInWithGoogle() async {
    // TODO: Implement Google Sign-In
    throw UnimplementedError();
  }

  /// Sign in with Apple
  Future<SocialAuthResult> signInWithApple() async {
    // TODO: Implement Apple Sign-In
    throw UnimplementedError();
  }

  /// Link social account to existing user
  Future<bool> linkSocialAccount({
    required String provider,
    required String token,
  }) async {
    // TODO: Implement account linking
    throw UnimplementedError();
  }
}

class SocialAuthResult {
  final bool success;
  final bool needsLinking;
  final String? email;
  final String? errorMessage;

  SocialAuthResult({
    required this.success,
    this.needsLinking = false,
    this.email,
    this.errorMessage,
  });
}`,
      "solution": `// Complete solution implementing Google and Apple Sign-In
// with OAuth flow, account linking, and error handling
// See the full implementation in the lesson examples above`,
      "language": "dart",
      "testCases": [],
      "hints": [
        {
          "level": 1,
          "text": "Use google_sign_in and sign_in_with_apple packages for OAuth flows"
        },
        {
          "level": 2,
          "text": "Apple Sign-In requires configuration in Xcode and Apple Developer Portal"
        },
        {
          "level": 3,
          "text": "Handle account linking by first verifying the email/password, then linking the social provider"
        }
      ],
      "commonMistakes": [
        {
          "mistake": "Not handling the case where user cancels social sign-in",
          "consequence": "App shows error instead of gracefully returning to login",
          "correction": "Check for null return from Google Sign-In and AuthorizationErrorCode.canceled from Apple"
        },
        {
          "mistake": "Storing social tokens in regular SharedPreferences",
          "consequence": "Tokens could be extracted from device, compromising security",
          "correction": "Always use flutter_secure_storage for any authentication tokens"
        }
      ],
      "difficulty": "advanced"
    },
    {
      "type": "FREE_CODING",
      "id": "18.7-challenge-1",
      "title": "Biometric Authentication",
      "description": "Add fingerprint and face unlock as an optional authentication method with secure storage integration.",
      "instructions": "Implement biometric authentication:\n\n1. Check device biometric capabilities\n2. Add biometric toggle in security settings\n3. Store encrypted credentials for biometric unlock\n4. Show biometric prompt on app launch (if enabled)\n5. Implement fallback to password entry\n6. Handle biometric failures gracefully\n7. Clear biometric data on logout",
      "starterCode": `// lib/features/auth/data/biometric_auth_repository.dart
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:local_auth/local_auth.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';

class BiometricAuthRepository {
  final LocalAuthentication _localAuth;
  final FlutterSecureStorage _secureStorage;

  BiometricAuthRepository(this._localAuth, this._secureStorage);

  /// Check if biometric auth is available
  Future<BiometricCapability> checkBiometricCapability() async {
    // TODO: Implement
    throw UnimplementedError();
  }

  /// Enable biometric authentication for current user
  Future<bool> enableBiometric({
    required String email,
    required String authToken,
  }) async {
    // TODO: Implement
    throw UnimplementedError();
  }

  /// Authenticate with biometrics
  Future<BiometricAuthResult> authenticateWithBiometric() async {
    // TODO: Implement
    throw UnimplementedError();
  }

  /// Disable biometric authentication
  Future<void> disableBiometric() async {
    // TODO: Implement
    throw UnimplementedError();
  }

  /// Check if biometric is enabled for current user
  Future<bool> isBiometricEnabled() async {
    // TODO: Implement
    throw UnimplementedError();
  }
}

class BiometricCapability {
  final bool isAvailable;
  final bool isEnrolled;
  final List<BiometricType> availableTypes;
  final String? errorMessage;

  BiometricCapability({
    required this.isAvailable,
    required this.isEnrolled,
    required this.availableTypes,
    this.errorMessage,
  });
}

class BiometricAuthResult {
  final bool success;
  final String? authToken;
  final String? email;
  final String? errorMessage;
  final bool shouldFallback;

  BiometricAuthResult({
    required this.success,
    this.authToken,
    this.email,
    this.errorMessage,
    this.shouldFallback = false,
  });
}`,
      "solution": `// Complete solution implementing biometric auth with
// local_auth package, secure storage, and fallback handling
// See the full implementation in the lesson examples above`,
      "language": "dart",
      "testCases": [],
      "hints": [
        {
          "level": 1,
          "text": "Use the local_auth package for cross-platform biometric authentication"
        },
        {
          "level": 2,
          "text": "Always check both canCheckBiometrics and isDeviceSupported for full compatibility"
        },
        {
          "level": 3,
          "text": "Handle lockout states (temporary and permanent) by falling back to password"
        }
      ],
      "commonMistakes": [
        {
          "mistake": "Not clearing biometric data when user logs out",
          "consequence": "Previous user's credentials could be accessed by next user",
          "correction": "Always call disableBiometric() as part of the logout flow"
        },
        {
          "mistake": "Using biometricOnly: true without fallback option",
          "consequence": "User locked out if biometric fails repeatedly",
          "correction": "Provide password fallback option and handle lockedOut/permanentlyLockedOut errors"
        }
      ],
      "difficulty": "advanced"
    }
  ]
};

// Add the lesson to module 18
module18.lessons.push(lesson187);

// Write the file back
fs.writeFileSync(coursePath, JSON.stringify(course, null, 2), 'utf8');

console.log('Successfully added lesson 18.7: Frontend: Auth Screens');
