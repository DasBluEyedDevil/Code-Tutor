---
type: "EXAMPLE"
title: "Email Verification Flow"
---

After successful registration, users typically need to verify their email address. This section covers the email verification screen and handling the verification process.

**Create the Email Verification Screen**

Create `lib/screens/auth/verify_email_screen.dart`:

```dart
import 'dart:async';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../../providers/auth_provider.dart';

/// Screen shown after registration prompting user to verify their email.
class VerifyEmailScreen extends ConsumerStatefulWidget {
  final String email;
  
  const VerifyEmailScreen({
    super.key,
    required this.email,
  });
  
  @override
  ConsumerState<VerifyEmailScreen> createState() => _VerifyEmailScreenState();
}

class _VerifyEmailScreenState extends ConsumerState<VerifyEmailScreen> {
  bool _isResending = false;
  bool _isCheckingVerification = false;
  int _resendCooldown = 0;
  Timer? _cooldownTimer;
  Timer? _verificationCheckTimer;
  
  @override
  void initState() {
    super.initState();
    // Start periodic verification check
    _startVerificationPolling();
  }
  
  @override
  void dispose() {
    _cooldownTimer?.cancel();
    _verificationCheckTimer?.cancel();
    super.dispose();
  }
  
  /// Periodically checks if the user has verified their email.
  void _startVerificationPolling() {
    _verificationCheckTimer = Timer.periodic(
      const Duration(seconds: 5),
      (_) => _checkVerificationStatus(),
    );
  }
  
  Future<void> _checkVerificationStatus() async {
    if (_isCheckingVerification) return;
    
    setState(() => _isCheckingVerification = true);
    
    try {
      final authService = ref.read(authServiceProvider);
      final isVerified = await authService.isEmailVerified();
      
      if (!mounted) return;
      
      if (isVerified) {
        // Email verified! Navigate to home
        _verificationCheckTimer?.cancel();
        _showVerificationSuccessAndNavigate();
      }
    } finally {
      if (mounted) {
        setState(() => _isCheckingVerification = false);
      }
    }
  }
  
  void _showVerificationSuccessAndNavigate() {
    showDialog(
      context: context,
      barrierDismissible: false,
      builder: (context) => AlertDialog(
        icon: const Icon(
          Icons.check_circle,
          color: Colors.green,
          size: 48,
        ),
        title: const Text('Email Verified!'),
        content: const Text(
          'Your email has been verified successfully. Welcome aboard!',
        ),
        actions: [
          FilledButton(
            onPressed: () {
              Navigator.of(context).pop();
              Navigator.of(context).pushReplacementNamed('/home');
            },
            child: const Text('Continue'),
          ),
        ],
      ),
    );
  }
  
  Future<void> _resendVerificationEmail() async {
    if (_resendCooldown > 0 || _isResending) return;
    
    setState(() => _isResending = true);
    
    try {
      final authService = ref.read(authServiceProvider);
      final success = await authService.resendVerificationEmail(widget.email);
      
      if (!mounted) return;
      
      if (success) {
        // Start cooldown timer (60 seconds)
        _startResendCooldown();
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(
            content: Text('Verification email sent! Check your inbox.'),
            backgroundColor: Colors.green,
          ),
        );
      } else {
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(
            content: Text('Failed to send email. Please try again.'),
            backgroundColor: Colors.red,
          ),
        );
      }
    } finally {
      if (mounted) {
        setState(() => _isResending = false);
      }
    }
  }
  
  void _startResendCooldown() {
    setState(() => _resendCooldown = 60);
    
    _cooldownTimer = Timer.periodic(
      const Duration(seconds: 1),
      (timer) {
        if (_resendCooldown > 0) {
          setState(() => _resendCooldown--);
        } else {
          timer.cancel();
        }
      },
    );
  }
  
  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    
    return Scaffold(
      appBar: AppBar(
        title: const Text('Verify Your Email'),
        automaticallyImplyLeading: false, // Prevent back navigation
      ),
      body: SafeArea(
        child: Padding(
          padding: const EdgeInsets.all(24.0),
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              // Email icon
              Container(
                padding: const EdgeInsets.all(24),
                decoration: BoxDecoration(
                  color: theme.colorScheme.primaryContainer,
                  shape: BoxShape.circle,
                ),
                child: Icon(
                  Icons.mark_email_unread_outlined,
                  size: 64,
                  color: theme.colorScheme.primary,
                ),
              ),
              const SizedBox(height: 32),
              
              // Title
              Text(
                'Check Your Email',
                style: theme.textTheme.headlineSmall?.copyWith(
                  fontWeight: FontWeight.bold,
                ),
                textAlign: TextAlign.center,
              ),
              const SizedBox(height: 16),
              
              // Description
              Text(
                'We sent a verification link to:',
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
              
              // Instructions
              Container(
                padding: const EdgeInsets.all(16),
                decoration: BoxDecoration(
                  color: theme.colorScheme.surfaceVariant,
                  borderRadius: BorderRadius.circular(12),
                ),
                child: Column(
                  children: [
                    _buildInstructionRow(
                      context,
                      '1',
                      'Open the email we sent you',
                    ),
                    const SizedBox(height: 8),
                    _buildInstructionRow(
                      context,
                      '2',
                      'Click the verification link',
                    ),
                    const SizedBox(height: 8),
                    _buildInstructionRow(
                      context,
                      '3',
                      'Return to this app',
                    ),
                  ],
                ),
              ),
              const SizedBox(height: 24),
              
              // Checking status indicator
              if (_isCheckingVerification)
                Row(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    const SizedBox(
                      width: 16,
                      height: 16,
                      child: CircularProgressIndicator(strokeWidth: 2),
                    ),
                    const SizedBox(width: 8),
                    Text(
                      'Checking verification status...',
                      style: theme.textTheme.bodySmall,
                    ),
                  ],
                ),
              
              const Spacer(),
              
              // Resend button
              Text(
                "Didn't receive the email?",
                style: theme.textTheme.bodyMedium,
              ),
              const SizedBox(height: 8),
              OutlinedButton.icon(
                onPressed: _resendCooldown > 0 || _isResending
                    ? null
                    : _resendVerificationEmail,
                icon: _isResending
                    ? const SizedBox(
                        width: 16,
                        height: 16,
                        child: CircularProgressIndicator(strokeWidth: 2),
                      )
                    : const Icon(Icons.refresh),
                label: Text(
                  _resendCooldown > 0
                      ? 'Resend in ${_resendCooldown}s'
                      : 'Resend Email',
                ),
              ),
              const SizedBox(height: 16),
              
              // Check spam note
              Text(
                'Check your spam folder if you do not see the email.',
                style: theme.textTheme.bodySmall?.copyWith(
                  color: theme.colorScheme.onSurfaceVariant,
                ),
                textAlign: TextAlign.center,
              ),
              const SizedBox(height: 24),
              
              // Manual check button
              TextButton(
                onPressed: _checkVerificationStatus,
                child: const Text('I have verified my email'),
              ),
            ],
          ),
        ),
      ),
    );
  }
  
  Widget _buildInstructionRow(BuildContext context, String number, String text) {
    final theme = Theme.of(context);
    return Row(
      children: [
        Container(
          width: 24,
          height: 24,
          decoration: BoxDecoration(
            color: theme.colorScheme.primary,
            shape: BoxShape.circle,
          ),
          child: Center(
            child: Text(
              number,
              style: TextStyle(
                color: theme.colorScheme.onPrimary,
                fontWeight: FontWeight.bold,
                fontSize: 12,
              ),
            ),
          ),
        ),
        const SizedBox(width: 12),
        Text(text, style: theme.textTheme.bodyMedium),
      ],
    );
  }
}
```

This verification screen provides a polished user experience with automatic polling for verification status, resend functionality with cooldown, and clear instructions.

