---
type: "EXAMPLE"
title: "Settings Screen"
---


**Building the SettingsScreen**

The SettingsScreen organizes settings into logical sections (Account, Notifications, Privacy, Appearance), uses toggle switches for boolean settings, provides navigation to detail screens, and includes a logout option.



```dart
// lib/features/settings/presentation/screens/settings_screen.dart
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../../providers/settings_provider.dart';
import '../../providers/auth_provider.dart';
import '../widgets/settings_section.dart';
import '../widgets/settings_tile.dart';

class SettingsScreen extends ConsumerWidget {
  const SettingsScreen({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final settings = ref.watch(settingsProvider);
    final theme = Theme.of(context);

    return Scaffold(
      appBar: AppBar(
        title: const Text('Settings'),
      ),
      body: ListView(
        children: [
          // Account Section
          SettingsSection(
            title: 'Account',
            children: [
              SettingsTile(
                leading: const Icon(Icons.person_outline),
                title: 'Edit Profile',
                onTap: () => _navigateTo(context, '/profile/edit'),
              ),
              SettingsTile(
                leading: const Icon(Icons.lock_outline),
                title: 'Change Password',
                onTap: () => _navigateTo(context, '/settings/password'),
              ),
              SettingsTile(
                leading: const Icon(Icons.email_outlined),
                title: 'Email',
                subtitle: settings.email,
                onTap: () => _navigateTo(context, '/settings/email'),
              ),
              SettingsTile(
                leading: const Icon(Icons.phone_outlined),
                title: 'Phone Number',
                subtitle: settings.phone ?? 'Not set',
                onTap: () => _navigateTo(context, '/settings/phone'),
              ),
            ],
          ),

          // Notifications Section
          SettingsSection(
            title: 'Notifications',
            children: [
              SettingsTile(
                leading: const Icon(Icons.notifications_outlined),
                title: 'Push Notifications',
                trailing: Switch(
                  value: settings.pushNotificationsEnabled,
                  onChanged: (value) {
                    ref
                        .read(settingsProvider.notifier)
                        .setPushNotifications(value);
                  },
                ),
              ),
              SettingsTile(
                leading: const Icon(Icons.email_outlined),
                title: 'Email Notifications',
                trailing: Switch(
                  value: settings.emailNotificationsEnabled,
                  onChanged: (value) {
                    ref
                        .read(settingsProvider.notifier)
                        .setEmailNotifications(value);
                  },
                ),
              ),
              SettingsTile(
                leading: const Icon(Icons.chat_bubble_outline),
                title: 'Message Notifications',
                onTap: () =>
                    _navigateTo(context, '/settings/notifications/messages'),
              ),
              SettingsTile(
                leading: const Icon(Icons.favorite_border),
                title: 'Like & Comment Notifications',
                onTap: () =>
                    _navigateTo(context, '/settings/notifications/activity'),
              ),
            ],
          ),

          // Privacy Section
          SettingsSection(
            title: 'Privacy',
            children: [
              SettingsTile(
                leading: const Icon(Icons.lock_outline),
                title: 'Private Account',
                subtitle: settings.isPrivate
                    ? 'Only approved followers can see your posts'
                    : 'Anyone can see your posts',
                trailing: Switch(
                  value: settings.isPrivate,
                  onChanged: (value) {
                    _confirmPrivacyChange(context, ref, value);
                  },
                ),
              ),
              SettingsTile(
                leading: const Icon(Icons.visibility_off_outlined),
                title: 'Activity Status',
                subtitle: settings.showActivityStatus
                    ? 'Others can see when you\'re active'
                    : 'Your activity status is hidden',
                trailing: Switch(
                  value: settings.showActivityStatus,
                  onChanged: (value) {
                    ref
                        .read(settingsProvider.notifier)
                        .setActivityStatus(value);
                  },
                ),
              ),
              SettingsTile(
                leading: const Icon(Icons.block),
                title: 'Blocked Accounts',
                onTap: () => _navigateTo(context, '/settings/blocked'),
              ),
              SettingsTile(
                leading: const Icon(Icons.volume_off_outlined),
                title: 'Muted Accounts',
                onTap: () => _navigateTo(context, '/settings/muted'),
              ),
            ],
          ),

          // Appearance Section
          SettingsSection(
            title: 'Appearance',
            children: [
              SettingsTile(
                leading: const Icon(Icons.dark_mode_outlined),
                title: 'Dark Mode',
                trailing: Switch(
                  value: settings.isDarkMode,
                  onChanged: (value) {
                    ref.read(settingsProvider.notifier).setDarkMode(value);
                  },
                ),
              ),
              SettingsTile(
                leading: const Icon(Icons.text_fields),
                title: 'Text Size',
                subtitle: _getTextSizeLabel(settings.textSize),
                onTap: () => _showTextSizePicker(context, ref, settings),
              ),
              SettingsTile(
                leading: const Icon(Icons.language),
                title: 'Language',
                subtitle: settings.languageLabel,
                onTap: () => _navigateTo(context, '/settings/language'),
              ),
            ],
          ),

          // Data & Storage Section
          SettingsSection(
            title: 'Data & Storage',
            children: [
              SettingsTile(
                leading: const Icon(Icons.data_usage),
                title: 'Data Saver',
                subtitle: 'Reduce data usage',
                trailing: Switch(
                  value: settings.dataSaverEnabled,
                  onChanged: (value) {
                    ref.read(settingsProvider.notifier).setDataSaver(value);
                  },
                ),
              ),
              SettingsTile(
                leading: const Icon(Icons.storage),
                title: 'Clear Cache',
                subtitle: '${settings.cacheSize} used',
                onTap: () => _confirmClearCache(context, ref),
              ),
              SettingsTile(
                leading: const Icon(Icons.download),
                title: 'Download Data',
                subtitle: 'Get a copy of your data',
                onTap: () => _navigateTo(context, '/settings/download-data'),
              ),
            ],
          ),

          // Support Section
          SettingsSection(
            title: 'Support',
            children: [
              SettingsTile(
                leading: const Icon(Icons.help_outline),
                title: 'Help Center',
                onTap: () => _openHelpCenter(),
              ),
              SettingsTile(
                leading: const Icon(Icons.feedback_outlined),
                title: 'Send Feedback',
                onTap: () => _navigateTo(context, '/settings/feedback'),
              ),
              SettingsTile(
                leading: const Icon(Icons.info_outline),
                title: 'About',
                subtitle: 'Version ${settings.appVersion}',
                onTap: () => _navigateTo(context, '/settings/about'),
              ),
            ],
          ),

          // Danger Zone
          SettingsSection(
            title: '',
            children: [
              SettingsTile(
                leading: Icon(
                  Icons.logout,
                  color: theme.colorScheme.error,
                ),
                title: 'Log Out',
                titleStyle: TextStyle(color: theme.colorScheme.error),
                onTap: () => _confirmLogout(context, ref),
              ),
            ],
          ),

          const SizedBox(height: 32),
        ],
      ),
    );
  }

  void _navigateTo(BuildContext context, String route) {
    Navigator.of(context).pushNamed(route);
  }

  String _getTextSizeLabel(double size) {
    if (size <= 0.85) return 'Small';
    if (size <= 1.0) return 'Normal';
    if (size <= 1.15) return 'Large';
    return 'Extra Large';
  }

  void _showTextSizePicker(
    BuildContext context,
    WidgetRef ref,
    AppSettings settings,
  ) {
    showModalBottomSheet(
      context: context,
      builder: (context) => SafeArea(
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            const Padding(
              padding: EdgeInsets.all(16),
              child: Text(
                'Text Size',
                style: TextStyle(
                  fontSize: 18,
                  fontWeight: FontWeight.bold,
                ),
              ),
            ),
            ...[0.85, 1.0, 1.15, 1.3].map((size) {
              return RadioListTile<double>(
                title: Text(_getTextSizeLabel(size)),
                value: size,
                groupValue: settings.textSize,
                onChanged: (value) {
                  if (value != null) {
                    ref.read(settingsProvider.notifier).setTextSize(value);
                    Navigator.pop(context);
                  }
                },
              );
            }),
          ],
        ),
      ),
    );
  }

  void _confirmPrivacyChange(
    BuildContext context,
    WidgetRef ref,
    bool isPrivate,
  ) {
    if (!isPrivate) {
      // Making account public - show warning
      showDialog(
        context: context,
        builder: (context) => AlertDialog(
          title: const Text('Make account public?'),
          content: const Text(
            'Everyone will be able to see your posts and profile. '
            'Your current followers will remain.',
          ),
          actions: [
            TextButton(
              onPressed: () => Navigator.pop(context),
              child: const Text('Cancel'),
            ),
            FilledButton(
              onPressed: () {
                ref.read(settingsProvider.notifier).setPrivate(false);
                Navigator.pop(context);
              },
              child: const Text('Make Public'),
            ),
          ],
        ),
      );
    } else {
      ref.read(settingsProvider.notifier).setPrivate(true);
    }
  }

  void _confirmClearCache(BuildContext context, WidgetRef ref) {
    showDialog(
      context: context,
      builder: (context) => AlertDialog(
        title: const Text('Clear cache?'),
        content: const Text(
          'This will clear all cached images and data. '
          'You may need to reload content.',
        ),
        actions: [
          TextButton(
            onPressed: () => Navigator.pop(context),
            child: const Text('Cancel'),
          ),
          FilledButton(
            onPressed: () {
              ref.read(settingsProvider.notifier).clearCache();
              Navigator.pop(context);
              ScaffoldMessenger.of(context).showSnackBar(
                const SnackBar(content: Text('Cache cleared')),
              );
            },
            child: const Text('Clear'),
          ),
        ],
      ),
    );
  }

  void _confirmLogout(BuildContext context, WidgetRef ref) {
    showDialog(
      context: context,
      builder: (context) => AlertDialog(
        title: const Text('Log out?'),
        content: const Text('Are you sure you want to log out?'),
        actions: [
          TextButton(
            onPressed: () => Navigator.pop(context),
            child: const Text('Cancel'),
          ),
          FilledButton(
            onPressed: () {
              Navigator.pop(context);
              ref.read(authProvider.notifier).logout();
            },
            style: FilledButton.styleFrom(
              backgroundColor: Theme.of(context).colorScheme.error,
            ),
            child: const Text('Log Out'),
          ),
        ],
      ),
    );
  }

  void _openHelpCenter() {
    // Use url_launcher to open help center URL
  }
}

---

// lib/features/settings/presentation/widgets/settings_section.dart
import 'package:flutter/material.dart';

class SettingsSection extends StatelessWidget {
  final String title;
  final List<Widget> children;

  const SettingsSection({
    super.key,
    required this.title,
    required this.children,
  });

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);

    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        if (title.isNotEmpty)
          Padding(
            padding: const EdgeInsets.fromLTRB(16, 24, 16, 8),
            child: Text(
              title.toUpperCase(),
              style: theme.textTheme.labelSmall?.copyWith(
                color: theme.colorScheme.onSurfaceVariant,
                fontWeight: FontWeight.w600,
                letterSpacing: 1.2,
              ),
            ),
          ),
        ...children,
      ],
    );
  }
}

---

// lib/features/settings/presentation/widgets/settings_tile.dart
import 'package:flutter/material.dart';

class SettingsTile extends StatelessWidget {
  final Widget? leading;
  final String title;
  final String? subtitle;
  final Widget? trailing;
  final VoidCallback? onTap;
  final TextStyle? titleStyle;

  const SettingsTile({
    super.key,
    this.leading,
    required this.title,
    this.subtitle,
    this.trailing,
    this.onTap,
    this.titleStyle,
  });

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);

    return ListTile(
      leading: leading,
      title: Text(
        title,
        style: titleStyle ?? theme.textTheme.bodyLarge,
      ),
      subtitle: subtitle != null
          ? Text(
              subtitle!,
              style: theme.textTheme.bodySmall?.copyWith(
                color: theme.colorScheme.onSurfaceVariant,
              ),
            )
          : null,
      trailing: trailing ??
          (onTap != null
              ? Icon(
                  Icons.chevron_right,
                  color: theme.colorScheme.onSurfaceVariant,
                )
              : null),
      onTap: onTap,
    );
  }
}

---

// lib/features/settings/domain/app_settings.dart
import 'package:freezed_annotation/freezed_annotation.dart';

part 'app_settings.freezed.dart';
part 'app_settings.g.dart';

@freezed
class AppSettings with _$AppSettings {
  const AppSettings._();

  const factory AppSettings({
    required String email,
    String? phone,
    @Default(true) bool pushNotificationsEnabled,
    @Default(true) bool emailNotificationsEnabled,
    @Default(false) bool isPrivate,
    @Default(true) bool showActivityStatus,
    @Default(false) bool isDarkMode,
    @Default(1.0) double textSize,
    @Default('en') String language,
    @Default(false) bool dataSaverEnabled,
    @Default('0 MB') String cacheSize,
    @Default('1.0.0') String appVersion,
  }) = _AppSettings;

  factory AppSettings.fromJson(Map<String, dynamic> json) =>
      _$AppSettingsFromJson(json);

  String get languageLabel {
    switch (language) {
      case 'en':
        return 'English';
      case 'es':
        return 'Spanish';
      case 'fr':
        return 'French';
      case 'de':
        return 'German';
      case 'ja':
        return 'Japanese';
      default:
        return 'English';
    }
  }
}
```
