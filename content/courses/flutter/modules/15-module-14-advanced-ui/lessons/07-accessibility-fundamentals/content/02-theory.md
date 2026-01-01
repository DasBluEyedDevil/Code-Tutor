---
type: "THEORY"
title: "Semantics Widget - Making Content Meaningful"
---


**What is Semantic Information?**

Screen readers need to understand what each UI element represents. The `Semantics` widget provides this information.

**Without Semantics:**
```dart
// Screen reader says: "Button"
GestureDetector(
  onTap: () => addToCart(),
  child: Container(
    decoration: BoxDecoration(color: Colors.blue),
    child: Icon(Icons.add),
  ),
)
```

**With Semantics:**
```dart
// Screen reader says: "Add to cart, button"
Semantics(
  label: 'Add to cart',
  button: true,
  onTap: () => addToCart(),
  child: Container(
    decoration: BoxDecoration(color: Colors.blue),
    child: Icon(Icons.add),
  ),
)
```

**Key Semantics Properties:**

| Property | Purpose | Example |
|----------|---------|--------|
| `label` | Description read aloud | 'Submit order' |
| `hint` | Additional context | 'Double-tap to submit' |
| `value` | Current value | '50%' for a slider |
| `button` | Marks as button | `true` |
| `header` | Marks as heading | `true` |
| `image` | Marks as image | `true` |
| `link` | Marks as link | `true` |
| `slider` | Marks as slider | `true` |
| `readOnly` | Not editable | `true` |
| `enabled` | Interactive state | `true` or `false` |
| `excludeSemantics` | Hide from accessibility | `true` |

**Semantic Traits:**

Traits tell the screen reader how to treat the element:

```dart
Semantics(
  label: 'Settings',
  button: true,        // Announces as button
  enabled: true,       // Can be activated
  focused: isFocused,  // Currently focused
  selected: isSelected, // Currently selected
)
```

