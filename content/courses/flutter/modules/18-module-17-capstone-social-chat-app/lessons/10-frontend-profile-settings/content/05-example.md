---
type: "EXAMPLE"
title: "Theme and Preferences"
---


**Implementing Theme Switching and Preference Persistence**

This section demonstrates building a ThemeNotifier for dark/light mode switching, persisting preferences with SharedPreferences, and managing notification and privacy settings.



```dart
// lib/features/settings/providers/theme_provider.dart
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:shared_preferences/shared_preferences.dart';

final themeProvider = NotifierProvider<ThemeNotifier, ThemeMode>(() {
  return ThemeNotifier();
});

class ThemeNotifier extends Notifier<ThemeMode> {
  static const _themeKey = 'theme_mode';

  @override
  ThemeMode build() {
    _loadTheme();
    return ThemeMode.system;
  }

  Future<void> _loadTheme() async {
    final prefs = await SharedPreferences.getInstance();
    final themeIndex = prefs.getInt(_themeKey);
    if (themeIndex != null) {
      state = ThemeMode.values[themeIndex];
    }
  }

  Future<void> setThemeMode(ThemeMode mode) async {
    state = mode;
    final prefs = await SharedPreferences.getInstance();
    await prefs.setInt(_themeKey, mode.index);
  }

  Future<void> toggleDarkMode() async {
    if (state == ThemeMode.dark) {
      await setThemeMode(ThemeMode.light);
    } else {
      await setThemeMode(ThemeMode.dark);
    }
  }

  bool get isDarkMode => state == ThemeMode.dark;
}

---

// lib/features/settings/providers/settings_provider.dart
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:shared_preferences/shared_preferences.dart';
import '../domain/app_settings.dart';
import '../data/settings_repository.dart';
import 'theme_provider.dart';

final settingsProvider =
    NotifierProvider<SettingsNotifier, AppSettings>(() {
  return SettingsNotifier();
});

class SettingsNotifier extends Notifier<AppSettings> {
  late SharedPreferences _prefs;

  static const _pushNotificationsKey = 'push_notifications';
  static const _emailNotificationsKey = 'email_notifications';
  static const _activityStatusKey = 'activity_status';
  static const _dataSaverKey = 'data_saver';
  static const _textSizeKey = 'text_size';
  static const _languageKey = 'language';

  @override
  AppSettings build() {
    _loadSettings();
    return const AppSettings(
      email: 'user@example.com',
      appVersion: '1.0.0',
    );
  }

  Future<void> _loadSettings() async {
    _prefs = await SharedPreferences.getInstance();

    // Load local settings
    final pushEnabled = _prefs.getBool(_pushNotificationsKey) ?? true;
    final emailEnabled = _prefs.getBool(_emailNotificationsKey) ?? true;
    final activityStatus = _prefs.getBool(_activityStatusKey) ?? true;
    final dataSaver = _prefs.getBool(_dataSaverKey) ?? false;
    final textSize = _prefs.getDouble(_textSizeKey) ?? 1.0;
    final language = _prefs.getString(_languageKey) ?? 'en';

    // Load synced settings from server
    try {
      final serverSettings = await ref
          .read(settingsRepositoryProvider)
          .getSettings();

      state = state.copyWith(
        email: serverSettings.email,
        phone: serverSettings.phone,
        isPrivate: serverSettings.isPrivate,
        pushNotificationsEnabled: pushEnabled,
        emailNotificationsEnabled: emailEnabled,
        showActivityStatus: activityStatus,
        dataSaverEnabled: dataSaver,
        textSize: textSize,
        language: language,
        cacheSize: await _calculateCacheSize(),
      );
    } catch (e) {
      // Use local settings on error
      state = state.copyWith(
        pushNotificationsEnabled: pushEnabled,
        emailNotificationsEnabled: emailEnabled,
        showActivityStatus: activityStatus,
        dataSaverEnabled: dataSaver,
        textSize: textSize,
        language: language,
      );
    }
  }

  Future<String> _calculateCacheSize() async {
    // Calculate cache size
    // In real app, use path_provider and check cache directory
    return '24.5 MB';
  }

  // Push Notifications
  Future<void> setPushNotifications(bool enabled) async {
    state = state.copyWith(pushNotificationsEnabled: enabled);
    await _prefs.setBool(_pushNotificationsKey, enabled);

    // Sync with server
    try {
      await ref
          .read(settingsRepositoryProvider)
          .updateNotificationSettings(pushEnabled: enabled);
    } catch (e) {
      // Revert on failure
      state = state.copyWith(pushNotificationsEnabled: !enabled);
      await _prefs.setBool(_pushNotificationsKey, !enabled);
      rethrow;
    }
  }

  // Email Notifications
  Future<void> setEmailNotifications(bool enabled) async {
    state = state.copyWith(emailNotificationsEnabled: enabled);
    await _prefs.setBool(_emailNotificationsKey, enabled);

    try {
      await ref
          .read(settingsRepositoryProvider)
          .updateNotificationSettings(emailEnabled: enabled);
    } catch (e) {
      state = state.copyWith(emailNotificationsEnabled: !enabled);
      await _prefs.setBool(_emailNotificationsKey, !enabled);
      rethrow;
    }
  }

  // Activity Status
  Future<void> setActivityStatus(bool visible) async {
    state = state.copyWith(showActivityStatus: visible);
    await _prefs.setBool(_activityStatusKey, visible);

    try {
      await ref
          .read(settingsRepositoryProvider)
          .updatePrivacySettings(showActivityStatus: visible);
    } catch (e) {
      state = state.copyWith(showActivityStatus: !visible);
      await _prefs.setBool(_activityStatusKey, !visible);
      rethrow;
    }
  }

  // Private Account
  Future<void> setPrivate(bool isPrivate) async {
    final previousValue = state.isPrivate;
    state = state.copyWith(isPrivate: isPrivate);

    try {
      await ref
          .read(settingsRepositoryProvider)
          .updatePrivacySettings(isPrivate: isPrivate);
    } catch (e) {
      state = state.copyWith(isPrivate: previousValue);
      rethrow;
    }
  }

  // Dark Mode
  Future<void> setDarkMode(bool isDark) async {
    state = state.copyWith(isDarkMode: isDark);
    await ref.read(themeProvider.notifier).setThemeMode(
          isDark ? ThemeMode.dark : ThemeMode.light,
        );
  }

  // Text Size
  Future<void> setTextSize(double size) async {
    state = state.copyWith(textSize: size);
    await _prefs.setDouble(_textSizeKey, size);
  }

  // Language
  Future<void> setLanguage(String languageCode) async {
    state = state.copyWith(language: languageCode);
    await _prefs.setString(_languageKey, languageCode);
  }

  // Data Saver
  Future<void> setDataSaver(bool enabled) async {
    state = state.copyWith(dataSaverEnabled: enabled);
    await _prefs.setBool(_dataSaverKey, enabled);
  }

  // Clear Cache
  Future<void> clearCache() async {
    // Clear cached images
    // await DefaultCacheManager().emptyCache();

    state = state.copyWith(cacheSize: '0 MB');
  }
}

---

// lib/features/settings/data/settings_repository.dart
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../../../core/providers/serverpod_client_provider.dart';

final settingsRepositoryProvider = Provider<SettingsRepository>((ref) {
  return SettingsRepository(ref);
});

class SettingsRepository {
  final Ref _ref;

  SettingsRepository(this._ref);

  Future<ServerSettings> getSettings() async {
    final client = _ref.read(serverpodClientProvider);
    final response = await client.settings.getSettings();
    return ServerSettings(
      email: response.email,
      phone: response.phone,
      isPrivate: response.isPrivate,
    );
  }

  Future<void> updateNotificationSettings({
    bool? pushEnabled,
    bool? emailEnabled,
  }) async {
    final client = _ref.read(serverpodClientProvider);
    await client.settings.updateNotifications(
      pushEnabled: pushEnabled,
      emailEnabled: emailEnabled,
    );
  }

  Future<void> updatePrivacySettings({
    bool? isPrivate,
    bool? showActivityStatus,
  }) async {
    final client = _ref.read(serverpodClientProvider);
    await client.settings.updatePrivacy(
      isPrivate: isPrivate,
      showActivityStatus: showActivityStatus,
    );
  }
}

class ServerSettings {
  final String email;
  final String? phone;
  final bool isPrivate;

  ServerSettings({
    required this.email,
    this.phone,
    this.isPrivate = false,
  });
}

---

// lib/main.dart (partial - showing theme integration)
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'features/settings/providers/theme_provider.dart';

void main() {
  runApp(
    const ProviderScope(
      child: MyApp(),
    ),
  );
}

class MyApp extends ConsumerWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final themeMode = ref.watch(themeProvider);

    return MaterialApp(
      title: 'Social App',
      theme: ThemeData(
        colorScheme: ColorScheme.fromSeed(
          seedColor: Colors.blue,
          brightness: Brightness.light,
        ),
        useMaterial3: true,
      ),
      darkTheme: ThemeData(
        colorScheme: ColorScheme.fromSeed(
          seedColor: Colors.blue,
          brightness: Brightness.dark,
        ),
        useMaterial3: true,
      ),
      themeMode: themeMode,
      // ... rest of app
    );
  }
}

---

// lib/features/settings/presentation/screens/notification_settings_screen.dart
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../../providers/notification_settings_provider.dart';

class NotificationSettingsScreen extends ConsumerWidget {
  const NotificationSettingsScreen({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final settings = ref.watch(notificationSettingsProvider);

    return Scaffold(
      appBar: AppBar(
        title: const Text('Notifications'),
      ),
      body: ListView(
        children: [
          SwitchListTile(
            title: const Text('Pause All'),
            subtitle: const Text('Temporarily pause all notifications'),
            value: settings.pauseAll,
            onChanged: (value) {
              ref.read(notificationSettingsProvider.notifier).setPauseAll(value);
            },
          ),
          const Divider(),
          _buildSection(
            context,
            'Posts, Stories and Comments',
            [
              _buildSettingRow(
                context,
                ref,
                'Likes',
                settings.likesEnabled,
                (v) => ref
                    .read(notificationSettingsProvider.notifier)
                    .setLikes(v),
              ),
              _buildSettingRow(
                context,
                ref,
                'Comments',
                settings.commentsEnabled,
                (v) => ref
                    .read(notificationSettingsProvider.notifier)
                    .setComments(v),
              ),
              _buildSettingRow(
                context,
                ref,
                'Comment Likes',
                settings.commentLikesEnabled,
                (v) => ref
                    .read(notificationSettingsProvider.notifier)
                    .setCommentLikes(v),
              ),
            ],
          ),
          _buildSection(
            context,
            'Following and Followers',
            [
              _buildSettingRow(
                context,
                ref,
                'New Followers',
                settings.newFollowersEnabled,
                (v) => ref
                    .read(notificationSettingsProvider.notifier)
                    .setNewFollowers(v),
              ),
              _buildSettingRow(
                context,
                ref,
                'Follow Requests',
                settings.followRequestsEnabled,
                (v) => ref
                    .read(notificationSettingsProvider.notifier)
                    .setFollowRequests(v),
              ),
            ],
          ),
          _buildSection(
            context,
            'Messages',
            [
              _buildSettingRow(
                context,
                ref,
                'Direct Messages',
                settings.messagesEnabled,
                (v) => ref
                    .read(notificationSettingsProvider.notifier)
                    .setMessages(v),
              ),
              _buildSettingRow(
                context,
                ref,
                'Message Requests',
                settings.messageRequestsEnabled,
                (v) => ref
                    .read(notificationSettingsProvider.notifier)
                    .setMessageRequests(v),
              ),
            ],
          ),
        ],
      ),
    );
  }

  Widget _buildSection(
    BuildContext context,
    String title,
    List<Widget> children,
  ) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Padding(
          padding: const EdgeInsets.fromLTRB(16, 24, 16, 8),
          child: Text(
            title,
            style: Theme.of(context).textTheme.titleSmall?.copyWith(
                  fontWeight: FontWeight.bold,
                ),
          ),
        ),
        ...children,
      ],
    );
  }

  Widget _buildSettingRow(
    BuildContext context,
    WidgetRef ref,
    String title,
    bool value,
    Function(bool) onChanged,
  ) {
    return SwitchListTile(
      title: Text(title),
      value: value,
      onChanged: onChanged,
    );
  }
}
```
