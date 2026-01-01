---
type: "EXAMPLE"
title: "Section 8: Linked Accounts Settings Screen"
---

Let us create a settings screen where users can view and manage their linked accounts.

**Create Linked Accounts Screen**

Create `lib/screens/settings/linked_accounts_screen.dart`:

```dart
import 'dart:io';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../../providers/auth_provider.dart';
import '../../services/account_linking_service.dart';

/// Screen for managing linked OAuth accounts.
class LinkedAccountsScreen extends ConsumerStatefulWidget {
  const LinkedAccountsScreen({super.key});

  @override
  ConsumerState<LinkedAccountsScreen> createState() => _LinkedAccountsScreenState();
}

class _LinkedAccountsScreenState extends ConsumerState<LinkedAccountsScreen> {
  List<String> _linkedProviders = [];
  bool _isLoading = true;
  bool _isLinking = false;
  String? _error;
  
  @override
  void initState() {
    super.initState();
    _loadLinkedProviders();
  }
  
  Future<void> _loadLinkedProviders() async {
    setState(() {
      _isLoading = true;
      _error = null;
    });
    
    try {
      final linkingService = ref.read(accountLinkingServiceProvider);
      final providers = await linkingService.getLinkedProviders();
      
      if (mounted) {
        setState(() {
          _linkedProviders = providers;
          _isLoading = false;
        });
      }
    } catch (e) {
      if (mounted) {
        setState(() {
          _error = 'Failed to load linked accounts';
          _isLoading = false;
        });
      }
    }
  }
  
  Future<void> _linkGoogle() async {
    setState(() => _isLinking = true);
    
    try {
      final linkingService = ref.read(accountLinkingServiceProvider);
      final result = await linkingService.linkGoogleAccount();
      
      if (mounted) {
        if (result.success) {
          setState(() => _linkedProviders = result.linkedProviders);
          ScaffoldMessenger.of(context).showSnackBar(
            const SnackBar(
              content: Text('Google account linked successfully'),
              backgroundColor: Colors.green,
            ),
          );
        } else {
          ScaffoldMessenger.of(context).showSnackBar(
            SnackBar(
              content: Text(result.errorMessage ?? 'Failed to link account'),
              backgroundColor: Colors.red,
            ),
          );
        }
      }
    } finally {
      if (mounted) {
        setState(() => _isLinking = false);
      }
    }
  }
  
  Future<void> _linkApple() async {
    setState(() => _isLinking = true);
    
    try {
      final linkingService = ref.read(accountLinkingServiceProvider);
      final result = await linkingService.linkAppleAccount();
      
      if (mounted) {
        if (result.success) {
          setState(() => _linkedProviders = result.linkedProviders);
          ScaffoldMessenger.of(context).showSnackBar(
            const SnackBar(
              content: Text('Apple account linked successfully'),
              backgroundColor: Colors.green,
            ),
          );
        } else {
          ScaffoldMessenger.of(context).showSnackBar(
            SnackBar(
              content: Text(result.errorMessage ?? 'Failed to link account'),
              backgroundColor: Colors.red,
            ),
          );
        }
      }
    } finally {
      if (mounted) {
        setState(() => _isLinking = false);
      }
    }
  }
  
  Future<void> _unlinkProvider(String provider) async {
    // Show confirmation dialog
    final confirmed = await showDialog<bool>(
      context: context,
      builder: (context) => AlertDialog(
        title: Text('Unlink $provider?'),
        content: Text(
          'You will no longer be able to sign in with $provider. '
          'Are you sure you want to unlink this account?',
        ),
        actions: [
          TextButton(
            onPressed: () => Navigator.of(context).pop(false),
            child: const Text('Cancel'),
          ),
          FilledButton(
            onPressed: () => Navigator.of(context).pop(true),
            child: const Text('Unlink'),
          ),
        ],
      ),
    );
    
    if (confirmed != true) return;
    
    setState(() => _isLinking = true);
    
    try {
      final linkingService = ref.read(accountLinkingServiceProvider);
      final result = await linkingService.unlinkProvider(provider);
      
      if (mounted) {
        if (result.success) {
          setState(() => _linkedProviders = result.linkedProviders);
          ScaffoldMessenger.of(context).showSnackBar(
            SnackBar(
              content: Text('$provider account unlinked'),
              backgroundColor: Colors.green,
            ),
          );
        } else {
          ScaffoldMessenger.of(context).showSnackBar(
            SnackBar(
              content: Text(result.errorMessage ?? 'Failed to unlink account'),
              backgroundColor: Colors.red,
            ),
          );
        }
      }
    } finally {
      if (mounted) {
        setState(() => _isLinking = false);
      }
    }
  }
  
  bool _isProviderLinked(String provider) {
    return _linkedProviders.contains(provider.toLowerCase());
  }
  
  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    
    return Scaffold(
      appBar: AppBar(
        title: const Text('Linked Accounts'),
      ),
      body: _isLoading
          ? const Center(child: CircularProgressIndicator())
          : _error != null
              ? Center(
                  child: Column(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      Text(_error!, style: TextStyle(color: theme.colorScheme.error)),
                      const SizedBox(height: 16),
                      FilledButton(
                        onPressed: _loadLinkedProviders,
                        child: const Text('Retry'),
                      ),
                    ],
                  ),
                )
              : RefreshIndicator(
                  onRefresh: _loadLinkedProviders,
                  child: ListView(
                    padding: const EdgeInsets.all(16),
                    children: [
                      // Email account (always linked if using email auth)
                      if (_isProviderLinked('email'))
                        _buildProviderTile(
                          icon: Icons.email,
                          title: 'Email',
                          subtitle: 'Primary sign-in method',
                          isLinked: true,
                          canUnlink: false,
                        ),
                      
                      // Google account
                      _buildProviderTile(
                        icon: Icons.g_mobiledata,
                        iconColor: Colors.red,
                        title: 'Google',
                        subtitle: _isProviderLinked('google')
                            ? 'Connected'
                            : 'Not connected',
                        isLinked: _isProviderLinked('google'),
                        canUnlink: _linkedProviders.length > 1,
                        onLink: _linkGoogle,
                        onUnlink: () => _unlinkProvider('google'),
                      ),
                      
                      // Apple account (only on Apple platforms)
                      if (Platform.isIOS || Platform.isMacOS)
                        _buildProviderTile(
                          icon: Icons.apple,
                          title: 'Apple',
                          subtitle: _isProviderLinked('apple')
                              ? 'Connected'
                              : 'Not connected',
                          isLinked: _isProviderLinked('apple'),
                          canUnlink: _linkedProviders.length > 1,
                          onLink: _linkApple,
                          onUnlink: () => _unlinkProvider('apple'),
                        ),
                      
                      const SizedBox(height: 24),
                      
                      // Info text
                      Text(
                        'Link additional accounts to sign in with different methods. '
                        'You must keep at least one sign-in method linked.',
                        style: theme.textTheme.bodySmall?.copyWith(
                          color: theme.colorScheme.onSurfaceVariant,
                        ),
                        textAlign: TextAlign.center,
                      ),
                    ],
                  ),
                ),
    );
  }
  
  Widget _buildProviderTile({
    required IconData icon,
    Color? iconColor,
    required String title,
    required String subtitle,
    required bool isLinked,
    required bool canUnlink,
    VoidCallback? onLink,
    VoidCallback? onUnlink,
  }) {
    return Card(
      margin: const EdgeInsets.only(bottom: 8),
      child: ListTile(
        leading: CircleAvatar(
          backgroundColor: iconColor?.withOpacity(0.1) ?? Colors.grey.shade100,
          child: Icon(icon, color: iconColor ?? Colors.grey),
        ),
        title: Text(title),
        subtitle: Text(subtitle),
        trailing: _isLinking
            ? const SizedBox(
                width: 24,
                height: 24,
                child: CircularProgressIndicator(strokeWidth: 2),
              )
            : isLinked
                ? canUnlink
                    ? TextButton(
                        onPressed: onUnlink,
                        child: const Text('Unlink'),
                      )
                    : const Icon(Icons.check_circle, color: Colors.green)
                : TextButton(
                    onPressed: onLink,
                    child: const Text('Link'),
                  ),
      ),
    );
  }
}
```

This screen provides a complete interface for users to manage their linked accounts with proper feedback and safety measures.

