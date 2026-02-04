---
type: KEY_POINT
---

- `ElevatedButton` has a raised shadow, `TextButton` is flat, `OutlinedButton` has a border -- choose based on visual hierarchy
- Pass a function to `onPressed` to handle taps; set `onPressed: null` to disable the button (grayed out automatically)
- `IconButton` is ideal for toolbar actions; `FloatingActionButton` is for the primary screen action
- `ElevatedButton.styleFrom()` customizes background color, text style, padding, and shape in one call
- Every button press should provide feedback -- use `SnackBar`, navigation, or state changes so users know their tap registered
