---
type: "EXAMPLE"
title: "Logout Flow: Clearing Sessions Properly"
---

A secure logout flow must clear all local data and invalidate the session on the server. This prevents session hijacking if someone gains access to stored tokens.

**Add Logout to Auth Service**

Add this method to `lib/services/auth_service.dart`:

```dart
/// Signs out the current user.
/// Invalidates the session on the server and clears local storage.
Future<void> signOut() async {
  try {
    // Get the current auth key to invalidate
    final authToken = await _secureStorage.getAuthToken();
    
    if (authToken != null) {
      // Call Serverpod to invalidate the session on the server
      // This prevents the token from being used even if someone captured it
      await _client.auth.signOut();
    }
  } catch (e) {
    // Log the error but continue with local logout
    // We don't want a server error to prevent local cleanup
    if (kDebugMode) {
      print('Server logout error: $e');
    }
  } finally {
    // Always clear local data regardless of server response
    await _secureStorage.clearAllAuthData();
  }
}

/// Signs out from all devices by invalidating all sessions.
Future<void> signOutAllDevices() async {
  try {
    // Call Serverpod to invalidate all sessions for this user
    await _client.auth.signOutAllDevices();
  } catch (e) {
    if (kDebugMode) {
      print('Sign out all devices error: $e');
    }
  } finally {
    await _secureStorage.clearAllAuthData();
  }
}
```

**Create a Logout Confirmation Dialog**

Create `lib/widgets/logout_confirmation_dialog.dart`:

```dart
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../providers/auth_provider.dart';

/// Shows a confirmation dialog before logging out.
class LogoutConfirmationDialog extends ConsumerStatefulWidget {
  /// If true, offers option to sign out from all devices.
  final bool showSignOutAllOption;
  
  const LogoutConfirmationDialog({
    super.key,
    this.showSignOutAllOption = false,
  });
  
  /// Shows the logout confirmation dialog.
  static Future<bool?> show(
    BuildContext context, {
    bool showSignOutAllOption = false,
  }) {
    return showDialog<bool>(
      context: context,
      builder: (context) => LogoutConfirmationDialog(
        showSignOutAllOption: showSignOutAllOption,
      ),
    );
  }

  @override
  ConsumerState<LogoutConfirmationDialog> createState() =>
      _LogoutConfirmationDialogState();
}

class _LogoutConfirmationDialogState
    extends ConsumerState<LogoutConfirmationDialog> {
  bool _isLoading = false;
  bool _signOutAll = false;
  
  Future<void> _handleLogout() async {
    setState(() => _isLoading = true);
    
    try {
      final authNotifier = ref.read(authStateProvider.notifier);
      
      if (_signOutAll) {
        // Sign out from all devices
        final authService = ref.read(authServiceProvider);
        await authService.signOutAllDevices();
      }
      
      await authNotifier.signOut();
      
      if (mounted) {
        Navigator.of(context).pop(true);
      }
    } catch (e) {
      if (mounted) {
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(
            content: Text('Logout failed. Please try again.'),
            backgroundColor: Colors.red,
          ),
        );
        setState(() => _isLoading = false);
      }
    }
  }
  
  @override
  Widget build(BuildContext context) {
    return AlertDialog(
      title: const Text('Sign Out'),
      content: Column(
        mainAxisSize: MainAxisSize.min,
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          const Text('Are you sure you want to sign out?'),
          if (widget.showSignOutAllOption) ...[
            const SizedBox(height: 16),
            Row(
              children: [
                Checkbox(
                  value: _signOutAll,
                  onChanged: (value) {
                    setState(() => _signOutAll = value ?? false);
                  },
                ),
                Expanded(
                  child: GestureDetector(
                    onTap: () => setState(() => _signOutAll = !_signOutAll),
                    child: const Text('Sign out from all devices'),
                  ),
                ),
              ],
            ),
          ],
        ],
      ),
      actions: [
        TextButton(
          onPressed: _isLoading ? null : () => Navigator.of(context).pop(false),
          child: const Text('Cancel'),
        ),
        FilledButton(
          onPressed: _isLoading ? null : _handleLogout,
          child: _isLoading
              ? const SizedBox(
                  width: 16,
                  height: 16,
                  child: CircularProgressIndicator(
                    strokeWidth: 2,
                    color: Colors.white,
                  ),
                )
              : const Text('Sign Out'),
        ),
      ],
    );
  }
}
```

**Using the Logout Dialog**

```dart
// In your app bar or settings screen:
IconButton(
  icon: const Icon(Icons.logout),
  onPressed: () async {
    final confirmed = await LogoutConfirmationDialog.show(
      context,
      showSignOutAllOption: true,
    );
    
    if (confirmed == true && mounted) {
      // Navigate to login screen
      Navigator.of(context).pushNamedAndRemoveUntil(
        '/login',
        (route) => false,
      );
    }
  },
)
```

This logout implementation ensures sessions are properly invalidated on both client and server.

