---
type: "THEORY"
title: "Introduction - When You Need Explicit Animations"
---


**Beyond Implicit Animations**

Implicit animations are great for simple property changes, but sometimes you need more control:

**Use Explicit Animations When:**
- **Looping animations** - Spinning loaders, pulsing effects, continuous motion
- **Sequencing** - Animation A finishes, then B starts, then C
- **Synchronized animations** - Multiple elements moving in coordination
- **User-controlled animations** - Drag gestures, scrubbing through animation
- **Reversing mid-animation** - Changing direction based on user input
- **Complex timing** - Staggered reveals, overlapping animations

**Implicit vs Explicit:**

| Implicit | Explicit |
|----------|----------|
| Automatic transitions | Manual control |
| Fire and forget | Start, stop, reverse, repeat |
| Limited to property changes | Any animation imaginable |
| AnimatedContainer | AnimationController + Tween |

**The Trade-off:**
Explicit animations require more code but give you complete control over every aspect of the animation - timing, direction, repetition, and coordination with other animations.

