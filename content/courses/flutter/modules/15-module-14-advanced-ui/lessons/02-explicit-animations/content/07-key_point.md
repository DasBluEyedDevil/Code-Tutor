---
type: "KEY_POINT"
title: "Common Animation Curves"
---


**Easing Curves:**
| Curve | Effect | Use Case |
|-------|--------|----------|
| `Curves.linear` | Constant speed | Progress bars |
| `Curves.easeIn` | Slow start | Elements exiting |
| `Curves.easeOut` | Slow end | Elements entering |
| `Curves.easeInOut` | Slow start and end | Most UI transitions |

**Bounce and Elastic:**
| Curve | Effect | Use Case |
|-------|--------|----------|
| `Curves.bounceIn` | Bounce at start | Dramatic entrances |
| `Curves.bounceOut` | Bounce at end | Playful UI |
| `Curves.elasticIn` | Elastic at start | Pull-back effect |
| `Curves.elasticOut` | Elastic at end | Spring-like motion |

**Custom Curves:**
Use `Cubic(a, b, c, d)` for custom bezier curves, or extend `Curve` class for complete control.

