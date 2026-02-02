---
type: "THEORY"
title: "Color and Contrast - WCAG Requirements"
---


**Why Contrast Matters:**

Low contrast text is difficult to read for:
- Users with low vision
- Users in bright sunlight
- Users with aging eyes
- Everyone on low-quality displays

**WCAG Contrast Ratios:**

| Level | Normal Text (< 18pt) | Large Text (18pt+ or 14pt bold) |
|-------|---------------------|----------------------------------|
| AA | 4.5:1 minimum | 3:1 minimum |
| AAA | 7:1 minimum | 4.5:1 minimum |

**How to Calculate:**

Contrast ratio = (L1 + 0.05) / (L2 + 0.05)

Where L1 is lighter color luminance and L2 is darker.

**Common Failures:**

| Foreground | Background | Ratio | Verdict |
|------------|------------|-------|---------|
| #777777 | #FFFFFF | 4.48:1 | Fails AA |
| #757575 | #FFFFFF | 4.6:1 | Passes AA |
| #000000 | #FFFFFF | 21:1 | Passes AAA |
| #FF0000 | #00FF00 | 1:1 | Fails badly |

**Color Blindness Considerations:**

| Type | Affects | Population |
|------|---------|------------|
| Protanopia | Red perception | 1% of men |
| Deuteranopia | Green perception | 6% of men |
| Tritanopia | Blue perception | 0.01% |

**Don't Rely on Color Alone:**

- Use icons + color for status
- Add patterns to charts
- Include text labels
- Use shapes + color for indicators

