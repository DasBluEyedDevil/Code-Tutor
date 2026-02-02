---
type: "THEORY"
title: "Accessible Forms - Labels, Errors, and Validation"
---


**Form Accessibility Requirements**

Forms are critical accessibility points. Users must be able to:

1. **Understand** what each field requires
2. **Navigate** between fields efficiently
3. **Receive** clear error feedback
4. **Correct** mistakes easily

**WCAG Form Requirements:**

| Criterion | Requirement |
|-----------|-------------|
| 1.3.1 Info and Relationships | Labels are programmatically associated with inputs |
| 2.4.6 Headings and Labels | Labels describe the purpose of inputs |
| 3.3.1 Error Identification | Errors are identified and described in text |
| 3.3.2 Labels or Instructions | Inputs have visible labels or instructions |
| 3.3.3 Error Suggestion | Suggest corrections when errors are detected |

**Common Form Accessibility Mistakes:**

1. **Placeholder as label** - Disappears when typing
2. **Color-only errors** - Not visible to colorblind users
3. **Missing error announcements** - Screen reader users don't hear errors
4. **No validation until submit** - Users don't know about errors until the end
5. **Poor focus management** - Focus doesn't move to error messages

**Best Practices:**

- Always use visible labels above or beside inputs
- Announce errors to screen readers with `SemanticsService.announce`
- Move focus to first error after validation
- Show errors inline near the affected field
- Use icons + color + text for error states

