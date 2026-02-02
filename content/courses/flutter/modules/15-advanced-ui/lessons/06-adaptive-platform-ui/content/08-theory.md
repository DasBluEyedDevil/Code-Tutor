---
type: "THEORY"
title: "Mixed Platform Apps - When to Be Adaptive vs Consistent"
---


**Not every app should be fully adaptive.** Consider your goals:

**Go Adaptive When:**

- Building productivity tools (settings, utilities)
- User experience is paramount
- Platform conventions are strong (date pickers, navigation)
- Users switch between platforms frequently

**Stay Consistent When:**

- Strong brand identity (Spotify, Instagram)
- Gaming or media apps
- Custom design language
- Complex interactions that need consistency across platforms

**Hybrid Approach (Recommended):**

Mix adaptive and consistent elements:

| Element | Approach | Reason |
|---------|----------|--------|
| Navigation | Adaptive | Users expect platform patterns |
| Buttons | Consistent | Brand identity |
| Dialogs | Adaptive | Different platform expectations |
| Cards/Content | Consistent | Design system |
| Date Pickers | Adaptive | Completely different UX |
| Loading States | Consistent | Brand feel |

**Testing Strategy:**

Always test on both platforms. Use `Theme(data: ThemeData(platform: TargetPlatform.iOS))` to preview iOS on Android device during development.

