---
type: "KEY_POINT"
title: "Flutter's Rendering Engine: Impeller"
---


**What is Impeller?**

Impeller is Flutter's rendering engine that replaced Skia. Think of it as the "graphics card driver" that draws everything you see on screen.

**Why Impeller Matters:**
- **No more shader jank**: Impeller pre-compiles shaders at build time, eliminating first-frame stutters
- **Predictable performance**: Consistent 60/120fps frame rates from the first frame
- **Native GPU acceleration**: Uses Metal (iOS) and Vulkan (Android)

**Current Status (Flutter 3.38+):**
- **iOS**: Default since Flutter 3.29 - Skia fallback has been removed
- **Android**: Default since Flutter 3.27 for API 29+ devices with Vulkan support
- **Android Fallback**: Devices without Vulkan (or with known driver issues) automatically use OpenGL

You don't need to do anything to enable Impeller—it's automatic! This is why Flutter apps feel smoother than ever.

