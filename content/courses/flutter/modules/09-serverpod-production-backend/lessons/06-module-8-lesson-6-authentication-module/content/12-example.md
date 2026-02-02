---
type: "EXAMPLE"
title: "Google Sign-In Button Widget"
---

Here is a reusable Google Sign-In button that follows Google's branding guidelines.



```dart
// lib/widgets/google_sign_in_button.dart

import 'package:flutter/material.dart';

class GoogleSignInButton extends StatelessWidget {
  final VoidCallback onPressed;
  final bool isLoading;

  const GoogleSignInButton({
    super.key,
    required this.onPressed,
    this.isLoading = false,
  });

  @override
  Widget build(BuildContext context) {
    return OutlinedButton(
      onPressed: isLoading ? null : onPressed,
      style: OutlinedButton.styleFrom(
        padding: const EdgeInsets.symmetric(vertical: 12, horizontal: 16),
        side: const BorderSide(color: Colors.grey),
        shape: RoundedRectangleBorder(
          borderRadius: BorderRadius.circular(8),
        ),
      ),
      child: Row(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          if (isLoading)
            const SizedBox(
              height: 24,
              width: 24,
              child: CircularProgressIndicator(strokeWidth: 2),
            )
          else ...[  
            // Google logo
            Image.asset(
              'assets/images/google_logo.png',
              height: 24,
              width: 24,
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
        ],
      ),
    );
  }
}

// Usage in your sign-in screen:
//
// GoogleSignInButton(
//   isLoading: _isGoogleLoading,
//   onPressed: () async {
//     setState(() => _isGoogleLoading = true);
//     try {
//       final user = await googleAuthService.signInWithGoogle();
//       if (user != null && mounted) {
//         Navigator.of(context).pushReplacementNamed('/home');
//       }
//     } catch (e) {
//       ScaffoldMessenger.of(context).showSnackBar(
//         SnackBar(content: Text('Google sign-in failed: $e')),
//       );
//     } finally {
//       if (mounted) setState(() => _isGoogleLoading = false);
//     }
//   },
// )
```
