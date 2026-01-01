---
type: "THEORY"
title: "Introduction"
---

### Add Google Sign-In Button to Login Screen


Add the method:




```dart
Future<void> _handleGoogleSignIn() async {
  setState(() => _isLoading = true);

  try {
    final user = await _authService.signInWithGoogle();

    if (user != null && mounted) {
      Navigator.of(context).pushReplacement(
        MaterialPageRoute(builder: (_) => const HomeScreen()),
      );
    } else {
      setState(() => _isLoading = false);
    }
  } catch (e) {
    setState(() => _isLoading = false);

    if (mounted) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text(e.toString())),
      );
    }
  }
}
```
