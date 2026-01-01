---
type: "THEORY"
title: "Introduction - i18n vs l10n"
---


**What is Internationalization?**

Internationalization (i18n) and Localization (l10n) are related but distinct concepts:

| Term | Meaning | Example |
|------|---------|--------|
| **Internationalization (i18n)** | Designing your app to support multiple languages | Extracting strings, handling RTL |
| **Localization (l10n)** | Actually translating content for a specific locale | English -> Spanish translation |

The "18" in i18n represents the 18 letters between "i" and "n" in "internationalization".

**Why Localize Your App?**

1. **Reach more users** - 75% of internet users don't speak English as their first language
2. **Better UX** - Users prefer apps in their native language
3. **Market expansion** - Required for app stores in many countries
4. **Legal compliance** - Some regions require local language support

**What Gets Localized:**

- UI text and labels
- Dates and times
- Numbers and currencies
- Images with text
- Audio and video content
- Text direction (LTR vs RTL)

**Supported Locales in Flutter:**

Flutter supports all locales defined by the Unicode CLDR (Common Locale Data Repository). A locale is identified by:
- Language code (required): `en`, `es`, `ar`, `zh`
- Country/region code (optional): `en_US`, `en_GB`, `es_MX`
- Script (optional): `zh_Hans` (Simplified Chinese), `zh_Hant` (Traditional Chinese)

