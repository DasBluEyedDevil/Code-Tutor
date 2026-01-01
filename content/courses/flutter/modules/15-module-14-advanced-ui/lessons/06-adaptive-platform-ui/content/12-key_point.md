---
type: "KEY_POINT"
title: "Adaptive UI Best Practices"
---


**Platform Detection:**

1. **Use Theme.of(context).platform** for widget decisions
2. **Check kIsWeb first** before using dart:io Platform
3. **Create helper utilities** for consistent platform checks
4. **Test by overriding** platform in ThemeData

**Adaptive Widgets:**

1. **Start with navigation** - Users expect platform-native navigation
2. **Adapt dialogs and pickers** - Very different UX per platform
3. **Keep content consistent** - Brand identity in cards, lists, content
4. **Use flutter_platform_widgets** for quick setup

**Testing:**

1. **Test on real devices** - Emulators miss subtle platform differences
2. **Override platform in tests** - `Theme(data: ThemeData(platform: TargetPlatform.iOS))`
3. **Test accessibility** - Both platforms have different a11y patterns

**Common Mistakes:**

- Using `dart:io` Platform on web (crashes)
- Mixing Material and Cupertino inconsistently
- Forgetting SafeArea on iOS (notch/home indicator)
- Not testing navigation patterns on both platforms

