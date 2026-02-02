---
type: "EXAMPLE"
title: "Connecting to Serverpod Auth: Registration API Call"
---

Now let us connect the registration form to your Serverpod backend. This section shows how to call the registration endpoint and handle the response.

**Create the Authentication Service**

First, create a service that wraps all authentication API calls. Create `lib/services/auth_service.dart`:

```dart
import 'package:your_app_client/your_app_client.dart';
import 'package:serverpod_flutter/serverpod_flutter.dart';
import 'secure_storage_service.dart';

/// Result of a registration attempt.
class RegistrationResult {
  final bool success;
  final String? errorMessage;
  final String? errorCode;
  final UserInfo? user;
  final bool requiresEmailVerification;
  
  RegistrationResult({
    required this.success,
    this.errorMessage,
    this.errorCode,
    this.user,
    this.requiresEmailVerification = false,
  });
  
  factory RegistrationResult.success(UserInfo user, {bool requiresVerification = true}) {
    return RegistrationResult(
      success: true,
      user: user,
      requiresEmailVerification: requiresVerification,
    );
  }
  
  factory RegistrationResult.failure(String message, {String? code}) {
    return RegistrationResult(
      success: false,
      errorMessage: message,
      errorCode: code,
    );
  }
}

/// Service handling all authentication operations with Serverpod backend.
class AuthService {
  final Client _client;
  final SecureStorageService _secureStorage;
  
  AuthService({
    required Client client,
    required SecureStorageService secureStorage,
  })  : _client = client,
        _secureStorage = secureStorage;
  
  /// Registers a new user with email and password.
  /// 
  /// Returns a [RegistrationResult] indicating success or failure with details.
  Future<RegistrationResult> registerWithEmail({
    required String email,
    required String password,
    required String name,
  }) async {
    try {
      // Call the Serverpod authentication endpoint
      // This assumes you have created this endpoint in Module 8.6
      final response = await _client.auth.createAccountWithEmail(
        email: email.trim().toLowerCase(),
        password: password,
        userName: name.trim(),
      );
      
      // Check the response type
      if (response.success) {
        // Store the authentication tokens securely
        if (response.keyId != null && response.key != null) {
          await _secureStorage.saveAuthData(
            authToken: response.key!,
            refreshToken: response.keyId.toString(),
            userId: response.userInfo!.id!,
            email: email.trim().toLowerCase(),
          );
        }
        
        return RegistrationResult.success(
          response.userInfo!,
          requiresVerification: true, // Email verification required
        );
      } else {
        // Registration failed - parse the error
        return _parseRegistrationError(response);
      }
    } on ServerpodClientException catch (e) {
      // Handle Serverpod-specific exceptions
      return _handleServerpodException(e);
    } on SocketException {
      return RegistrationResult.failure(
        'No internet connection. Please check your network and try again.',
        code: 'network_error',
      );
    } catch (e) {
      // Handle unexpected errors
      return RegistrationResult.failure(
        'An unexpected error occurred. Please try again.',
        code: 'unknown_error',
      );
    }
  }
  
  /// Parses error response from failed registration.
  RegistrationResult _parseRegistrationError(AuthenticationResponse response) {
    // Check for specific error types from Serverpod auth
    switch (response.failReason) {
      case AuthenticationFailReason.userAlreadyExists:
        return RegistrationResult.failure(
          'An account with this email already exists. Please sign in or use a different email.',
          code: 'email_exists',
        );
      case AuthenticationFailReason.invalidEmail:
        return RegistrationResult.failure(
          'The email address format is invalid.',
          code: 'invalid_email',
        );
      case AuthenticationFailReason.invalidPassword:
        return RegistrationResult.failure(
          'The password does not meet security requirements.',
          code: 'weak_password',
        );
      case AuthenticationFailReason.tooManyFailedAttempts:
        return RegistrationResult.failure(
          'Too many attempts. Please wait a few minutes before trying again.',
          code: 'rate_limited',
        );
      default:
        return RegistrationResult.failure(
          'Registration failed. Please try again.',
          code: 'registration_failed',
        );
    }
  }
  
  /// Handles Serverpod client exceptions.
  RegistrationResult _handleServerpodException(ServerpodClientException e) {
    if (e.statusCode == 429) {
      return RegistrationResult.failure(
        'Too many requests. Please wait a moment and try again.',
        code: 'rate_limited',
      );
    } else if (e.statusCode >= 500) {
      return RegistrationResult.failure(
        'Server error. Our team has been notified. Please try again later.',
        code: 'server_error',
      );
    } else {
      return RegistrationResult.failure(
        'Connection error. Please check your internet and try again.',
        code: 'connection_error',
      );
    }
  }
  
  /// Requests a new verification email to be sent.
  Future<bool> resendVerificationEmail(String email) async {
    try {
      await _client.auth.sendVerificationEmail(email: email.trim().toLowerCase());
      return true;
    } catch (e) {
      return false;
    }
  }
  
  /// Checks if the user's email has been verified.
  Future<bool> isEmailVerified() async {
    try {
      final userInfo = await _client.auth.getUserInfo();
      return userInfo?.verified ?? false;
    } catch (e) {
      return false;
    }
  }
}
```

**Implement the Registration Handler in the Screen**

Now update the `_handleRegistration` method in your registration screen:

```dart
Future<void> _handleRegistration() async {
  // Clear any previous error
  setState(() => _errorMessage = null);
  
  // Check terms acceptance
  if (!_acceptedTerms) {
    setState(() {
      _errorMessage = 'Please accept the Terms of Service and Privacy Policy to continue.';
    });
    return;
  }
  
  // Validate the form
  if (!_formKey.currentState!.validate()) {
    // Form validation failed - errors shown inline
    return;
  }
  
  // Start loading state
  setState(() => _isLoading = true);
  
  try {
    // Get the auth service from Riverpod
    final authService = ref.read(authServiceProvider);
    
    // Attempt registration
    final result = await authService.registerWithEmail(
      email: _emailController.text,
      password: _passwordController.text,
      name: _nameController.text,
    );
    
    if (!mounted) return; // Check if widget is still in tree
    
    if (result.success) {
      // Registration successful!
      if (result.requiresEmailVerification) {
        // Navigate to email verification screen
        Navigator.of(context).pushReplacementNamed(
          '/verify-email',
          arguments: _emailController.text,
        );
      } else {
        // Navigate directly to home screen
        Navigator.of(context).pushReplacementNamed('/home');
      }
    } else {
      // Registration failed - show error
      setState(() {
        _errorMessage = result.errorMessage;
        _isLoading = false;
      });
      
      // If email already exists, offer to go to login
      if (result.errorCode == 'email_exists') {
        _showEmailExistsDialog();
      }
    }
  } catch (e) {
    if (!mounted) return;
    setState(() {
      _errorMessage = 'An unexpected error occurred. Please try again.';
      _isLoading = false;
    });
  }
}

/// Shows a dialog when the email is already registered.
void _showEmailExistsDialog() {
  showDialog(
    context: context,
    builder: (context) => AlertDialog(
      title: const Text('Account Exists'),
      content: const Text(
        'An account with this email already exists. Would you like to sign in instead?',
      ),
      actions: [
        TextButton(
          onPressed: () => Navigator.of(context).pop(),
          child: const Text('Cancel'),
        ),
        FilledButton(
          onPressed: () {
            Navigator.of(context).pop();
            Navigator.of(context).pushReplacementNamed(
              '/login',
              arguments: _emailController.text, // Pre-fill email
            );
          },
          child: const Text('Sign In'),
        ),
      ],
    ),
  );
}
```

This implementation provides a complete registration flow with proper error handling and user feedback.

