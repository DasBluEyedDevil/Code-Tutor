import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class AdaptiveSettingsPage extends StatefulWidget {
  const AdaptiveSettingsPage({super.key});

  @override
  State<AdaptiveSettingsPage> createState() => _AdaptiveSettingsPageState();
}

class _AdaptiveSettingsPageState extends State<AdaptiveSettingsPage> {
  bool _darkMode = false;
  bool _notifications = true;

  bool get _isIOS => Theme.of(context).platform == TargetPlatform.iOS;

  @override
  Widget build(BuildContext context) {
    if (_isIOS) {
      return CupertinoPageScaffold(
        navigationBar: const CupertinoNavigationBar(
          middle: Text('Settings'),
        ),
        child: SafeArea(
          child: _buildSettingsContent(),
        ),
      );
    }

    return Scaffold(
      appBar: AppBar(
        title: const Text('Settings'),
      ),
      body: _buildSettingsContent(),
    );
  }

  Widget _buildSettingsContent() {
    return ListView(
      padding: const EdgeInsets.all(16),
      children: [
        _buildSettingsRow(
          'Dark Mode',
          _darkMode,
          (value) => setState(() => _darkMode = value),
        ),
        const SizedBox(height: 16),
        _buildSettingsRow(
          'Notifications',
          _notifications,
          (value) => setState(() => _notifications = value),
        ),
        const SizedBox(height: 32),
        _buildLogoutButton(),
      ],
    );
  }

  Widget _buildSettingsRow(String label, bool value, ValueChanged<bool> onChanged) {
    return Row(
      mainAxisAlignment: MainAxisAlignment.spaceBetween,
      children: [
        Text(label, style: const TextStyle(fontSize: 16)),
        _buildSwitch(value, onChanged),
      ],
    );
  }

  Widget _buildSwitch(bool value, ValueChanged<bool> onChanged) {
    if (_isIOS) {
      return CupertinoSwitch(
        value: value,
        onChanged: onChanged,
      );
    }
    return Switch(
      value: value,
      onChanged: onChanged,
    );
  }

  Widget _buildLogoutButton() {
    if (_isIOS) {
      return CupertinoButton.filled(
        onPressed: _showLogoutConfirmation,
        child: const Text('Log Out'),
      );
    }
    return ElevatedButton(
      onPressed: _showLogoutConfirmation,
      child: const Text('Log Out'),
    );
  }

  Future<void> _showLogoutConfirmation() async {
    bool? confirmed;

    if (_isIOS) {
      confirmed = await showCupertinoDialog<bool>(
        context: context,
        builder: (context) => CupertinoAlertDialog(
          title: const Text('Log Out'),
          content: const Text('Are you sure you want to log out?'),
          actions: [
            CupertinoDialogAction(
              onPressed: () => Navigator.pop(context, false),
              child: const Text('Cancel'),
            ),
            CupertinoDialogAction(
              isDestructiveAction: true,
              onPressed: () => Navigator.pop(context, true),
              child: const Text('Log Out'),
            ),
          ],
        ),
      );
    } else {
      confirmed = await showDialog<bool>(
        context: context,
        builder: (context) => AlertDialog(
          title: const Text('Log Out'),
          content: const Text('Are you sure you want to log out?'),
          actions: [
            TextButton(
              onPressed: () => Navigator.pop(context, false),
              child: const Text('Cancel'),
            ),
            TextButton(
              onPressed: () => Navigator.pop(context, true),
              style: TextButton.styleFrom(foregroundColor: Colors.red),
              child: const Text('Log Out'),
            ),
          ],
        ),
      );
    }

    if (confirmed == true) {
      // Handle logout
    }
  }
}