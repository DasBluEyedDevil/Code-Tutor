import 'dart:io' show Platform;

// Add these to your registration screen

Widget _buildSocialSignUpSection() {
  return Column(
    children: [
      // Google Sign-Up Button
      OutlinedButton.icon(
        onPressed: _isLoading ? null : _handleGoogleSignUp,
        icon: Image.asset(
          'assets/icons/google_logo.png',
          height: 24,
          width: 24,
        ),
        label: const Text('Continue with Google'),
        style: OutlinedButton.styleFrom(
          padding: const EdgeInsets.symmetric(vertical: 12),
          minimumSize: const Size(double.infinity, 48),
        ),
      ),
      const SizedBox(height: 12),
      
      // Apple Sign-Up Button (iOS only)
      if (Platform.isIOS) ...[
        FilledButton.icon(
          onPressed: _isLoading ? null : _handleAppleSignUp,
          icon: const Icon(Icons.apple, size: 24),
          label: const Text('Continue with Apple'),
          style: FilledButton.styleFrom(
            backgroundColor: Colors.black,
            foregroundColor: Colors.white,
            padding: const EdgeInsets.symmetric(vertical: 12),
            minimumSize: const Size(double.infinity, 48),
          ),
        ),
        const SizedBox(height: 12),
      ],
      
      // Divider
      const SizedBox(height: 16),
      Row(
        children: [
          const Expanded(child: Divider()),
          Padding(
            padding: const EdgeInsets.symmetric(horizontal: 16),
            child: Text(
              'or register with email',
              style: TextStyle(
                color: Theme.of(context).colorScheme.onSurfaceVariant,
                fontSize: 14,
              ),
            ),
          ),
          const Expanded(child: Divider()),
        ],
      ),
      const SizedBox(height: 24),
    ],
  );
}

Future<void> _handleGoogleSignUp() async {
  setState(() => _isLoading = true);
  
  try {
    final authService = ref.read(authServiceProvider);
    final result = await authService.signUpWithGoogle();
    
    if (!mounted) return;
    
    if (result.success) {
      Navigator.of(context).pushReplacementNamed('/home');
    } else {
      setState(() => _error = ErrorHandler.handleError(Exception(result.errorMessage)));
    }
  } catch (e) {
    if (e.toString().contains('canceled')) {
      // User canceled - do nothing
    } else if (mounted) {
      setState(() => _error = ErrorHandler.handleError(e));
    }
  } finally {
    if (mounted) setState(() => _isLoading = false);
  }
}

Future<void> _handleAppleSignUp() async {
  setState(() => _isLoading = true);
  
  try {
    final authService = ref.read(authServiceProvider);
    final result = await authService.signUpWithApple();
    
    if (!mounted) return;
    
    if (result.success) {
      Navigator.of(context).pushReplacementNamed('/home');
    } else {
      setState(() => _error = ErrorHandler.handleError(Exception(result.errorMessage)));
    }
  } catch (e) {
    if (e.toString().contains('canceled')) {
      // User canceled
    } else if (mounted) {
      setState(() => _error = ErrorHandler.handleError(e));
    }
  } finally {
    if (mounted) setState(() => _isLoading = false);
  }
}

// In build(), add before the form fields:
// _buildSocialSignUpSection(),