---
type: "KEY_POINT"
title: "Transition Best Practices"
---


**Choosing the Right Transition:**

| Navigation Type | Recommended Transition |
|----------------|------------------------|
| Forward navigation | Slide from right or fade |
| Modal/overlay | Slide from bottom |
| Tab switching | Fade (no slide) |
| Detail view | Hero + slide/fade |
| Settings/preferences | Slide from right |
| Full-screen media | Fade or zoom |

**Performance Tips:**
- Keep transitions under 400ms for responsiveness
- Use `Curves.easeOutCubic` for natural deceleration
- Avoid complex layouts during transition
- Test on lower-end devices

**Accessibility Considerations:**
- Respect `MediaQuery.disableAnimations`
- Provide reduced motion alternatives
- Keep transitions subtle and purposeful

```dart
// Respect accessibility settings
final reduceMotion = MediaQuery.of(context).disableAnimations;
final duration = reduceMotion 
  ? Duration.zero 
  : const Duration(milliseconds: 300);
```

**Consistency Guidelines:**
- Use the same transition style throughout the app
- Match platform conventions (iOS vs Android)
- Hero animations should have clear visual connection

