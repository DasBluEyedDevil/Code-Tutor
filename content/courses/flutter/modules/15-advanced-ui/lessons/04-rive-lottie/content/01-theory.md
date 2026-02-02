---
type: "THEORY"
title: "Introduction - When to Use Rive/Lottie vs Flutter Animations"
---


**Why Use External Animation Tools?**

Flutter's built-in animation system is powerful, but some animations are impractical to build in code:

- **Character animations** - Walking, jumping, expressions
- **Complex illustrations** - Animated logos, icons, mascots
- **Designer-created motion** - Precise timing and easing from After Effects
- **Interactive graphics** - Responsive animations that react to user input

**Flutter Animations vs. Design Tool Animations:**

| Aspect | Flutter Code | Rive/Lottie |
|--------|-------------|-------------|
| Best for | UI transitions, simple motion | Complex illustrations, characters |
| Created by | Developers | Designers (After Effects, Rive app) |
| Flexibility | Highly dynamic | Pre-defined, some interactivity |
| File size | No extra assets | Animation file required |
| Learning curve | Dart/Flutter skills | Design tool + integration |
| Performance | Native, optimized | Render-based, varies |

**When to Use Each:**

- **Flutter Animations**: Page transitions, button states, loading indicators, layout changes
- **Lottie**: Animated illustrations, success/error states, onboarding graphics, icon animations
- **Rive**: Interactive characters, game-like UI, state-driven animations, complex state machines

**The Key Difference:**

- **Lottie** = Playback-focused (like a video you can control)
- **Rive** = Interactive-focused (responds to inputs and states)

