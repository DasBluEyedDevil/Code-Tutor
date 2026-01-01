---
type: "THEORY"
title: "Performance Considerations"
---


**File Size Optimization:**

| Format | Typical Size | Notes |
|--------|-------------|-------|
| Lottie JSON | 10-100 KB | Compress with gzip |
| Lottie (dotLottie) | 5-50 KB | Compressed format |
| Rive | 5-50 KB | Already optimized |

**Best Practices:**

1. **Compress Lottie files** - Use dotLottie format or gzip compression
2. **Limit complexity** - Fewer layers and keyframes = better performance
3. **Avoid embedded images** - Use vector graphics when possible
4. **Cache network animations** - Don't re-download every time
5. **Use frame rate limiting** - 30fps is usually sufficient

**Rendering Performance:**

```dart
// Limit frame rate for better performance
Lottie.asset(
  'assets/heavy_animation.json',
  frameRate: FrameRate(30), // Limit to 30fps
)

// For Rive, use antialiasing sparingly
RiveAnimation.asset(
  'assets/animation.riv',
  antialiasing: false, // Disable for performance
)
```

**Memory Considerations:**

- Large animations consume GPU memory
- Dispose controllers when not visible
- Use `Visibility` widget to pause off-screen animations
- Consider `RepaintBoundary` for complex animations

**Debugging Performance:**

```dart
// Enable performance overlay
MaterialApp(
  showPerformanceOverlay: true,
  // ...
)
```

