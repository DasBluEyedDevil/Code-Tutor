---
type: "KEY_POINT"
title: "i18n Best Practices Summary"
---


**Code Organization:**

- Keep all ARB files in `lib/l10n/`
- Use descriptive message keys with context
- Always add `@key` metadata with descriptions
- Create extension for `context.l10n` access

**Translatable Content:**

- Extract ALL user-visible strings
- Include error messages, tooltips, accessibility labels
- Don't concatenate translated strings
- Use placeholders for dynamic content

**Layout Considerations:**

- Use `EdgeInsetsDirectional` not `EdgeInsets`
- Use `start`/`end` not `left`/`right`
- Test with long translations (German is ~30% longer than English)
- Test RTL layouts with Arabic or Hebrew

**Date, Number, Currency:**

- Always use `intl` package formatters
- Pass locale to all formatters
- Don't hardcode date/number formats
- Test with different locales

**Testing Checklist:**

- [ ] All strings extracted to ARB files
- [ ] Pluralization works for 0, 1, 2, many
- [ ] Date/time displays correctly per locale
- [ ] Numbers and currency format correctly
- [ ] RTL layout mirrors correctly
- [ ] Icons flip appropriately in RTL
- [ ] No text overflow with longer translations
- [ ] Accessibility labels are translated

**Common Mistakes to Avoid:**

- Hardcoding strings: `Text('Hello')` instead of `Text(l10n.hello)`
- String concatenation: `'Hello ' + name` instead of `l10n.greeting(name)`
- Fixed left/right positioning in RTL languages
- Assuming all languages have same text length
- Forgetting to translate error messages

