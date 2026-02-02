---
type: "THEORY"
title: "Focus Management - Keyboard and Focus Traversal"
---


**Why Focus Matters:**

- **Keyboard users** navigate with Tab/Shift+Tab
- **Switch control users** move through focusable elements
- **Screen reader users** follow focus for context
- **Motor impaired users** may rely on sequential navigation

**Focus Order:**

Focus should follow **logical reading order**, typically:
1. Top to bottom
2. Left to right (in LTR languages)
3. Through related groups together

**Flutter's Focus System:**

| Widget | Purpose |
|--------|---------|
| `Focus` | Makes widget focusable |
| `FocusNode` | Manages focus state |
| `FocusScope` | Groups related focusables |
| `FocusTraversalGroup` | Controls traversal order |
| `FocusTraversalOrder` | Explicit ordering |

**Common Issues:**

- Focus jumps unexpectedly
- Hidden elements receive focus
- Custom widgets not focusable
- No visible focus indicator
- Modal dialogs don't trap focus

**Keyboard Shortcuts:**

| Key | Standard Action |
|-----|----------------|
| Tab | Next focusable element |
| Shift+Tab | Previous focusable element |
| Enter/Space | Activate focused element |
| Escape | Close dialog/modal |
| Arrow keys | Navigate within groups |

