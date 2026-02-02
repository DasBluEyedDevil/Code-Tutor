---
type: "THEORY"
title: "ARB Files - Application Resource Bundle"
---


**What are ARB Files?**

ARB (Application Resource Bundle) is a JSON-based file format for localization. Each locale has its own ARB file:

```
lib/
  l10n/
    app_en.arb       # English (template)
    app_es.arb       # Spanish
    app_ar.arb       # Arabic
    app_zh.arb       # Chinese
```

**ARB File Structure:**

```json
{
  "@@locale": "en",
  
  "appTitle": "My App",
  "@appTitle": {
    "description": "The title of the application"
  },
  
  "welcomeMessage": "Welcome to our app!",
  "@welcomeMessage": {
    "description": "Greeting shown on the home page"
  }
}
```

**Key Concepts:**

| Element | Purpose |
|---------|--------|
| `@@locale` | Declares the locale of this file |
| `keyName` | The message key (becomes method name) |
| `@keyName` | Metadata for translators |
| `description` | Explains context for translators |

**Message Keys Best Practices:**

- Use camelCase for keys
- Make keys descriptive: `loginButtonLabel` not `btn1`
- Group related keys: `settingsTitle`, `settingsTheme`, `settingsLanguage`
- Always add descriptions for context

