---
type: "THEORY"
title: "Section 1: Understanding Material 3 Theming"
---


### Material 3 Color System

Material 3 generates a complete color palette from a **single seed color**:


**Analogy:** Give an interior designer your favorite color. They create an entire palette—wall colors, furniture, accents—all coordinated automatically!

### Material 3 is Default (Flutter 3.16+)

As of Flutter 3.16, Material 3 is enabled by default. You don't need to set `useMaterial3: true` anymore!



```dart
Seed Color (e.g., Blue #2196F3)
    ↓
Generates:
- Primary (Main brand color)
- Secondary (Accent color)
- Tertiary (Complementary color)
- Error (Error states)
- Surface (Backgrounds)
- OnPrimary (Text on primary color)
- OnSecondary (Text on secondary color)
... (30+ colors total!)
```
