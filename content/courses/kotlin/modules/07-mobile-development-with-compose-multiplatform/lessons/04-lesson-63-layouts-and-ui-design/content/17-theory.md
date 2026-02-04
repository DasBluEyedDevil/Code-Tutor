---
type: "THEORY"
title: "Quiz Answers"
---


**Question 1: B) LazyColumn only renders visible items (efficient for long lists)**


**Performance**:
- Column with 1000 items: Slow, high memory usage
- LazyColumn with 1000 items: Fast, low memory (renders only visible items)

---

**Question 2: B) `Arrangement.spacedBy(16.dp)`**


---

**Question 3: B) Creates as many columns as fit (each min 120dp wide)**


**Benefits**:
- Responsive: Adapts to screen size
- Works on phones, tablets, foldables

---

**Question 4: C) sp scales with user's font size preference (accessibility)**


**Accessibility**:
- Users can increase font size in Settings
- `sp` respects this preference
- `dp` does not

**Use `dp` for**: padding, margins, component sizes
**Use `sp` for**: text size only

---

**Question 5: B) Colors adapt based on user's wallpaper (Android 12+)**

Material Design 3's dynamic color extracts colors from the user's wallpaper:


**Benefits**:
- Personalized: Each user gets unique colors
- Cohesive: Matches system UI
- Fresh: Changes with wallpaper

---

> **Note:** Dynamic color is an Android-specific feature (Android 12+). In a Compose Multiplatform project, this code belongs in `androidMain`. The `commonMain` theme should use static color schemes that work on all platforms.

```kotlin
// androidMain -- Android-specific dynamic theming
@Composable
fun AppTheme(
    dynamicColor: Boolean = true,
    darkTheme: Boolean = isSystemInDarkTheme(),
    content: @Composable () -> Unit
) {
    val colorScheme = when {
        dynamicColor && Build.VERSION.SDK_INT >= Build.VERSION_CODES.S -> {
            // Extract colors from wallpaper (Android 12+ only)
            if (darkTheme) {
                dynamicDarkColorScheme(LocalContext.current)
            } else {
                dynamicLightColorScheme(LocalContext.current)
            }
        }
        else -> {
            // Fallback to static colors (used on all platforms)
            if (darkTheme) DarkColorScheme else LightColorScheme
        }
    }

    MaterialTheme(colorScheme = colorScheme, content = content)
}

// commonMain -- Cross-platform theme (no dynamic color)
@Composable
fun AppTheme(
    darkTheme: Boolean = isSystemInDarkTheme(),
    content: @Composable () -> Unit
) {
    val colorScheme = if (darkTheme) DarkColorScheme else LightColorScheme
    MaterialTheme(colorScheme = colorScheme, content = content)
}
```
