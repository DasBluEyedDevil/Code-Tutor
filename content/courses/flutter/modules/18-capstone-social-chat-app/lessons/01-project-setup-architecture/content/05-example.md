---
type: "EXAMPLE"
title: "Flutter App Initial Setup"
---


**Main Entry Point**

Set up the Flutter app with proper initialization:



```dart
// app/lib/main.dart
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:serverpod_flutter/serverpod_flutter.dart';

import 'core/config/app_config.dart';
import 'core/router/app_router.dart';
import 'core/theme/app_theme.dart';
import 'shared/services/client_provider.dart';

void main() async {
  WidgetsFlutterBinding.ensureInitialized();
  
  // Initialize Serverpod client
  await initializeServerpodClient();
  
  runApp(
    const ProviderScope(
      child: SocialChatApp(),
    ),
  );
}

Future<void> initializeServerpodClient() async {
  // Client is initialized lazily through the provider
  // This is where you'd do any async setup needed before the client
}

class SocialChatApp extends ConsumerWidget {
  const SocialChatApp({super.key});
  
  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final router = ref.watch(appRouterProvider);
    
    return MaterialApp.router(
      title: 'Social Chat',
      debugShowCheckedModeBanner: false,
      theme: AppTheme.light,
      darkTheme: AppTheme.dark,
      themeMode: ThemeMode.system,
      routerConfig: router,
    );
  }
}
```
