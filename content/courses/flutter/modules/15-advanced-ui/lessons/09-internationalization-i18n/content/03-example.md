---
type: "EXAMPLE"
title: "MaterialApp Localization Setup"
---


Configuring your app to support multiple locales:



```dart
import 'package:flutter/material.dart';
import 'package:flutter_localizations/flutter_localizations.dart';
import 'package:flutter_gen/gen_l10n/app_localizations.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Localized App',
      
      // Supported locales for your app
      supportedLocales: const [
        Locale('en'),      // English (primary)
        Locale('es'),      // Spanish
        Locale('ar'),      // Arabic (RTL)
        Locale('zh'),      // Chinese
        Locale('fr'),      // French
        Locale('de'),      // German
        Locale('ja'),      // Japanese
      ],
      
      // Localization delegates
      localizationsDelegates: const [
        // Your generated localizations
        AppLocalizations.delegate,
        
        // Built-in Material localizations
        GlobalMaterialLocalizations.delegate,
        
        // Built-in Cupertino localizations
        GlobalCupertinoLocalizations.delegate,
        
        // Built-in widget localizations (text direction, etc.)
        GlobalWidgetsLocalizations.delegate,
      ],
      
      // Optional: Custom locale resolution
      localeResolutionCallback: (locale, supportedLocales) {
        // Check if the current device locale is supported
        for (final supportedLocale in supportedLocales) {
          if (supportedLocale.languageCode == locale?.languageCode) {
            return supportedLocale;
          }
        }
        // Default to English if locale not supported
        return const Locale('en');
      },
      
      home: const HomePage(),
    );
  }
}

class HomePage extends StatelessWidget {
  const HomePage({super.key});

  @override
  Widget build(BuildContext context) {
    // Access localized strings
    final l10n = AppLocalizations.of(context);
    
    return Scaffold(
      appBar: AppBar(
        title: Text(l10n.appTitle),
      ),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Text(l10n.welcomeMessage),
            const SizedBox(height: 16),
            Text(l10n.currentLocale(Localizations.localeOf(context).toString())),
          ],
        ),
      ),
    );
  }
}
```
