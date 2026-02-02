---
type: "EXAMPLE"
title: "Section 7: Building Social Login UI Components"
---

Now let us create polished, platform-adaptive social login buttons that follow platform guidelines and integrate with our authentication flow.

**Create Social Login Buttons**

Create `lib/widgets/social_login_buttons.dart`:

```dart
import 'dart:io';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../providers/auth_provider.dart';
import '../services/google_auth_service.dart';
import '../services/apple_auth_service.dart';

/// Google Sign-In button following Google's brand guidelines.
class GoogleSignInButton extends ConsumerStatefulWidget {
  /// Called when sign-in is successful.
  final VoidCallback? onSuccess;
  
  /// Called when sign-in fails.
  final void Function(String error)? onError;
  
  const GoogleSignInButton({
    super.key,
    this.onSuccess,
    this.onError,
  });
  
  @override
  ConsumerState<GoogleSignInButton> createState() => _GoogleSignInButtonState();
}

class _GoogleSignInButtonState extends ConsumerState<GoogleSignInButton> {
  bool _isLoading = false;
  
  Future<void> _handleGoogleSignIn() async {
    if (_isLoading) return;
    
    setState(() => _isLoading = true);
    
    try {
      final googleAuthService = ref.read(googleAuthServiceProvider);
      final authService = ref.read(authServiceProvider);
      
      // Step 1: Get tokens from Google
      final googleResult = await googleAuthService.signIn();
      
      if (!googleResult.success) {
        if (googleResult.errorCode != 'cancelled') {
          widget.onError?.call(googleResult.errorMessage ?? 'Google sign-in failed');
        }
        return;
      }
      
      // Step 2: Authenticate with Serverpod
      final authResult = await authService.signInWithGoogle(
        idToken: googleResult.idToken!,
        accessToken: googleResult.accessToken!,
        email: googleResult.email,
        displayName: googleResult.displayName,
        photoUrl: googleResult.photoUrl,
      );
      
      if (authResult.success) {
        // Update auth state
        ref.read(authStateProvider.notifier).setAuthenticated(authResult.user!);
        widget.onSuccess?.call();
      } else {
        widget.onError?.call(authResult.errorMessage ?? 'Authentication failed');
      }
    } finally {
      if (mounted) {
        setState(() => _isLoading = false);
      }
    }
  }
  
  @override
  Widget build(BuildContext context) {
    return OutlinedButton(
      onPressed: _isLoading ? null : _handleGoogleSignIn,
      style: OutlinedButton.styleFrom(
        padding: const EdgeInsets.symmetric(vertical: 12, horizontal: 16),
        side: BorderSide(color: Colors.grey.shade300),
        shape: RoundedRectangleBorder(
          borderRadius: BorderRadius.circular(8),
        ),
      ),
      child: _isLoading
          ? const SizedBox(
              width: 24,
              height: 24,
              child: CircularProgressIndicator(strokeWidth: 2),
            )
          : Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                Image.asset(
                  'assets/images/google_logo.png',
                  width: 24,
                  height: 24,
                ),
                const SizedBox(width: 12),
                const Text(
                  'Continue with Google',
                  style: TextStyle(
                    fontSize: 16,
                    fontWeight: FontWeight.w500,
                    color: Colors.black87,
                  ),
                ),
              ],
            ),
    );
  }
}

/// Apple Sign-In button following Apple's Human Interface Guidelines.
/// Only shows on iOS and macOS devices.
class AppleSignInButton extends ConsumerStatefulWidget {
  /// Called when sign-in is successful.
  final VoidCallback? onSuccess;
  
  /// Called when sign-in fails.
  final void Function(String error)? onError;
  
  const AppleSignInButton({
    super.key,
    this.onSuccess,
    this.onError,
  });
  
  @override
  ConsumerState<AppleSignInButton> createState() => _AppleSignInButtonState();
}

class _AppleSignInButtonState extends ConsumerState<AppleSignInButton> {
  bool _isLoading = false;
  bool _isAvailable = false;
  
  @override
  void initState() {
    super.initState();
    _checkAvailability();
  }
  
  Future<void> _checkAvailability() async {
    final appleAuthService = ref.read(appleAuthServiceProvider);
    final available = await appleAuthService.isAvailable();
    if (mounted) {
      setState(() => _isAvailable = available);
    }
  }
  
  Future<void> _handleAppleSignIn() async {
    if (_isLoading) return;
    
    setState(() => _isLoading = true);
    
    try {
      final appleAuthService = ref.read(appleAuthServiceProvider);
      final authService = ref.read(authServiceProvider);
      
      // Step 1: Get tokens from Apple
      final appleResult = await appleAuthService.signIn();
      
      if (!appleResult.success) {
        if (appleResult.errorCode != 'cancelled') {
          widget.onError?.call(appleResult.errorMessage ?? 'Apple sign-in failed');
        }
        return;
      }
      
      // Step 2: Authenticate with Serverpod
      final authResult = await authService.signInWithApple(
        identityToken: appleResult.identityToken!,
        authorizationCode: appleResult.authorizationCode!,
        userIdentifier: appleResult.userIdentifier!,
        email: appleResult.email,
        fullName: appleResult.fullName,
      );
      
      if (authResult.success) {
        // Update auth state
        ref.read(authStateProvider.notifier).setAuthenticated(authResult.user!);
        widget.onSuccess?.call();
      } else {
        widget.onError?.call(authResult.errorMessage ?? 'Authentication failed');
      }
    } finally {
      if (mounted) {
        setState(() => _isLoading = false);
      }
    }
  }
  
  @override
  Widget build(BuildContext context) {
    // Only show on Apple platforms where Sign In with Apple is available
    if (!_isAvailable) {
      return const SizedBox.shrink();
    }
    
    final isDark = Theme.of(context).brightness == Brightness.dark;
    
    return ElevatedButton(
      onPressed: _isLoading ? null : _handleAppleSignIn,
      style: ElevatedButton.styleFrom(
        backgroundColor: isDark ? Colors.white : Colors.black,
        foregroundColor: isDark ? Colors.black : Colors.white,
        padding: const EdgeInsets.symmetric(vertical: 12, horizontal: 16),
        shape: RoundedRectangleBorder(
          borderRadius: BorderRadius.circular(8),
        ),
        elevation: 0,
      ),
      child: _isLoading
          ? SizedBox(
              width: 24,
              height: 24,
              child: CircularProgressIndicator(
                strokeWidth: 2,
                color: isDark ? Colors.black : Colors.white,
              ),
            )
          : Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                Icon(
                  Icons.apple,
                  size: 24,
                  color: isDark ? Colors.black : Colors.white,
                ),
                const SizedBox(width: 12),
                Text(
                  'Continue with Apple',
                  style: TextStyle(
                    fontSize: 16,
                    fontWeight: FontWeight.w500,
                    color: isDark ? Colors.black : Colors.white,
                  ),
                ),
              ],
            ),
    );
  }
}

/// A widget that displays all available social login options.
class SocialLoginSection extends ConsumerWidget {
  /// Called when any social login is successful.
  final VoidCallback? onSuccess;
  
  /// Called when any social login fails.
  final void Function(String error)? onError;
  
  const SocialLoginSection({
    super.key,
    this.onSuccess,
    this.onError,
  });
  
  @override
  Widget build(BuildContext context, WidgetRef ref) {
    return Column(
      children: [
        // Google Sign-In (always available)
        SizedBox(
          width: double.infinity,
          child: GoogleSignInButton(
            onSuccess: onSuccess,
            onError: onError,
          ),
        ),
        const SizedBox(height: 12),
        
        // Apple Sign-In (only on Apple platforms)
        if (Platform.isIOS || Platform.isMacOS)
          SizedBox(
            width: double.infinity,
            child: AppleSignInButton(
              onSuccess: onSuccess,
              onError: onError,
            ),
          ),
      ],
    );
  }
}
```

**Update Login Screen to Use Social Buttons**

Update your login screen to include the social login section:

```dart
// In your login_screen.dart, replace the placeholder social button with:

// Divider with "or" text
Row(
  children: [
    const Expanded(child: Divider()),
    Padding(
      padding: const EdgeInsets.symmetric(horizontal: 16),
      child: Text(
        'or continue with',
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
SocialLoginSection(
  onSuccess: () {
    // Navigate to home on success
    Navigator.of(context).pushReplacementNamed('/home');
  },
  onError: (error) {
    ScaffoldMessenger.of(context).showSnackBar(
      SnackBar(
        content: Text(error),
        backgroundColor: Colors.red,
      ),
    );
  },
),
```

Your social login UI is now complete with proper platform adaptation and error handling.

