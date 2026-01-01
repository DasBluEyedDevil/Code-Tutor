import 'package:flutter/material.dart';

class LanguageSwitcher extends StatelessWidget {
  final Locale currentLocale;
  final List<Locale> supportedLocales;
  final ValueChanged<Locale> onLocaleChanged;

  const LanguageSwitcher({
    super.key,
    required this.currentLocale,
    required this.supportedLocales,
    required this.onLocaleChanged,
  });

  // Native language names
  static const Map<String, String> _languageNames = {
    'en': 'English',
    'es': 'Espanol',
    'ar': 'العربية',
    'zh': '中文',
    'fr': 'Francais',
    'de': 'Deutsch',
    'ja': '日本語',
    'ko': '한국어',
    'pt': 'Portugues',
    'ru': 'Русский',
    'hi': 'हिन्दी',
    'it': 'Italiano',
  };

  // Flag emojis
  static const Map<String, String> _flags = {
    'en': '🇺🇸',
    'es': '🇪🇸',
    'ar': '🇸🇦',
    'zh': '🇨🇳',
    'fr': '🇫🇷',
    'de': '🇩🇪',
    'ja': '🇯🇵',
    'ko': '🇰🇷',
    'pt': '🇧🇷',
    'ru': '🇷🇺',
    'hi': '🇮🇳',
    'it': '🇮🇹',
  };

  String _getLanguageName(Locale locale) {
    return _languageNames[locale.languageCode] ?? locale.languageCode.toUpperCase();
  }

  String _getFlag(Locale locale) {
    return _flags[locale.languageCode] ?? '🌐';
  }

  @override
  Widget build(BuildContext context) {
    return Card(
      child: Padding(
        padding: const EdgeInsets.all(16),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          mainAxisSize: MainAxisSize.min,
          children: [
            Text(
              'Select Language',
              style: Theme.of(context).textTheme.titleMedium,
            ),
            const SizedBox(height: 16),
            ...supportedLocales.map((locale) {
              final isSelected = locale.languageCode == currentLocale.languageCode;
              
              return ListTile(
                leading: Text(
                  _getFlag(locale),
                  style: const TextStyle(fontSize: 24),
                ),
                title: Text(
                  _getLanguageName(locale),
                  style: TextStyle(
                    fontWeight: isSelected ? FontWeight.bold : FontWeight.normal,
                  ),
                ),
                trailing: isSelected
                    ? Icon(
                        Icons.check_circle,
                        color: Theme.of(context).colorScheme.primary,
                      )
                    : null,
                selected: isSelected,
                onTap: () => onLocaleChanged(locale),
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(8),
                ),
              );
            }),
          ],
        ),
      ),
    );
  }
}

// Alternative: Dropdown version
class LanguageDropdown extends StatelessWidget {
  final Locale currentLocale;
  final List<Locale> supportedLocales;
  final ValueChanged<Locale> onLocaleChanged;

  const LanguageDropdown({
    super.key,
    required this.currentLocale,
    required this.supportedLocales,
    required this.onLocaleChanged,
  });

  static const Map<String, String> _languageNames = {
    'en': 'English',
    'es': 'Espanol',
    'ar': 'العربية',
    'zh': '中文',
    'fr': 'Francais',
    'de': 'Deutsch',
  };

  @override
  Widget build(BuildContext context) {
    return DropdownButton<Locale>(
      value: currentLocale,
      items: supportedLocales.map((locale) {
        return DropdownMenuItem(
          value: locale,
          child: Text(
            _languageNames[locale.languageCode] ?? locale.languageCode,
          ),
        );
      }).toList(),
      onChanged: (locale) {
        if (locale != null) {
          onLocaleChanged(locale);
        }
      },
    );
  }
}

// Usage in app:
class LocaleProvider extends InheritedWidget {
  final Locale locale;
  final ValueChanged<Locale> onLocaleChanged;

  const LocaleProvider({
    super.key,
    required this.locale,
    required this.onLocaleChanged,
    required super.child,
  });

  static LocaleProvider of(BuildContext context) {
    return context.dependOnInheritedWidgetOfExactType<LocaleProvider>()!;
  }

  @override
  bool updateShouldNotify(LocaleProvider oldWidget) {
    return locale != oldWidget.locale;
  }
}