---
type: "THEORY"
title: "Reduced Motion - Animation Alternatives"
---


**Why Reduced Motion Matters**

For some users, motion causes real physical problems:

| Condition | Effect of Motion |
|-----------|------------------|
| Vestibular disorders | Dizziness, nausea, disorientation |
| Migraines | Can trigger or worsen headaches |
| Photosensitive epilepsy | Risk of seizures from rapid motion |
| Motion sickness | Nausea from parallax and zooming |
| Cognitive disabilities | Difficulty focusing, confusion |

**Accessing Reduced Motion Preference:**

```dart
// Check if user prefers reduced motion
final reduceMotion = MediaQuery.disableAnimationsOf(context);

// Or check the full accessibility features
final a11yFeatures = MediaQuery.accessibilityFeaturesOf(context);
final reduceMotion = a11yFeatures.reduceMotion;
```

**Motion Guidelines (WCAG 2.3.3):**

- Allow users to disable motion
- Avoid content that flashes more than 3 times per second
- Provide alternatives to motion-based interactions
- Parallax scrolling should be optional

**What to Do When Reduced Motion is Enabled:**

| Original Animation | Alternative |
|-------------------|-------------|
| Slide transitions | Fade or instant |
| Bounce effects | Static appearance |
| Parallax scrolling | Static background |
| Auto-playing videos | Pause by default |
| Loading spinners | Static progress or text |
| Hover animations | Static hover state |

