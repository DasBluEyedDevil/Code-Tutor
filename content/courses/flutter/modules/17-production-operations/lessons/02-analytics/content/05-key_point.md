---
type: "KEY_POINT"
title: "Custom Events"
---


**Event Naming Rules**

Firebase has strict naming requirements:

- 1-40 characters
- Start with a letter
- Only letters, numbers, underscores
- No spaces or special characters
- Case-sensitive

**Good Event Names**

```dart
// Action-based naming
'add_to_cart'
'complete_purchase'
'share_content'
'submit_rating'

// Feature-based naming  
'use_search'
'apply_filter'
'toggle_dark_mode'
```

**Bad Event Names**

```dart
// Too generic
'click'          // What was clicked?
'button_pressed' // Which button?

// Invalid characters
'add-to-cart'    // No hyphens allowed
'Add To Cart'    // No spaces allowed
```

**Event Parameters**

Parameters add context to events. Each event can have up to 25 parameters.

```dart
await analytics.logEvent(
  name: 'add_to_cart',
  parameters: {
    'item_id': product.id,
    'item_name': product.name,
    'item_category': product.category,
    'price': product.price,
    'currency': 'USD',
    'quantity': 1,
  },
);
```

**Parameter Rules**

- Parameter names: 1-40 characters, same rules as events
- String values: Max 100 characters
- Numeric values: Standard int/double
- No arrays or nested objects

