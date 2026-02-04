---
type: KEY_POINT
---

- Use the `Text` widget's `style` parameter with `TextStyle` to control font size, weight, color, and letter spacing
- `Theme.of(context).textTheme` provides pre-defined styles (headlineLarge, bodyMedium, etc.) for consistent typography
- `TextOverflow.ellipsis` truncates long text with "..." instead of causing layout overflow errors
- `RichText` with `TextSpan` children lets you style individual words or phrases differently within a single paragraph
- `maxLines` combined with `overflow` controls how multi-line text wraps and truncates in constrained spaces
