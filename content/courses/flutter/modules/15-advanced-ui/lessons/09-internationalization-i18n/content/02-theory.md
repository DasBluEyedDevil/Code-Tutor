---
type: "THEORY"
title: "Setup - flutter_localizations and intl"
---


**Step 1: Add Dependencies**

Flutter's localization support requires two packages:

```yaml
# pubspec.yaml
dependencies:
  flutter:
    sdk: flutter
  flutter_localizations:  # Built-in localizations
    sdk: flutter
  intl: ^0.19.0          # Formatting, messages

dev_dependencies:
  intl_utils: ^2.8.0     # ARB file generation (optional)
```

**Step 2: Enable Code Generation**

Add the `generate` flag to enable Flutter's built-in l10n code generation:

```yaml
# pubspec.yaml
flutter:
  generate: true
```

**Step 3: Create l10n.yaml Configuration**

Create `l10n.yaml` in your project root:

```yaml
# l10n.yaml
arb-dir: lib/l10n
template-arb-file: app_en.arb
output-localization-file: app_localizations.dart
output-class: AppLocalizations
nullable-getter: false
```

**Configuration Options:**

| Option | Description |
|--------|-------------|
| `arb-dir` | Directory containing ARB files |
| `template-arb-file` | The primary locale ARB file |
| `output-localization-file` | Generated Dart file name |
| `output-class` | Name of the generated class |
| `nullable-getter` | Whether getters can return null |

**Step 4: Configure MaterialApp**

Import and configure localization delegates in your app:

