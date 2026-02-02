---
type: "THEORY"
title: "Touch Targets - Size and Spacing Requirements"
---


**Minimum Touch Target Sizes:**

Small touch targets are difficult for users with motor impairments:

| Platform | Minimum Size | Recommended |
|----------|-------------|-------------|
| iOS (Apple HIG) | 44x44 points | 44x44+ |
| Android (Material) | 48x48 dp | 48x48+ |
| WCAG 2.1 AAA | 44x44 CSS pixels | 44x44+ |

**Flutter's Default Sizes:**

| Widget | Default Size | Accessible? |
|--------|-------------|-------------|
| IconButton | 48x48 | Yes |
| ElevatedButton | Height 48 | Yes |
| Checkbox | 48x48 (with padding) | Yes |
| Switch | 59x48 | Yes |
| ListTile | Height 56+ | Yes |

**Common Problems:**

1. **Dense layouts** - Icons too close together
2. **Small text links** - Hard to tap precisely
3. **Custom widgets** - Forgetting to add padding
4. **Close buttons** - Often too small in corners

**Spacing Requirements:**

- **Minimum 8dp spacing** between touch targets
- **24dp+ recommended** for frequently used actions
- **Consider thumb zones** on mobile devices

