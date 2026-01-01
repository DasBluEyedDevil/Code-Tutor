---
type: "THEORY"
title: "Lottie Basics - The lottie Package"
---


**What is Lottie?**

Lottie is a library that renders Adobe After Effects animations exported as JSON. Designers create animations in After Effects, export them using the Bodymovin plugin, and you play them in Flutter.

**Advantages:**
- Massive library of free animations (LottieFiles.com)
- Familiar workflow for motion designers
- Small file sizes compared to video/GIF
- Vector-based, scales to any size
- Cross-platform (same file works on iOS, Android, web)

**Installation:**

```yaml
# pubspec.yaml
dependencies:
  lottie: ^3.1.0
```

**LottieBuilder Options:**

| Constructor | Use Case |
|------------|----------|
| `Lottie.asset()` | Load from assets folder |
| `Lottie.network()` | Load from URL |
| `Lottie.file()` | Load from device file |
| `Lottie.memory()` | Load from bytes |

**Key Properties:**

| Property | Description |
|----------|-------------|
| `controller` | AnimationController for playback control |
| `animate` | Auto-play (true by default) |
| `repeat` | Loop animation |
| `reverse` | Play in reverse after forward |
| `fit` | How to fit in container (BoxFit) |
| `frameRate` | Limit frame rate for performance |
| `onLoaded` | Callback when composition loads |

