---
type: "THEORY"
title: "Rive Fundamentals - The rive Package"
---


**What is Rive?**

Rive (formerly Flare) is a real-time interactive animation tool. Unlike Lottie (which plays pre-rendered animations), Rive animations can respond to user input, change states, and blend between animations dynamically.

**Key Concepts:**

| Concept | Description |
|---------|-------------|
| **Artboard** | The canvas containing your animation |
| **Animation** | A timeline of keyframes (like Lottie) |
| **State Machine** | Logic that controls animation flow |
| **Inputs** | Triggers and values that drive state changes |
| **Blend States** | Smooth transitions between animations |

**Rive vs Lottie:**

| Feature | Lottie | Rive |
|---------|--------|------|
| Interactivity | Limited (play/pause/seek) | Full state machine support |
| File format | JSON (from After Effects) | .riv (from Rive app) |
| Runtime control | Playback only | State changes, blending |
| Designer tool | After Effects + Bodymovin | Rive editor (free web app) |
| Learning curve | Easier | Steeper but more powerful |

**Installation:**

```yaml
# pubspec.yaml
dependencies:
  rive: ^0.13.0
```

**When to Choose Rive:**
- You need animations that respond to user input
- You want smooth transitions between animation states
- You're building game-like UI or characters
- Designers want direct control over interactivity

