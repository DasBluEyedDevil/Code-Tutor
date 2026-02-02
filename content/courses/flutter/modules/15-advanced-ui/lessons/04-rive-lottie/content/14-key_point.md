---
type: "KEY_POINT"
title: "Animation Assets Setup"
---


**Adding Animation Files to Your Project:**

1. **Create assets folder:**
```
lib/
assets/
  animations/
    loading.json      # Lottie
    character.riv     # Rive
```

2. **Update pubspec.yaml:**
```yaml
flutter:
  assets:
    - assets/animations/
```

3. **Finding Animations:**
   - **Lottie:** LottieFiles.com (thousands of free animations)
   - **Rive:** rive.app/community (growing library)

4. **Creating Custom Animations:**
   - **Lottie:** Adobe After Effects + Bodymovin plugin
   - **Rive:** Rive editor (free at rive.app)

**Pro Tips:**
- Preview animations before downloading
- Check file size and complexity
- Test on target devices early
- Consider hiring a motion designer for custom work

